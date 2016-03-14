namespace AtlasReportToolkit
{
    partial class ConfigureReferenceListsDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigureReferenceListsDlg));
            Janus.Windows.GridEX.GridEXLayout gridItems_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout gridDefaultLists_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.butClose = new Janus.Windows.EditControls.UIButton();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.butRefresh = new Janus.Windows.EditControls.UIButton();
            this.ticShowActivatedValuesOnly = new Janus.Windows.EditControls.UICheckBox();
            this.gridItems = new Janus.Windows.GridEX.GridEX();
            this.gridDefaultLists = new Janus.Windows.GridEX.GridEX();
            this.label3 = new System.Windows.Forms.Label();
            this.txtReportName = new Janus.Windows.GridEX.EditControls.EditBox();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDefaultLists)).BeginInit();
            this.SuspendLayout();
            // 
            // butClose
            // 
            this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butClose.Location = new System.Drawing.Point(636, 514);
            this.butClose.Name = "butClose";
            this.butClose.Size = new System.Drawing.Size(75, 23);
            this.butClose.TabIndex = 4;
            this.butClose.Text = "Close";
            this.butClose.Click += new System.EventHandler(this.butClose_Click);
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiGroupBox2.Controls.Add(this.butRefresh);
            this.uiGroupBox2.Controls.Add(this.ticShowActivatedValuesOnly);
            this.uiGroupBox2.Controls.Add(this.gridItems);
            this.uiGroupBox2.Controls.Add(this.gridDefaultLists);
            this.uiGroupBox2.Location = new System.Drawing.Point(28, 35);
            this.uiGroupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(683, 474);
            this.uiGroupBox2.TabIndex = 7;
            this.uiGroupBox2.Text = "Default Reference Lists";
            // 
            // butRefresh
            // 
            this.butRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butRefresh.Icon = ((System.Drawing.Icon)(resources.GetObject("butRefresh.Icon")));
            this.butRefresh.Location = new System.Drawing.Point(644, 449);
            this.butRefresh.Name = "butRefresh";
            this.butRefresh.Size = new System.Drawing.Size(33, 23);
            this.butRefresh.TabIndex = 16;
            this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
            // 
            // ticShowActivatedValuesOnly
            // 
            this.ticShowActivatedValuesOnly.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ticShowActivatedValuesOnly.CausesValidation = false;
            this.ticShowActivatedValuesOnly.Checked = true;
            this.ticShowActivatedValuesOnly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ticShowActivatedValuesOnly.Location = new System.Drawing.Point(217, 450);
            this.ticShowActivatedValuesOnly.Margin = new System.Windows.Forms.Padding(2);
            this.ticShowActivatedValuesOnly.Name = "ticShowActivatedValuesOnly";
            this.ticShowActivatedValuesOnly.Size = new System.Drawing.Size(203, 19);
            this.ticShowActivatedValuesOnly.TabIndex = 2;
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
            this.gridItems.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.gridItems.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.gridItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridItems.GroupByBoxVisible = false;
            this.gridItems.Location = new System.Drawing.Point(217, 16);
            this.gridItems.Name = "gridItems";
            this.gridItems.NewRowPosition = Janus.Windows.GridEX.NewRowPosition.BottomRow;
            this.gridItems.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.gridItems.Size = new System.Drawing.Size(461, 429);
            this.gridItems.TabIndex = 1;
            this.gridItems.UpdateMode = Janus.Windows.GridEX.UpdateMode.CellUpdate;
            this.gridItems.GetNewRow += new Janus.Windows.GridEX.GetNewRowEventHandler(this.gridItems_GetNewRow);
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
            this.gridDefaultLists.Location = new System.Drawing.Point(4, 16);
            this.gridDefaultLists.Margin = new System.Windows.Forms.Padding(2);
            this.gridDefaultLists.Name = "gridDefaultLists";
            this.gridDefaultLists.Size = new System.Drawing.Size(207, 453);
            this.gridDefaultLists.TabIndex = 0;
            this.gridDefaultLists.SelectionChanged += new System.EventHandler(this.gridDefaultLists_SelectionChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Report Name";
            // 
            // txtReportName
            // 
            this.txtReportName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReportName.Enabled = false;
            this.txtReportName.Location = new System.Drawing.Point(100, 11);
            this.txtReportName.Margin = new System.Windows.Forms.Padding(2);
            this.txtReportName.Name = "txtReportName";
            this.txtReportName.ReadOnly = true;
            this.txtReportName.Size = new System.Drawing.Size(611, 20);
            this.txtReportName.TabIndex = 10;
            // 
            // ConfigureReferenceListsDlg
            // 
            this.AcceptButton = this.butClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(735, 545);
            this.Controls.Add(this.txtReportName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.uiGroupBox2);
            this.Controls.Add(this.butClose);
            this.Name = "ConfigureReferenceListsDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configure Reference Lists";
            this.Load += new System.EventHandler(this.CreateReportDlg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDefaultLists)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Janus.Windows.EditControls.UIButton butClose;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private Janus.Windows.GridEX.GridEX gridDefaultLists;
        private Janus.Windows.GridEX.GridEX gridItems;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.GridEX.EditControls.EditBox txtReportName;
        private Janus.Windows.EditControls.UICheckBox ticShowActivatedValuesOnly;
        private Janus.Windows.EditControls.UIButton butRefresh;
    }
}