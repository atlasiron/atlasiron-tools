namespace AtlasReportAdministrator
{
    partial class ManageWodginaMainWeighbridgeDlg
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
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem7 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem8 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem9 = new Janus.Windows.EditControls.UIComboBoxItem();
            this.butClose = new Janus.Windows.EditControls.UIButton();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.butSetDefault = new Janus.Windows.EditControls.UIButton();
            this.butSaveList = new Janus.Windows.EditControls.UIButton();
            this.label2 = new System.Windows.Forms.Label();
            this.lstWBLists = new Janus.Windows.EditControls.UIComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gridList = new Janus.Windows.GridEX.GridEX();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridList)).BeginInit();
            this.SuspendLayout();
            // 
            // butClose
            // 
            this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butClose.Location = new System.Drawing.Point(611, 670);
            this.butClose.Name = "butClose";
            this.butClose.Size = new System.Drawing.Size(75, 23);
            this.butClose.TabIndex = 2;
            this.butClose.Text = "Close";
            this.butClose.Click += new System.EventHandler(this.butClose_Click);
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiGroupBox1.Controls.Add(this.butSetDefault);
            this.uiGroupBox1.Controls.Add(this.butSaveList);
            this.uiGroupBox1.Controls.Add(this.label2);
            this.uiGroupBox1.Controls.Add(this.lstWBLists);
            this.uiGroupBox1.Controls.Add(this.label1);
            this.uiGroupBox1.Controls.Add(this.gridList);
            this.uiGroupBox1.Location = new System.Drawing.Point(12, 12);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(688, 645);
            this.uiGroupBox1.TabIndex = 5;
            this.uiGroupBox1.Text = "Weighbridge Lists";
            // 
            // butSetDefault
            // 
            this.butSetDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butSetDefault.Location = new System.Drawing.Point(594, 80);
            this.butSetDefault.Name = "butSetDefault";
            this.butSetDefault.Size = new System.Drawing.Size(75, 23);
            this.butSetDefault.TabIndex = 10;
            this.butSetDefault.Text = "Set Default";
            this.butSetDefault.Click += new System.EventHandler(this.butSetDefault_Click);
            // 
            // butSaveList
            // 
            this.butSaveList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butSaveList.Location = new System.Drawing.Point(594, 51);
            this.butSaveList.Name = "butSaveList";
            this.butSaveList.Size = new System.Drawing.Size(75, 23);
            this.butSaveList.TabIndex = 9;
            this.butSaveList.Text = "Save";
            this.butSaveList.Click += new System.EventHandler(this.butSaveList_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Items";
            // 
            // lstWBLists
            // 
            this.lstWBLists.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstWBLists.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            uiComboBoxItem7.FormatStyle.Alpha = 0;
            uiComboBoxItem7.IsSeparator = false;
            uiComboBoxItem7.Text = "Product Stockpiles";
            uiComboBoxItem8.FormatStyle.Alpha = 0;
            uiComboBoxItem8.IsSeparator = false;
            uiComboBoxItem8.Text = "Assets";
            uiComboBoxItem9.FormatStyle.Alpha = 0;
            uiComboBoxItem9.IsSeparator = false;
            uiComboBoxItem9.Text = "PayrollID";
            this.lstWBLists.Items.AddRange(new Janus.Windows.EditControls.UIComboBoxItem[] {
            uiComboBoxItem7,
            uiComboBoxItem8,
            uiComboBoxItem9});
            this.lstWBLists.Location = new System.Drawing.Point(70, 25);
            this.lstWBLists.Name = "lstWBLists";
            this.lstWBLists.Size = new System.Drawing.Size(518, 20);
            this.lstWBLists.TabIndex = 7;
            this.lstWBLists.SelectedIndexChanged += new System.EventHandler(this.lstWBLists_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Select List";
            // 
            // gridList
            // 
            this.gridList.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True;
            this.gridList.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True;
            this.gridList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridList.ColumnAutoResize = true;
            this.gridList.GroupByBoxVisible = false;
            this.gridList.Location = new System.Drawing.Point(70, 51);
            this.gridList.Name = "gridList";
            this.gridList.NewRowPosition = Janus.Windows.GridEX.NewRowPosition.BottomRow;
            this.gridList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.gridList.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection;
            this.gridList.Size = new System.Drawing.Size(518, 588);
            this.gridList.TabIndex = 5;
            this.gridList.GetNewRow += new Janus.Windows.GridEX.GetNewRowEventHandler(this.gridList_GetNewRow);
            // 
            // ManageWodginaMainWeighbridgeDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 705);
            this.Controls.Add(this.uiGroupBox1);
            this.Controls.Add(this.butClose);
            this.Name = "ManageWodginaMainWeighbridgeDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Manage Wodgina Main Weighbridge";
            this.Load += new System.EventHandler(this.ManageWodginaMainWeighbridgeDlg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIButton butClose;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.EditControls.UIButton butSaveList;
        private System.Windows.Forms.Label label2;
        private Janus.Windows.EditControls.UIComboBox lstWBLists;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.GridEX.GridEX gridList;
        private Janus.Windows.EditControls.UIButton butSetDefault;
    }
}