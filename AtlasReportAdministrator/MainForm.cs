using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using AtlasReportToolkit;

namespace AtlasReportAdministrator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            this.Definitions = new ReportDefinitions();
            this.Definitions.Templates = new BindingList<ReportTemplate>();
            this.Definitions.Templates.Add(new ReportTemplate { Name = "Test", Description = "Test2" });

            this.Definitions.Policies = new BindingList<ReportPolicy>();
            this.Definitions.Policies.Add(new ReportPolicy { Name = "Testy", Contractor = "IES", TemplateId = this.Definitions.Templates[0].Identifier });

            this.Definitions.ReferenceLists = new BindingList<ReportReferenceList>();
            this.Definitions.ReferenceLists.Add(new ReportReferenceList { Name = "Shift", Items = new List<ReportReferenceListValue>() { new ReportReferenceListValue { Value = "Day" },new ReportReferenceListValue { Value = "Night" } } });

            this.Definitions.ReferenceLists = new BindingList<ReportReferenceList>();
            this.Definitions.ReferenceLists.Add(new ReportReferenceList { Name = "Shift", Items = new List<ReportReferenceListValue>() { new ReportReferenceListValue { Value = "Day" }, new ReportReferenceListValue { Value = "Night" } } });

            this.Reports = new BindingList<Report>();
        }

        private ReportDefinitions m_definitions = null;
        public ReportDefinitions Definitions 
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(m_definitions.ConfigFileName))
                    if (!m_definitions.ConfigDateTime.Equals(File.GetLastWriteTime(m_definitions.ConfigFileName)))
                    {
                        m_definitions = ReportDefinitions.LoadFullConfig(Properties.Settings.Default.ConfigurationDirectory, Properties.Settings.Default.ConfigurationFilename, true);
                    }
                return m_definitions;
            }
            set
            {
                m_definitions = value;
            }
        }
        public BindingList<Report> Reports { get; set; }

        private void butConfigure_Click(object sender, EventArgs e)
        {
            try
            {
                ReportDefinitions definition = ReportDefinitions.LoadFullConfig(Properties.Settings.Default.ConfigurationDirectory,Properties.Settings.Default.ConfigurationFilename);

                ConfigureDlg config = new ConfigureDlg();
                config.Definitions = this.Definitions;
                config.Operation = Properties.Settings.Default.Operation;
                config.ShowDialog();
                Properties.Settings.Default.Operation = config.Operation;
                Properties.Settings.Default.Save();
                this.Definitions = config.Definitions;
            }
            catch (System.Exception exc)
            {
                MessageBox.Show("There is an unknown error while trying to configure the reports." + exc.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Version v = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            this.Text = "Atlas Report Manager V" + v.ToString();

            if (Properties.Settings.Default.ConfigurationDirectory.Contains(@"MINING\01_Pardoo\24_Daily Reporting\Daily reporting management\Config"))
                Properties.Settings.Default.ConfigurationDirectory = @"\\atl-04\10_Pilbara_Ops\12_Daily_Report\Report Manager\Mines\Config";

            SetCurrentOperationDlg set = new SetCurrentOperationDlg();
            set.CurrentOperation = Properties.Settings.Default.Operation;
            set.ShowDialog();

            try
            {
                Properties.Settings.Default.FirstInstallSetup = false;
                Properties.Settings.Default.Operation = set.CurrentOperation;
                Properties.Settings.Default.Save();
                if (String.IsNullOrWhiteSpace(Properties.Settings.Default.Operation))
                {
                    this.Close();
                    return;
                }
            }
            catch (System.Exception exc)
            {
                MessageBox.Show("An error occurred when setting the default operation [" + exc.Message + "].", "Error", MessageBoxButtons.OK);
            }

            try
            {
                Boolean configured = false;
                Boolean errorDetected = false;
                while (!configured)
                {
                    if (String.IsNullOrWhiteSpace(Properties.Settings.Default.ConfigurationDirectory) || errorDetected)
                    {
                        FolderBrowserDialog browse = new FolderBrowserDialog();
                        browse.Description = "Select reporting configuration directory";
                        if (browse.ShowDialog() != DialogResult.OK)
                        {
                            this.Close();
                            return;
                        }
                        Properties.Settings.Default.ConfigurationDirectory = browse.SelectedPath;
                        Properties.Settings.Default.Save();
                    }

                    try
                    {
                        RefreshReports();
                        configured = true;
                    }
                    catch (System.Exception exc)
                    {
                        MessageBox.Show("An error occurred when reading the reports [" + exc.Message + "] : " + Properties.Settings.Default.ConfigurationDirectory);
                        errorDetected = true;
                    }
                }
            }
            catch (System.Exception exc)
            {
            }

            if (this.Definitions == null)
                this.Definitions = new ReportDefinitions();
        }

        private void RefreshReports()
        {
            this.Reports.Clear();

            this.Definitions = ReportDefinitions.LoadFullConfig(Properties.Settings.Default.ConfigurationDirectory, Properties.Settings.Default.ConfigurationFilename, false);
            if (this.Definitions == null)
                return;

            List<Report> allReports = new List<Report>();
            List<String> directories = new List<String>();
            foreach (ReportPolicy policy in this.Definitions.Policies)
            {
                if (!String.IsNullOrWhiteSpace(Properties.Settings.Default.Operation) && !Properties.Settings.Default.Operation.Equals(policy.Operation) && !Properties.Settings.Default.Operation.Equals(ReportDefinitions.BypassOperationCode))
                    continue;

                if (directories.Contains(policy.ReportsDirectory))
                    continue;
                directories.Add(policy.ReportsDirectory);

                BindingList<Report> pendingReports = Report.LoadReports(policy.ReportsDirectory + "\\" + policy.RelativePendingDirectory);

                BindingList<Report> approvedReports = Report.LoadReports(policy.ReportsDirectory + "\\" + policy.RelativeApprovedDirectory);

                allReports.AddRange(pendingReports);
                allReports.AddRange(approvedReports);
            }

            List<Report> processedReports = new List<Report>();
            foreach (Report report in allReports)
            {
                try
                {
                    Report repeatedReport = (from a in processedReports where a.ReportPolicyId.Equals(report.ReportPolicyId) && a.ReportingPeriod.Equals(report.ReportingPeriod) select a).FirstOrDefault();
                    if (repeatedReport == null)
                        this.Reports.Add(report);
                    else
                    {
                        MessageBox.Show("Multiple copies of the same report have been detected and ignored [" + report.Name + " - " + report.Status.ToString() + "] and [" + repeatedReport.Name + " - " + repeatedReport.Status.ToString() + "]");
                        if (this.Reports.Contains(repeatedReport))
                            this.Reports.Remove(repeatedReport);
                    }

                    processedReports.Add(report);
                }
                catch (System.Exception exc)
                {
                }
            }
            this.Reports = new BindingList<Report>(this.Reports.OrderBy(a => a.ReportingPeriod).ToList());
            gridReportStatus.SetDataBinding(this.Reports, null);
        }

        private void gridReportStatus_SelectionChanged(object sender, EventArgs e)
        {
            SaveCurrentReport();

            GridEXRow curRow = gridReportStatus.GetRow();
            if (curRow == null)
            {
                editReport.ActiveReport = null;
                return;
            }

            editReport.ActiveReport = curRow.DataRow as Report;
        }

        private void SaveCurrentReport()
        {
            if (editReport.ActiveReport != null)
            {
                ReportPolicy policy = (from a in this.Definitions.Policies where a.Identifier.Equals(editReport.ActiveReport.ReportPolicyId) select a).FirstOrDefault();
                if (policy != null)
                    editReport.ActiveReport.Save(editReport.ActiveReport.GetCurrentFileName(policy.ReportsDirectory),false);
            }
        }

        private Report RefreshReport(Report report)
        {
            if (report != null)
            {
                ReportPolicy policy = (from a in this.Definitions.Policies where a.Identifier.Equals(report.ReportPolicyId) select a).FirstOrDefault();
                if (policy != null)
                    report = Extensions.Load<Report>(report.GetCurrentFileName(policy.ReportsDirectory));
            }
            return report;
        }
        
        private void butCreateNewReport_Click(object sender, EventArgs e)
        { 
            try
            {
                this.Definitions = ReportDefinitions.LoadFullConfig(Properties.Settings.Default.ConfigurationDirectory, Properties.Settings.Default.ConfigurationFilename,false);

                CreateNewReportDlg create = new CreateNewReportDlg() { Definitions = this.Definitions, Reports = this.Reports, Operation = Properties.Settings.Default.Operation, ConfigurationDirectory = Properties.Settings.Default.ConfigurationDirectory };
                if (create.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.Reports.Add(create.Report);
                    editReport.ActiveReport = create.Report;
                    int pos = -1;
                    foreach (GridEXRow row in gridReportStatus.GetRows())
                        if (row.DataRow == create.Report)
                        {
                            pos = row.Position;
                            break;
                        }
                    if (pos >= 0)
                        gridReportStatus.Row = pos;

                    ReportPolicy policy = (from a in this.Definitions.Policies where a.Identifier.Equals(create.Report.ReportPolicyId) select a).FirstOrDefault();
                    if (policy == null)
                        throw new ApplicationException();

                    String fileName = create.Report.GetPendingFileName(policy.ReportsDirectory);
                    create.Report.Save(fileName, true);
                }
            }
            catch (System.Exception exc)
            {
                MessageBox.Show("There was an error processing the request. The internal description is (" + exc.Message + ").", "Error", MessageBoxButtons.OK);
            }
        }

        private void butClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                SaveCurrentReport();
            }
            catch (System.Exception exc)
            {
            }
        }

        private void butSend_Click(object sender, EventArgs e)
        {
            if (editReport.ActiveReport == null)
                return;

            try
            {
                ReportPolicy policy = (from a in this.Definitions.Policies where a.Identifier.Equals(editReport.ActiveReport.ReportPolicyId) select a).FirstOrDefault();
                if (policy == null)
                    throw new ApplicationException();

                String fileName = editReport.ActiveReport.GetPendingFileName(policy.ReportsDirectory);

                editReport.ActiveReport.Status = ReportStatus.PendingApproval;
                editReport.ActiveReport.Save(fileName,true);

                if (!String.IsNullOrWhiteSpace(editReport.ActiveReport.SendToEmailList))
                {
                    SendEmail email = new SendEmail();
                    email.AddRecipientTo(editReport.ActiveReport.SendToEmailList);
                    if (!String.IsNullOrWhiteSpace(editReport.ActiveReport.SendCCEmailList))
                        email.AddRecipientCC(editReport.ActiveReport.SendCCEmailList);

                    email.AddAttachment(fileName);

                    String subject = "ATLAS : Requested activity report <" + editReport.ActiveReport.Name + "> for period " + editReport.ActiveReport.ReportingPeriod.ToString();
                    String body = "";
                    email.SendMailPopup(subject, body);
                }
                else
                    MessageBox.Show("The information has been requested.", "Atlas Reports", MessageBoxButtons.OK);

                gridReportStatus.Refetch();

                GridEXRow curRow = gridReportStatus.GetRow();
                if (curRow == null)
                {
                    editReport.ActiveReport = null;
                    return;
                }

                editReport.ActiveReport = curRow.DataRow as Report;
            }
            catch (System.Exception exc)
            {
                MessageBox.Show("There was an error processing the request. The internal description is (" + exc.Message + ").", "Error", MessageBoxButtons.OK);
            }
        }

        private void butApprove_Click(object sender, EventArgs e)
        {
            if (editReport.ActiveReport == null)
                return;

            try
            {
                if (editReport.ActiveReport.Status != ReportStatus.PendingApproval && editReport.ActiveReport.Status != ReportStatus.Requested)
                {
                    MessageBox.Show("You cannot approve a report that has just been created or already approved.", "Atlas Reports", MessageBoxButtons.OK);
                    return;
                }

                ReportPolicy policy = (from a in this.Definitions.Policies where a.Identifier.Equals(editReport.ActiveReport.ReportPolicyId) select a).FirstOrDefault();
                if (policy == null)
                    throw new ApplicationException();

                editReport.ActiveReport.Status = ReportStatus.Approved;
                editReport.ActiveReport.Save(editReport.ActiveReport.GetApprovedFileName(policy.ReportsDirectory),true);
                Report.PublishReportCSV(editReport.ActiveReport, policy, policy.PublishedDirectory);

                File.Delete(editReport.ActiveReport.GetPendingFileName(policy.ReportsDirectory));

                gridReportStatus.Refetch();
                if (!String.IsNullOrWhiteSpace(editReport.ActiveReport.SendToEmailList))
                {
                    SendEmail email = new SendEmail();
                    email.AddRecipientTo(editReport.ActiveReport.SendToEmailList);
                    if (!String.IsNullOrWhiteSpace(editReport.ActiveReport.SendCCEmailList))
                        email.AddRecipientCC(editReport.ActiveReport.SendCCEmailList);

                    email.AddAttachment(editReport.ActiveReport.GetApprovedFileName(policy.ReportsDirectory));

                    String subject = "ATLAS : Approved activity report <" + editReport.ActiveReport.Name + "> for period " + editReport.ActiveReport.ReportingPeriod.ToString();
                    String body = "";
                    email.SendMailPopup(subject, body);
                }
                else
                {
                    MessageBox.Show("The report has been approved.", "Atlas Reports", MessageBoxButtons.OK);
                }
                GridEXRow curRow = gridReportStatus.GetRow();
                if (curRow == null)
                {
                    editReport.ActiveReport = null;
                    return;
                }

                editReport.ActiveReport = curRow.DataRow as Report;
            }
            catch (System.Exception exc)
            {
                MessageBox.Show("There was an error processing the request. The internal description is (" + exc.Message + ").", "Error", MessageBoxButtons.OK);
            }
        }

        private void butRemoveCreatedReport_Click(object sender, EventArgs e)
        {
            if (editReport.ActiveReport == null)
            {
                MessageBox.Show("A  report must be selected first.", "Error", MessageBoxButtons.OK);
                return;
            }

            if (editReport.ActiveReport.Status != ReportStatus.Created)
            {
                if (MessageBox.Show("You want to remove a report with an advanced status (i.e. not <Created>. Do you still want to remove it?.", "Error", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                    return;
            }

            try
            {
                ReportPolicy policy = (from a in this.Definitions.Policies where a.Identifier.Equals(editReport.ActiveReport.ReportPolicyId) select a).FirstOrDefault();
                if (policy == null)
                    throw new ApplicationException();

                Report curReport = editReport.ActiveReport;
                editReport.ActiveReport = null;

                File.Delete(curReport.GetPendingFileName(policy.ReportsDirectory));
                File.Delete(curReport.GetApprovedFileName(policy.ReportsDirectory));
                
                foreach(Report thisReport in this.Reports.ToList())
                    if (curReport.Name.Equals(thisReport.Name))
                        this.Reports.Remove(thisReport);

                gridReportStatus.SelectedItems.Clear();
                if (this.Reports.Count > 0)
                    gridReportStatus.Row = 0;
            }
            catch (System.Exception exc)
            {
                editReport.ActiveReport = null;
            }
        }

        private void butPublish_Click(object sender, EventArgs e)
        {
            try
            {
                ReportPolicy policy = (from a in this.Definitions.Policies where a.Identifier.Equals(editReport.ActiveReport.ReportPolicyId) select a).FirstOrDefault();
                if (policy == null)
                    throw new ApplicationException();

                editReport.ActiveReport.Save(policy.ReportsDirectory);

                if (policy.PublishFormat == ReportPolicy.ReportPublishFormat.CSV)
                {
                    Report.PublishReportCSV(editReport.ActiveReport, policy, policy.PublishedDirectory);

                    MessageBox.Show("The report has been published.", "Atlas Reports", MessageBoxButtons.OK);
                }
                else if (policy.PublishFormat == ReportPolicy.ReportPublishFormat.SQL)
                {
                    Report.PublishReportSQL(editReport.ActiveReport, policy);

                    MessageBox.Show("The report has been published.", "Atlas Reports", MessageBoxButtons.OK);
                }
                else if (policy.PublishFormat == ReportPolicy.ReportPublishFormat.XML)
                {
                    String fileName = policy.PublishedDirectory + "\\" + editReport.ActiveReport.Name + ".xml";

                    Report.PublishReportXML(editReport.ActiveReport, fileName);

                    MessageBox.Show("The report has been published.", "Atlas Reports", MessageBoxButtons.OK);
                }
                else if (policy.PublishFormat == ReportPolicy.ReportPublishFormat.AURTransaction)
                {
                    List<AURTransferFile> files = new List<AURTransferFile>() 
                    { 
                        new AURTransferFile { AURFileName = policy.PublishedDirectory + "\\" + editReport.ActiveReport.Name + ".csv", AURFileNamePrefix = AURTransaction.c_AURiHubFileNamePrefix } ,
                        new AURTransferFile { AURFileName = policy.PublishedDirectory + "\\" + editReport.ActiveReport.Name + "SampleResults.csv", AURFileNamePrefix = AURSampleResults.c_AURiHubFileNamePrefix } 
                    };

                    Report.PublishReportAUR(editReport.ActiveReport, files);

                    TransferMovementsDlg transfer = new TransferMovementsDlg();
                    transfer.TransferFiles = files;
                    transfer.iHubPendingDirectory = Properties.Settings.Default.iHubPendingDirectory;
                    transfer.iHubProcessedDirectory = Properties.Settings.Default.iHubProcessedDirectory;
                    transfer.iHubProcessingDirectory = Properties.Settings.Default.iHubProcessingDirectory;
                    transfer.iHubErrorDirectory = Properties.Settings.Default.iHubErrorDirectory;

                    transfer.ShowDialog();
                }
            }
            catch (System.Exception exc)
            {
                MessageBox.Show("There was an error processing the request. The internal description is (" + exc.Message + ").", "Error", MessageBoxButtons.OK);
            }
        }

        private void butAutoSize_Click(object sender, EventArgs e)
        {
            editReport.FillSelectedCells((int)(txtNumToRepeat.Value));
//            editReport.AutoSizeColumns();
        }

        private void butRefresh_Click(object sender, EventArgs e)
        {
            RefreshReports();

            gridReportStatus.SelectedItems.Clear();
            if (this.Reports.Count > 0)
                gridReportStatus.Row = 0;
            else
                editReport.ActiveReport = null;
        }

        private void butGodMode_Click(object sender, EventArgs e)
        {
            if (editReport.ActiveReport == null)
                return;

            editReport.EnterGodMode();
        }

        private void butUnapproveReport_Click(object sender, EventArgs e)
        {
            if (editReport.ActiveReport == null)
                return;

            try
            {
                if (editReport.ActiveReport.Status != ReportStatus.Approved)
                {
                    MessageBox.Show("You cannot unapprove a report that has not yet been approved.", "Atlas Reports", MessageBoxButtons.OK);
                    return;
                }

                ReportPolicy policy = (from a in this.Definitions.Policies where a.Identifier.Equals(editReport.ActiveReport.ReportPolicyId) select a).FirstOrDefault();
                if (policy == null)
                    throw new ApplicationException();

                editReport.ActiveReport.Status = ReportStatus.PendingApproval;
                editReport.ActiveReport.Save(editReport.ActiveReport.GetPendingFileName(policy.ReportsDirectory), true);

                File.Delete(editReport.ActiveReport.GetApprovedFileName(policy.ReportsDirectory));

                gridReportStatus.Refetch();
                MessageBox.Show("The report has been unapproved.", "Atlas Reports", MessageBoxButtons.OK);

                GridEXRow curRow = gridReportStatus.GetRow();
                if (curRow == null)
                {
                    editReport.ActiveReport = null;
                    return;
                }

                editReport.ActiveReport = curRow.DataRow as Report;
            }
            catch (System.Exception exc)
            {
                MessageBox.Show("There was an error processing the request. The internal description is (" + exc.Message + ").", "Error", MessageBoxButtons.OK);
            }
        }

        private void butArchive_Click(object sender, EventArgs e)
        {
            if (editReport.ActiveReport == null)
                return;

            try
            {
                if (editReport.ActiveReport.Status != ReportStatus.Approved)
                {
                    MessageBox.Show("You cannot archive a report that has not yet been approved.", "Atlas Reports", MessageBoxButtons.OK);
                    return;
                }

                ReportPolicy policy = (from a in this.Definitions.Policies where a.Identifier.Equals(editReport.ActiveReport.ReportPolicyId) select a).FirstOrDefault();
                if (policy == null)
                    throw new ApplicationException();

                editReport.ActiveReport.Save(editReport.ActiveReport.GetArchiveFileName(policy.ReportsDirectory), true);

                if (File.Exists(editReport.ActiveReport.GetArchiveFileName(policy.ReportsDirectory)))
                    File.Delete(editReport.ActiveReport.GetCurrentFileName(policy.ReportsDirectory));
                else
                {
                    MessageBox.Show("There was error when archiving this report [" + editReport.ActiveReport.GetCurrentFileName(policy.ReportsDirectory) + "]", "Atlas Reports", MessageBoxButtons.OK);
                    return;
                }

                this.Reports.Remove(editReport.ActiveReport);
                editReport.ActiveReport = null;

                gridReportStatus.Refetch();
                MessageBox.Show("The report has been archived.", "Atlas Reports", MessageBoxButtons.OK);

                GridEXRow curRow = gridReportStatus.GetRow();
                if (curRow == null)
                {
                    editReport.ActiveReport = null;
                    return;
                }

                editReport.ActiveReport = curRow.DataRow as Report;
            }
            catch (System.Exception exc)
            {
                MessageBox.Show("There was an error processing the request. The internal description is (" + exc.Message + ").", "Error", MessageBoxButtons.OK);
            }
        }

        private void butValidate_Click(object sender, EventArgs e)
        {
            if (editReport.ActiveReport != null)
            {
                editReport.ValidateReport();
            }
        }

        private void butChangeLists_Click(object sender, EventArgs e)
        {
            if (editReport.ActiveReport != null)
            {
                ConfigureReferenceListsDlg configDlg = new ConfigureReferenceListsDlg { Operation = editReport.ActiveReport.Operation, ConfigurationDirectory = Properties.Settings.Default.ConfigurationDirectory, Report = editReport.ActiveReport, Definitions = this.Definitions };
                configDlg.ShowDialog();

                ReportPolicy policy = (from a in this.Definitions.Policies where a.Identifier.Equals(configDlg.Report.ReportPolicyId) select a).FirstOrDefault();
                if (policy == null)
                    throw new ApplicationException();

                editReport.ActiveReport = configDlg.Report;

                editReport.ActiveReport.Save(policy.ReportsDirectory);
            }
        }

        private void butCopy_Click(object sender, EventArgs e)
        {
            if (this.editReport.ActiveReport != null)
                this.editReport.CopySelectedCells(false);
        }

        private void butTools_Click(object sender, EventArgs e)
        {
            ManageWodginaMainWeighbridgeDlg wbDlg = new ManageWodginaMainWeighbridgeDlg();
            wbDlg.ShowDialog();
        }

        private void butOpenPending_Click(object sender, EventArgs e)
        {
            if (editReport.ActiveReport == null)
                return;

            ReportPolicy policy = (from a in this.Definitions.Policies where a.Identifier.Equals(editReport.ActiveReport.ReportPolicyId) select a).FirstOrDefault();
            if (policy == null)
                throw new ApplicationException();

            System.Diagnostics.Process.Start(policy.ReportsDirectory + "\\" + policy.RelativePendingDirectory);
        }
    }
}
