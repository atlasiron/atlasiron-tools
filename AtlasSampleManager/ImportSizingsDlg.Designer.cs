namespace AtlasSampleManager
{
    partial class ImportSizingsDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportSizingsDlg));
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
            this.lstResults.Location = new System.Drawing.Point(91, 10);
            this.lstResults.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lstResults.Name = "lstResults";
            this.lstResults.Size = new System.Drawing.Size(380, 20);
            this.lstResults.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Sizings Results";
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
            this.gridErrors.Location = new System.Drawing.Point(9, 54);
            this.gridErrors.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gridErrors.Name = "gridErrors";
            this.gridErrors.Size = new System.Drawing.Size(795, 463);
            this.gridErrors.TabIndex = 3;
            // 
            // butClose
            // 
            this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butClose.Location = new System.Drawing.Point(748, 522);
            this.butClose.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.butClose.Name = "butClose";
            this.butClose.Size = new System.Drawing.Size(56, 19);
            this.butClose.TabIndex = 4;
            this.butClose.Text = "Close";
            this.butClose.Click += new System.EventHandler(this.butClose_Click);
            // 
            // butImport
            // 
            this.butImport.Location = new System.Drawing.Point(475, 10);
            this.butImport.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.butImport.Name = "butImport";
            this.butImport.Size = new System.Drawing.Size(56, 19);
            this.butImport.TabIndex = 5;
            this.butImport.Text = "Import";
            this.butImport.Click += new System.EventHandler(this.butImport_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 36);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Errors";
            // 
            // ImportSizingsDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 547);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.butImport);
            this.Controls.Add(this.butClose);
            this.Controls.Add(this.gridErrors);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstResults);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ImportSizingsDlg";
            this.Text = "Import Sizings";
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