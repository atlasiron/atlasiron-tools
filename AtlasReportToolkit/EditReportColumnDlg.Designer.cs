namespace AtlasReportToolkit
{
    partial class EditReportColumnDlg
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
            this.butOK = new Janus.Windows.EditControls.UIButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new Janus.Windows.GridEX.EditControls.EditBox();
            this.ticMandatory = new Janus.Windows.EditControls.UICheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lstType = new Janus.Windows.EditControls.UIComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lstReferenceList = new Janus.Windows.EditControls.UIComboBox();
            this.txtDefault = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ticReadOnly = new Janus.Windows.EditControls.UICheckBox();
            this.ticLimitToReferenceList = new Janus.Windows.EditControls.UICheckBox();
            this.txtHelp = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ticConfigOnCreate = new Janus.Windows.EditControls.UICheckBox();
            this.txtImportAlias = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtValidation = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtErrorMessage = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCalculation = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label9 = new System.Windows.Forms.Label();
            this.ticForceUppercase = new Janus.Windows.EditControls.UICheckBox();
            this.ticForceLowercase = new Janus.Windows.EditControls.UICheckBox();
            this.txtAURColumn = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtAURValue = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label11 = new System.Windows.Forms.Label();
            this.ticHidden = new Janus.Windows.EditControls.UICheckBox();
            this.ticAutoDefault = new Janus.Windows.EditControls.UICheckBox();
            this.lstSubTotalType = new Janus.Windows.EditControls.UIComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // butOK
            // 
            this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.butOK.Location = new System.Drawing.Point(779, 274);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(75, 23);
            this.butOK.TabIndex = 1;
            this.butOK.Text = "Close";
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Name";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.CausesValidation = false;
            this.txtName.Location = new System.Drawing.Point(54, 10);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(173, 20);
            this.txtName.TabIndex = 3;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // ticMandatory
            // 
            this.ticMandatory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ticMandatory.CausesValidation = false;
            this.ticMandatory.Location = new System.Drawing.Point(483, 7);
            this.ticMandatory.Name = "ticMandatory";
            this.ticMandatory.Size = new System.Drawing.Size(77, 23);
            this.ticMandatory.TabIndex = 4;
            this.ticMandatory.Text = "Mandatory";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Type";
            // 
            // lstType
            // 
            this.lstType.CausesValidation = false;
            this.lstType.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            this.lstType.Location = new System.Drawing.Point(54, 36);
            this.lstType.Name = "lstType";
            this.lstType.Size = new System.Drawing.Size(173, 20);
            this.lstType.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Reference List";
            // 
            // lstReferenceList
            // 
            this.lstReferenceList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstReferenceList.CausesValidation = false;
            this.lstReferenceList.Location = new System.Drawing.Point(100, 62);
            this.lstReferenceList.Name = "lstReferenceList";
            this.lstReferenceList.Size = new System.Drawing.Size(534, 20);
            this.lstReferenceList.TabIndex = 8;
            // 
            // txtDefault
            // 
            this.txtDefault.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDefault.CausesValidation = false;
            this.txtDefault.Location = new System.Drawing.Point(302, 36);
            this.txtDefault.Name = "txtDefault";
            this.txtDefault.Size = new System.Drawing.Size(415, 20);
            this.txtDefault.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(237, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Default";
            // 
            // ticReadOnly
            // 
            this.ticReadOnly.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ticReadOnly.CausesValidation = false;
            this.ticReadOnly.Location = new System.Drawing.Point(565, 7);
            this.ticReadOnly.Name = "ticReadOnly";
            this.ticReadOnly.Size = new System.Drawing.Size(77, 23);
            this.ticReadOnly.TabIndex = 11;
            this.ticReadOnly.Text = "Read only";
            // 
            // ticLimitToReferenceList
            // 
            this.ticLimitToReferenceList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ticLimitToReferenceList.CausesValidation = false;
            this.ticLimitToReferenceList.Location = new System.Drawing.Point(640, 60);
            this.ticLimitToReferenceList.Name = "ticLimitToReferenceList";
            this.ticLimitToReferenceList.Size = new System.Drawing.Size(77, 23);
            this.ticLimitToReferenceList.TabIndex = 12;
            this.ticLimitToReferenceList.Text = "Limit to list";
            // 
            // txtHelp
            // 
            this.txtHelp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHelp.CausesValidation = false;
            this.txtHelp.Location = new System.Drawing.Point(94, 114);
            this.txtHelp.Name = "txtHelp";
            this.txtHelp.Size = new System.Drawing.Size(761, 20);
            this.txtHelp.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Help";
            // 
            // ticConfigOnCreate
            // 
            this.ticConfigOnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ticConfigOnCreate.CausesValidation = false;
            this.ticConfigOnCreate.Location = new System.Drawing.Point(711, 60);
            this.ticConfigOnCreate.Name = "ticConfigOnCreate";
            this.ticConfigOnCreate.Size = new System.Drawing.Size(132, 23);
            this.ticConfigOnCreate.TabIndex = 15;
            this.ticConfigOnCreate.Text = "Configure List on Create";
            // 
            // txtImportAlias
            // 
            this.txtImportAlias.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtImportAlias.CausesValidation = false;
            this.txtImportAlias.Location = new System.Drawing.Point(94, 187);
            this.txtImportAlias.Name = "txtImportAlias";
            this.txtImportAlias.Size = new System.Drawing.Size(761, 20);
            this.txtImportAlias.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 190);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Import Alias";
            // 
            // txtValidation
            // 
            this.txtValidation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtValidation.CausesValidation = false;
            this.txtValidation.Location = new System.Drawing.Point(94, 138);
            this.txtValidation.Name = "txtValidation";
            this.txtValidation.Size = new System.Drawing.Size(760, 20);
            this.txtValidation.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 141);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Validation";
            // 
            // txtErrorMessage
            // 
            this.txtErrorMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtErrorMessage.CausesValidation = false;
            this.txtErrorMessage.Location = new System.Drawing.Point(94, 162);
            this.txtErrorMessage.Name = "txtErrorMessage";
            this.txtErrorMessage.Size = new System.Drawing.Size(760, 20);
            this.txtErrorMessage.TabIndex = 21;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 166);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Error Message";
            // 
            // txtCalculation
            // 
            this.txtCalculation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCalculation.CausesValidation = false;
            this.txtCalculation.Location = new System.Drawing.Point(94, 89);
            this.txtCalculation.Name = "txtCalculation";
            this.txtCalculation.Size = new System.Drawing.Size(760, 20);
            this.txtCalculation.TabIndex = 23;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 92);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "Calculation";
            // 
            // ticForceUppercase
            // 
            this.ticForceUppercase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ticForceUppercase.CausesValidation = false;
            this.ticForceUppercase.Location = new System.Drawing.Point(637, 7);
            this.ticForceUppercase.Name = "ticForceUppercase";
            this.ticForceUppercase.Size = new System.Drawing.Size(99, 23);
            this.ticForceUppercase.TabIndex = 24;
            this.ticForceUppercase.Text = "Make uppercase";
            // 
            // ticForceLowercase
            // 
            this.ticForceLowercase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ticForceLowercase.CausesValidation = false;
            this.ticForceLowercase.Location = new System.Drawing.Point(744, 7);
            this.ticForceLowercase.Name = "ticForceLowercase";
            this.ticForceLowercase.Size = new System.Drawing.Size(99, 23);
            this.ticForceLowercase.TabIndex = 25;
            this.ticForceLowercase.Text = "Make lowercase";
            // 
            // txtAURColumn
            // 
            this.txtAURColumn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAURColumn.CausesValidation = false;
            this.txtAURColumn.Location = new System.Drawing.Point(94, 213);
            this.txtAURColumn.Name = "txtAURColumn";
            this.txtAURColumn.Size = new System.Drawing.Size(760, 20);
            this.txtAURColumn.TabIndex = 27;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 216);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 13);
            this.label10.TabIndex = 26;
            this.label10.Text = "AUR Column";
            // 
            // txtAURValue
            // 
            this.txtAURValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAURValue.CausesValidation = false;
            this.txtAURValue.Location = new System.Drawing.Point(94, 239);
            this.txtAURValue.Name = "txtAURValue";
            this.txtAURValue.Size = new System.Drawing.Size(760, 20);
            this.txtAURValue.TabIndex = 29;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(13, 242);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(60, 13);
            this.label11.TabIndex = 28;
            this.label11.Text = "AUR Value";
            // 
            // ticHidden
            // 
            this.ticHidden.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ticHidden.CausesValidation = false;
            this.ticHidden.Location = new System.Drawing.Point(400, 7);
            this.ticHidden.Name = "ticHidden";
            this.ticHidden.Size = new System.Drawing.Size(77, 23);
            this.ticHidden.TabIndex = 30;
            this.ticHidden.Text = "Hidden";
            // 
            // ticAutoDefault
            // 
            this.ticAutoDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ticAutoDefault.CausesValidation = false;
            this.ticAutoDefault.Location = new System.Drawing.Point(723, 31);
            this.ticAutoDefault.Name = "ticAutoDefault";
            this.ticAutoDefault.Size = new System.Drawing.Size(99, 23);
            this.ticAutoDefault.TabIndex = 31;
            this.ticAutoDefault.Text = "Auto default";
            // 
            // lstSubTotalType
            // 
            this.lstSubTotalType.CausesValidation = false;
            this.lstSubTotalType.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            this.lstSubTotalType.Location = new System.Drawing.Point(276, 10);
            this.lstSubTotalType.Name = "lstSubTotalType";
            this.lstSubTotalType.Size = new System.Drawing.Size(118, 20);
            this.lstSubTotalType.TabIndex = 33;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(235, 14);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(50, 13);
            this.label12.TabIndex = 32;
            this.label12.Text = "SubTotal";
            // 
            // EditReportColumnDlg
            // 
            this.AcceptButton = this.butOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(867, 308);
            this.Controls.Add(this.lstSubTotalType);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.ticAutoDefault);
            this.Controls.Add(this.ticHidden);
            this.Controls.Add(this.txtAURValue);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtAURColumn);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.ticForceLowercase);
            this.Controls.Add(this.ticForceUppercase);
            this.Controls.Add(this.txtCalculation);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtErrorMessage);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtValidation);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtImportAlias);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ticConfigOnCreate);
            this.Controls.Add(this.txtHelp);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ticLimitToReferenceList);
            this.Controls.Add(this.ticReadOnly);
            this.Controls.Add(this.txtDefault);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lstReferenceList);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lstType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ticMandatory);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.butOK);
            this.Name = "EditReportColumnDlg";
            this.Text = "Edit Report Column";
            this.Load += new System.EventHandler(this.EditReportColumnDlg_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Janus.Windows.EditControls.UIButton butOK;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.GridEX.EditControls.EditBox txtName;
        private Janus.Windows.EditControls.UICheckBox ticMandatory;
        private System.Windows.Forms.Label label2;
        private Janus.Windows.EditControls.UIComboBox lstType;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.EditControls.UIComboBox lstReferenceList;
        private Janus.Windows.GridEX.EditControls.EditBox txtDefault;
        private System.Windows.Forms.Label label4;
        private Janus.Windows.EditControls.UICheckBox ticReadOnly;
        private Janus.Windows.EditControls.UICheckBox ticLimitToReferenceList;
        private Janus.Windows.GridEX.EditControls.EditBox txtHelp;
        private System.Windows.Forms.Label label5;
        private Janus.Windows.EditControls.UICheckBox ticConfigOnCreate;
        private Janus.Windows.GridEX.EditControls.EditBox txtImportAlias;
        private System.Windows.Forms.Label label6;
        private Janus.Windows.GridEX.EditControls.EditBox txtValidation;
        private System.Windows.Forms.Label label7;
        private Janus.Windows.GridEX.EditControls.EditBox txtErrorMessage;
        private System.Windows.Forms.Label label8;
        private Janus.Windows.GridEX.EditControls.EditBox txtCalculation;
        private System.Windows.Forms.Label label9;
        private Janus.Windows.EditControls.UICheckBox ticForceUppercase;
        private Janus.Windows.EditControls.UICheckBox ticForceLowercase;
        private Janus.Windows.GridEX.EditControls.EditBox txtAURColumn;
        private System.Windows.Forms.Label label10;
        private Janus.Windows.GridEX.EditControls.EditBox txtAURValue;
        private System.Windows.Forms.Label label11;
        private Janus.Windows.EditControls.UICheckBox ticHidden;
        private Janus.Windows.EditControls.UICheckBox ticAutoDefault;
        private Janus.Windows.EditControls.UIComboBox lstSubTotalType;
        private System.Windows.Forms.Label label12;
    }
}