using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using AtlasReportToolkit;

namespace AtlasReportAdministrator
{
    public partial class ConfigureDlg : Form
    {
        public ConfigureDlg()
        {
            InitializeComponent();
        }

        public String Operation { get; set; }
        public ReportDefinitions Definitions { get; set; }

        private void butConfigurePolicies_Click(object sender, EventArgs e)
        {
            DefineReportingPoliciesDlg policies = new DefineReportingPoliciesDlg();
            policies.Definitions = this.Definitions;
            policies.ShowDialog();
        }

        private void butConfigureTemplates_Click(object sender, EventArgs e)
        {
            DefineReportTemplatesDlg templates = new DefineReportTemplatesDlg();
            templates.Definitions = this.Definitions;
            templates.ShowDialog();
        }

        private void butConfigureDictionary_Click(object sender, EventArgs e)
        {
            DefineReportReferenceListsDlg lists = new DefineReportReferenceListsDlg();
            lists.Definitions = this.Definitions;
            lists.ShowDialog();
        }
        
        private void butClose_Click(object sender, EventArgs e)
        {
            try
            {
                ReportDefinitions.SaveFullConfig(this.Definitions, Properties.Settings.Default.ConfigurationDirectory, Properties.Settings.Default.ConfigurationFilename);
            }
            catch (System.Exception exc)
            {
            }

            this.Close();
        }

        private void ConfigureDlg_Load(object sender, EventArgs e)
        {
            m_ignoreConfigFileChange = true;
            txtConfigDirectory.DataBindings.Add("Text", Properties.Settings.Default, "ConfigurationDirectory", false, DataSourceUpdateMode.OnPropertyChanged);
            txtCurrentOperation.DataBindings.Add("Text", this, "Operation", false, DataSourceUpdateMode.OnPropertyChanged);
            m_ignoreConfigFileChange = false;
        }

        private void txtConfigFile_ButtonClick(object sender, EventArgs e)
        {
            FolderBrowserDialog browse = new FolderBrowserDialog();
            browse.SelectedPath = txtConfigDirectory.Text;
            if (browse.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                txtConfigDirectory.Text = browse.SelectedPath;
        }

        private Boolean m_ignoreConfigFileChange = true;
        private void txtConfigFile_TextChanged(object sender, EventArgs e)
        {
            if (m_ignoreConfigFileChange)
                return;

            try
            {
                if (!String.IsNullOrWhiteSpace(txtConfigDirectory.Text))
                {
                    this.Definitions = ReportDefinitions.LoadFullConfig(Properties.Settings.Default.ConfigurationDirectory, Properties.Settings.Default.ConfigurationFilename);

                    Properties.Settings.Default.ConfigurationDirectory = txtConfigDirectory.Text;
                    Properties.Settings.Default.Operation = this.Operation;
                    Properties.Settings.Default.Save();
                }
            }
            catch (System.Exception exc)
            {
                MessageBox.Show("An error occurred when reading from this configuration directory.", "Error", MessageBoxButtons.OK);
            }
        }
    }
}
