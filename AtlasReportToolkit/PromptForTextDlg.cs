using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AtlasReportToolkit
{
    public partial class PromptForTextDlg : Form
    {
        public PromptForTextDlg()
        {
            InitializeComponent();
        }

        public String Prompt { get; set; }
        public String Answer { get; set; }

        private void PromptForTextDlg_Load(object sender, EventArgs e)
        {
            label1.Text = this.Prompt;
            txtAnswer.DataBindings.Add("Text", this, "Answer");
        }
    }
}
