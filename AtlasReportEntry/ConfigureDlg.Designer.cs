namespace AtlasReportEntry
{
    partial class ConfigureDlg
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
            Janus.Windows.GridEX.GridEXLayout gridDirectories_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigureDlg));
            this.gridDirectories = new Janus.Windows.GridEX.GridEX();
            this.label1 = new System.Windows.Forms.Label();
            this.butClose = new Janus.Windows.EditControls.UIButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridDirectories)).BeginInit();
            this.SuspendLayout();
            // 
            // gridDirectories
            // 
            this.gridDirectories.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True;
            this.gridDirectories.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True;
            this.gridDirectories.ColumnAutoResize = true;
            gridDirectories_DesignTimeLayout.LayoutString = resources.GetString("gridDirectories_DesignTimeLayout.LayoutString");
            this.gridDirectories.DesignTimeLayout = gridDirectories_DesignTimeLayout;
            this.gridDirectories.GroupByBoxVisible = false;
            this.gridDirectories.Location = new System.Drawing.Point(21, 42);
            this.gridDirectories.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gridDirectories.Name = "gridDirectories";
            this.gridDirectories.NewRowPosition = Janus.Windows.GridEX.NewRowPosition.BottomRow;
            this.gridDirectories.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.gridDirectories.Size = new System.Drawing.Size(657, 427);
            this.gridDirectories.TabIndex = 0;
            this.gridDirectories.GetNewRow += new Janus.Windows.GridEX.GetNewRowEventHandler(this.gridDirectories_GetNewRow);
            this.gridDirectories.ColumnButtonClick += new Janus.Windows.GridEX.ColumnActionEventHandler(this.gridDirectories_ColumnButtonClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Report Directories";
            // 
            // butClose
            // 
            this.butClose.Location = new System.Drawing.Point(577, 476);
            this.butClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.butClose.Name = "butClose";
            this.butClose.Size = new System.Drawing.Size(100, 28);
            this.butClose.TabIndex = 2;
            this.butClose.Text = "Close";
            this.butClose.Click += new System.EventHandler(this.butClose_Click);
            // 
            // ConfigureDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 519);
            this.Controls.Add(this.butClose);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gridDirectories);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ConfigureDlg";
            this.Text = "ConfigureDlg";
            this.Load += new System.EventHandler(this.ConfigureDlg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridDirectories)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Janus.Windows.GridEX.GridEX gridDirectories;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.EditControls.UIButton butClose;
    }
}