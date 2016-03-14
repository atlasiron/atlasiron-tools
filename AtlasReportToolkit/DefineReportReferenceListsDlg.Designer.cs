namespace AtlasReportToolkit
{
    partial class DefineReportReferenceListsDlg
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
            Janus.Windows.GridEX.GridEXLayout gridItems_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DefineReportReferenceListsDlg));
            this.uiGroupBox4 = new Janus.Windows.EditControls.UIGroupBox();
            this.lstReportReferenceLists = new Janus.Windows.EditControls.UIComboBox();
            this.butRemoveList = new Janus.Windows.EditControls.UIButton();
            this.butAddList = new Janus.Windows.EditControls.UIButton();
            this.label2 = new System.Windows.Forms.Label();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.gridItems = new Janus.Windows.GridEX.GridEX();
            this.butClose = new Janus.Windows.EditControls.UIButton();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.txtDBSqlQuery = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDBConnectionString = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label1 = new System.Windows.Forms.Label();
            this.uiGroupBox3 = new Janus.Windows.EditControls.UIGroupBox();
            this.lstLinkedListName = new Janus.Windows.EditControls.UIComboBox();
            this.txtLinkedListCalc = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.butCopy = new Janus.Windows.EditControls.UIButton();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox4)).BeginInit();
            this.uiGroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox3)).BeginInit();
            this.uiGroupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiGroupBox4
            // 
            this.uiGroupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiGroupBox4.Controls.Add(this.butCopy);
            this.uiGroupBox4.Controls.Add(this.lstReportReferenceLists);
            this.uiGroupBox4.Controls.Add(this.butRemoveList);
            this.uiGroupBox4.Controls.Add(this.butAddList);
            this.uiGroupBox4.Controls.Add(this.label2);
            this.uiGroupBox4.Location = new System.Drawing.Point(12, 12);
            this.uiGroupBox4.Name = "uiGroupBox4";
            this.uiGroupBox4.Size = new System.Drawing.Size(704, 61);
            this.uiGroupBox4.TabIndex = 18;
            this.uiGroupBox4.Text = "Reference Lists";
            // 
            // lstReportReferenceLists
            // 
            this.lstReportReferenceLists.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstReportReferenceLists.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            this.lstReportReferenceLists.Location = new System.Drawing.Point(52, 23);
            this.lstReportReferenceLists.Name = "lstReportReferenceLists";
            this.lstReportReferenceLists.Size = new System.Drawing.Size(403, 20);
            this.lstReportReferenceLists.TabIndex = 18;
            this.lstReportReferenceLists.SelectedIndexChanged += new System.EventHandler(this.lstReportReferenceLists_SelectedIndexChanged);
            // 
            // butRemoveList
            // 
            this.butRemoveList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butRemoveList.Location = new System.Drawing.Point(542, 22);
            this.butRemoveList.Name = "butRemoveList";
            this.butRemoveList.Size = new System.Drawing.Size(75, 23);
            this.butRemoveList.TabIndex = 17;
            this.butRemoveList.Text = "Remove";
            this.butRemoveList.Click += new System.EventHandler(this.butRemoveList_Click);
            // 
            // butAddList
            // 
            this.butAddList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butAddList.Location = new System.Drawing.Point(461, 22);
            this.butAddList.Name = "butAddList";
            this.butAddList.Size = new System.Drawing.Size(75, 23);
            this.butAddList.TabIndex = 16;
            this.butAddList.Text = "Add";
            this.butAddList.Click += new System.EventHandler(this.butAddList_Click);
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
            this.uiGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiGroupBox1.Controls.Add(this.gridItems);
            this.uiGroupBox1.Location = new System.Drawing.Point(12, 79);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(704, 337);
            this.uiGroupBox1.TabIndex = 19;
            this.uiGroupBox1.Text = "List Items";
            // 
            // gridItems
            // 
            this.gridItems.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True;
            this.gridItems.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True;
            this.gridItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridItems.ColumnAutoResize = true;
            gridItems_DesignTimeLayout.LayoutString = resources.GetString("gridItems_DesignTimeLayout.LayoutString");
            this.gridItems.DesignTimeLayout = gridItems_DesignTimeLayout;
            this.gridItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridItems.GroupByBoxVisible = false;
            this.gridItems.Location = new System.Drawing.Point(14, 19);
            this.gridItems.Name = "gridItems";
            this.gridItems.NewRowPosition = Janus.Windows.GridEX.NewRowPosition.BottomRow;
            this.gridItems.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.gridItems.Size = new System.Drawing.Size(684, 303);
            this.gridItems.TabIndex = 0;
            this.gridItems.GetNewRow += new Janus.Windows.GridEX.GetNewRowEventHandler(this.gridItems_GetNewRow);
            // 
            // butClose
            // 
            this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butClose.Location = new System.Drawing.Point(641, 639);
            this.butClose.Name = "butClose";
            this.butClose.Size = new System.Drawing.Size(75, 23);
            this.butClose.TabIndex = 27;
            this.butClose.Text = "Close";
            this.butClose.Click += new System.EventHandler(this.butClose_Click);
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiGroupBox2.Controls.Add(this.txtDBSqlQuery);
            this.uiGroupBox2.Controls.Add(this.label3);
            this.uiGroupBox2.Controls.Add(this.txtDBConnectionString);
            this.uiGroupBox2.Controls.Add(this.label1);
            this.uiGroupBox2.Location = new System.Drawing.Point(13, 422);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(697, 95);
            this.uiGroupBox2.TabIndex = 28;
            this.uiGroupBox2.Text = "Database Query";
            // 
            // txtDBSqlQuery
            // 
            this.txtDBSqlQuery.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDBSqlQuery.Location = new System.Drawing.Point(84, 55);
            this.txtDBSqlQuery.Name = "txtDBSqlQuery";
            this.txtDBSqlQuery.Size = new System.Drawing.Size(597, 20);
            this.txtDBSqlQuery.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "SQL Query";
            // 
            // txtDBConnectionString
            // 
            this.txtDBConnectionString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDBConnectionString.Location = new System.Drawing.Point(84, 29);
            this.txtDBConnectionString.Name = "txtDBConnectionString";
            this.txtDBConnectionString.Size = new System.Drawing.Size(597, 20);
            this.txtDBConnectionString.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Connection";
            // 
            // uiGroupBox3
            // 
            this.uiGroupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiGroupBox3.Controls.Add(this.lstLinkedListName);
            this.uiGroupBox3.Controls.Add(this.txtLinkedListCalc);
            this.uiGroupBox3.Controls.Add(this.label4);
            this.uiGroupBox3.Controls.Add(this.label5);
            this.uiGroupBox3.Location = new System.Drawing.Point(13, 523);
            this.uiGroupBox3.Name = "uiGroupBox3";
            this.uiGroupBox3.Size = new System.Drawing.Size(697, 95);
            this.uiGroupBox3.TabIndex = 29;
            this.uiGroupBox3.Text = "Linked List";
            // 
            // lstLinkedListName
            // 
            this.lstLinkedListName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstLinkedListName.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            this.lstLinkedListName.Location = new System.Drawing.Point(84, 29);
            this.lstLinkedListName.Name = "lstLinkedListName";
            this.lstLinkedListName.Size = new System.Drawing.Size(597, 20);
            this.lstLinkedListName.TabIndex = 20;
            this.lstLinkedListName.SelectedValueChanged += new System.EventHandler(this.lstLinkedListName_SelectedValueChanged);
            // 
            // txtLinkedListCalc
            // 
            this.txtLinkedListCalc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLinkedListCalc.Location = new System.Drawing.Point(84, 55);
            this.txtLinkedListCalc.Name = "txtLinkedListCalc";
            this.txtLinkedListCalc.Size = new System.Drawing.Size(597, 20);
            this.txtLinkedListCalc.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Calculation";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "List Nme";
            // 
            // butCopy
            // 
            this.butCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butCopy.Location = new System.Drawing.Point(623, 22);
            this.butCopy.Name = "butCopy";
            this.butCopy.Size = new System.Drawing.Size(75, 23);
            this.butCopy.TabIndex = 19;
            this.butCopy.Text = "Copy";
            this.butCopy.Click += new System.EventHandler(this.butCopy_Click);
            // 
            // DefineReportReferenceListsDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 671);
            this.Controls.Add(this.uiGroupBox3);
            this.Controls.Add(this.uiGroupBox2);
            this.Controls.Add(this.butClose);
            this.Controls.Add(this.uiGroupBox1);
            this.Controls.Add(this.uiGroupBox4);
            this.Name = "DefineReportReferenceListsDlg";
            this.Text = "Define Report Reference Lists";
            this.Load += new System.EventHandler(this.DefineReportReferenceListsDlg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox4)).EndInit();
            this.uiGroupBox4.ResumeLayout(false);
            this.uiGroupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            this.uiGroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox3)).EndInit();
            this.uiGroupBox3.ResumeLayout(false);
            this.uiGroupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox uiGroupBox4;
        private Janus.Windows.EditControls.UIComboBox lstReportReferenceLists;
        private Janus.Windows.EditControls.UIButton butRemoveList;
        private Janus.Windows.EditControls.UIButton butAddList;
        private System.Windows.Forms.Label label2;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.GridEX.GridEX gridItems;
        private Janus.Windows.EditControls.UIButton butClose;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private Janus.Windows.GridEX.EditControls.EditBox txtDBSqlQuery;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.GridEX.EditControls.EditBox txtDBConnectionString;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox3;
        private Janus.Windows.GridEX.EditControls.EditBox txtLinkedListCalc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private Janus.Windows.EditControls.UIComboBox lstLinkedListName;
        private Janus.Windows.EditControls.UIButton butCopy;

    }
}