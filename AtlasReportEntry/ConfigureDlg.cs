using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AtlasReportEntry
{
    public partial class ConfigureDlg : Form
    {
        public ConfigureDlg()
        {
            InitializeComponent();
        }

        public List<ReportsDirectory> Directories { get; set; }

        private void gridDirectories_GetNewRow(object sender, Janus.Windows.GridEX.GetNewRowEventArgs e)
        {
            e.NewRow = new ReportsDirectory() { Directory = "<Click button to browse for the directory>" };
        }

        private void gridDirectories_ColumnButtonClick(object sender, Janus.Windows.GridEX.ColumnActionEventArgs e)
        {
            FolderBrowserDialog browse = new FolderBrowserDialog();
            browse.SelectedPath = gridDirectories.GetRow().Cells[e.Column].Text;
            if (browse.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                gridDirectories.GetRow().BeginEdit();
                gridDirectories.GetRow().Cells[e.Column].Value = browse.SelectedPath;
                gridDirectories.GetRow().EndEdit();
                gridDirectories.Refresh();
            }
        }

        private void ConfigureDlg_Load(object sender, EventArgs e)
        {
            gridDirectories.SetDataBinding(this.Directories, null);
        }

        private void butClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
