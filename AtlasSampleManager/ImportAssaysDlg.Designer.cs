namespace AtlasSampleManager
{
    partial class ImportAssaysDlg
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
            Janus.Windows.GridEX.GridEXLayout gridErrors_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportAssaysDlg));
            this.lstResults = new Janus.Windows.EditControls.UIComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gridErrors = new Janus.Windows.GridEX.GridEX();
            this.butClose = new Janus.Windows.EditControls.UIButton();
            this.butImport = new Janus.Windows.EditControls.UIButton();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridErrors)).BeginInit();
            this.SuspendLayout();
            // 
            // lstResults
            // 
            this.lstResults.Location = new System.Drawing.Point(115, 12);
            this.lstResults.Name = "lstResults";
            this.lstResults.Size = new System.Drawing.Size(271, 22);
            this.lstResults.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Assay Results";
            // 
            // gridErrors
            // 
            this.gridErrors.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.gridErrors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridErrors.ColumnAutoResize = true;
            this.gridErrors.ColumnHeaders = Janus.Windows.GridEX.InheritableBoolean.False;
            gridErrors_DesignTimeLayout.LayoutString = resources.GetString("gridErrors_DesignTimeLayout.LayoutString");
            this.gridErrors.DesignTimeLayout = gridErrors_DesignTimeLayout;
            this.gridErrors.GroupByBoxVisible = false;
            this.gridErrors.Location = new System.Drawing.Point(12, 66);
            this.gridErrors.Name = "gridErrors";
            this.gridErrors.Size = new System.Drawing.Size(1060, 570);
            this.gridErrors.TabIndex = 3;
            // 
            // butClose
            // 
            this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butClose.Location = new System.Drawing.Point(997, 642);
            this.butClose.Name = "butClose";
            this.butClose.Size = new System.Drawing.Size(75, 23);
            this.butClose.TabIndex = 4;
            this.butClose.Text = "Close";
            this.butClose.Click += new System.EventHandler(this.butClose_Click);
            // 
            // butImport
            // 
            this.butImport.Location = new System.Drawing.Point(392, 12);
            this.butImport.Name = "butImport";
            this.butImport.Size = new System.Drawing.Size(75, 23);
            this.butImport.TabIndex = 5;
            this.butImport.Text = "Import";
            this.butImport.Click += new System.EventHandler(this.butImport_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Errors";
            // 
            // ImportAssaysDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 673);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.butImport);
            this.Controls.Add(this.butClose);
            this.Controls.Add(this.gridErrors);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstResults);
            this.Name = "ImportAssaysDlg";
            this.Text = "Import Assays";
            this.Load += new System.EventHandler(this.ImportAssaysDlg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridErrors)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Janus.Windows.EditControls.UIComboBox lstResults;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.GridEX.GridEX gridErrors;
        private Janus.Windows.EditControls.UIButton butClose;
        private Janus.Windows.EditControls.UIButton butImport;
        private System.Windows.Forms.Label label2;
    }
}