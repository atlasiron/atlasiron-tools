using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using AtlasReportToolkit;

namespace AtlasReportToolkit
{
    public partial class DefineReportTemplatesDlg : Form
    {
        public DefineReportTemplatesDlg()
        {
            InitializeComponent();

            this.AvailableColumns = new List<ReportTemplateColumn>();
            this.AvailableColumns.Add(new ReportTemplateColumn { Name = "Contractor", Type = ReportTemplateColumnType.Text });
            this.AvailableColumns.Add(new ReportTemplateColumn { Name = "Mine", Type = ReportTemplateColumnType.Text });
            this.AvailableColumns.Add(new ReportTemplateColumn { Name = "Report Date", Type = ReportTemplateColumnType.Date });
            this.AvailableColumns.Add(new ReportTemplateColumn { Name = "Day", Type = ReportTemplateColumnType.Date });
            this.AvailableColumns.Add(new ReportTemplateColumn { Name = "Date", Type = ReportTemplateColumnType.Date });
            this.AvailableColumns.Add(new ReportTemplateColumn { Name = "Time", Type = ReportTemplateColumnType.DateTime });
            this.AvailableColumns.Add(new ReportTemplateColumn { Name = "Shift", Type = ReportTemplateColumnType.Text });
            this.AvailableColumns.Add(new ReportTemplateColumn { Name = "Pit", Type = ReportTemplateColumnType.Text });
            this.AvailableColumns.Add(new ReportTemplateColumn { Name = "Bench", Type = ReportTemplateColumnType.Text });
            this.AvailableColumns.Add(new ReportTemplateColumn { Name = "Shot", Type = ReportTemplateColumnType.Text });
            this.AvailableColumns.Add(new ReportTemplateColumn { Name = "BlockId", Type = ReportTemplateColumnType.Text });
            this.AvailableColumns.Add(new ReportTemplateColumn { Name = "MaterialType", Type = ReportTemplateColumnType.Text });
            this.AvailableColumns.Add(new ReportTemplateColumn { Name = "From", Type = ReportTemplateColumnType.Text });
            this.AvailableColumns.Add(new ReportTemplateColumn { Name = "To", Type = ReportTemplateColumnType.Text });
            this.AvailableColumns.Add(new ReportTemplateColumn { Name = "Loader", Type = ReportTemplateColumnType.Text });
            this.AvailableColumns.Add(new ReportTemplateColumn { Name = "Driver", Type = ReportTemplateColumnType.Text });
            this.AvailableColumns.Add(new ReportTemplateColumn { Name = "Truck", Type = ReportTemplateColumnType.Text });
            this.AvailableColumns.Add(new ReportTemplateColumn { Name = "Tonnes", Type = ReportTemplateColumnType.Numeric });
            this.AvailableColumns.Add(new ReportTemplateColumn { Name = "BCM", Type = ReportTemplateColumnType.Numeric });
            this.AvailableColumns.Add(new ReportTemplateColumn { Name = "Loads", Type = ReportTemplateColumnType.Numeric });
            this.AvailableColumns.Add(new ReportTemplateColumn { Name = "Burden", Type = ReportTemplateColumnType.Numeric });
            this.AvailableColumns.Add(new ReportTemplateColumn { Name = "Spacing", Type = ReportTemplateColumnType.Numeric });
            this.AvailableColumns.Add(new ReportTemplateColumn { Name = "SubDrill", Type = ReportTemplateColumnType.Numeric });
            this.AvailableColumns.Add(new ReportTemplateColumn { Name = "HoleDiameter", Type = ReportTemplateColumnType.Numeric });
            this.AvailableColumns.Add(new ReportTemplateColumn { Name = "DrillRig", Type = ReportTemplateColumnType.Text });
            this.AvailableColumns.Add(new ReportTemplateColumn { Name = "DrillMetres", Type = ReportTemplateColumnType.Numeric });
            this.AvailableColumns.Add(new ReportTemplateColumn { Name = "DrillHours", Type = ReportTemplateColumnType.Numeric });
            this.AvailableColumns.Add(new ReportTemplateColumn { Name = "AxleWeight1", Type = ReportTemplateColumnType.Numeric });
            this.AvailableColumns.Add(new ReportTemplateColumn { Name = "AxleWeight2", Type = ReportTemplateColumnType.Numeric });
            this.AvailableColumns.Add(new ReportTemplateColumn { Name = "AxleWeight3", Type = ReportTemplateColumnType.Numeric });
            this.AvailableColumns.Add(new ReportTemplateColumn { Name = "AxleWeight4", Type = ReportTemplateColumnType.Numeric });
            this.AvailableColumns.Add(new ReportTemplateColumn { Name = "AxleWeight5", Type = ReportTemplateColumnType.Numeric });
            this.AvailableColumns.Add(new ReportTemplateColumn { Name = "AxleWeight6", Type = ReportTemplateColumnType.Numeric });
            this.AvailableColumns.Add(new ReportTemplateColumn { Name = "AxleWeight7", Type = ReportTemplateColumnType.Numeric });
            this.AvailableColumns.Add(new ReportTemplateColumn { Name = "AxleWeight8", Type = ReportTemplateColumnType.Numeric });
            this.AvailableColumns.Add(new ReportTemplateColumn { Name = "AxleWeight9", Type = ReportTemplateColumnType.Numeric });
            this.AvailableColumns.Add(new ReportTemplateColumn { Name = "AxleWeight10", Type = ReportTemplateColumnType.Numeric });
            this.AvailableColumns.Add(new ReportTemplateColumn { Name = "GrossWeight", Type = ReportTemplateColumnType.Numeric });
            this.AvailableColumns.Add(new ReportTemplateColumn { Name = "TareWeight", Type = ReportTemplateColumnType.Numeric });
            this.AvailableColumns.Add(new ReportTemplateColumn { Name = "NetWeight", Type = ReportTemplateColumnType.Numeric });
            this.AvailableColumns.Add(new ReportTemplateColumn { Name = "GDocket", Type = ReportTemplateColumnType.Text });
            this.AvailableColumns.Add(new ReportTemplateColumn { Name = "PrimaryFrom", Type = ReportTemplateColumnType.Text });
            this.AvailableColumns.Add(new ReportTemplateColumn { Name = "PrimaryBuckets", Type = ReportTemplateColumnType.Numeric });
            this.AvailableColumns.Add(new ReportTemplateColumn { Name = "SecondaryFrom", Type = ReportTemplateColumnType.Text });
            this.AvailableColumns.Add(new ReportTemplateColumn { Name = "SecondaryBuckets", Type = ReportTemplateColumnType.Numeric });
            this.AvailableColumns.Add(new ReportTemplateColumn { Name = "Weightometer", Type = ReportTemplateColumnType.Numeric });
            this.AvailableColumns.Add(new ReportTemplateColumn { Name = "SampleBucket", Type = ReportTemplateColumnType.Numeric });
        }

        private List<ReportTemplateColumn> AvailableColumns { get; set; }

        public ReportDefinitions Definitions { get; set; }

        private ReportTemplate ActiveTemplate { get; set; }

        private void butAddTemplate_Click(object sender, EventArgs e)
        {
            PromptForTextDlg prompt = new PromptForTextDlg { Prompt = "Report Template Name" };
            if (prompt.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ReportTemplate template = new ReportTemplate();
                template.Name = prompt.Answer;
                this.Definitions.Templates.Add(template);

                lstTemplates.SelectedItem = template;
            }
        }

        private void butRemoveTemplate_Click(object sender, EventArgs e)
        {
            if (lstTemplates.SelectedItem == null)
                return;
            ReportTemplate template = lstTemplates.SelectedItem as ReportTemplate;
            this.Definitions.Templates.Remove(template);

            if (this.Definitions.Templates.Count > 0)
                lstTemplates.SelectedItem = this.Definitions.Templates[0];
            else
                lstTemplates.SelectedItem = null;
        }

        private void butAddHeaderColumn_Click(object sender, EventArgs e)
        {
            if (this.ActiveTemplate == null)
                return;

            foreach (GridEXSelectedItem row in gridAvailableColumns.SelectedItems)
            {
                ReportTemplateColumn column = row.GetRow().DataRow as ReportTemplateColumn;
                if (!(from a in this.ActiveTemplate.Header where a.Name.Equals(column.Name) select true).FirstOrDefault())
                    this.ActiveTemplate.Header.Add(Extensions.BinaryClone<ReportTemplateColumn>(column));
            }
        }

        private void butRemoveHeaderColumn_Click(object sender, EventArgs e)
        {
            if (this.ActiveTemplate == null)
                return;

            List<ReportTemplateColumn> columns = new List<ReportTemplateColumn>();
            foreach (GridEXSelectedItem row in gridHeaderColumns.SelectedItems)
                columns.Add(row.GetRow().DataRow as ReportTemplateColumn);

            foreach(ReportTemplateColumn column in columns)
                this.ActiveTemplate.Header.Remove(column);
        }

        private void butAddRowColumn_Click(object sender, EventArgs e)
        {
            if (this.ActiveTemplate == null)
                return;

            foreach (GridEXSelectedItem row in gridAvailableColumns.SelectedItems)
            {
                ReportTemplateColumn column = row.GetRow().DataRow as ReportTemplateColumn;
                if (!(from a in this.ActiveTemplate.Rows where a.Name.Equals(column.Name) select true).FirstOrDefault())
                    this.ActiveTemplate.Rows.Add(Extensions.BinaryClone<ReportTemplateColumn>(column));
            }
        }

        private void butRemoveRowColumn_Click(object sender, EventArgs e)
        {
            if (this.ActiveTemplate == null)
                return;

            List<ReportTemplateColumn> columns = new List<ReportTemplateColumn>();
            foreach (GridEXSelectedItem row in gridRowColumns.SelectedItems)
                columns.Add(row.GetRow().DataRow as ReportTemplateColumn);

            foreach (ReportTemplateColumn column in columns)
                this.ActiveTemplate.Rows.Remove(column);
        }

        private void DefineReportTemplatesDlg_Load(object sender, EventArgs e)
        {
            lstTemplates.DropDownList.DataSource = this.Definitions.Templates;
            
            gridAvailableColumns.SetDataBinding(this.AvailableColumns, null);

            butAddHeaderColumn.Enabled = false;
            butRemoveHeaderColumn.Enabled = false;
            butCreateHeaderColumn.Enabled = false;
            butAddRowColumn.Enabled = false;
            butRemoveRowColumn.Enabled = false;
            butCreateRowColumn.Enabled = false;

            if (this.Definitions.Templates.Count > 0)
                lstTemplates.SelectedItem = this.Definitions.Templates[0];
        }

        private void lstTemplates_ValueChanged(object sender, EventArgs e)
        {           
            this.ActiveTemplate = lstTemplates.SelectedItem as ReportTemplate;
            if (this.ActiveTemplate == null)
                return;

            ticAllowAddNew.DataBindings.Clear();
            ticAllowAddNew.DataBindings.Add("Checked", this.ActiveTemplate, "AllowAddNew", false, DataSourceUpdateMode.OnPropertyChanged);
            ticAllowDelete.DataBindings.Clear();
            ticAllowDelete.DataBindings.Add("Checked", this.ActiveTemplate, "AllowDelete", false, DataSourceUpdateMode.OnPropertyChanged);
            ticAllowFilter.DataBindings.Clear();
            ticAllowFilter.DataBindings.Add("Checked", this.ActiveTemplate, "AllowFilter", false, DataSourceUpdateMode.OnPropertyChanged);
            ticShowTotals.DataBindings.Clear();
            ticShowTotals.DataBindings.Add("Checked", this.ActiveTemplate, "ShowTotals", false, DataSourceUpdateMode.OnPropertyChanged);
            ticAllowInsertFromPaste.DataBindings.Clear();
            ticAllowInsertFromPaste.DataBindings.Add("Checked", this.ActiveTemplate, "AllowInsertFromPaste", false, DataSourceUpdateMode.OnPropertyChanged);
            txtAURPublishTarget.DataBindings.Clear();
            txtAURPublishTarget.DataBindings.Add("Text", this.ActiveTemplate, "AURPublishTarget", false, DataSourceUpdateMode.OnPropertyChanged);
            ticShowSubtotals.DataBindings.Clear();
            ticShowSubtotals.DataBindings.Add("Checked", this.ActiveTemplate, "ShowSubTotals", false, DataSourceUpdateMode.OnPropertyChanged);

            gridHeaderColumns.SetDataBinding(this.ActiveTemplate.Header, null);
            gridRowColumns.SetDataBinding(this.ActiveTemplate.Rows, null);
            butAddHeaderColumn.Enabled = true;
            butRemoveHeaderColumn.Enabled = true;
            butCreateHeaderColumn.Enabled = true;
            butAddRowColumn.Enabled = true;
            butRemoveRowColumn.Enabled = true;
            butCreateRowColumn.Enabled = true;
        }

        private void butClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridHeaderColumns_RowDoubleClick(object sender, RowActionEventArgs e)
        {
            EditReportColumnDlg edit = new EditReportColumnDlg();
            edit.Definitions = this.Definitions;
            edit.Column = (e.Row.DataRow as ReportTemplateColumn);
            edit.ShowDialog();

            gridHeaderColumns.Refetch();
        }

        private void gridRowColumns_RowDoubleClick(object sender, RowActionEventArgs e)
        {
            EditReportColumnDlg edit = new EditReportColumnDlg();
            edit.Column = (e.Row.DataRow as ReportTemplateColumn);
            edit.Definitions = this.Definitions;
            edit.ShowDialog();

            gridRowColumns.Refetch();
        }

        private void butCreateHeaderColumn_Click(object sender, EventArgs e)
        {
            EditReportColumnDlg edit = new EditReportColumnDlg();
            edit.Column = new ReportTemplateColumn();
            edit.Definitions = this.Definitions;
            if (edit.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.ActiveTemplate.Header.Add(edit.Column);

            gridRowColumns.Refetch();
        }

        private void butCreateRowColumn_Click(object sender, EventArgs e)
        {
            EditReportColumnDlg edit = new EditReportColumnDlg();
            edit.Column = new ReportTemplateColumn();
            edit.Definitions = this.Definitions;
            if (edit.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.ActiveTemplate.Rows.Add(edit.Column);

            gridRowColumns.Refetch();
        }

        private void butMoveHeaderUp_Click(object sender, EventArgs e)
        {
            gridHeaderColumns.SelectedItems.Sort();
            List<ReportTemplateColumn> columns = new List<ReportTemplateColumn>();
            for (int i = 0; i < gridHeaderColumns.SelectedItems.Count; i++)
                columns.Add(gridHeaderColumns.SelectedItems[i].GetRow().DataRow as ReportTemplateColumn);

            foreach (ReportTemplateColumn column in columns)
            {
                for(int j=0; j < this.ActiveTemplate.Header.Count; j++)
                    if (this.ActiveTemplate.Header[j] == column && j > 0)
                    {
                        this.ActiveTemplate.Header.RemoveAt(j);
                        this.ActiveTemplate.Header.Insert(j - 1, column);
                        break;
                    }
            }

            gridHeaderColumns.SelectedItems.Clear();
            foreach(GridEXRow row in gridHeaderColumns.GetRows())
                if (columns.Contains(row.DataRow as ReportTemplateColumn))
                    gridHeaderColumns.SelectedItems.Add(row.Position);
        }

        private void butMoveHeaderDown_Click(object sender, EventArgs e)
        {
            gridHeaderColumns.SelectedItems.Sort();
            List<ReportTemplateColumn> columns = new List<ReportTemplateColumn>();
            for (int i = gridHeaderColumns.SelectedItems.Count - 1; i >= 0 ; i--)
                columns.Add(gridHeaderColumns.SelectedItems[i].GetRow().DataRow as ReportTemplateColumn);

            foreach(ReportTemplateColumn column in columns)
            {
                for (int j = 0; j < this.ActiveTemplate.Header.Count; j++)
                    if (this.ActiveTemplate.Header[j] == column && j < this.ActiveTemplate.Header.Count - 1)
                    {
                        this.ActiveTemplate.Header.RemoveAt(j);
                        this.ActiveTemplate.Header.Insert(j + 1, column);
                        break;
                    }
            }

            gridHeaderColumns.SelectedItems.Clear();
            foreach (GridEXRow row in gridHeaderColumns.GetRows())
                if (columns.Contains(row.DataRow as ReportTemplateColumn))
                    gridHeaderColumns.SelectedItems.Add(row.Position);
        }

        private void butMoveRowUp_Click(object sender, EventArgs e)
        {
            gridRowColumns.SelectedItems.Sort();
            List<ReportTemplateColumn> columns = new List<ReportTemplateColumn>();
            for (int i = 0; i < gridRowColumns.SelectedItems.Count; i++)
                columns.Add(gridRowColumns.SelectedItems[i].GetRow().DataRow as ReportTemplateColumn);

            foreach (ReportTemplateColumn column in columns)
            {
                for (int j = 0; j < this.ActiveTemplate.Rows.Count; j++)
                    if (this.ActiveTemplate.Rows[j] == column && j > 0)
                    {
                        this.ActiveTemplate.Rows.RemoveAt(j);
                        this.ActiveTemplate.Rows.Insert(j - 1, column);
                        break;
                    }
            }

            gridRowColumns.SelectedItems.Clear();
            foreach (GridEXRow row in gridRowColumns.GetRows())
                if (columns.Contains(row.DataRow as ReportTemplateColumn))
                    gridRowColumns.SelectedItems.Add(row.Position);
        }

        private void butMoveRowDown_Click(object sender, EventArgs e)
        {
            gridRowColumns.SelectedItems.Sort();
            List<ReportTemplateColumn> columns = new List<ReportTemplateColumn>();
            for (int i = gridRowColumns.SelectedItems.Count - 1; i >= 0; i--)
                columns.Add(gridRowColumns.SelectedItems[i].GetRow().DataRow as ReportTemplateColumn);

            foreach (ReportTemplateColumn column in columns)
            {
                for (int j = 0; j < this.ActiveTemplate.Rows.Count; j++)
                    if (this.ActiveTemplate.Rows[j] == column && j < this.ActiveTemplate.Rows.Count - 1)
                    {
                        this.ActiveTemplate.Rows.RemoveAt(j);
                        this.ActiveTemplate.Rows.Insert(j + 1, column);
                        break;
                    }
            }

            gridRowColumns.SelectedItems.Clear();
            foreach (GridEXRow row in gridRowColumns.GetRows())
                if (columns.Contains(row.DataRow as ReportTemplateColumn))
                    gridRowColumns.SelectedItems.Add(row.Position);
        }
    }
}
