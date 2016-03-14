using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AtlasReportToolkit;

namespace AtlasReportToolkit
{
    public partial class EditReportColumnDlg : Form
    {
        public EditReportColumnDlg()
        {
            InitializeComponent();
        }

        public ReportDefinitions Definitions { get; set; }
        public ReportTemplateColumn Column { get; set; }

        private void EditReportColumnDlg_Load(object sender, EventArgs e)
        {
            lstReferenceList.DisplayMember = "Name";
            lstReferenceList.ValueMember = "Name";
            lstReferenceList.DataSource = this.Definitions.ReferenceLists;

            ticHidden.DataBindings.Add("Checked", this.Column, "Hidden", false, DataSourceUpdateMode.OnPropertyChanged);
            txtName.DataBindings.Add("Text",this.Column,"Name",false,DataSourceUpdateMode.OnPropertyChanged);
            lstType.DataBindings.Add("SelectedValue", this.Column, "Type", false, DataSourceUpdateMode.OnPropertyChanged);
            lstSubTotalType.DataBindings.Add("SelectedValue", this.Column, "SubTotalType", false, DataSourceUpdateMode.OnPropertyChanged);
            ticMandatory.DataBindings.Add("Checked", this.Column, "Mandatory", false, DataSourceUpdateMode.OnPropertyChanged);
            ticReadOnly.DataBindings.Add("Checked", this.Column, "ReadOnly", false, DataSourceUpdateMode.OnPropertyChanged);
            txtDefault.DataBindings.Add("Text", this.Column, "DefaultValue", false, DataSourceUpdateMode.OnPropertyChanged);
            txtHelp.DataBindings.Add("Text", this.Column, "Help", false, DataSourceUpdateMode.OnPropertyChanged);
            ticLimitToReferenceList.DataBindings.Add("Checked", this.Column, "LimitToReferenceList", false, DataSourceUpdateMode.OnPropertyChanged);
            ticAutoDefault.DataBindings.Add("Checked", this.Column, "AllowAutoDefault", false, DataSourceUpdateMode.OnPropertyChanged);
            ticConfigOnCreate.DataBindings.Add("Checked", this.Column, "ConfigureReferenceListOnCreateReport", false, DataSourceUpdateMode.OnPropertyChanged);
            txtImportAlias.DataBindings.Add("Text", this.Column, "ImportAlias", false, DataSourceUpdateMode.OnPropertyChanged);
            txtValidation.DataBindings.Add("Text", this.Column, "Validation", false, DataSourceUpdateMode.OnPropertyChanged);
            txtErrorMessage.DataBindings.Add("Text", this.Column, "ErrorMessage", false, DataSourceUpdateMode.OnPropertyChanged);
            txtCalculation.DataBindings.Add("Text", this.Column, "Calculation", false, DataSourceUpdateMode.OnPropertyChanged);
            lstReferenceList.DataBindings.Add("Text", this.Column, "ReferenceListName", false, DataSourceUpdateMode.OnPropertyChanged);
            ticForceUppercase.DataBindings.Add("Checked", this.Column, "ForceUppercase", false, DataSourceUpdateMode.OnPropertyChanged);
            ticForceLowercase.DataBindings.Add("Checked", this.Column, "ForceLowercase", false, DataSourceUpdateMode.OnPropertyChanged);
            txtAURColumn.DataBindings.Add("Text", this.Column, "AURColumn", false, DataSourceUpdateMode.OnPropertyChanged);
            txtAURValue.DataBindings.Add("Text", this.Column, "AURValue", false, DataSourceUpdateMode.OnPropertyChanged);
            
            foreach (ReportTemplateColumnType type in Enum.GetValues(typeof(ReportTemplateColumnType)))
                lstType.Items.Add(type);
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
