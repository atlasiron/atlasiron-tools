namespace AtlasSampleManager
{
    partial class PublishCrushingDataDlg
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
            Janus.Windows.GridEX.GridEXLayout gridStreams_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PublishCrushingDataDlg));
            this.label1 = new System.Windows.Forms.Label();
            this.calDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.butOK = new Janus.Windows.EditControls.UIButton();
            this.butCancel = new Janus.Windows.EditControls.UIButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtStartDateTime = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtEndDateTime = new Janus.Windows.GridEX.EditControls.EditBox();
            this.calEndDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.gridStreams = new Janus.Windows.GridEX.GridEX();
            ((System.ComponentModel.ISupportInitialize)(this.gridStreams)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Shift Date";
            // 
            // calDate
            // 
            this.calDate.Location = new System.Drawing.Point(198, 18);
            this.calDate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.calDate.Name = "calDate";
            this.calDate.Size = new System.Drawing.Size(350, 26);
            this.calDate.TabIndex = 1;
            this.calDate.ValueChanged += new System.EventHandler(this.calDate_ValueChanged);
            // 
            // butOK
            // 
            this.butOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.butOK.Location = new System.Drawing.Point(315, 345);
            this.butOK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(112, 35);
            this.butOK.TabIndex = 2;
            this.butOK.Text = "OK";
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // butCancel
            // 
            this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.butCancel.Location = new System.Drawing.Point(436, 345);
            this.butCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(112, 35);
            this.butCancel.TabIndex = 3;
            this.butCancel.Text = "Cancel";
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 102);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Start date and time";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 157);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "End date and time";
            // 
            // txtStartDateTime
            // 
            this.txtStartDateTime.Enabled = false;
            this.txtStartDateTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStartDateTime.Location = new System.Drawing.Point(198, 102);
            this.txtStartDateTime.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtStartDateTime.Name = "txtStartDateTime";
            this.txtStartDateTime.ReadOnly = true;
            this.txtStartDateTime.Size = new System.Drawing.Size(350, 26);
            this.txtStartDateTime.TabIndex = 6;
            // 
            // txtEndDateTime
            // 
            this.txtEndDateTime.Enabled = false;
            this.txtEndDateTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEndDateTime.Location = new System.Drawing.Point(198, 157);
            this.txtEndDateTime.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtEndDateTime.Name = "txtEndDateTime";
            this.txtEndDateTime.ReadOnly = true;
            this.txtEndDateTime.Size = new System.Drawing.Size(350, 26);
            this.txtEndDateTime.TabIndex = 7;
            // 
            // calEndDate
            // 
            this.calEndDate.Location = new System.Drawing.Point(198, 58);
            this.calEndDate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.calEndDate.Name = "calEndDate";
            this.calEndDate.Size = new System.Drawing.Size(350, 26);
            this.calEndDate.TabIndex = 8;
            this.calEndDate.ValueChanged += new System.EventHandler(this.calEndDate_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(144, 18);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "From";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(144, 65);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 20);
            this.label5.TabIndex = 10;
            this.label5.Text = "To";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 211);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(136, 20);
            this.label6.TabIndex = 11;
            this.label6.Text = "Crushing Streams";
            // 
            // gridStreams
            // 
            this.gridStreams.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.gridStreams.ColumnAutoResize = true;
            this.gridStreams.ColumnHeaders = Janus.Windows.GridEX.InheritableBoolean.False;
            gridStreams_DesignTimeLayout.LayoutString = resources.GetString("gridStreams_DesignTimeLayout.LayoutString");
            this.gridStreams.DesignTimeLayout = gridStreams_DesignTimeLayout;
            this.gridStreams.GroupByBoxVisible = false;
            this.gridStreams.Location = new System.Drawing.Point(198, 211);
            this.gridStreams.Name = "gridStreams";
            this.gridStreams.Size = new System.Drawing.Size(350, 118);
            this.gridStreams.TabIndex = 12;
            // 
            // PublishCrushingDataDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 398);
            this.Controls.Add(this.gridStreams);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.calEndDate);
            this.Controls.Add(this.txtEndDateTime);
            this.Controls.Add(this.txtStartDateTime);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.butOK);
            this.Controls.Add(this.calDate);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "PublishCrushingDataDlg";
            this.Text = "Publish Crushing Data";
            this.Load += new System.EventHandler(this.PublishCrushingDataDlg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridStreams)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Janus.Windows.CalendarCombo.CalendarCombo calDate;
        private Janus.Windows.EditControls.UIButton butOK;
        private Janus.Windows.EditControls.UIButton butCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.GridEX.EditControls.EditBox txtStartDateTime;
        private Janus.Windows.GridEX.EditControls.EditBox txtEndDateTime;
        private Janus.Windows.CalendarCombo.CalendarCombo calEndDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private Janus.Windows.GridEX.GridEX gridStreams;
    }
}