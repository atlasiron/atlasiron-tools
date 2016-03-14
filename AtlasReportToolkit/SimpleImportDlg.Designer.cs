namespace AtlasReportToolkit
{
    partial class SimpleImportDlg
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtFileName = new Janus.Windows.GridEX.EditControls.EditBox();
            this.butImport = new Janus.Windows.EditControls.UIButton();
            this.butCancel = new Janus.Windows.EditControls.UIButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "File name";
            // 
            // txtFileName
            // 
            this.txtFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFileName.ButtonStyle = Janus.Windows.GridEX.EditControls.EditButtonStyle.Ellipsis;
            this.txtFileName.Location = new System.Drawing.Point(87, 9);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(627, 22);
            this.txtFileName.TabIndex = 1;
            this.txtFileName.ButtonClick += new System.EventHandler(this.txtFileName_ButtonClick);
            // 
            // butImport
            // 
            this.butImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butImport.Location = new System.Drawing.Point(558, 37);
            this.butImport.Name = "butImport";
            this.butImport.Size = new System.Drawing.Size(75, 23);
            this.butImport.TabIndex = 2;
            this.butImport.Text = "Import";
            this.butImport.Click += new System.EventHandler(this.butImport_Click);
            // 
            // butCancel
            // 
            this.butCancel.Location = new System.Drawing.Point(639, 37);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 3;
            this.butCancel.Text = "Cancel";
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // SimpleImportDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 78);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.butImport);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.label1);
            this.Name = "SimpleImportDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import Data into Report";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Janus.Windows.GridEX.EditControls.EditBox txtFileName;
        private Janus.Windows.EditControls.UIButton butImport;
        private Janus.Windows.EditControls.UIButton butCancel;
    }
}