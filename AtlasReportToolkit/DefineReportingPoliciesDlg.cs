using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AtlasReportToolkit;
using Janus.Windows.EditControls;
using Janus.Windows.GridEX;

namespace AtlasReportToolkit
{
    public partial class DefineReportingPoliciesDlg : Form
    {
        public DefineReportingPoliciesDlg()
        {
            InitializeComponent();
        }

        public ReportDefinitions Definitions { get; set; }

        private void DefineReportingPoliciesDlg_Load(object sender, EventArgs e)
        {
            gridSelectedTemplates.SetDataBinding(this.Definitions.Templates, null);
            try
            {
                lstReportPolicies.DataSource = this.Definitions.Policies;
                //lstReportPolicies.Items.Clear();
                //foreach (ReportPolicy policy in this.Policies)
                //    lstReportPolicies.Items.Add(policy);
                if (this.Definitions.Policies.Count > 0)
                    lstReportPolicies.SelectedValue = this.Definitions.Policies[0];
            }
            catch (System.Exception exc)
            {
            }
        }

        private void lstReportPolicies_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstReportPolicies.SelectedValue == null)
                return;

            ReportPolicy policy = lstReportPolicies.SelectedValue as ReportPolicy;

            ReportTemplate template = (from a in this.Definitions.Templates where a.Identifier.Equals(policy.TemplateId) select a).FirstOrDefault();

            txtName.DataBindings.Clear();
            txtOperation.DataBindings.Clear();
            txtContractor.DataBindings.Clear();
            txtReportsDirectory.DataBindings.Clear();
            txtPendingDirectory.DataBindings.Clear();
            txtPublishDirectory.DataBindings.Clear();
            txtApprovedDirectory.DataBindings.Clear();
            txtSendToEmailList.DataBindings.Clear();
            txtSendCCEmailList.DataBindings.Clear();
            txtReplyToEmailList.DataBindings.Clear();
            lstPublishFormat.DataBindings.Clear();
            lstImportFileFormat.DataBindings.Clear();
            ticIncludePreviousReports.DataBindings.Clear();
            ticRemovePreviousData.DataBindings.Clear();
            lstRemoveReportDataColumn.DataBindings.Clear();
            txtRemoveReportDataDays.DataBindings.Clear();
            txtArchiveDirectory.DataBindings.Clear();
            txtDBConnectionString.DataBindings.Clear();

            txtName.DataBindings.Add("Text", policy, "Name",false,DataSourceUpdateMode.OnPropertyChanged);
            txtOperation.DataBindings.Add("Text", policy, "Operation", false, DataSourceUpdateMode.OnPropertyChanged);
            txtContractor.DataBindings.Add("Text", policy, "Contractor", false, DataSourceUpdateMode.OnPropertyChanged);
            txtReportsDirectory.DataBindings.Add("Text", policy, "ReportsDirectory", false, DataSourceUpdateMode.OnPropertyChanged);
            txtPendingDirectory.DataBindings.Add("Text", policy, "RelativePendingDirectory", false, DataSourceUpdateMode.OnPropertyChanged);
            txtApprovedDirectory.DataBindings.Add("Text", policy, "RelativeApprovedDirectory", false, DataSourceUpdateMode.OnPropertyChanged);
            txtPublishDirectory.DataBindings.Add("Text", policy, "PublishedDirectory", false, DataSourceUpdateMode.OnPropertyChanged);
            txtSendToEmailList.DataBindings.Add("Text", policy, "SendToEmailList", false, DataSourceUpdateMode.OnPropertyChanged);
            txtSendCCEmailList.DataBindings.Add("Text", policy, "SendCCEmailList", false, DataSourceUpdateMode.OnPropertyChanged);
            txtReplyToEmailList.DataBindings.Add("Text", policy, "ReplyToEmailList", false, DataSourceUpdateMode.OnPropertyChanged);
            lstPublishFormat.DataBindings.Add("SelectedValue", policy, "PublishFormat", false, DataSourceUpdateMode.OnPropertyChanged);
            lstImportFileFormat.DataBindings.Add("SelectedValue", policy, "ImportFileFormat", false, DataSourceUpdateMode.OnPropertyChanged);
            ticIncludePreviousReports.DataBindings.Add("Checked", policy, "IncludePreviousReports", false, DataSourceUpdateMode.OnPropertyChanged);
            ticRemovePreviousData.DataBindings.Add("Checked", policy, "RemoveReportData", false, DataSourceUpdateMode.OnPropertyChanged);
            txtRemoveReportDataDays.DataBindings.Add("Text", policy, "RemoveReportDataDays", false, DataSourceUpdateMode.OnPropertyChanged);
            lstRemoveReportDataColumn.DataBindings.Add("Enabled", ticRemovePreviousData, "Checked", false, DataSourceUpdateMode.OnPropertyChanged);
            txtRemoveReportDataDays.DataBindings.Add("Enabled", ticRemovePreviousData, "Checked", false, DataSourceUpdateMode.OnPropertyChanged);
            txtArchiveDirectory.DataBindings.Add("Text", policy, "RelativeArchiveDirectory", false, DataSourceUpdateMode.OnPropertyChanged);
            txtDBConnectionString.DataBindings.Add("Text", policy, "DBConnectionString", false, DataSourceUpdateMode.OnPropertyChanged);            

            lstPublishFormat.Items.Clear();
            foreach (ReportPolicy.ReportPublishFormat format in Enum.GetValues(typeof(ReportPolicy.ReportPublishFormat)))
            {
                lstPublishFormat.Items.Add(format.ToString(), format);
                if (format == policy.PublishFormat)
                    lstPublishFormat.SelectedValue = format;
            }

            lstImportFileFormat.Items.Clear();
            foreach (ReportPolicy.ImportFormatType format in Enum.GetValues(typeof(ReportPolicy.ImportFormatType)))
            {
                lstImportFileFormat.Items.Add(format.ToString(), format);
                if (format == policy.ImportFileFormat)
                    lstImportFileFormat.SelectedValue = format;
            }

            foreach (GridEXRow row in gridSelectedTemplates.GetRows())
                row.IsChecked = false;

            foreach (Guid templateId in policy.TemplateIds)
            {
                foreach (GridEXRow row in gridSelectedTemplates.GetRows())
                {
                    if ((row.DataRow as ReportTemplate).Identifier.Equals(templateId))
                    {
                        row.IsChecked = true;
                        break;
                    }
                }
            }

            gridHeaderDefaults.RootTable.Columns["ColumnName"].ValueList.Clear();
            foreach (ReportTemplateColumn column in template.Header)
                gridHeaderDefaults.RootTable.Columns["ColumnName"].ValueList.Add(column.Name, column.Name);
            gridHeaderDefaults.SetDataBinding(policy.HeaderDefaults, null);

            lstRemoveReportDataColumn.Items.Clear();
            foreach (ReportTemplateColumn column in template.Rows)
            {
                lstRemoveReportDataColumn.Items.Add(new Janus.Windows.EditControls.UIComboBoxItem(column.Name));
                if (column.Name.Equals(policy.RemoveReportDataDateColumn))
                    lstRemoveReportDataColumn.SelectedValue = column.Name;
            }
        }

        private void butClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lstTemplates_ValueChanged(object sender, EventArgs e)
        {

        }

        private void butAddPolicy_Click(object sender, EventArgs e)
        {
            PromptForTextDlg prompt = new PromptForTextDlg { Prompt = "Report Policy Name" };
            if (prompt.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ReportPolicy policy = new ReportPolicy();
                policy.Name = prompt.Answer;
                policy.TemplateId = this.Definitions.Templates[0].Identifier;
                this.Definitions.Policies.Add(policy);

                lstReportPolicies.SelectedValue = policy;
            }
        }

        private void butRemovePolicy_Click(object sender, EventArgs e)
        {
            if (lstReportPolicies.SelectedValue == null)
                return;
            ReportPolicy policy = lstReportPolicies.SelectedValue as ReportPolicy;
            this.Definitions.Policies.Remove(policy);
            if (this.Definitions.Policies.Count > 0)
                lstReportPolicies.SelectedValue = this.Definitions.Policies[0];
            else
                lstReportPolicies.SelectedValue = null;
        }

        private void txtReportsDirectory_ButtonClick(object sender, EventArgs e)
        {
            FolderBrowserDialog browse = new FolderBrowserDialog();
            browse.SelectedPath = txtReportsDirectory.Text;
            if (browse.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                txtReportsDirectory.Text = browse.SelectedPath;
        }

        private void gridRowDefaults_GetNewRow(object sender, Janus.Windows.GridEX.GetNewRowEventArgs e)
        {
            e.NewRow = new ReportPolicyColumnDefault();
        }

        private void gridHeaderDefaults_GetNewRow(object sender, Janus.Windows.GridEX.GetNewRowEventArgs e)
        {
            e.NewRow = new ReportPolicyColumnDefault();
        }

        private void lstRemoveReportDataColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstReportPolicies.SelectedValue == null || lstRemoveReportDataColumn.SelectedItem == null)
                return;

            ReportPolicy policy = lstReportPolicies.SelectedValue as ReportPolicy;
            policy.RemoveReportDataDateColumn = lstRemoveReportDataColumn.SelectedItem.Text;

        }

        private void lstTemplates_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void gridSelectedTemplates_SelectionChanged(object sender, EventArgs e)
        {
            GridEXRow row = gridSelectedTemplates.GetRow();

            if (row == null == null || !row.IsChecked)
                return;
            ReportPolicy policy = lstReportPolicies.SelectedValue as ReportPolicy;

            ReportTemplate template = row.DataRow as ReportTemplate;

            gridHeaderDefaults.RootTable.Columns["ColumnName"].ValueList.Clear();
            foreach (ReportTemplateColumn column in template.Header)
                gridHeaderDefaults.RootTable.Columns["ColumnName"].ValueList.Add(column.Name, column.Name);

            lstRemoveReportDataColumn.Items.Clear();
            foreach (ReportTemplateColumn column in template.Rows)
            {
                lstRemoveReportDataColumn.Items.Add(new Janus.Windows.EditControls.UIComboBoxItem(column.Name));
                if (column.Name.Equals(policy.RemoveReportDataDateColumn))
                    lstRemoveReportDataColumn.SelectedValue = column.Name;
            }
        }

        private void gridSelectedTemplates_RowCheckStateChanged(object sender, RowCheckStateChangeEventArgs e)
        {
            ReportPolicy policy = lstReportPolicies.SelectedValue as ReportPolicy;
            policy.TemplateIds.Clear();
            foreach (GridEXRow row in gridSelectedTemplates.GetRows())
            {
                if (row.IsChecked)
                {
                    policy.TemplateIds.Add((row.DataRow as ReportTemplate).Identifier);
                }
            }
        }

        private void butCopy_Click(object sender, EventArgs e)
        {
            if (lstReportPolicies.SelectedValue == null)
                return;

            ReportPolicy policy = lstReportPolicies.SelectedValue as ReportPolicy;

            PromptForTextDlg prompt = new PromptForTextDlg { Prompt = "Report Policy Name" };
            if (prompt.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ReportPolicy newPolicy = new ReportPolicy();
                newPolicy = Extensions.BinaryClone<ReportPolicy>(policy);
                newPolicy.Identifier = Guid.NewGuid();
                newPolicy.Name = prompt.Answer;

                this.Definitions.Policies.Add(newPolicy);

                lstReportPolicies.SelectedValue = newPolicy;
            }
        }
    }
}
