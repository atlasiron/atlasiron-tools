namespace AtlasReportToolkit
{
    partial class TransferMovementsDlg
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Janus.Windows.GridEX.GridEXLayout gridLog_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransferMovementsDlg));
            this.gridLog = new Janus.Windows.GridEX.GridEX();
            this.butClose = new Janus.Windows.EditControls.UIButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridLog)).BeginInit();
            this.SuspendLayout();
            // 
            // gridLog
            // 
            this.gridLog.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.gridLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridLog.ColumnAutoResize = true;
            this.gridLog.ColumnHeaders = Janus.Windows.GridEX.InheritableBoolean.False;
            gridLog_DesignTimeLayout.LayoutString = resources.GetString("gridLog_DesignTimeLayout.LayoutString");
            this.gridLog.DesignTimeLayout = gridLog_DesignTimeLayout;
            this.gridLog.GridLines = Janus.Windows.GridEX.GridLines.None;
            this.gridLog.GroupByBoxVisible = false;
            this.gridLog.Location = new System.Drawing.Point(12, 12);
            this.gridLog.Name = "gridLog";
            this.gridLog.Size = new System.Drawing.Size(1053, 500);
            this.gridLog.TabIndex = 0;
            // 
            // butClose
            // 
            this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butClose.Location = new System.Drawing.Point(989, 518);
            this.butClose.Name = "butClose";
            this.butClose.Size = new System.Drawing.Size(75, 23);
            this.butClose.TabIndex = 1;
            this.butClose.Text = "Close";
            this.butClose.Click += new System.EventHandler(this.butClose_Click);
            // 
            // TransferMovementsDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1077, 553);
            this.Controls.Add(this.butClose);
            this.Controls.Add(this.gridLog);
            this.DoubleBuffered = true;
            this.Name = "TransferMovementsDlg";
            this.Text = "Transfer Material Movements";
            this.Activated += new System.EventHandler(this.TransferMovementsDlg_Activated);
            this.Load += new System.EventHandler(this.TransferMovementsDlg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridLog)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.GridEX.GridEX gridLog;
        private Janus.Windows.EditControls.UIButton butClose;
    }
}