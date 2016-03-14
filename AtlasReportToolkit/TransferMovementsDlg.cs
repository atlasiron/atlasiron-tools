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
    public partial class TransferMovementsDlg : Form
    {
        private class LogMessage
        {
            public String Text { get; set; }
        }

        public String iHubPendingDirectory { get; set; }
        public String iHubProcessingDirectory { get; set; }
        public String iHubProcessedDirectory { get; set; }
        public String iHubErrorDirectory { get; set; }

        private iHubMonitor Monitor { get; set; }
        private BindingList<LogMessage> Messages { get; set; }
        public List<AURTransferFile> TransferFiles { get; set; }

        public TransferMovementsDlg()
        {
            InitializeComponent();

            this.TransferFiles = new List<AURTransferFile>();
        }

        private void TransferMovementsDlg_Load(object sender, EventArgs e)
        {
            this.Messages = new BindingList<LogMessage>();
            this.Monitor = new iHubMonitor() { iHubPendingDirectory = iHubPendingDirectory, iHubProcessingDirectory = iHubProcessingDirectory, iHubProcessedDirectory = iHubProcessedDirectory, iHubErrorDirectory = iHubErrorDirectory, TransferFiles = this.TransferFiles };
            this.Monitor.LogChanged = this.LogMessageToScreen;

            this.gridLog.SetDataBinding(this.Messages, null);

            this.Monitor.Start();
        }

        private void LogMessageToScreen(String msg)
        {
            this.BeginInvoke(new MethodInvoker(delegate { this.Messages.Add(new LogMessage { Text = msg }); gridLog.MoveLast(); gridLog.Refresh(); }));
        }

        private void butClose_Click(object sender, EventArgs e)
        {
            this.Monitor.Stop();
            this.Close();
        }

        private void TransferMovementsDlg_Activated(object sender, EventArgs e)
        {
        }
    }
}
