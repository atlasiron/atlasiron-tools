namespace AtlasReportToolkit
{
    partial class DefineReportingPoliciesDlg
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
            Janus.Windows.GridEX.GridEXLayout gridSelectedTemplates_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DefineReportingPoliciesDlg));
            Janus.Windows.GridEX.GridEXLayout gridHeaderDefaults_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.uiGroupBox4 = new Janus.Windows.EditControls.UIGroupBox();
            this.butCopy = new Janus.Windows.EditControls.UIButton();
            this.lstReportPolicies = new Janus.Windows.EditControls.UIComboBox();
            this.butRemovePolicy = new Janus.Windows.EditControls.UIButton();
            this.butAddPolicy = new Janus.Windows.EditControls.UIButton();
            this.label2 = new System.Windows.Forms.Label();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.txtDBConnectionString = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtArchiveDirectory = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtRemoveReportDataDays = new Janus.Windows.GridEX.EditControls.EditBox();
            this.lstRemoveReportDataColumn = new Janus.Windows.EditControls.UIComboBox();
            this.ticRemovePreviousData = new Janus.Windows.EditControls.UICheckBox();
            this.ticIncludePreviousReports = new Janus.Windows.EditControls.UICheckBox();
            this.lstPublishFormat = new Janus.Windows.EditControls.UIComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtSendCCEmailList = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtReplyToEmailList = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtSendToEmailList = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtOperation = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label11 = new System.Windows.Forms.Label();
            this.gridSelectedTemplates = new Janus.Windows.GridEX.GridEX();
            this.label5 = new System.Windows.Forms.Label();
            this.gridHeaderDefaults = new Janus.Windows.GridEX.GridEX();
            this.txtPublishDirectory = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtPendingDirectory = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtContractor = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtApprovedDirectory = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtReportsDirectory = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new Janus.Windows.GridEX.EditControls.EditBox();
            this.butClose = new Janus.Windows.EditControls.UIButton();
            this.label18 = new System.Windows.Forms.Label();
            this.lstImportFileFormat = new Janus.Windows.EditControls.UIComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox4)).BeginInit();
            this.uiGroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSelectedTemplates)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridHeaderDefaults)).BeginInit();
            this.SuspendLayout();
            // 
            // uiGroupBox4
            // 
            this.uiGroupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiGroupBox4.Controls.Add(this.butCopy);
            this.uiGroupBox4.Controls.Add(this.lstReportPolicies);
            this.uiGroupBox4.Controls.Add(this.butRemovePolicy);
            this.uiGroupBox4.Controls.Add(this.butAddPolicy);
            this.uiGroupBox4.Controls.Add(this.label2);
            this.uiGroupBox4.Location = new System.Drawing.Point(12, 12);
            this.uiGroupBox4.Name = "uiGroupBox4";
            this.uiGroupBox4.Size = new System.Drawing.Size(652, 61);
            this.uiGroupBox4.TabIndex = 15;
            this.uiGroupBox4.Text = "Policies";
            // 
            // butCopy
            // 
            this.butCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butCopy.Location = new System.Drawing.Point(571, 22);
            this.butCopy.Name = "butCopy";
            this.butCopy.Size = new System.Drawing.Size(75, 23);
            this.butCopy.TabIndex = 19;
            this.butCopy.Text = "Copy";
            this.butCopy.Click += new System.EventHandler(this.butCopy_Click);
            // 
            // lstReportPolicies
            // 
            this.lstReportPolicies.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstReportPolicies.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            this.lstReportPolicies.Location = new System.Drawing.Point(52, 23);
            this.lstReportPolicies.Name = "lstReportPolicies";
            this.lstReportPolicies.Size = new System.Drawing.Size(354, 20);
            this.lstReportPolicies.TabIndex = 18;
            this.lstReportPolicies.SelectedIndexChanged += new System.EventHandler(this.lstReportPolicies_SelectedIndexChanged);
            // 
            // butRemovePolicy
            // 
            this.butRemovePolicy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butRemovePolicy.Location = new System.Drawing.Point(493, 22);
            this.butRemovePolicy.Name = "butRemovePolicy";
            this.butRemovePolicy.Size = new System.Drawing.Size(75, 23);
            this.butRemovePolicy.TabIndex = 17;
            this.butRemovePolicy.Text = "Remove";
            this.butRemovePolicy.Click += new System.EventHandler(this.butRemovePolicy_Click);
            // 
            // butAddPolicy
            // 
            this.butAddPolicy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butAddPolicy.Location = new System.Drawing.Point(412, 22);
            this.butAddPolicy.Name = "butAddPolicy";
            this.butAddPolicy.Size = new System.Drawing.Size(75, 23);
            this.butAddPolicy.TabIndex = 16;
            this.butAddPolicy.Text = "Add";
            this.butAddPolicy.Click += new System.EventHandler(this.butAddPolicy_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Name";
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiGroupBox1.Controls.Add(this.lstImportFileFormat);
            this.uiGroupBox1.Controls.Add(this.label18);
            this.uiGroupBox1.Controls.Add(this.txtDBConnectionString);
            this.uiGroupBox1.Controls.Add(this.label3);
            this.uiGroupBox1.Controls.Add(this.txtArchiveDirectory);
            this.uiGroupBox1.Controls.Add(this.label17);
            this.uiGroupBox1.Controls.Add(this.label16);
            this.uiGroupBox1.Controls.Add(this.txtRemoveReportDataDays);
            this.uiGroupBox1.Controls.Add(this.lstRemoveReportDataColumn);
            this.uiGroupBox1.Controls.Add(this.ticRemovePreviousData);
            this.uiGroupBox1.Controls.Add(this.ticIncludePreviousReports);
            this.uiGroupBox1.Controls.Add(this.lstPublishFormat);
            this.uiGroupBox1.Controls.Add(this.label9);
            this.uiGroupBox1.Controls.Add(this.label15);
            this.uiGroupBox1.Controls.Add(this.txtSendCCEmailList);
            this.uiGroupBox1.Controls.Add(this.label14);
            this.uiGroupBox1.Controls.Add(this.txtReplyToEmailList);
            this.uiGroupBox1.Controls.Add(this.label13);
            this.uiGroupBox1.Controls.Add(this.txtSendToEmailList);
            this.uiGroupBox1.Controls.Add(this.label12);
            this.uiGroupBox1.Controls.Add(this.txtOperation);
            this.uiGroupBox1.Controls.Add(this.label11);
            this.uiGroupBox1.Controls.Add(this.gridSelectedTemplates);
            this.uiGroupBox1.Controls.Add(this.label5);
            this.uiGroupBox1.Controls.Add(this.gridHeaderDefaults);
            this.uiGroupBox1.Controls.Add(this.txtPublishDirectory);
            this.uiGroupBox1.Controls.Add(this.label10);
            this.uiGroupBox1.Controls.Add(this.txtPendingDirectory);
            this.uiGroupBox1.Controls.Add(this.label7);
            this.uiGroupBox1.Controls.Add(this.label8);
            this.uiGroupBox1.Controls.Add(this.txtContractor);
            this.uiGroupBox1.Controls.Add(this.txtApprovedDirectory);
            this.uiGroupBox1.Controls.Add(this.label6);
            this.uiGroupBox1.Controls.Add(this.txtReportsDirectory);
            this.uiGroupBox1.Controls.Add(this.label4);
            this.uiGroupBox1.Controls.Add(this.label1);
            this.uiGroupBox1.Controls.Add(this.txtName);
            this.uiGroupBox1.Location = new System.Drawing.Point(11, 79);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(655, 562);
            this.uiGroupBox1.TabIndex = 18;
            this.uiGroupBox1.Text = "Policy";
            // 
            // txtDBConnectionString
            // 
            this.txtDBConnectionString.Location = new System.Drawing.Point(130, 295);
            this.txtDBConnectionString.Name = "txtDBConnectionString";
            this.txtDBConnectionString.Size = new System.Drawing.Size(508, 20);
            this.txtDBConnectionString.TabIndex = 54;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 299);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 13);
            this.label3.TabIndex = 53;
            this.label3.Text = "DB Connection String";
            // 
            // txtArchiveDirectory
            // 
            this.txtArchiveDirectory.Location = new System.Drawing.Point(447, 244);
            this.txtArchiveDirectory.Name = "txtArchiveDirectory";
            this.txtArchiveDirectory.Size = new System.Drawing.Size(188, 20);
            this.txtArchiveDirectory.TabIndex = 52;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(333, 248);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(111, 13);
            this.label17.TabIndex = 51;
            this.label17.Text = "Archived Subdirectory";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(538, 75);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(31, 13);
            this.label16.TabIndex = 50;
            this.label16.Text = "Days";
            // 
            // txtRemoveReportDataDays
            // 
            this.txtRemoveReportDataDays.Location = new System.Drawing.Point(578, 71);
            this.txtRemoveReportDataDays.Name = "txtRemoveReportDataDays";
            this.txtRemoveReportDataDays.Size = new System.Drawing.Size(64, 20);
            this.txtRemoveReportDataDays.TabIndex = 49;
            // 
            // lstRemoveReportDataColumn
            // 
            this.lstRemoveReportDataColumn.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            this.lstRemoveReportDataColumn.Location = new System.Drawing.Point(371, 71);
            this.lstRemoveReportDataColumn.Name = "lstRemoveReportDataColumn";
            this.lstRemoveReportDataColumn.Size = new System.Drawing.Size(161, 20);
            this.lstRemoveReportDataColumn.TabIndex = 48;
            this.lstRemoveReportDataColumn.SelectedIndexChanged += new System.EventHandler(this.lstRemoveReportDataColumn_SelectedIndexChanged);
            // 
            // ticRemovePreviousData
            // 
            this.ticRemovePreviousData.Location = new System.Drawing.Point(229, 69);
            this.ticRemovePreviousData.Name = "ticRemovePreviousData";
            this.ticRemovePreviousData.Size = new System.Drawing.Size(147, 23);
            this.ticRemovePreviousData.TabIndex = 47;
            this.ticRemovePreviousData.Text = "Remove previous data";
            // 
            // ticIncludePreviousReports
            // 
            this.ticIncludePreviousReports.Location = new System.Drawing.Point(71, 69);
            this.ticIncludePreviousReports.Name = "ticIncludePreviousReports";
            this.ticIncludePreviousReports.Size = new System.Drawing.Size(175, 23);
            this.ticIncludePreviousReports.TabIndex = 46;
            this.ticIncludePreviousReports.Text = "Include previous report data";
            // 
            // lstPublishFormat
            // 
            this.lstPublishFormat.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            this.lstPublishFormat.Location = new System.Drawing.Point(127, 243);
            this.lstPublishFormat.Name = "lstPublishFormat";
            this.lstPublishFormat.Size = new System.Drawing.Size(188, 20);
            this.lstPublishFormat.TabIndex = 45;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 247);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 13);
            this.label9.TabIndex = 44;
            this.label9.Text = "Publish Format";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(12, 125);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(49, 13);
            this.label15.TabIndex = 43;
            this.label15.Text = "Send CC";
            // 
            // txtSendCCEmailList
            // 
            this.txtSendCCEmailList.Location = new System.Drawing.Point(71, 124);
            this.txtSendCCEmailList.Name = "txtSendCCEmailList";
            this.txtSendCCEmailList.Size = new System.Drawing.Size(567, 20);
            this.txtSendCCEmailList.TabIndex = 42;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 151);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(50, 13);
            this.label14.TabIndex = 41;
            this.label14.Text = "Reply To";
            // 
            // txtReplyToEmailList
            // 
            this.txtReplyToEmailList.Location = new System.Drawing.Point(71, 150);
            this.txtReplyToEmailList.Name = "txtReplyToEmailList";
            this.txtReplyToEmailList.Size = new System.Drawing.Size(567, 20);
            this.txtReplyToEmailList.TabIndex = 40;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 99);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(48, 13);
            this.label13.TabIndex = 39;
            this.label13.Text = "Send To";
            // 
            // txtSendToEmailList
            // 
            this.txtSendToEmailList.Location = new System.Drawing.Point(71, 98);
            this.txtSendToEmailList.Name = "txtSendToEmailList";
            this.txtSendToEmailList.Size = new System.Drawing.Size(567, 20);
            this.txtSendToEmailList.TabIndex = 38;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 46);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 13);
            this.label12.TabIndex = 37;
            this.label12.Text = "Operation";
            // 
            // txtOperation
            // 
            this.txtOperation.Location = new System.Drawing.Point(71, 43);
            this.txtOperation.Name = "txtOperation";
            this.txtOperation.Size = new System.Drawing.Size(226, 20);
            this.txtOperation.TabIndex = 36;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(332, 333);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(56, 13);
            this.label11.TabIndex = 35;
            this.label11.Text = "Templates";
            // 
            // gridSelectedTemplates
            // 
            this.gridSelectedTemplates.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True;
            this.gridSelectedTemplates.ColumnAutoResize = true;
            gridSelectedTemplates_DesignTimeLayout.LayoutString = resources.GetString("gridSelectedTemplates_DesignTimeLayout.LayoutString");
            this.gridSelectedTemplates.DesignTimeLayout = gridSelectedTemplates_DesignTimeLayout;
            this.gridSelectedTemplates.GroupByBoxVisible = false;
            this.gridSelectedTemplates.Location = new System.Drawing.Point(336, 348);
            this.gridSelectedTemplates.Margin = new System.Windows.Forms.Padding(2);
            this.gridSelectedTemplates.Name = "gridSelectedTemplates";
            this.gridSelectedTemplates.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.gridSelectedTemplates.Size = new System.Drawing.Size(300, 197);
            this.gridSelectedTemplates.TabIndex = 34;
            this.gridSelectedTemplates.RowCheckStateChanged += new Janus.Windows.GridEX.RowCheckStateChangeEventHandler(this.gridSelectedTemplates_RowCheckStateChanged);
            this.gridSelectedTemplates.GetNewRow += new Janus.Windows.GridEX.GetNewRowEventHandler(this.gridRowDefaults_GetNewRow);
            this.gridSelectedTemplates.SelectionChanged += new System.EventHandler(this.gridSelectedTemplates_SelectionChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 333);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 13);
            this.label5.TabIndex = 33;
            this.label5.Text = "Header Defaults";
            // 
            // gridHeaderDefaults
            // 
            this.gridHeaderDefaults.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True;
            this.gridHeaderDefaults.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True;
            this.gridHeaderDefaults.ColumnAutoResize = true;
            gridHeaderDefaults_DesignTimeLayout.LayoutString = resources.GetString("gridHeaderDefaults_DesignTimeLayout.LayoutString");
            this.gridHeaderDefaults.DesignTimeLayout = gridHeaderDefaults_DesignTimeLayout;
            this.gridHeaderDefaults.GroupByBoxVisible = false;
            this.gridHeaderDefaults.Location = new System.Drawing.Point(15, 348);
            this.gridHeaderDefaults.Margin = new System.Windows.Forms.Padding(2);
            this.gridHeaderDefaults.Name = "gridHeaderDefaults";
            this.gridHeaderDefaults.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.gridHeaderDefaults.Size = new System.Drawing.Size(300, 197);
            this.gridHeaderDefaults.TabIndex = 32;
            this.gridHeaderDefaults.GetNewRow += new Janus.Windows.GridEX.GetNewRowEventHandler(this.gridHeaderDefaults_GetNewRow);
            // 
            // txtPublishDirectory
            // 
            this.txtPublishDirectory.ButtonStyle = Janus.Windows.GridEX.EditControls.EditButtonStyle.Ellipsis;
            this.txtPublishDirectory.Location = new System.Drawing.Point(127, 269);
            this.txtPublishDirectory.Name = "txtPublishDirectory";
            this.txtPublishDirectory.Size = new System.Drawing.Size(508, 20);
            this.txtPublishDirectory.TabIndex = 31;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 273);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(86, 13);
            this.label10.TabIndex = 30;
            this.label10.Text = "Publish Directory";
            // 
            // txtPendingDirectory
            // 
            this.txtPendingDirectory.Location = new System.Drawing.Point(127, 217);
            this.txtPendingDirectory.Name = "txtPendingDirectory";
            this.txtPendingDirectory.Size = new System.Drawing.Size(188, 20);
            this.txtPendingDirectory.TabIndex = 27;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(303, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 24;
            this.label7.Text = "Contractor";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 220);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(108, 13);
            this.label8.TabIndex = 26;
            this.label8.Text = "Pending Subdirectory";
            // 
            // txtContractor
            // 
            this.txtContractor.Location = new System.Drawing.Point(371, 22);
            this.txtContractor.Name = "txtContractor";
            this.txtContractor.Size = new System.Drawing.Size(226, 20);
            this.txtContractor.TabIndex = 23;
            // 
            // txtApprovedDirectory
            // 
            this.txtApprovedDirectory.Location = new System.Drawing.Point(447, 217);
            this.txtApprovedDirectory.Name = "txtApprovedDirectory";
            this.txtApprovedDirectory.Size = new System.Drawing.Size(188, 20);
            this.txtApprovedDirectory.TabIndex = 22;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(332, 220);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(115, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Approved Subdirectory";
            // 
            // txtReportsDirectory
            // 
            this.txtReportsDirectory.ButtonStyle = Janus.Windows.GridEX.EditControls.EditButtonStyle.Ellipsis;
            this.txtReportsDirectory.Location = new System.Drawing.Point(127, 191);
            this.txtReportsDirectory.Name = "txtReportsDirectory";
            this.txtReportsDirectory.Size = new System.Drawing.Size(508, 20);
            this.txtReportsDirectory.TabIndex = 18;
            this.txtReportsDirectory.ButtonClick += new System.EventHandler(this.txtReportsDirectory_ButtonClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 194);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Reports Directory";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Name";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(71, 21);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(226, 20);
            this.txtName.TabIndex = 2;
            // 
            // butClose
            // 
            this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butClose.Location = new System.Drawing.Point(588, 651);
            this.butClose.Name = "butClose";
            this.butClose.Size = new System.Drawing.Size(75, 23);
            this.butClose.TabIndex = 26;
            this.butClose.Text = "Close";
            this.butClose.Click += new System.EventHandler(this.butClose_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(303, 46);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(90, 13);
            this.label18.TabIndex = 55;
            this.label18.Text = "Import File Format";
            // 
            // lstImportFileFormat
            // 
            this.lstImportFileFormat.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            this.lstImportFileFormat.Location = new System.Drawing.Point(399, 43);
            this.lstImportFileFormat.Name = "lstImportFileFormat";
            this.lstImportFileFormat.Size = new System.Drawing.Size(198, 20);
            this.lstImportFileFormat.TabIndex = 56;
            // 
            // DefineReportingPoliciesDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 686);
            this.Controls.Add(this.butClose);
            this.Controls.Add(this.uiGroupBox1);
            this.Controls.Add(this.uiGroupBox4);
            this.Name = "DefineReportingPoliciesDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = " Define Reporting Policy";
            this.Load += new System.EventHandler(this.DefineReportingPoliciesDlg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox4)).EndInit();
            this.uiGroupBox4.ResumeLayout(false);
            this.uiGroupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSelectedTemplates)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridHeaderDefaults)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox uiGroupBox4;
        private Janus.Windows.EditControls.UIButton butRemovePolicy;
        private Janus.Windows.EditControls.UIButton butAddPolicy;
        private System.Windows.Forms.Label label2;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.GridEX.EditControls.EditBox txtReportsDirectory;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.GridEX.EditControls.EditBox txtName;
        private Janus.Windows.GridEX.EditControls.EditBox txtApprovedDirectory;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private Janus.Windows.GridEX.EditControls.EditBox txtContractor;
        private Janus.Windows.EditControls.UIButton butClose;
        private Janus.Windows.EditControls.UIComboBox lstReportPolicies;
        private Janus.Windows.GridEX.EditControls.EditBox txtPendingDirectory;
        private System.Windows.Forms.Label label8;
        private Janus.Windows.GridEX.EditControls.EditBox txtPublishDirectory;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private Janus.Windows.GridEX.GridEX gridSelectedTemplates;
        private System.Windows.Forms.Label label5;
        private Janus.Windows.GridEX.GridEX gridHeaderDefaults;
        private System.Windows.Forms.Label label12;
        private Janus.Windows.GridEX.EditControls.EditBox txtOperation;
        private System.Windows.Forms.Label label14;
        private Janus.Windows.GridEX.EditControls.EditBox txtReplyToEmailList;
        private System.Windows.Forms.Label label13;
        private Janus.Windows.GridEX.EditControls.EditBox txtSendToEmailList;
        private System.Windows.Forms.Label label15;
        private Janus.Windows.GridEX.EditControls.EditBox txtSendCCEmailList;
        private Janus.Windows.EditControls.UIComboBox lstPublishFormat;
        private System.Windows.Forms.Label label9;
        private Janus.Windows.EditControls.UICheckBox ticIncludePreviousReports;
        private System.Windows.Forms.Label label16;
        private Janus.Windows.GridEX.EditControls.EditBox txtRemoveReportDataDays;
        private Janus.Windows.EditControls.UIComboBox lstRemoveReportDataColumn;
        private Janus.Windows.EditControls.UICheckBox ticRemovePreviousData;
        private Janus.Windows.GridEX.EditControls.EditBox txtArchiveDirectory;
        private System.Windows.Forms.Label label17;
        private Janus.Windows.GridEX.EditControls.EditBox txtDBConnectionString;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.EditControls.UIButton butCopy;
        private Janus.Windows.EditControls.UIComboBox lstImportFileFormat;
        private System.Windows.Forms.Label label18;
    }
}