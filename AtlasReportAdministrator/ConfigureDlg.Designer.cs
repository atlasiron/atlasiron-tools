namespace AtlasReportAdministrator
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
            this.butClose = new Janus.Windows.EditControls.UIButton();
            this.butConfigurePolicies = new Janus.Windows.EditControls.UIButton();
            this.butConfigureTemplates = new Janus.Windows.EditControls.UIButton();
            this.butConfigureDictionary = new Janus.Windows.EditControls.UIButton();
            this.txtConfigDirectory = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCurrentOperation = new Janus.Windows.GridEX.EditControls.EditBox();
            this.SuspendLayout();
            // 
            // butClose
            // 
            this.butClose.Location = new System.Drawing.Point(360, 185);
            this.butClose.Name = "butClose";
            this.butClose.Size = new System.Drawing.Size(75, 23);
            this.butClose.TabIndex = 0;
            this.butClose.Text = "Close";
            this.butClose.Click += new System.EventHandler(this.butClose_Click);
            // 
            // butConfigurePolicies
            // 
            this.butConfigurePolicies.Location = new System.Drawing.Point(12, 12);
            this.butConfigurePolicies.Name = "butConfigurePolicies";
            this.butConfigurePolicies.Size = new System.Drawing.Size(137, 112);
            this.butConfigurePolicies.TabIndex = 1;
            this.butConfigurePolicies.Text = "Policies";
            this.butConfigurePolicies.Click += new System.EventHandler(this.butConfigurePolicies_Click);
            // 
            // butConfigureTemplates
            // 
            this.butConfigureTemplates.Location = new System.Drawing.Point(155, 12);
            this.butConfigureTemplates.Name = "butConfigureTemplates";
            this.butConfigureTemplates.Size = new System.Drawing.Size(137, 112);
            this.butConfigureTemplates.TabIndex = 2;
            this.butConfigureTemplates.Text = "Templates";
            this.butConfigureTemplates.Click += new System.EventHandler(this.butConfigureTemplates_Click);
            // 
            // butConfigureDictionary
            // 
            this.butConfigureDictionary.Location = new System.Drawing.Point(298, 12);
            this.butConfigureDictionary.Name = "butConfigureDictionary";
            this.butConfigureDictionary.Size = new System.Drawing.Size(137, 112);
            this.butConfigureDictionary.TabIndex = 3;
            this.butConfigureDictionary.Text = "Reference Lists";
            this.butConfigureDictionary.Click += new System.EventHandler(this.butConfigureDictionary_Click);
            // 
            // txtConfigDirectory
            // 
            this.txtConfigDirectory.ButtonStyle = Janus.Windows.GridEX.EditControls.EditButtonStyle.Ellipsis;
            this.txtConfigDirectory.Location = new System.Drawing.Point(105, 131);
            this.txtConfigDirectory.Margin = new System.Windows.Forms.Padding(2);
            this.txtConfigDirectory.Name = "txtConfigDirectory";
            this.txtConfigDirectory.Size = new System.Drawing.Size(330, 20);
            this.txtConfigDirectory.TabIndex = 4;
            this.txtConfigDirectory.ButtonClick += new System.EventHandler(this.txtConfigFile_ButtonClick);
            this.txtConfigDirectory.TextChanged += new System.EventHandler(this.txtConfigFile_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 134);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Config Directory";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 165);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Current Operation";
            // 
            // txtCurrentOperation
            // 
            this.txtCurrentOperation.Location = new System.Drawing.Point(105, 161);
            this.txtCurrentOperation.Name = "txtCurrentOperation";
            this.txtCurrentOperation.Size = new System.Drawing.Size(100, 20);
            this.txtCurrentOperation.TabIndex = 7;
            // 
            // ConfigureDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 220);
            this.Controls.Add(this.txtCurrentOperation);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtConfigDirectory);
            this.Controls.Add(this.butConfigureDictionary);
            this.Controls.Add(this.butConfigureTemplates);
            this.Controls.Add(this.butConfigurePolicies);
            this.Controls.Add(this.butClose);
            this.Name = "ConfigureDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configuration";
            this.Load += new System.EventHandler(this.ConfigureDlg_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Janus.Windows.EditControls.UIButton butClose;
        private Janus.Windows.EditControls.UIButton butConfigurePolicies;
        private Janus.Windows.EditControls.UIButton butConfigureTemplates;
        private Janus.Windows.EditControls.UIButton butConfigureDictionary;
        private Janus.Windows.GridEX.EditControls.EditBox txtConfigDirectory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Janus.Windows.GridEX.EditControls.EditBox txtCurrentOperation;
    }
}