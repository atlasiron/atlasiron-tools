namespace AtlasReportToolkit
{
    partial class CreateNewReportDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateNewReportDlg));
            Janus.Windows.GridEX.GridEXLayout gridItems_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout gridDefaultLists_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout gridPreviousReports_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.uiCreateReportTab = new Janus.Windows.UI.Tab.UITab();
            this.uiReportPeriodTabPage = new Janus.Windows.UI.Tab.UITabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtReportingPeriod = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.lstReportPolicies = new Janus.Windows.EditControls.UIComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.uiDefineListsTabPage = new Janus.Windows.UI.Tab.UITabPage();
            this.butAllInActive = new Janus.Windows.EditControls.UIButton();
            this.butAllActive = new Janus.Windows.EditControls.UIButton();
            this.label5 = new System.Windows.Forms.Label();
            this.butRefresh = new Janus.Windows.EditControls.UIButton();
            this.ticShowActivatedValuesOnly = new Janus.Windows.EditControls.UICheckBox();
            this.gridItems = new Janus.Windows.GridEX.GridEX();
            this.gridDefaultLists = new Janus.Windows.GridEX.GridEX();
            this.uiImportDataTabPage = new Janus.Windows.UI.Tab.UITabPage();
            this.gridPreviousReports = new Janus.Windows.GridEX.GridEX();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFileName = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ticIncludeData = new Janus.Windows.EditControls.UICheckBox();
            this.butNext = new Janus.Windows.EditControls.UIButton();
            this.butPrev = new Janus.Windows.EditControls.UIButton();
            this.butCancel = new Janus.Windows.EditControls.UIButton();
            ((System.ComponentModel.ISupportInitialize)(this.uiCreateReportTab)).BeginInit();
            this.uiCreateReportTab.SuspendLayout();
            this.uiReportPeriodTabPage.SuspendLayout();
            this.uiDefineListsTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDefaultLists)).BeginInit();
            this.uiImportDataTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPreviousReports)).BeginInit();
            this.SuspendLayout();
            // 
            // uiCreateReportTab
            // 
            this.uiCreateReportTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiCreateReportTab.BackColor = System.Drawing.Color.Transparent;
            this.uiCreateReportTab.FirstTabOffset = 3;
            this.uiCreateReportTab.Location = new System.Drawing.Point(12, 12);
            this.uiCreateReportTab.Name = "uiCreateReportTab";
            this.uiCreateReportTab.ShowFocusRectangle = false;
            this.uiCreateReportTab.ShowTabs = false;
            this.uiCreateReportTab.Size = new System.Drawing.Size(661, 452);
            this.uiCreateReportTab.TabIndex = 0;
            this.uiCreateReportTab.TabPages.AddRange(new Janus.Windows.UI.Tab.UITabPage[] {
            this.uiReportPeriodTabPage,
            this.uiDefineListsTabPage,
            this.uiImportDataTabPage});
            // 
            // uiReportPeriodTabPage
            // 
            this.uiReportPeriodTabPage.Controls.Add(this.label6);
            this.uiReportPeriodTabPage.Controls.Add(this.label2);
            this.uiReportPeriodTabPage.Controls.Add(this.txtReportingPeriod);
            this.uiReportPeriodTabPage.Controls.Add(this.lstReportPolicies);
            this.uiReportPeriodTabPage.Controls.Add(this.label1);
            this.uiReportPeriodTabPage.Key = "tabReportPeriod";
            this.uiReportPeriodTabPage.Location = new System.Drawing.Point(1, 1);
            this.uiReportPeriodTabPage.Name = "uiReportPeriodTabPage";
            this.uiReportPeriodTabPage.Size = new System.Drawing.Size(657, 448);
            this.uiReportPeriodTabPage.TabStop = true;
            this.uiReportPeriodTabPage.Text = "Report Period";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(20, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(165, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Set Report Type and Period";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(55, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Reporting Period";
            // 
            // txtReportingPeriod
            // 
            this.txtReportingPeriod.CausesValidation = false;
            this.txtReportingPeriod.Location = new System.Drawing.Point(158, 117);
            this.txtReportingPeriod.Name = "txtReportingPeriod";
            this.txtReportingPeriod.Size = new System.Drawing.Size(102, 20);
            this.txtReportingPeriod.TabIndex = 6;
            this.txtReportingPeriod.ValueChanged += new System.EventHandler(this.txtReportingPeriod_ValueChanged);
            // 
            // lstReportPolicies
            // 
            this.lstReportPolicies.CausesValidation = false;
            this.lstReportPolicies.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            this.lstReportPolicies.Location = new System.Drawing.Point(158, 88);
            this.lstReportPolicies.Name = "lstReportPolicies";
            this.lstReportPolicies.Size = new System.Drawing.Size(216, 20);
            this.lstReportPolicies.TabIndex = 5;
            this.lstReportPolicies.SelectedIndexChanged += new System.EventHandler(this.lstReportPolicies_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(55, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Report Type";
            // 
            // uiDefineListsTabPage
            // 
            this.uiDefineListsTabPage.Controls.Add(this.butAllInActive);
            this.uiDefineListsTabPage.Controls.Add(this.butAllActive);
            this.uiDefineListsTabPage.Controls.Add(this.label5);
            this.uiDefineListsTabPage.Controls.Add(this.butRefresh);
            this.uiDefineListsTabPage.Controls.Add(this.ticShowActivatedValuesOnly);
            this.uiDefineListsTabPage.Controls.Add(this.gridItems);
            this.uiDefineListsTabPage.Controls.Add(this.gridDefaultLists);
            this.uiDefineListsTabPage.Key = "tabDefineLists";
            this.uiDefineListsTabPage.Location = new System.Drawing.Point(1, 1);
            this.uiDefineListsTabPage.Name = "uiDefineListsTabPage";
            this.uiDefineListsTabPage.Size = new System.Drawing.Size(567, 380);
            this.uiDefineListsTabPage.TabStop = true;
            this.uiDefineListsTabPage.Text = "Define Lists";
            // 
            // butAllInActive
            // 
            this.butAllInActive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butAllInActive.Icon = ((System.Drawing.Icon)(resources.GetObject("butAllInActive.Icon")));
            this.butAllInActive.ImageVerticalAlignment = Janus.Windows.EditControls.ImageVerticalAlignment.Far;
            this.butAllInActive.Location = new System.Drawing.Point(492, 351);
            this.butAllInActive.Name = "butAllInActive";
            this.butAllInActive.Size = new System.Drawing.Size(33, 23);
            this.butAllInActive.TabIndex = 22;
            this.butAllInActive.Click += new System.EventHandler(this.butAllInActive_Click);
            // 
            // butAllActive
            // 
            this.butAllActive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butAllActive.Icon = ((System.Drawing.Icon)(resources.GetObject("butAllActive.Icon")));
            this.butAllActive.Location = new System.Drawing.Point(453, 351);
            this.butAllActive.Name = "butAllActive";
            this.butAllActive.Size = new System.Drawing.Size(33, 23);
            this.butAllActive.TabIndex = 21;
            this.butAllActive.Click += new System.EventHandler(this.butAllActive_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(15, 12);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(188, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Review Report Reference Lists ";
            // 
            // butRefresh
            // 
            this.butRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butRefresh.Icon = ((System.Drawing.Icon)(resources.GetObject("butRefresh.Icon")));
            this.butRefresh.Location = new System.Drawing.Point(531, 351);
            this.butRefresh.Name = "butRefresh";
            this.butRefresh.Size = new System.Drawing.Size(33, 23);
            this.butRefresh.TabIndex = 20;
            this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
            // 
            // ticShowActivatedValuesOnly
            // 
            this.ticShowActivatedValuesOnly.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ticShowActivatedValuesOnly.BackColor = System.Drawing.Color.Transparent;
            this.ticShowActivatedValuesOnly.CausesValidation = false;
            this.ticShowActivatedValuesOnly.Checked = true;
            this.ticShowActivatedValuesOnly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ticShowActivatedValuesOnly.Location = new System.Drawing.Point(192, 350);
            this.ticShowActivatedValuesOnly.Margin = new System.Windows.Forms.Padding(2);
            this.ticShowActivatedValuesOnly.Name = "ticShowActivatedValuesOnly";
            this.ticShowActivatedValuesOnly.Size = new System.Drawing.Size(203, 19);
            this.ticShowActivatedValuesOnly.TabIndex = 19;
            this.ticShowActivatedValuesOnly.Text = "Show activated values only";
            this.ticShowActivatedValuesOnly.CheckedChanged += new System.EventHandler(this.ticShowActivatedValuesOnly_CheckedChanged);
            // 
            // gridItems
            // 
            this.gridItems.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True;
            this.gridItems.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True;
            this.gridItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridItems.CausesValidation = false;
            this.gridItems.ColumnAutoResize = true;
            gridItems_DesignTimeLayout.LayoutString = resources.GetString("gridItems_DesignTimeLayout.LayoutString");
            this.gridItems.DesignTimeLayout = gridItems_DesignTimeLayout;
            this.gridItems.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.gridItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.gridItems.GroupByBoxVisible = false;
            this.gridItems.Location = new System.Drawing.Point(192, 41);
            this.gridItems.Name = "gridItems";
            this.gridItems.NewRowPosition = Janus.Windows.GridEX.NewRowPosition.BottomRow;
            this.gridItems.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.gridItems.Size = new System.Drawing.Size(372, 304);
            this.gridItems.TabIndex = 18;
            this.gridItems.GetNewRow += new Janus.Windows.GridEX.GetNewRowEventHandler(this.gridItems_GetNewRow);
            this.gridItems.RecordUpdated += new System.EventHandler(this.gridItems_RecordUpdated);
            // 
            // gridDefaultLists
            // 
            this.gridDefaultLists.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.gridDefaultLists.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gridDefaultLists.CausesValidation = false;
            this.gridDefaultLists.ColumnAutoResize = true;
            this.gridDefaultLists.ColumnHeaders = Janus.Windows.GridEX.InheritableBoolean.False;
            gridDefaultLists_DesignTimeLayout.LayoutString = resources.GetString("gridDefaultLists_DesignTimeLayout.LayoutString");
            this.gridDefaultLists.DesignTimeLayout = gridDefaultLists_DesignTimeLayout;
            this.gridDefaultLists.GroupByBoxVisible = false;
            this.gridDefaultLists.Location = new System.Drawing.Point(18, 41);
            this.gridDefaultLists.Margin = new System.Windows.Forms.Padding(2);
            this.gridDefaultLists.Name = "gridDefaultLists";
            this.gridDefaultLists.Size = new System.Drawing.Size(168, 328);
            this.gridDefaultLists.TabIndex = 17;
            this.gridDefaultLists.SelectionChanged += new System.EventHandler(this.gridDefaultLists_SelectionChanged);
            // 
            // uiImportDataTabPage
            // 
            this.uiImportDataTabPage.Controls.Add(this.gridPreviousReports);
            this.uiImportDataTabPage.Controls.Add(this.label3);
            this.uiImportDataTabPage.Controls.Add(this.txtFileName);
            this.uiImportDataTabPage.Controls.Add(this.label4);
            this.uiImportDataTabPage.Controls.Add(this.ticIncludeData);
            this.uiImportDataTabPage.Key = "tabImportData";
            this.uiImportDataTabPage.Location = new System.Drawing.Point(1, 1);
            this.uiImportDataTabPage.Name = "uiImportDataTabPage";
            this.uiImportDataTabPage.Size = new System.Drawing.Size(567, 380);
            this.uiImportDataTabPage.TabStop = true;
            this.uiImportDataTabPage.Text = "Import Data";
            // 
            // gridPreviousReports
            // 
            this.gridPreviousReports.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.gridPreviousReports.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridPreviousReports.ColumnAutoResize = true;
            gridPreviousReports_DesignTimeLayout.LayoutString = resources.GetString("gridPreviousReports_DesignTimeLayout.LayoutString");
            this.gridPreviousReports.DesignTimeLayout = gridPreviousReports_DesignTimeLayout;
            this.gridPreviousReports.Enabled = false;
            this.gridPreviousReports.GroupByBoxVisible = false;
            this.gridPreviousReports.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.gridPreviousReports.Location = new System.Drawing.Point(72, 100);
            this.gridPreviousReports.Name = "gridPreviousReports";
            this.gridPreviousReports.Size = new System.Drawing.Size(477, 265);
            this.gridPreviousReports.TabIndex = 16;
            this.gridPreviousReports.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 13);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Import Report Data ";
            // 
            // txtFileName
            // 
            this.txtFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFileName.ButtonStyle = Janus.Windows.GridEX.EditControls.EditButtonStyle.Ellipsis;
            this.txtFileName.CausesValidation = false;
            this.txtFileName.Location = new System.Drawing.Point(72, 46);
            this.txtFileName.Margin = new System.Windows.Forms.Padding(2);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(477, 20);
            this.txtFileName.TabIndex = 14;
            this.txtFileName.ButtonClick += new System.EventHandler(this.txtFileName_ButtonClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(16, 46);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "File name";
            // 
            // ticIncludeData
            // 
            this.ticIncludeData.BackColor = System.Drawing.Color.Transparent;
            this.ticIncludeData.Location = new System.Drawing.Point(72, 71);
            this.ticIncludeData.Name = "ticIncludeData";
            this.ticIncludeData.Size = new System.Drawing.Size(225, 23);
            this.ticIncludeData.TabIndex = 6;
            this.ticIncludeData.Text = "Include current report data";
            this.ticIncludeData.CheckedChanged += new System.EventHandler(this.ticIncludeData_CheckedChanged);
            // 
            // butNext
            // 
            this.butNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butNext.Location = new System.Drawing.Point(517, 470);
            this.butNext.Name = "butNext";
            this.butNext.Size = new System.Drawing.Size(75, 23);
            this.butNext.TabIndex = 1;
            this.butNext.Text = "Next";
            this.butNext.Click += new System.EventHandler(this.butNext_Click);
            // 
            // butPrev
            // 
            this.butPrev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butPrev.Location = new System.Drawing.Point(436, 470);
            this.butPrev.Name = "butPrev";
            this.butPrev.Size = new System.Drawing.Size(75, 23);
            this.butPrev.TabIndex = 2;
            this.butPrev.Text = "Previous";
            this.butPrev.Click += new System.EventHandler(this.butPrev_Click);
            // 
            // butCancel
            // 
            this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butCancel.Location = new System.Drawing.Point(598, 470);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 3;
            this.butCancel.Text = "Cancel";
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // CreateNewReportDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 511);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.butPrev);
            this.Controls.Add(this.butNext);
            this.Controls.Add(this.uiCreateReportTab);
            this.Name = "CreateNewReportDlg";
            this.Text = "Create New Report";
            this.Load += new System.EventHandler(this.CreateNewReportDlg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.uiCreateReportTab)).EndInit();
            this.uiCreateReportTab.ResumeLayout(false);
            this.uiReportPeriodTabPage.ResumeLayout(false);
            this.uiReportPeriodTabPage.PerformLayout();
            this.uiDefineListsTabPage.ResumeLayout(false);
            this.uiDefineListsTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDefaultLists)).EndInit();
            this.uiImportDataTabPage.ResumeLayout(false);
            this.uiImportDataTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPreviousReports)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.UI.Tab.UITab uiCreateReportTab;
        private Janus.Windows.UI.Tab.UITabPage uiReportPeriodTabPage;
        private Janus.Windows.UI.Tab.UITabPage uiDefineListsTabPage;
        private Janus.Windows.EditControls.UIButton butNext;
        private Janus.Windows.EditControls.UIButton butPrev;
        private Janus.Windows.UI.Tab.UITabPage uiImportDataTabPage;
        private Janus.Windows.EditControls.UIButton butCancel;
        private System.Windows.Forms.Label label2;
        private Janus.Windows.CalendarCombo.CalendarCombo txtReportingPeriod;
        private Janus.Windows.EditControls.UIComboBox lstReportPolicies;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.EditControls.UICheckBox ticIncludeData;
        private Janus.Windows.EditControls.UIButton butRefresh;
        private Janus.Windows.EditControls.UICheckBox ticShowActivatedValuesOnly;
        private Janus.Windows.GridEX.GridEX gridItems;
        private Janus.Windows.GridEX.GridEX gridDefaultLists;
        private Janus.Windows.GridEX.EditControls.EditBox txtFileName;
        private System.Windows.Forms.Label label4;
        private Janus.Windows.GridEX.GridEX gridPreviousReports;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private Janus.Windows.EditControls.UIButton butAllInActive;
        private Janus.Windows.EditControls.UIButton butAllActive;
    }
}