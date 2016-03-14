namespace AtlasSampleManager
{
    partial class ManageSubmissionsDlg
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
            Janus.Windows.GridEX.GridEXLayout gridSamples_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageSubmissionsDlg));
            Janus.Windows.GridEX.GridEXLayout gridSubmission_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.gridSamples = new Janus.Windows.GridEX.GridEX();
            this.gridSubmission = new Janus.Windows.GridEX.GridEX();
            this.butAdd = new Janus.Windows.EditControls.UIButton();
            this.butRemove = new Janus.Windows.EditControls.UIButton();
            this.butClose = new Janus.Windows.EditControls.UIButton();
            this.butAddAll = new Janus.Windows.EditControls.UIButton();
            this.butRemoveAll = new Janus.Windows.EditControls.UIButton();
            this.label1 = new System.Windows.Forms.Label();
            this.lstSubmissions = new Janus.Windows.EditControls.UIComboBox();
            this.butNewSubmission = new Janus.Windows.EditControls.UIButton();
            this.butSubmit = new Janus.Windows.EditControls.UIButton();
            this.butSubmissionSheet = new Janus.Windows.EditControls.UIButton();
            this.txtNumberOfSamples = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridSamples)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSubmission)).BeginInit();
            this.SuspendLayout();
            // 
            // gridSamples
            // 
            this.gridSamples.AllowColumnDrag = false;
            this.gridSamples.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.gridSamples.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridSamples.ColumnAutoResize = true;
            gridSamples_DesignTimeLayout.LayoutString = resources.GetString("gridSamples_DesignTimeLayout.LayoutString");
            this.gridSamples.DesignTimeLayout = gridSamples_DesignTimeLayout;
            this.gridSamples.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridSamples.GroupByBoxVisible = false;
            this.gridSamples.HideSelection = Janus.Windows.GridEX.HideSelection.HighlightInactive;
            this.gridSamples.Location = new System.Drawing.Point(10, 55);
            this.gridSamples.Margin = new System.Windows.Forms.Padding(2);
            this.gridSamples.Name = "gridSamples";
            this.gridSamples.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelectionSameTable;
            this.gridSamples.Size = new System.Drawing.Size(532, 570);
            this.gridSamples.TabIndex = 0;
            // 
            // gridSubmission
            // 
            this.gridSubmission.AllowColumnDrag = false;
            this.gridSubmission.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridSubmission.ColumnAutoResize = true;
            gridSubmission_DesignTimeLayout.LayoutString = resources.GetString("gridSubmission_DesignTimeLayout.LayoutString");
            this.gridSubmission.DesignTimeLayout = gridSubmission_DesignTimeLayout;
            this.gridSubmission.GroupByBoxVisible = false;
            this.gridSubmission.HideSelection = Janus.Windows.GridEX.HideSelection.HighlightInactive;
            this.gridSubmission.Location = new System.Drawing.Point(588, 55);
            this.gridSubmission.Margin = new System.Windows.Forms.Padding(2);
            this.gridSubmission.Name = "gridSubmission";
            this.gridSubmission.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelectionSameTable;
            this.gridSubmission.Size = new System.Drawing.Size(447, 570);
            this.gridSubmission.TabIndex = 1;
            this.gridSubmission.CellValueChanged += new Janus.Windows.GridEX.ColumnActionEventHandler(this.gridSubmission_CellValueChanged);
            // 
            // butAdd
            // 
            this.butAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butAdd.Location = new System.Drawing.Point(546, 165);
            this.butAdd.Margin = new System.Windows.Forms.Padding(2);
            this.butAdd.Name = "butAdd";
            this.butAdd.Size = new System.Drawing.Size(38, 19);
            this.butAdd.TabIndex = 2;
            this.butAdd.Text = ">";
            this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
            // 
            // butRemove
            // 
            this.butRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butRemove.Location = new System.Drawing.Point(546, 279);
            this.butRemove.Margin = new System.Windows.Forms.Padding(2);
            this.butRemove.Name = "butRemove";
            this.butRemove.Size = new System.Drawing.Size(38, 19);
            this.butRemove.TabIndex = 3;
            this.butRemove.Text = "<";
            this.butRemove.Click += new System.EventHandler(this.butRemove_Click);
            // 
            // butClose
            // 
            this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butClose.Location = new System.Drawing.Point(1040, 607);
            this.butClose.Margin = new System.Windows.Forms.Padding(2);
            this.butClose.Name = "butClose";
            this.butClose.Size = new System.Drawing.Size(56, 19);
            this.butClose.TabIndex = 4;
            this.butClose.Text = "Close";
            this.butClose.Click += new System.EventHandler(this.butClose_Click);
            // 
            // butAddAll
            // 
            this.butAddAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butAddAll.Location = new System.Drawing.Point(546, 188);
            this.butAddAll.Margin = new System.Windows.Forms.Padding(2);
            this.butAddAll.Name = "butAddAll";
            this.butAddAll.Size = new System.Drawing.Size(38, 19);
            this.butAddAll.TabIndex = 5;
            this.butAddAll.Text = ">>";
            this.butAddAll.Click += new System.EventHandler(this.butAddAll_Click);
            // 
            // butRemoveAll
            // 
            this.butRemoveAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butRemoveAll.Location = new System.Drawing.Point(546, 302);
            this.butRemoveAll.Margin = new System.Windows.Forms.Padding(2);
            this.butRemoveAll.Name = "butRemoveAll";
            this.butRemoveAll.Size = new System.Drawing.Size(38, 19);
            this.butRemoveAll.TabIndex = 6;
            this.butRemoveAll.Text = "<<";
            this.butRemoveAll.Click += new System.EventHandler(this.butRemoveAll_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Submission";
            // 
            // lstSubmissions
            // 
            this.lstSubmissions.Location = new System.Drawing.Point(75, 11);
            this.lstSubmissions.Margin = new System.Windows.Forms.Padding(2);
            this.lstSubmissions.Name = "lstSubmissions";
            this.lstSubmissions.Size = new System.Drawing.Size(195, 20);
            this.lstSubmissions.TabIndex = 8;
            this.lstSubmissions.TextChanged += new System.EventHandler(this.lstSubmissions_TextChanged);
            // 
            // butNewSubmission
            // 
            this.butNewSubmission.Location = new System.Drawing.Point(282, 11);
            this.butNewSubmission.Margin = new System.Windows.Forms.Padding(2);
            this.butNewSubmission.Name = "butNewSubmission";
            this.butNewSubmission.Size = new System.Drawing.Size(109, 19);
            this.butNewSubmission.TabIndex = 9;
            this.butNewSubmission.Text = "Next Submisssion";
            this.butNewSubmission.Click += new System.EventHandler(this.butNewSubmission_Click);
            // 
            // butSubmit
            // 
            this.butSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butSubmit.Location = new System.Drawing.Point(1040, 55);
            this.butSubmit.Margin = new System.Windows.Forms.Padding(2);
            this.butSubmit.Name = "butSubmit";
            this.butSubmit.Size = new System.Drawing.Size(56, 19);
            this.butSubmit.TabIndex = 10;
            this.butSubmit.Text = "Submit";
            this.butSubmit.Click += new System.EventHandler(this.butSubmit_Click);
            // 
            // butSubmissionSheet
            // 
            this.butSubmissionSheet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butSubmissionSheet.Location = new System.Drawing.Point(1040, 78);
            this.butSubmissionSheet.Margin = new System.Windows.Forms.Padding(2);
            this.butSubmissionSheet.Name = "butSubmissionSheet";
            this.butSubmissionSheet.Size = new System.Drawing.Size(56, 19);
            this.butSubmissionSheet.TabIndex = 11;
            this.butSubmissionSheet.Text = "Sheet";
            this.butSubmissionSheet.Visible = false;
            this.butSubmissionSheet.Click += new System.EventHandler(this.butSubmissionSheet_Click);
            // 
            // txtNumberOfSamples
            // 
            this.txtNumberOfSamples.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumberOfSamples.Location = new System.Drawing.Point(752, 27);
            this.txtNumberOfSamples.Name = "txtNumberOfSamples";
            this.txtNumberOfSamples.ReadOnly = true;
            this.txtNumberOfSamples.Size = new System.Drawing.Size(78, 20);
            this.txtNumberOfSamples.TabIndex = 12;
            this.txtNumberOfSamples.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(585, 30);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(162, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Number of samples in submission";
            // 
            // ManageSubmissionsDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1102, 639);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNumberOfSamples);
            this.Controls.Add(this.butSubmissionSheet);
            this.Controls.Add(this.butSubmit);
            this.Controls.Add(this.butNewSubmission);
            this.Controls.Add(this.lstSubmissions);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.butRemoveAll);
            this.Controls.Add(this.butAddAll);
            this.Controls.Add(this.butClose);
            this.Controls.Add(this.butRemove);
            this.Controls.Add(this.butAdd);
            this.Controls.Add(this.gridSubmission);
            this.Controls.Add(this.gridSamples);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ManageSubmissionsDlg";
            this.Text = "Manage Submissions";
            this.Load += new System.EventHandler(this.ManageSubmissionsDlg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridSamples)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSubmission)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Janus.Windows.GridEX.GridEX gridSamples;
        private Janus.Windows.GridEX.GridEX gridSubmission;
        private Janus.Windows.EditControls.UIButton butAdd;
        private Janus.Windows.EditControls.UIButton butRemove;
        private Janus.Windows.EditControls.UIButton butClose;
        private Janus.Windows.EditControls.UIButton butAddAll;
        private Janus.Windows.EditControls.UIButton butRemoveAll;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.EditControls.UIComboBox lstSubmissions;
        private Janus.Windows.EditControls.UIButton butNewSubmission;
        private Janus.Windows.EditControls.UIButton butSubmit;
        private Janus.Windows.EditControls.UIButton butSubmissionSheet;
        private Janus.Windows.GridEX.EditControls.EditBox txtNumberOfSamples;
        private System.Windows.Forms.Label label2;
    }
}