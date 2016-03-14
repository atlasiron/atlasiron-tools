using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AtlasReportToolkit;

namespace AtlasReportEntry
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public BindingList<Report> Reports { get; set; }
        public List<ReportsDirectory> Directories { get; set; }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Reports = new BindingList<Report>();
            this.Directories = new List<ReportsDirectory>();
            if (Properties.Settings.Default.ReportsDirectory != null && Properties.Settings.Default.ReportsDirectory.Count > 0)
            {
                foreach (String reportDirectory in Properties.Settings.Default.ReportsDirectory)
                {
                    this.Directories.Add(new ReportsDirectory { Directory = reportDirectory });
                    try
                    {
                        BindingList<Report> reports = Report.LoadReports(reportDirectory);
                        foreach (Report report in reports)
                            this.Reports.Add(report);
                    }
                    catch (System.Exception exc)
                    {
                    }
                }
            }

            butPublish.Enabled = false;
            butAutoFill.Enabled = false;
            butValidate.Enabled = false;
            butSubmit.Enabled = false;
            butClose.Enabled = true;
            lstReports.DropDownList.DataSource = this.Reports;
            if (this.Reports.Count > 0)
                lstReports.SelectedItem = this.Reports[0];
        }

        private void lstReports_ValueChanged(object sender, EventArgs e)
        {
            if (editReport.ActiveReport != null)
                editReport.ActiveReport.Save(editReport.ActiveReport.CurrentFileName);

            if (lstReports.SelectedItem == null)
            {
                editReport.ActiveReport = null;
                return;
            }
            Report report = lstReports.SelectedItem as Report;
            if (report == null)
            {
                editReport.ActiveReport = null;
                return;
            }
            editReport.ActiveReport = report;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (editReport.ActiveReport != null)
                editReport.ActiveReport.Save(editReport.ActiveReport.CurrentFileName);
        }

        private void butClose_Click(object sender, EventArgs e)
        {
            if (editReport.ActiveReport != null)
                editReport.ActiveReport.Save(editReport.ActiveReport.CurrentFileName);
            this.Close();
        }

        private void butSubmit_Click(object sender, EventArgs e)
        {
            if (editReport.ActiveReport != null)
            {
                editReport.ActiveReport.Status = ReportStatus.PendingApproval;
                editReport.ActiveReport.Save(editReport.ActiveReport.CurrentFileName, true);
                lstReports.DataSource = this.Reports;
                lstReports.SelectedItem = editReport.ActiveReport;
                if (!String.IsNullOrWhiteSpace(editReport.ActiveReport.ReplyToEmailList))
                {
                    SendEmail email = new SendEmail();
                    email.AddRecipientTo(editReport.ActiveReport.ReplyToEmailList);
                    email.AddAttachment(editReport.ActiveReport.CurrentFileName);

                    String subject = "ATLAS REPORT : Submitted activity report <" + editReport.ActiveReport.Name + "> for period " + editReport.ActiveReport.ReportingPeriod.ToString();
                    String body = "";
                    if (email.SendMailPopup(subject, body) > 1)
                    {
                        MessageBox.Show("The report could not be sent by email automatically. Please send the report file using your normal email tool.", "Error", MessageBoxButtons.OK);
                    }
                }
                else
                    MessageBox.Show("Report has been submitted.", "Atlas Reports", MessageBoxButtons.OK);
            }
        }

        private void butConfigure_Click(object sender, EventArgs e)
        {
            ConfigureDlg configure = new ConfigureDlg() { Directories = this.Directories };
            configure.ShowDialog();

            Properties.Settings.Default.ReportsDirectory = new System.Collections.Specialized.StringCollection();
            foreach (ReportsDirectory directory in this.Directories)
                Properties.Settings.Default.ReportsDirectory.Add(directory.Directory);
            Properties.Settings.Default.Save();
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            if (editReport.ActiveReport == null)
            {
                MessageBox.Show("You must pick a report to publish first.", "Error", MessageBoxButtons.OK);
                return;
            }
            if (editReport.ActiveReport.Status != ReportStatus.Approved)
            {
                MessageBox.Show("You may only publish approved reports.", "Error", MessageBoxButtons.OK);
                return;
            }

            SaveFileDialog saveAs = new SaveFileDialog();
            saveAs.DefaultExt = "xml";
            saveAs.AddExtension = true;
            saveAs.Filter = "Atlas Published Reports (*.xml)|*.xml||";
            if (saveAs.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (String.Equals(saveAs.FileName, editReport.ActiveReport.CurrentFileName, StringComparison.OrdinalIgnoreCase))
                    MessageBox.Show("You may not save the published report on top of the Atlas report. This will overwrite the existing data.", "Error", MessageBoxButtons.OK);
                else
                    Report.PublishReportXML(editReport.ActiveReport, saveAs.FileName);
            }
        }

        private void butOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.AddExtension = true;
            openDlg.DefaultExt = "xml";
            openDlg.Filter = "Atlas Report Files (" + Report.AtlasReportPrefix + "*.xml)|" + Report.AtlasReportPrefix + "*.xml||";
            if (openDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Report report = Report.LoadReport(openDlg.FileName);
                this.Reports.Add(report);

                lstReports.SelectedItem = report;
            }
            butPublish.Enabled = true;
            butAutoFill.Enabled = true;
            butValidate.Enabled = true;
            butSubmit.Enabled = false;
            butClose.Enabled = false;
        }

        private void butAutoFill_Click(object sender, EventArgs e)
        {
            if (this.editReport.ActiveReport != null)
                this.editReport.FillSelectedCells();
        }

        private void butCopy_Click(object sender, EventArgs e)
        {
            if (this.editReport.ActiveReport != null)
                this.editReport.CopySelectedCells(true);
        }

        private void butWidenColumns_Click(object sender, EventArgs e)
        {
            editReport.WidenColumns();
        }

        private void butValidate_Click(object sender, EventArgs e)
        {
            if (editReport.ActiveReport != null)
            {
                editReport.ValidateReport();
                butSubmit.Enabled = true;
                butClose.Enabled = true;
            }
        }
    }
}
