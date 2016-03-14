using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

using Janus.Windows.GridEX;
using USP.Express.Pro;

namespace AtlasReportToolkit
{
    public partial class CreateNewReportDlg : Form
    {
        private class iGanttMovement
        {
            public string Pit { get; set; }
            public string Bench { get; set; }
            public string Flitch { get; set; }
            public string Block { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime FinishDate { get; set; }
            public string MaterialType { get; set; }
            public double? Tonnes { get; set; }
            public double? BCM { get; set; }
            public double? Fe { get; set; }
            public double? SiO2 { get; set; }
            public double? Al2O3 { get; set; }
            public double? P { get; set; }
            public double? S { get; set; }
            public double? Mn { get; set; }
            public double? LOI1000 { get; set; }
        }

        public ReportDefinitions Definitions { get; set; }
        public BindingList<Report> Reports { get; set; }
        public Report Report { get; set; }
        public String Operation { get; set; }
        public String ConfigurationDirectory { get; set; }

        public CreateNewReportDlg()
        {
            InitializeComponent();
        }

        [Conditional("DEBUG")]
        public void DebugCreateReport()
        {
            this.Report = Report.LoadReport(@"D:\dropbox\home\Clients\ati004\data\reports\Pending\Atlas_Wodgina_Port_Haulage_07-10-2014_Pending.xml");

            ImportData(@"D:\dropbox\home\Clients\ati004\data\reports\Pending\Wodgina Port Haulage 7 Oct 2014.csv");
            this.Report.Name = "NEW" + this.Report.Name;
        }

        private void CreateNewReportDlg_Load(object sender, EventArgs e)
        {
            SetWizardButtons();

            try
            {
                lstReportPolicies.Items.Clear();
                ReportPolicy selectedPolicy = null;
                foreach (ReportPolicy policy in this.Definitions.Policies)
                {
                    if (String.IsNullOrWhiteSpace(this.Operation) || this.Operation.Equals(policy.Operation) || this.Operation.Equals(ReportDefinitions.BypassOperationCode))
                    {
                        lstReportPolicies.Items.Add(policy);
                        if (selectedPolicy == null)
                            selectedPolicy = policy;
                    }
                }
                if (this.Definitions.Policies.Count > 0)
                    lstReportPolicies.SelectedValue = selectedPolicy;
                txtReportingPeriod.Value = DateTime.Now.AddDays(-1);
            }
            catch (System.Exception exc)
            {
            }
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void butPrev_Click(object sender, EventArgs e)
        {
            if (uiCreateReportTab.SelectedIndex > 0)
                uiCreateReportTab.SelectedIndex--;

            SetWizardButtons();
        }

        private void butNext_Click(object sender, EventArgs e)
        {
            if (uiCreateReportTab.SelectedIndex == uiCreateReportTab.TabPages.Count - 1)
            {
                this.DialogResult = DialogResult.OK;
                if (!CreateReport())
                    this.DialogResult = DialogResult.None;

                this.Close();
                return;
            }
            else if (uiCreateReportTab.SelectedIndex == 0)
            {
                ReportPolicy policy = (from a in this.Definitions.Policies where a.Identifier.Equals(this.Report.ReportPolicyId) select a).FirstOrDefault();
                if (policy != null)
                {
                    if (this.Report.CheckExists(policy.ReportsDirectory))
                    {
                        MessageBox.Show("This report already exists.", "Error", MessageBoxButtons.OK);
                        return;
                    }
                }
                if (txtReportingPeriod.Value >= DateTime.Now)
                {
                    if (MessageBox.Show("You are creating a report for a future reporting day. Are you sure this is correct?", "Error", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }
                }

            }

            if (uiCreateReportTab.SelectedIndex < uiCreateReportTab.TabPages.Count - 1)
                uiCreateReportTab.SelectedIndex++;

            SetWizardButtons();
        }

        private Boolean CreateReport()
        {
            ReportPolicy policy = (from a in this.Definitions.Policies where a.Identifier.Equals(this.Report.ReportPolicyId) select a).FirstOrDefault();
            if (policy != null)
            {
                if (this.Report.CheckExists(policy.ReportsDirectory))
                {
                    MessageBox.Show("This report already exists.", "Error", MessageBoxButtons.OK);
                    return false;
                }
            }

            try
            {
                foreach (ReportSheet sheet in this.Report.Sheets)
                    sheet.Template.UpdateReferenceLists(this.ConfigurationDirectory, (DateTime?)txtReportingPeriod.Value, true);
                
                string importFileName = txtFileName.Text;
                if (policy.ImportFileFormat == ReportPolicy.ImportFormatType.iGantt)
                    importFileName = ConvertIGanttToFlatCSV(policy, txtFileName.Text);
                else if (policy.ImportFileFormat == ReportPolicy.ImportFormatType.iGanttActivityLoadHaul)
                    importFileName = ConvertIGanttActivityLoadHaulToFlatCSV(policy, txtFileName.Text);

                ImportData(importFileName);
                if (policy.RemoveReportData)
                    RemovePreviousReportData(policy);
            }
            catch (System.Exception exc)
            {
                return false;
            }

            return true;
        }

        private void RemovePreviousReportData(ReportPolicy policy)
        {
            DateTime lastDate = this.Report.ReportingPeriod.AddDays(-policy.RemoveReportDataDays);
            lastDate = new DateTime(lastDate.Year,lastDate.Month,lastDate.Day,6,0,0);
            foreach (ReportRow row in this.Report.Sheets[0].Rows.ToList())
            {
                try
                {
                    DateTime? dt = row[policy.RemoveReportDataDateColumn] as DateTime?;
                    if (dt.HasValue)
                        if (dt < lastDate)
                            this.Report.Sheets[0].Rows.Remove(row);
                }
                catch (System.Exception exc)
                {
                }
            }
        }

        private void SetWizardButtons()
        {
            butPrev.Enabled = (uiCreateReportTab.SelectedIndex != 0);
            butNext.Enabled = true;
            butNext.Text = (uiCreateReportTab.SelectedIndex == uiCreateReportTab.TabPages.Count - 1 ? "OK" : "Next");
        }

        private void txtFileName_ButtonClick(object sender, EventArgs e)
        {
            OpenFileDialog browse = new OpenFileDialog();
            browse.CheckPathExists = true;
            browse.CheckFileExists = true;
            browse.RestoreDirectory = true;
            browse.Filter = "Text Files (*.txt)|*.txt|Comma-delimited Files (*.csv)|*.csv|All Files (*.*)|*.*||";
            browse.Title = "Select file to import";
            browse.AddExtension = true;
            browse.DefaultExt = ".txt";
            if (browse.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                txtFileName.Text = browse.FileName;
        }

        private void butRefresh_Click(object sender, EventArgs e)
        {
            GridEXRow row = gridDefaultLists.GetRow();
            if (row == null)
                return;

            ReportReferenceList list = row.DataRow as ReportReferenceList;
            if (list == null)
                return;

            list.Update(this.ConfigurationDirectory, this.Report.ReportingPeriod);
            Report.UpdateLinkedReferenceLists(this.Report.GetAllReferenceLists(this.Operation, this.Definitions, false));

            gridItems.SetDataBinding(list.Items, null);
            gridItems.RootTable.ApplyFilter(gridItems.RootTable.StoredFilters[0].FilterCondition);
        }

        private void gridDefaultLists_SelectionChanged(object sender, EventArgs e)
        {
            GridEXRow row = gridDefaultLists.GetRow();
            if (row == null)
                return;

            ReportReferenceList list = row.DataRow as ReportReferenceList;
            if (list == null)
                return;

            Report.UpdateLinkedReferenceLists(gridDefaultLists.DataSource as List<ReportReferenceList>);

            gridItems.SetDataBinding(list.Items, null);
            gridItems.RootTable.ApplyFilter(gridItems.RootTable.StoredFilters[0].FilterCondition);

            gridItems.RootTable.Columns["Value"].EditType = EditType.TextBox;
            gridItems.RootTable.Columns["Description"].EditType = EditType.TextBox;
            if (!String.IsNullOrWhiteSpace(list.LinkedListName) || !String.IsNullOrWhiteSpace(list.LinkedListCalculation))
            {
                gridItems.AllowDelete = InheritableBoolean.False;
                gridItems.AllowAddNew = InheritableBoolean.False;
                gridItems.AllowEdit = InheritableBoolean.False;
            }
            else if (!String.IsNullOrWhiteSpace(list.DBConnectionString) || !String.IsNullOrWhiteSpace(list.DBSqlQuery))
            {
                gridItems.AllowDelete = InheritableBoolean.False;
                gridItems.AllowAddNew = InheritableBoolean.False;
                gridItems.AllowEdit = InheritableBoolean.True;
                gridItems.RootTable.Columns["Value"].EditType = EditType.NoEdit;
                gridItems.RootTable.Columns["Description"].EditType = EditType.NoEdit;
            }
            else
            {
                gridItems.AllowDelete = InheritableBoolean.True;
                gridItems.AllowAddNew = InheritableBoolean.True;
                gridItems.AllowEdit = InheritableBoolean.True;
            }
        }

        private void gridItems_GetNewRow(object sender, GetNewRowEventArgs e)
        {
            e.NewRow = new ReportReferenceListValue();
        }

        private void ticShowActivatedValuesOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (ticShowActivatedValuesOnly.Checked)
                gridItems.RootTable.ApplyFilter(gridItems.RootTable.StoredFilters[0].FilterCondition);
            else
                gridItems.RootTable.RemoveFilter();
        }

        private void lstReportPolicies_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstReportPolicies.SelectedValue == null)
                return;

            ReportPolicy policy = lstReportPolicies.SelectedValue as ReportPolicy;

            List<ReportTemplate> templates = new List<ReportTemplate>();
            foreach (Guid templateId in policy.TemplateIds)
            {
                ReportTemplate template = (from a in this.Definitions.Templates where a.Identifier == templateId select a).FirstOrDefault();
                if (template == null)
                    continue;

                ReportTemplate b = Extensions.BinaryClone<ReportTemplate>(template);
                
                b.AssignPolicyDefaults(policy);
                b.FillReferenceLists(policy.Operation, this.Definitions);
                b.UpdateReferenceLists(this.ConfigurationDirectory, (DateTime?)txtReportingPeriod.Value);

                templates.Add(b);
            }
            if (templates == null)
                return;


            this.Report = new Report(policy, templates);
            this.Report.GodMode = true;
            this.Report.ReportingPeriod = txtReportingPeriod.Value;

            this.Report.Name = SetReportName();
            this.Report.Operation = policy.Operation;

            Report.UpdateLinkedReferenceLists(this.Report.GetAllReferenceLists(this.Operation, this.Definitions, false));

            gridDefaultLists.SetDataBinding(this.Report.GetAllReferenceLists(this.Operation, this.Definitions, true), null);

            List<Report> currentReports = (from a in this.Reports where a.ReportPolicyId == policy.Identifier select a).ToList();
            gridPreviousReports.SetDataBinding(currentReports, null);
            gridPreviousReports.MoveFirst();

            ticIncludeData.Checked = policy.IncludePreviousReports;
        }


        private String SetReportName()
        {
            String name = lstReportPolicies.SelectedValue.ToString() + " " + txtReportingPeriod.Value.ToString("dd-MM-yyyy");
            name = name.Replace(" ", "_");
            name = name.Replace("/", "_");
            name = name.Replace(".", "_");
            name = name.Replace("\\", "_");
            name = name.Replace(":", "_");
            name = name.Replace("{", "_");
            name = name.Replace("}", "_");
            return name;
        }

        private void txtReportingPeriod_ValueChanged(object sender, EventArgs e)
        {
            this.Report.ReportingPeriod = txtReportingPeriod.Value;
            this.Report.Name = SetReportName();
        }

        private void ticIncludeData_CheckedChanged(object sender, EventArgs e)
        {
            gridPreviousReports.Enabled = ticIncludeData.Checked;
            gridPreviousReports.Visible = ticIncludeData.Checked;
        }


        private string ConvertIGanttActivityLoadHaulToFlatCSV(ReportPolicy policy, string originalFileName)
        {
            if (String.IsNullOrWhiteSpace(originalFileName))
                return originalFileName;

            string importFileName = Path.GetTempFileName();
            try
            {
                StreamReader reader = new StreamReader(originalFileName);

                List<iGanttMovement> movements = new List<iGanttMovement>();

                string line = reader.ReadLine();
                string[] header = line.Split(',');
                List<string> materialTypes = new List<string>();
                foreach (string column in header)
                {
                    if (column.EndsWith("_Fe"))
                    {
                        string materialType = column.Replace("_Fe", "");
                        if (!materialType.Equals("Total"))
                            if (!(from a in materialTypes where a.Equals(materialType) select true).FirstOrDefault())
                                materialTypes.Add(materialType);
                    }
                }
                materialTypes.Add("Waste");

                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    if (String.IsNullOrWhiteSpace(line))
                        continue;

                    string[] figures = line.Split(',');
                    if (figures.Length != header.Length)
                        continue;

                    Dictionary<string, string> lineValues = new Dictionary<string, string>();
                    for (int i = 0; i < header.Length; i++)
                        lineValues[header[i]] = figures[i];

                    if (!lineValues["Activity Type"].Equals("03_LH"))
                        continue;

                    DateTime sd, fd;
                    if (!DateTime.TryParse(lineValues.ContainsKey("Start") ? lineValues["Start"] : "-", out sd))
                        continue;
                    if (!DateTime.TryParse(lineValues.ContainsKey("Finish") ? lineValues["Finish"] : "-", out fd))
                        continue;

                    TimeSpan activityLength = fd - sd;
                    if (fd == sd)
                        continue;

                    DateTime sds = new DateTime(sd.Year, sd.Month, sd.Day, 6, 0, 0);
                    DateTime fds = new DateTime(fd.Year, fd.Month, fd.Day, 6, 0, 0);
                    sds = sds.AddDays(-1);
                    fds = fds.AddDays(1);

                    double tonnes;
                    if (!Double.TryParse(lineValues.ContainsKey("Total_Tonnes") ? lineValues["Total_Tonnes"] : "-", out tonnes))
                        continue;
                    if (tonnes == 0.0)
                        continue;

                    while (sds < fds)
                    {
                        DateTime sfds = sds.AddDays(1);
                        if (sds <= fd && sfds >= sd)
                        {
                            DateTime dmin = (sds > sd) ? sds : sd;
                            DateTime dmax = (sfds < fd) ? sfds : fd;

                            foreach (string materialType in materialTypes)
                            {
                                double v;

                                double dayTonnes = lineValues.ContainsKey(materialType + "_Tonnes") ? (Double.TryParse(lineValues[materialType+"_Tonnes"], out v) ? v * (double)(dmax - dmin).Ticks / (double)activityLength.Ticks : 0.0) : 0.0;
                                if (dayTonnes <= 0.0)
                                    continue;

                                double dayBCM = lineValues.ContainsKey(materialType + "_Vol") ? (Double.TryParse(lineValues[materialType + "_Vol"], out v) ? v * (double)(dmax - dmin).Ticks / (double)activityLength.Ticks : 0.0) : 0.0;
                                if (dayBCM <= 0.0)
                                    continue;

                                double? fe = lineValues.ContainsKey(materialType + "_Fe") ? (Double.TryParse(lineValues[materialType + "_Fe"], out v) ? v : (double?)null) : (double?)null;
                                double? sio2 = lineValues.ContainsKey(materialType + "_SiO2") ? (Double.TryParse(lineValues[materialType + "_SiO2"], out v) ? v : (double?)null) : (double?)null;
                                double? al2o3 = lineValues.ContainsKey(materialType + "_Al2O3") ? (Double.TryParse(lineValues[materialType + "_Al2O3"], out v) ? v : (double?)null) : (double?)null;
                                double? p = lineValues.ContainsKey(materialType + "_P") ? (Double.TryParse(lineValues[materialType + "_P"], out v) ? v : (double?)null) : (double?)null;
                                double? s = lineValues.ContainsKey(materialType + "_S") ? (Double.TryParse(lineValues[materialType + "_S"], out v) ? v : (double?)null) : (double?)null;
                                double? mn = lineValues.ContainsKey(materialType + "_Mn") ? (Double.TryParse(lineValues[materialType + "_Mn"], out v) ? v : (double?)null) : (double?)null;
                                double? loi = lineValues.ContainsKey(materialType + "_LOI") ? (Double.TryParse(lineValues[materialType + "_LOI"], out v) ? v : (double?)null) : (double?)null;

                                iGanttMovement movement = new iGanttMovement()
                                {
                                    StartDate = dmin,
                                    FinishDate = dmax,
                                    Pit = lineValues.ContainsKey("Pit") ? lineValues["Pit"] : null,
                                    Bench = lineValues.ContainsKey("Bench") ? lineValues["Bench"] : null,
                                    Flitch = lineValues.ContainsKey("Flitch") ? lineValues["Flitch"] : null,
                                    Block = lineValues.ContainsKey("Mining_Region") ? lineValues["Mining_Region"] : null,
                                    Tonnes = dayTonnes,
                                    BCM = dayBCM,
                                    MaterialType = materialType.Equals("LG") ? "MW" : materialType,
                                    Fe = fe,
                                    SiO2 = sio2,
                                    Al2O3 = al2o3,
                                    P = p,
                                    S = s,
                                    Mn = mn,
                                    LOI1000 = loi
                                };
                                movements.Add(movement);
                            }
                        }
                        sds = sfds;
                    }
                }

                reader.Close();

                StreamWriter writer = new StreamWriter(importFileName, false);
                StringBuilder oline = new StringBuilder();
                writer.WriteLine("Operation,Pit,StartDate,FinishDate,MaterialType,Tonnes,BCM,Fe,SiO2,Al2O3,P,S,Mn,LOI1000");
                for (int i = 0; i < movements.Count; i++)
                {
                    oline.Clear();
                    oline.Append((policy.Operation ?? "") + ",");
                    oline.Append((movements[i].Pit ?? "") + ",");
                    oline.Append(movements[i].StartDate.ToString("dd/MM/yyyy HH:mm") + ",");
                    oline.Append(movements[i].FinishDate.ToString("dd/MM/yyyy HH:mm") + ",");
                    oline.Append((movements[i].MaterialType ?? "") + ",");
                    oline.Append(movements[i].Tonnes + ",");
                    oline.Append(movements[i].BCM + ",");
                    oline.Append(movements[i].Fe + ",");
                    oline.Append(movements[i].SiO2 + ",");
                    oline.Append(movements[i].Al2O3 + ",");
                    oline.Append(movements[i].P + ",");
                    oline.Append(movements[i].S + ",");
                    oline.Append(movements[i].Mn + ",");
                    oline.Append(movements[i].LOI1000);
                    writer.WriteLine(oline);
                }
                writer.Close();
            }
            catch (System.Exception e)
            {
                return "";
            }


            return importFileName;
        }


        private string ConvertIGanttToFlatCSV(ReportPolicy policy, string originalFileName)
        {
            if (String.IsNullOrWhiteSpace(originalFileName))
                return originalFileName;

            string importFileName = Path.GetTempFileName();
            try
            {
                StreamReader reader = new StreamReader(originalFileName);

                List<iGanttMovement> movements = new List<iGanttMovement>();

                string line = reader.ReadLine();
                string[] header = line.Split(',');
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    if (String.IsNullOrWhiteSpace(line))
                        continue;

                    string[] tonnes = line.Split(',');
                    if (tonnes.Length != header.Length)
                        continue;

                    string[] tags = tonnes[0].Trim().Split(' ');
                    if (tags.Length < 3)
                        continue;

                    string units = tonnes[1];

                    string pit = tags[0];
                    string materialType = tags[1];
                    string property = tags[2].ToLower();
                    if (materialType.ToLower().Equals("total"))
                        continue;
                    if (materialType.ToLower().Equals("waste"))
                        materialType = "W";
                    for (int i = 2; i < header.Length - 1; i++)
                    {
                        DateTime sd,fd;

                        if (!DateTime.TryParse(header[i], out sd))
                            continue;
                        if (!DateTime.TryParse((i < header.Length-2 ? header[i+1] : header[i-1]), out fd))
                            continue;
                        int numDays = Math.Abs((fd - sd).Days);

                        sd = new DateTime(sd.Year, sd.Month, sd.Day, 6, 0, 0);
                        fd = sd.AddDays(numDays);

                        double v;
                        if (!Double.TryParse(tonnes[i], out v))
                            continue;

                        iGanttMovement movement = null;
                        for(int j=0; j < movements.Count; j++)
                            if (movements[j].StartDate == sd && movements[j].FinishDate == fd && movements[j].Pit.Equals(pit) && movements[j].MaterialType.Equals(materialType))
                            {
                                movement = movements[j];
                                break;
                            }
                        Boolean addValidNew = false;
                        if (movement == null)
                        {
                            movement = new iGanttMovement { Pit = pit, StartDate = sd, FinishDate = fd, MaterialType = materialType };
                            addValidNew = true;
                        }
                        if (property.Equals("tonnes"))
                            movement.Tonnes = v;
                        else if (property.Equals("volume"))
                            movement.BCM = v;
                        else if (property.Equals("fe"))
                            movement.Fe = v;
                        else if (property.Equals("sio2"))
                            movement.SiO2 = v;
                        else if (property.Equals("al2o3"))
                            movement.Al2O3 = v;
                        else if (property.Equals("p"))
                            movement.P = v;
                        else if (property.Equals("s"))
                            movement.S = v;
                        else if (property.Equals("loi"))
                            movement.LOI1000 = v;
                        else if (property.Equals("mn"))
                            movement.Mn = v;
                        else
                            addValidNew = false;

                        if (addValidNew)
                            movements.Add(movement);
                    }
                }

                reader.Close();

                StreamWriter writer = new StreamWriter(importFileName, false);
                StringBuilder oline = new StringBuilder();
                writer.WriteLine("Operation,Pit,StartDate,FinishDate,MaterialType,Tonnes,BCM,Fe,SiO2,Al2O3,P,S,Mn,LOI1000");
                for (int i = 0; i < movements.Count; i++)
                {
                    oline.Clear();
                    oline.Append(policy.Operation + ",");
                    oline.Append(movements[i].Pit + ",");
                    oline.Append(movements[i].StartDate.ToString("dd/MM/yyyy") + ",");
                    oline.Append(movements[i].FinishDate.ToString("dd/MM/yyyy") +",");
                    oline.Append(movements[i].MaterialType +",");
                    oline.Append(movements[i].Tonnes + ",");
                    oline.Append(movements[i].BCM + ",");
                    oline.Append(movements[i].Fe + ",");
                    oline.Append(movements[i].SiO2 +",");
                    oline.Append(movements[i].Al2O3+",");
                    oline.Append(movements[i].P+",");
                    oline.Append(movements[i].S + ",");
                    oline.Append(movements[i].Mn + ",");
                    oline.Append(movements[i].LOI1000);
                    writer.WriteLine(oline);
                }
                writer.Close();
            }
            catch(System.Exception e)
            {
                return "";
            }


            return importFileName;
        }

        private void ImportData(string importFileName)
        {
            if (String.IsNullOrWhiteSpace(importFileName))
                return;

            try
            {
                StreamReader reader = new StreamReader(importFileName);
                String header = reader.ReadLine();
                if (String.IsNullOrWhiteSpace(header))
                {
                    reader.Close();
                    return;
                }
                String[] fields = header.Split(',');
                Parser defaultParser = EditReportSheetCtrl.PrepareForValidation(this.Report.Sheets[0]);
                Parser importParser = EditReportSheetCtrl.PrepareForValidation(this.Report.Sheets[0]);
                foreach (String fieldName in fields)
                {
                    String goodFieldName = fieldName.Replace(" ", "_");
                    goodFieldName = goodFieldName.Replace(",", "");
                    goodFieldName = goodFieldName.Replace("[", "");
                    goodFieldName = goodFieldName.Replace("]", "");
                    goodFieldName = goodFieldName.Replace("{", "");
                    goodFieldName = goodFieldName.Replace("}", "");
                    goodFieldName = goodFieldName.Replace("}", "");
                    goodFieldName = goodFieldName.Replace("}", "");
                    goodFieldName = goodFieldName.Replace("@", "");
                    goodFieldName = goodFieldName.Replace("#", "");
                    goodFieldName = goodFieldName.Replace("$", "");
                    goodFieldName = goodFieldName.Replace("%", "");
                    goodFieldName = goodFieldName.Replace("^", "");
                    goodFieldName = goodFieldName.Replace("&", "");
                    goodFieldName = goodFieldName.Replace("*", "");
                    goodFieldName = goodFieldName.Replace("(", "");
                    goodFieldName = goodFieldName.Replace(")", "");
                    goodFieldName = goodFieldName.Replace("/", "");
                    goodFieldName = goodFieldName.Replace("?", "");
                    goodFieldName = goodFieldName.Replace("<", "");
                    goodFieldName = goodFieldName.Replace(">", "");
                    goodFieldName = goodFieldName.Replace(".", "");
                    goodFieldName = goodFieldName.Replace("\"", "");
                    goodFieldName = goodFieldName.Replace("'", "");
                    goodFieldName = goodFieldName.Replace(";", "");
                    goodFieldName = goodFieldName.Replace(":", "");

                    importParser.Variables.Add(new Variable("{import" + goodFieldName + "}", typeof(String)));
                }

                ReportRow reportRow = new ReportRow(this.Report.Sheets[0]);
                PropertyDescriptorCollection propDescriptors = reportRow.GetProperties();

                Dictionary<String,ExpressionTree> defaultTrees = new Dictionary<string,ExpressionTree>();
                Dictionary<String, ExpressionTree> importTrees = new Dictionary<string, ExpressionTree>();
                foreach (PropertyDescriptor propDescriptor in propDescriptors)
                {
                    ReportColumnPropertyDescriptor reportDescriptor = propDescriptor as ReportColumnPropertyDescriptor;
                    try
                    {
                        if (!String.IsNullOrWhiteSpace(reportDescriptor.Column.ImportAlias))
                            importTrees[reportDescriptor.Column.Name] = importParser.Parse(reportDescriptor.Column.ImportAlias);
                    }
                    catch (System.Exception exc)
                    {
                    }
                    try
                    {
                        if (!String.IsNullOrWhiteSpace(reportDescriptor.Column.DefaultValue))
                            defaultTrees[reportDescriptor.Column.Name] = defaultParser.Parse(reportDescriptor.Column.DefaultValue);
                    }
                    catch (System.Exception exc)
                    {
                    }
                }

                if (ticIncludeData.Checked)
                {
                    GridEXRow curRow = gridPreviousReports.GetRow();
                    if (curRow != null)
                    {
                        Report includeReport = curRow.DataRow as Report;
                        foreach (ReportRow row in includeReport.Sheets[0].Rows)
                        {
                            reportRow = new ReportRow(this.Report.Sheets[0]);
                            foreach (PropertyDescriptor propDescriptor in propDescriptors)
                                propDescriptor.SetValue(reportRow, row[propDescriptor.Name]);

                            this.Report.Sheets[0].Rows.Add(reportRow);
                        }
                    }
                }

                while (!reader.EndOfStream)
                {
                    String row = reader.ReadLine();
                    String[] values = row.Split(',');
                    for (int i = 0; i < values.Length; i++)
                        values[i] = values[i].Trim();
                    reportRow = new ReportRow(this.Report.Sheets[0]);

                    Boolean isEmptyRow = true;
                    Object[] rowValues = EditReportSheetCtrl.GetRowValues(defaultParser, this.Report, this.Report.Sheets[0], null, null);
                    List<Object> calcValues = new List<object>(rowValues);
                    calcValues.AddRange(values);

                    rowValues = calcValues.ToArray();
                    foreach (PropertyDescriptor propDescriptor in propDescriptors)
                    {
                        ReportColumnPropertyDescriptor reportDescriptor = propDescriptor as ReportColumnPropertyDescriptor;
                        try
                        {
                            if (importTrees.ContainsKey(reportDescriptor.Column.Name))
                            {
                                Object v = importTrees[reportDescriptor.Column.Name].Evaluate(rowValues);
                                if (v == null || String.IsNullOrWhiteSpace(v.ToString()) || v is DBNull)
                                    continue;
                                reportDescriptor.SetValue(reportRow, v);
                                isEmptyRow = false;
                            }
                        }
                        catch (System.Exception exc)
                        {
                        }
                    }

                    rowValues = EditReportSheetCtrl.GetRowValues(defaultParser, this.Report, this.Report.Sheets[0], reportRow, null);
                    foreach (PropertyDescriptor propDescriptor in propDescriptors)
                    {
                        ReportColumnPropertyDescriptor reportDescriptor = propDescriptor as ReportColumnPropertyDescriptor;
                        try
                        {
                            if (defaultTrees.ContainsKey(reportDescriptor.Column.Name))
                            {
                                Object v = reportDescriptor.GetValue(reportRow);
                                if (!String.IsNullOrWhiteSpace(reportDescriptor.Column.DefaultValue) && (v == null || String.IsNullOrWhiteSpace(v.ToString())))
                                {
                                    Object result = defaultTrees[reportDescriptor.Column.Name].Evaluate(rowValues);
                                    reportDescriptor.SetValue(reportRow, result);
                                }
                            }
                        }
                        catch (System.Exception exc)
                        {
                        }
                    }


                    if (!isEmptyRow)
                        this.Report.Sheets[0].Rows.Add(reportRow);
                }
            }
            catch (System.Exception exc)
            {
                MessageBox.Show("Data import failed", "Data Import", MessageBoxButtons.OK);
                throw exc;
            }
        }

        public static void ImportStream(Report report, ReportSheet sheet, StreamReader reader, ReportSheet includeSheet = null)
        {
            try
            {
                String header = reader.ReadLine();
                if (String.IsNullOrWhiteSpace(header))
                {
                    reader.Close();
                    return;
                }
                String[] fields = CreateNewReportDlg.SplitCSV(header);
                Parser defaultParser = EditReportSheetCtrl.PrepareForValidation(sheet);
                Parser importParser = EditReportSheetCtrl.PrepareForValidation(sheet);
                foreach (String fieldName in fields)
                {
                    String goodFieldName = fieldName.Replace(" ", "_");
                    goodFieldName = goodFieldName.Replace(",", "");
                    goodFieldName = goodFieldName.Replace("[", "");
                    goodFieldName = goodFieldName.Replace("]", "");
                    goodFieldName = goodFieldName.Replace("{", "");
                    goodFieldName = goodFieldName.Replace("}", "");
                    goodFieldName = goodFieldName.Replace("}", "");
                    goodFieldName = goodFieldName.Replace("}", "");
                    goodFieldName = goodFieldName.Replace("@", "");
                    goodFieldName = goodFieldName.Replace("#", "");
                    goodFieldName = goodFieldName.Replace("$", "");
                    goodFieldName = goodFieldName.Replace("%", "");
                    goodFieldName = goodFieldName.Replace("^", "");
                    goodFieldName = goodFieldName.Replace("&", "");
                    goodFieldName = goodFieldName.Replace("*", "");
                    goodFieldName = goodFieldName.Replace("(", "");
                    goodFieldName = goodFieldName.Replace(")", "");
                    goodFieldName = goodFieldName.Replace("/", "");
                    goodFieldName = goodFieldName.Replace("?", "");
                    goodFieldName = goodFieldName.Replace("<", "");
                    goodFieldName = goodFieldName.Replace(">", "");
                    goodFieldName = goodFieldName.Replace(".", "");
                    goodFieldName = goodFieldName.Replace("\"", "");
                    goodFieldName = goodFieldName.Replace("'", "");
                    goodFieldName = goodFieldName.Replace(";", "");
                    goodFieldName = goodFieldName.Replace(":", "");

                    importParser.Variables.Add(new Variable("{import" + goodFieldName + "}", typeof(String)));
                }

                ReportRow reportRow = new ReportRow(sheet);
                PropertyDescriptorCollection propDescriptors = reportRow.GetProperties();

                Dictionary<String, ExpressionTree> defaultTrees = new Dictionary<string, ExpressionTree>();
                Dictionary<String, ExpressionTree> importTrees = new Dictionary<string, ExpressionTree>();
                foreach (PropertyDescriptor propDescriptor in propDescriptors)
                {
                    ReportColumnPropertyDescriptor reportDescriptor = propDescriptor as ReportColumnPropertyDescriptor;
                    try
                    {
                        if (!String.IsNullOrWhiteSpace(reportDescriptor.Column.ImportAlias))
                            importTrees[reportDescriptor.Column.Name] = importParser.Parse(reportDescriptor.Column.ImportAlias);
                    }
                    catch (System.Exception exc)
                    {
                    }
                    try
                    {
                        if (!String.IsNullOrWhiteSpace(reportDescriptor.Column.DefaultValue))
                            defaultTrees[reportDescriptor.Column.Name] = defaultParser.Parse(reportDescriptor.Column.DefaultValue);
                    }
                    catch (System.Exception exc)
                    {
                    }
                }

                if (includeSheet != null)
                {
                    foreach (ReportRow row in includeSheet.Rows)
                    {
                        reportRow = new ReportRow(sheet);
                        foreach (PropertyDescriptor propDescriptor in propDescriptors)
                            propDescriptor.SetValue(reportRow, row[propDescriptor.Name]);

                        sheet.Rows.Add(reportRow);
                    }
                }

                while (!reader.EndOfStream)
                {
                    String row = reader.ReadLine();
                    String[] values = CreateNewReportDlg.SplitCSV(row);
                    for (int i = 0; i < values.Length; i++)
                        values[i] = values[i].Trim();
                    reportRow = new ReportRow(sheet);

                    Boolean isEmptyRow = true;
                    Object[] rowValues = EditReportSheetCtrl.GetRowValues(defaultParser, report, sheet, null, null);
                    List<Object> calcValues = new List<object>(rowValues);
                    calcValues.AddRange(values);

                    rowValues = calcValues.ToArray();
                    foreach (PropertyDescriptor propDescriptor in propDescriptors)
                    {
                        ReportColumnPropertyDescriptor reportDescriptor = propDescriptor as ReportColumnPropertyDescriptor;
                        try
                        {
                            if (importTrees.ContainsKey(reportDescriptor.Column.Name))
                            {
                                if (rowValues.Length == importTrees[reportDescriptor.Column.Name].Variables.Count)
                                {
                                    Object v = importTrees[reportDescriptor.Column.Name].Evaluate(rowValues);
                                    if (v == null || String.IsNullOrWhiteSpace(v.ToString()) || v is DBNull)
                                        continue;
                                    reportDescriptor.SetValue(reportRow, v);
                                    isEmptyRow = false;
                                }
                            }
                        }
                        catch (System.Exception exc)
                        {
                        }
                    }

                    rowValues = EditReportSheetCtrl.GetRowValues(defaultParser, report, sheet, reportRow, null);
                    foreach (PropertyDescriptor propDescriptor in propDescriptors)
                    {
                        ReportColumnPropertyDescriptor reportDescriptor = propDescriptor as ReportColumnPropertyDescriptor;
                        try
                        {
                            if (defaultTrees.ContainsKey(reportDescriptor.Column.Name))
                            {
                                if (rowValues.Length == defaultTrees[reportDescriptor.Column.Name].Variables.Count)
                                {
                                    Object v = reportDescriptor.GetValue(reportRow);
                                    if (!String.IsNullOrWhiteSpace(reportDescriptor.Column.DefaultValue) && (v == null || String.IsNullOrWhiteSpace(v.ToString())))
                                    {
                                        Object result = defaultTrees[reportDescriptor.Column.Name].Evaluate(rowValues);
                                        reportDescriptor.SetValue(reportRow, result);
                                    }
                                }
                            }
                        }
                        catch (System.Exception exc)
                        {
                        }
                    }


                    if (!isEmptyRow)
                        sheet.Rows.Add(reportRow);
                }
            }
            catch (System.Exception exc)
            {
                throw exc;
            }
        }

        private static String[] SplitCSV(String line)
        {
            List<String> values = new List<string>();
            int l = 0;
            String v = "";
            while(l < line.Length)
            {
                if (line[l] == '"')
                {
                    while (line[++l] != '"')
                        v += line[l];
                }
                if (line[l] == ',')
                {
                    values.Add(v.Trim());
                    v = "";
                    l++;
                }
                if (l < line.Length && line[l] != ',')
                {
                    v += line[l];
                    l++;
                }
            }
            if (!String.IsNullOrWhiteSpace(v))
                values.Add(v.Trim());
            return values.ToArray();
        }

        private void butAllActive_Click(object sender, EventArgs e)
        {
            foreach (GridEXRow row in gridItems.GetRows())
            {
                row.BeginEdit();
                row.Cells["Activated"].Value = true;
                row.EndEdit();
            }
        }

        private void butAllInActive_Click(object sender, EventArgs e)
        {
            foreach (GridEXRow row in gridItems.GetRows())
            {
                row.BeginEdit();
                row.Cells["Activated"].Value = false;
                row.EndEdit();
            }
        }

        private void gridItems_RecordUpdated(object sender, EventArgs e)
        {
            Report.UpdateLinkedReferenceLists(this.Report.GetAllReferenceLists(this.Operation, this.Definitions, false));
        }
    }
}
