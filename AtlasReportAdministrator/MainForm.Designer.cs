namespace AtlasReportAdministrator
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            Janus.Windows.GridEX.GridEXLayout gridReportStatus_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.butConfigure = new Janus.Windows.EditControls.UIButton();
            this.butClose = new Janus.Windows.EditControls.UIButton();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.gridReportStatus = new Janus.Windows.GridEX.GridEX();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.butOpenPending = new Janus.Windows.EditControls.UIButton();
            this.butRefresh = new Janus.Windows.EditControls.UIButton();
            this.butArchive = new Janus.Windows.EditControls.UIButton();
            this.butRemoveCreatedReport = new Janus.Windows.EditControls.UIButton();
            this.butCreateNewReport = new Janus.Windows.EditControls.UIButton();
            this.editReport = new AtlasReportToolkit.EditReportSheetCtrl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtNumToRepeat = new System.Windows.Forms.NumericUpDown();
            this.butCopy = new Janus.Windows.EditControls.UIButton();
            this.butChangeLists = new Janus.Windows.EditControls.UIButton();
            this.butValidate = new Janus.Windows.EditControls.UIButton();
            this.butGodMode = new Janus.Windows.EditControls.UIButton();
            this.butAutoFill = new Janus.Windows.EditControls.UIButton();
            this.butPublish = new Janus.Windows.EditControls.UIButton();
            this.butApprove = new Janus.Windows.EditControls.UIButton();
            this.butSend = new Janus.Windows.EditControls.UIButton();
            this.butUnapproveReport = new Janus.Windows.EditControls.UIButton();
            this.butTools = new Janus.Windows.EditControls.UIButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridReportStatus)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumToRepeat)).BeginInit();
            this.SuspendLayout();
            // 
            // butConfigure
            // 
            this.butConfigure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.butConfigure.Icon = ((System.Drawing.Icon)(resources.GetObject("butConfigure.Icon")));
            this.butConfigure.Location = new System.Drawing.Point(12, 635);
            this.butConfigure.Name = "butConfigure";
            this.butConfigure.Size = new System.Drawing.Size(33, 23);
            this.butConfigure.TabIndex = 0;
            this.butConfigure.Click += new System.EventHandler(this.butConfigure_Click);
            // 
            // butClose
            // 
            this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butClose.Location = new System.Drawing.Point(1077, 635);
            this.butClose.Name = "butClose";
            this.butClose.Size = new System.Drawing.Size(119, 23);
            this.butClose.TabIndex = 3;
            this.butClose.Text = "Close";
            this.butClose.Click += new System.EventHandler(this.butClose_Click);
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.Location = new System.Drawing.Point(12, 28);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.gridReportStatus);
            this.splitContainer.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.editReport);
            this.splitContainer.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer.Size = new System.Drawing.Size(1184, 601);
            this.splitContainer.SplitterDistance = 392;
            this.splitContainer.TabIndex = 4;
            // 
            // gridReportStatus
            // 
            this.gridReportStatus.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.gridReportStatus.ColumnAutoResize = true;
            gridReportStatus_DesignTimeLayout.LayoutString = resources.GetString("gridReportStatus_DesignTimeLayout.LayoutString");
            this.gridReportStatus.DesignTimeLayout = gridReportStatus_DesignTimeLayout;
            this.gridReportStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridReportStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.gridReportStatus.GroupByBoxVisible = false;
            this.gridReportStatus.HideSelection = Janus.Windows.GridEX.HideSelection.HighlightInactive;
            this.gridReportStatus.Location = new System.Drawing.Point(0, 0);
            this.gridReportStatus.Name = "gridReportStatus";
            this.gridReportStatus.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.gridReportStatus.Size = new System.Drawing.Size(392, 540);
            this.gridReportStatus.TabIndex = 3;
            this.gridReportStatus.SelectionChanged += new System.EventHandler(this.gridReportStatus_SelectionChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.butOpenPending);
            this.groupBox2.Controls.Add(this.butRefresh);
            this.groupBox2.Controls.Add(this.butArchive);
            this.groupBox2.Controls.Add(this.butRemoveCreatedReport);
            this.groupBox2.Controls.Add(this.butCreateNewReport);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 540);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(392, 61);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Report Management";
            // 
            // butOpenPending
            // 
            this.butOpenPending.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.butOpenPending.Icon = ((System.Drawing.Icon)(resources.GetObject("butOpenPending.Icon")));
            this.butOpenPending.Location = new System.Drawing.Point(297, 19);
            this.butOpenPending.Name = "butOpenPending";
            this.butOpenPending.Size = new System.Drawing.Size(33, 23);
            this.butOpenPending.TabIndex = 23;
            this.butOpenPending.Click += new System.EventHandler(this.butOpenPending_Click);
            // 
            // butRefresh
            // 
            this.butRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.butRefresh.Icon = ((System.Drawing.Icon)(resources.GetObject("butRefresh.Icon")));
            this.butRefresh.Location = new System.Drawing.Point(258, 19);
            this.butRefresh.Name = "butRefresh";
            this.butRefresh.Size = new System.Drawing.Size(33, 23);
            this.butRefresh.TabIndex = 22;
            this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
            // 
            // butArchive
            // 
            this.butArchive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.butArchive.Location = new System.Drawing.Point(177, 19);
            this.butArchive.Name = "butArchive";
            this.butArchive.Size = new System.Drawing.Size(75, 23);
            this.butArchive.TabIndex = 21;
            this.butArchive.Text = "Archive";
            this.butArchive.Click += new System.EventHandler(this.butArchive_Click);
            // 
            // butRemoveCreatedReport
            // 
            this.butRemoveCreatedReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.butRemoveCreatedReport.Location = new System.Drawing.Point(96, 19);
            this.butRemoveCreatedReport.Name = "butRemoveCreatedReport";
            this.butRemoveCreatedReport.Size = new System.Drawing.Size(75, 23);
            this.butRemoveCreatedReport.TabIndex = 20;
            this.butRemoveCreatedReport.Text = "Delete";
            this.butRemoveCreatedReport.Click += new System.EventHandler(this.butRemoveCreatedReport_Click);
            // 
            // butCreateNewReport
            // 
            this.butCreateNewReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.butCreateNewReport.Location = new System.Drawing.Point(15, 19);
            this.butCreateNewReport.Name = "butCreateNewReport";
            this.butCreateNewReport.Size = new System.Drawing.Size(75, 23);
            this.butCreateNewReport.TabIndex = 19;
            this.butCreateNewReport.Text = "Create";
            this.butCreateNewReport.Click += new System.EventHandler(this.butCreateNewReport_Click);
            // 
            // editReport
            // 
            this.editReport.ActiveReport = null;
            this.editReport.ActiveReportSheet = null;
            this.editReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editReport.Location = new System.Drawing.Point(0, 0);
            this.editReport.Margin = new System.Windows.Forms.Padding(4);
            this.editReport.Name = "editReport";
            this.editReport.Size = new System.Drawing.Size(788, 540);
            this.editReport.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtNumToRepeat);
            this.groupBox1.Controls.Add(this.butCopy);
            this.groupBox1.Controls.Add(this.butChangeLists);
            this.groupBox1.Controls.Add(this.butValidate);
            this.groupBox1.Controls.Add(this.butGodMode);
            this.groupBox1.Controls.Add(this.butAutoFill);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 540);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(788, 61);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Report Editing";
            // 
            // txtNumToRepeat
            // 
            this.txtNumToRepeat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNumToRepeat.Location = new System.Drawing.Point(659, 19);
            this.txtNumToRepeat.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.txtNumToRepeat.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtNumToRepeat.Name = "txtNumToRepeat";
            this.txtNumToRepeat.Size = new System.Drawing.Size(32, 20);
            this.txtNumToRepeat.TabIndex = 25;
            this.txtNumToRepeat.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // butCopy
            // 
            this.butCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butCopy.Location = new System.Drawing.Point(335, 19);
            this.butCopy.Name = "butCopy";
            this.butCopy.Size = new System.Drawing.Size(75, 23);
            this.butCopy.TabIndex = 24;
            this.butCopy.Text = "Copy";
            this.butCopy.Click += new System.EventHandler(this.butCopy_Click);
            // 
            // butChangeLists
            // 
            this.butChangeLists.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butChangeLists.Location = new System.Drawing.Point(416, 19);
            this.butChangeLists.Name = "butChangeLists";
            this.butChangeLists.Size = new System.Drawing.Size(75, 23);
            this.butChangeLists.TabIndex = 23;
            this.butChangeLists.Text = "Change Lists";
            this.butChangeLists.Click += new System.EventHandler(this.butChangeLists_Click);
            // 
            // butValidate
            // 
            this.butValidate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butValidate.Location = new System.Drawing.Point(497, 19);
            this.butValidate.Name = "butValidate";
            this.butValidate.Size = new System.Drawing.Size(75, 23);
            this.butValidate.TabIndex = 22;
            this.butValidate.Text = "Validate";
            this.butValidate.Click += new System.EventHandler(this.butValidate_Click);
            // 
            // butGodMode
            // 
            this.butGodMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butGodMode.Location = new System.Drawing.Point(697, 19);
            this.butGodMode.Name = "butGodMode";
            this.butGodMode.Size = new System.Drawing.Size(75, 23);
            this.butGodMode.TabIndex = 21;
            this.butGodMode.Text = "Full Edit";
            this.butGodMode.Click += new System.EventHandler(this.butGodMode_Click);
            // 
            // butAutoFill
            // 
            this.butAutoFill.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butAutoFill.Location = new System.Drawing.Point(578, 19);
            this.butAutoFill.Name = "butAutoFill";
            this.butAutoFill.Size = new System.Drawing.Size(75, 23);
            this.butAutoFill.TabIndex = 20;
            this.butAutoFill.Text = "Auto Fill";
            this.butAutoFill.Click += new System.EventHandler(this.butAutoSize_Click);
            // 
            // butPublish
            // 
            this.butPublish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butPublish.Location = new System.Drawing.Point(802, 635);
            this.butPublish.Name = "butPublish";
            this.butPublish.Size = new System.Drawing.Size(119, 23);
            this.butPublish.TabIndex = 12;
            this.butPublish.Text = "Publish";
            this.butPublish.Click += new System.EventHandler(this.butPublish_Click);
            // 
            // butApprove
            // 
            this.butApprove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butApprove.Location = new System.Drawing.Point(677, 635);
            this.butApprove.Name = "butApprove";
            this.butApprove.Size = new System.Drawing.Size(119, 23);
            this.butApprove.TabIndex = 13;
            this.butApprove.Text = "Approve Report";
            this.butApprove.Click += new System.EventHandler(this.butApprove_Click);
            // 
            // butSend
            // 
            this.butSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butSend.Location = new System.Drawing.Point(552, 635);
            this.butSend.Name = "butSend";
            this.butSend.Size = new System.Drawing.Size(119, 23);
            this.butSend.TabIndex = 11;
            this.butSend.Text = "Request Information";
            this.butSend.Click += new System.EventHandler(this.butSend_Click);
            // 
            // butUnapproveReport
            // 
            this.butUnapproveReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butUnapproveReport.Location = new System.Drawing.Point(927, 635);
            this.butUnapproveReport.Name = "butUnapproveReport";
            this.butUnapproveReport.Size = new System.Drawing.Size(119, 23);
            this.butUnapproveReport.TabIndex = 17;
            this.butUnapproveReport.Text = "Unapprove Report";
            this.butUnapproveReport.Click += new System.EventHandler(this.butUnapproveReport_Click);
            // 
            // butTools
            // 
            this.butTools.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.butTools.Icon = ((System.Drawing.Icon)(resources.GetObject("butTools.Icon")));
            this.butTools.Location = new System.Drawing.Point(51, 635);
            this.butTools.Name = "butTools";
            this.butTools.Size = new System.Drawing.Size(33, 23);
            this.butTools.TabIndex = 18;
            this.butTools.Click += new System.EventHandler(this.butTools_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1208, 670);
            this.Controls.Add(this.butTools);
            this.Controls.Add(this.butUnapproveReport);
            this.Controls.Add(this.butPublish);
            this.Controls.Add(this.butApprove);
            this.Controls.Add(this.butSend);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.butClose);
            this.Controls.Add(this.butConfigure);
            this.Name = "MainForm";
            this.Text = "Atlas Report Manager";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridReportStatus)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtNumToRepeat)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIButton butConfigure;
        private Janus.Windows.EditControls.UIButton butClose;
        private System.Windows.Forms.SplitContainer splitContainer;
        private Janus.Windows.GridEX.GridEX gridReportStatus;
        private Janus.Windows.EditControls.UIButton butPublish;
        private Janus.Windows.EditControls.UIButton butApprove;
        private Janus.Windows.EditControls.UIButton butSend;
        private AtlasReportToolkit.EditReportSheetCtrl editReport;
        private Janus.Windows.EditControls.UIButton butUnapproveReport;
        private System.Windows.Forms.GroupBox groupBox2;
        private Janus.Windows.EditControls.UIButton butRefresh;
        private Janus.Windows.EditControls.UIButton butArchive;
        private Janus.Windows.EditControls.UIButton butRemoveCreatedReport;
        private Janus.Windows.EditControls.UIButton butCreateNewReport;
        private System.Windows.Forms.GroupBox groupBox1;
        private Janus.Windows.EditControls.UIButton butValidate;
        private Janus.Windows.EditControls.UIButton butGodMode;
        private Janus.Windows.EditControls.UIButton butAutoFill;
        private Janus.Windows.EditControls.UIButton butChangeLists;
        private Janus.Windows.EditControls.UIButton butCopy;
        private Janus.Windows.EditControls.UIButton butTools;
        private Janus.Windows.EditControls.UIButton butOpenPending;
        private System.Windows.Forms.NumericUpDown txtNumToRepeat;
    }
}

