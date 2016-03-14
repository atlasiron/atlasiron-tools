namespace AtlasSampleManager
{
    partial class PublishOreBlocksDlg
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
            this.butOK = new Janus.Windows.EditControls.UIButton();
            this.butCancel = new Janus.Windows.EditControls.UIButton();
            this.gridBlasts = new Janus.Windows.GridEX.GridEX();
            this.label2 = new System.Windows.Forms.Label();
            this.calOpenDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.lstShotNos = new Janus.Windows.EditControls.UIComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lstBench = new Janus.Windows.EditControls.UIComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lstPits = new Janus.Windows.EditControls.UIComboBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridBlasts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Flitch";
            // 
            // butOK
            // 
            this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butOK.Location = new System.Drawing.Point(647, 495);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(75, 23);
            this.butOK.TabIndex = 2;
            this.butOK.Text = "OK";
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // butCancel
            // 
            this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.butCancel.Location = new System.Drawing.Point(728, 495);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 3;
            this.butCancel.Text = "Cancel";
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // gridBlasts
            // 
            this.gridBlasts.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.gridBlasts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridBlasts.ColumnAutoResize = true;
            this.gridBlasts.GroupByBoxVisible = false;
            this.gridBlasts.Location = new System.Drawing.Point(84, 107);
            this.gridBlasts.Name = "gridBlasts";
            this.gridBlasts.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection;
            this.gridBlasts.Size = new System.Drawing.Size(719, 382);
            this.gridBlasts.TabIndex = 4;
            this.gridBlasts.SelectionChanged += new System.EventHandler(this.gridBlasts_SelectionChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Open Date";
            // 
            // calOpenDate
            // 
            this.calOpenDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.calOpenDate.Location = new System.Drawing.Point(84, 81);
            this.calOpenDate.Name = "calOpenDate";
            this.calOpenDate.Nullable = true;
            this.calOpenDate.Size = new System.Drawing.Size(719, 20);
            this.calOpenDate.TabIndex = 6;
            this.calOpenDate.Value = new System.DateTime(2, 2, 2, 0, 0, 0, 0);
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.lstShotNos);
            this.uiGroupBox1.Controls.Add(this.label3);
            this.uiGroupBox1.Controls.Add(this.lstBench);
            this.uiGroupBox1.Controls.Add(this.label4);
            this.uiGroupBox1.Controls.Add(this.lstPits);
            this.uiGroupBox1.Controls.Add(this.label5);
            this.uiGroupBox1.Location = new System.Drawing.Point(12, 12);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(391, 63);
            this.uiGroupBox1.TabIndex = 31;
            this.uiGroupBox1.Text = "Filter By";
            // 
            // lstShotNos
            // 
            this.lstShotNos.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            this.lstShotNos.Location = new System.Drawing.Point(288, 26);
            this.lstShotNos.Margin = new System.Windows.Forms.Padding(2);
            this.lstShotNos.Name = "lstShotNos";
            this.lstShotNos.Size = new System.Drawing.Size(77, 20);
            this.lstShotNos.TabIndex = 36;
            this.lstShotNos.TextChanged += new System.EventHandler(this.lstShotNos_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(240, 27);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 35;
            this.label3.Text = "Shot No";
            // 
            // lstBench
            // 
            this.lstBench.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            this.lstBench.Location = new System.Drawing.Point(159, 26);
            this.lstBench.Margin = new System.Windows.Forms.Padding(2);
            this.lstBench.Name = "lstBench";
            this.lstBench.Size = new System.Drawing.Size(77, 20);
            this.lstBench.TabIndex = 34;
            this.lstBench.TextChanged += new System.EventHandler(this.lstBench_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(118, 27);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 33;
            this.label4.Text = "Bench";
            // 
            // lstPits
            // 
            this.lstPits.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            this.lstPits.Location = new System.Drawing.Point(44, 27);
            this.lstPits.Margin = new System.Windows.Forms.Padding(2);
            this.lstPits.Name = "lstPits";
            this.lstPits.Size = new System.Drawing.Size(69, 20);
            this.lstPits.TabIndex = 32;
            this.lstPits.TextChanged += new System.EventHandler(this.lstPits_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(22, 27);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(19, 13);
            this.label5.TabIndex = 31;
            this.label5.Text = "Pit";
            // 
            // PublishOreBlocksDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 528);
            this.Controls.Add(this.uiGroupBox1);
            this.Controls.Add(this.calOpenDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gridBlasts);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.butOK);
            this.Controls.Add(this.label1);
            this.Name = "PublishOreBlocksDlg";
            this.Text = "Publish Ore Blocks";
            this.Load += new System.EventHandler(this.PublishOreBlocksDlg_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.gridBlasts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Janus.Windows.EditControls.UIButton butOK;
        private Janus.Windows.EditControls.UIButton butCancel;
        private Janus.Windows.GridEX.GridEX gridBlasts;
        private System.Windows.Forms.Label label2;
        private Janus.Windows.CalendarCombo.CalendarCombo calOpenDate;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.EditControls.UIComboBox lstShotNos;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.EditControls.UIComboBox lstBench;
        private System.Windows.Forms.Label label4;
        private Janus.Windows.EditControls.UIComboBox lstPits;
        private System.Windows.Forms.Label label5;
    }
}