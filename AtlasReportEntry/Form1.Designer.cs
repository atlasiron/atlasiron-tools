namespace AtlasReportEntry
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
            Janus.Windows.GridEX.GridEXLayout lstReports_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lstReports = new Janus.Windows.GridEX.EditControls.MultiColumnCombo();
            this.label1 = new System.Windows.Forms.Label();
            this.butSubmit = new Janus.Windows.EditControls.UIButton();
            this.butClose = new Janus.Windows.EditControls.UIButton();
            this.butConfigure = new Janus.Windows.EditControls.UIButton();
            this.editReport = new AtlasReportToolkit.EditReportSheetCtrl();
            this.butPublish = new Janus.Windows.EditControls.UIButton();
            this.butOpen = new Janus.Windows.EditControls.UIButton();
            this.butAutoFill = new Janus.Windows.EditControls.UIButton();
            this.butCopy = new Janus.Windows.EditControls.UIButton();
            this.butWidenColumns = new Janus.Windows.EditControls.UIButton();
            this.butValidate = new Janus.Windows.EditControls.UIButton();
            ((System.ComponentModel.ISupportInitialize)(this.lstReports)).BeginInit();
            this.SuspendLayout();
            // 
            // lstReports
            // 
            this.lstReports.ComboStyle = Janus.Windows.GridEX.ComboStyle.DropDownList;
            lstReports_DesignTimeLayout.LayoutString = resources.GetString("lstReports_DesignTimeLayout.LayoutString");
            this.lstReports.DesignTimeLayout = lstReports_DesignTimeLayout;
            this.lstReports.DisplayMember = "Name";
            this.lstReports.Enabled = false;
            this.lstReports.Location = new System.Drawing.Point(62, 6);
            this.lstReports.Name = "lstReports";
            this.lstReports.SelectedIndex = -1;
            this.lstReports.SelectedItem = null;
            this.lstReports.Size = new System.Drawing.Size(459, 20);
            this.lstReports.TabIndex = 0;
            this.lstReports.ValueMember = "Name";
            this.lstReports.ValueChanged += new System.EventHandler(this.lstReports_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Reports";
            // 
            // butSubmit
            // 
            this.butSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butSubmit.Location = new System.Drawing.Point(929, 730);
            this.butSubmit.Name = "butSubmit";
            this.butSubmit.Size = new System.Drawing.Size(75, 23);
            this.butSubmit.TabIndex = 3;
            this.butSubmit.Text = "Submit";
            this.butSubmit.Click += new System.EventHandler(this.butSubmit_Click);
            // 
            // butClose
            // 
            this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butClose.Location = new System.Drawing.Point(1010, 730);
            this.butClose.Name = "butClose";
            this.butClose.Size = new System.Drawing.Size(75, 23);
            this.butClose.TabIndex = 4;
            this.butClose.Text = "Close";
            this.butClose.Click += new System.EventHandler(this.butClose_Click);
            // 
            // butConfigure
            // 
            this.butConfigure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.butConfigure.Icon = ((System.Drawing.Icon)(resources.GetObject("butConfigure.Icon")));
            this.butConfigure.Location = new System.Drawing.Point(456, 730);
            this.butConfigure.Name = "butConfigure";
            this.butConfigure.Size = new System.Drawing.Size(33, 23);
            this.butConfigure.TabIndex = 5;
            this.butConfigure.Visible = false;
            this.butConfigure.Click += new System.EventHandler(this.butConfigure_Click);
            // 
            // editReport
            // 
            this.editReport.ActiveReport = null;
            this.editReport.ActiveReportSheet = null;
            this.editReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editReport.Location = new System.Drawing.Point(15, 32);
            this.editReport.Name = "editReport";
            this.editReport.Size = new System.Drawing.Size(1070, 692);
            this.editReport.TabIndex = 2;
            // 
            // butPublish
            // 
            this.butPublish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.butPublish.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butPublish.Location = new System.Drawing.Point(15, 730);
            this.butPublish.Name = "butPublish";
            this.butPublish.Size = new System.Drawing.Size(114, 23);
            this.butPublish.TabIndex = 6;
            this.butPublish.Text = "Publish To XML";
            this.butPublish.Click += new System.EventHandler(this.uiButton1_Click);
            // 
            // butOpen
            // 
            this.butOpen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butOpen.Location = new System.Drawing.Point(527, 4);
            this.butOpen.Name = "butOpen";
            this.butOpen.Size = new System.Drawing.Size(75, 23);
            this.butOpen.TabIndex = 7;
            this.butOpen.Text = "Open";
            this.butOpen.Click += new System.EventHandler(this.butOpen_Click);
            // 
            // butAutoFill
            // 
            this.butAutoFill.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butAutoFill.Location = new System.Drawing.Point(588, 730);
            this.butAutoFill.Name = "butAutoFill";
            this.butAutoFill.Size = new System.Drawing.Size(75, 23);
            this.butAutoFill.TabIndex = 15;
            this.butAutoFill.Text = "Auto Fill";
            this.butAutoFill.Click += new System.EventHandler(this.butAutoFill_Click);
            // 
            // butCopy
            // 
            this.butCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butCopy.Location = new System.Drawing.Point(135, 730);
            this.butCopy.Name = "butCopy";
            this.butCopy.Size = new System.Drawing.Size(75, 23);
            this.butCopy.TabIndex = 16;
            this.butCopy.Text = "Copy";
            this.butCopy.Click += new System.EventHandler(this.butCopy_Click);
            // 
            // butWidenColumns
            // 
            this.butWidenColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butWidenColumns.Location = new System.Drawing.Point(507, 730);
            this.butWidenColumns.Name = "butWidenColumns";
            this.butWidenColumns.Size = new System.Drawing.Size(75, 23);
            this.butWidenColumns.TabIndex = 17;
            this.butWidenColumns.Text = "Widen Cols";
            this.butWidenColumns.Click += new System.EventHandler(this.butWidenColumns_Click);
            // 
            // butValidate
            // 
            this.butValidate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butValidate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butValidate.Location = new System.Drawing.Point(848, 730);
            this.butValidate.Name = "butValidate";
            this.butValidate.Size = new System.Drawing.Size(75, 23);
            this.butValidate.TabIndex = 18;
            this.butValidate.Text = "Validate";
            this.butValidate.Click += new System.EventHandler(this.butValidate_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1097, 760);
            this.Controls.Add(this.butValidate);
            this.Controls.Add(this.butWidenColumns);
            this.Controls.Add(this.butCopy);
            this.Controls.Add(this.butAutoFill);
            this.Controls.Add(this.butOpen);
            this.Controls.Add(this.butPublish);
            this.Controls.Add(this.butConfigure);
            this.Controls.Add(this.butClose);
            this.Controls.Add(this.butSubmit);
            this.Controls.Add(this.editReport);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstReports);
            this.Name = "MainForm";
            this.Text = "Atlas Reports";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lstReports)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Janus.Windows.GridEX.EditControls.MultiColumnCombo lstReports;
        private System.Windows.Forms.Label label1;
        private AtlasReportToolkit.EditReportSheetCtrl editReport;
        private Janus.Windows.EditControls.UIButton butSubmit;
        private Janus.Windows.EditControls.UIButton butClose;
        private Janus.Windows.EditControls.UIButton butConfigure;
        private Janus.Windows.EditControls.UIButton butPublish;
        private Janus.Windows.EditControls.UIButton butOpen;
        private Janus.Windows.EditControls.UIButton butAutoFill;
        private Janus.Windows.EditControls.UIButton butCopy;
        private Janus.Windows.EditControls.UIButton butWidenColumns;
        private Janus.Windows.EditControls.UIButton butValidate;
    }
}

