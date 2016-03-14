using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AtlasReportAdministrator
{
    public partial class SetCurrentOperationDlg : Form
    {
        public SetCurrentOperationDlg()
        {
            InitializeComponent();
        }


        public String CurrentOperation { get; set; }

        private void SetCurrentOperationDlg_Load(object sender, EventArgs e)
        {
            txtCurrentOperation.DataBindings.Add("Text", this, "CurrentOperation", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void butClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
