namespace AtlasReportToolkit
{
    partial class EditReportSheetCtrl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Janus.Windows.GridEX.GridEXLayout gridHeader_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout gridRows_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditReportSheetCtrl));
            this.gridHeader = new Janus.Windows.GridEX.GridEX();
            this.gridRows = new Janus.Windows.GridEX.GridEX();
            this.tabReports = new Janus.Windows.UI.Tab.UITab();
            this.uiTabPage1 = new Janus.Windows.UI.Tab.UITabPage();
            ((System.ComponentModel.ISupportInitialize)(this.gridHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabReports)).BeginInit();
            this.tabReports.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridHeader
            // 
            this.gridHeader.CardCaptionFormatStyle.Appearance = Janus.Windows.GridEX.Appearance.Flat;
            this.gridHeader.CardCaptionFormatStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.gridHeader.CardCaptionFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.gridHeader.CardCaptionFormatStyle.ForeColor = System.Drawing.Color.Black;
            this.gridHeader.CardCaptionFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.gridHeader.CardInnerSpacing = 10;
            this.gridHeader.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.FieldsOnly;
            this.gridHeader.CardWidth = 568;
            this.gridHeader.ColumnAutoResize = true;
            this.gridHeader.ControlStyle.WindowTextColor = System.Drawing.SystemColors.MenuHighlight;
            gridHeader_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><CellLayoutMode>UseColumnSets</CellLayoutMode><Group" +
    "Condition /></RootTable></GridEXLayoutData>";
            this.gridHeader.DesignTimeLayout = gridHeader_DesignTimeLayout;
            this.gridHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.gridHeader.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None;
            this.gridHeader.ExpandableCards = false;
            this.gridHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.gridHeader.KeepRowSettings = true;
            this.gridHeader.Location = new System.Drawing.Point(0, 20);
            this.gridHeader.Name = "gridHeader";
            this.gridHeader.Size = new System.Drawing.Size(596, 123);
            this.gridHeader.TabIndex = 1;
            this.gridHeader.ThemedAreas = ((Janus.Windows.GridEX.ThemedArea)(((((((((Janus.Windows.GridEX.ThemedArea.ScrollBars | Janus.Windows.GridEX.ThemedArea.EditControls) 
            | Janus.Windows.GridEX.ThemedArea.Headers) 
            | Janus.Windows.GridEX.ThemedArea.GroupByBox) 
            | Janus.Windows.GridEX.ThemedArea.TreeGliphs) 
            | Janus.Windows.GridEX.ThemedArea.GroupRows) 
            | Janus.Windows.GridEX.ThemedArea.ControlBorder) 
            | Janus.Windows.GridEX.ThemedArea.Gridlines) 
            | Janus.Windows.GridEX.ThemedArea.CheckBoxes)));
            this.gridHeader.UpdateMode = Janus.Windows.GridEX.UpdateMode.CellUpdate;
            this.gridHeader.View = Janus.Windows.GridEX.View.SingleCard;
            this.gridHeader.FormattingRow += new Janus.Windows.GridEX.RowLoadEventHandler(this.gridHeader_FormattingRow);
            this.gridHeader.RecordsDeleted += new System.EventHandler(this.gridHeader_RecordsDeleted);
            this.gridHeader.RecordUpdated += new System.EventHandler(this.gridHeader_RecordUpdated);
            this.gridHeader.RecordAdded += new System.EventHandler(this.gridHeader_RecordAdded);
            this.gridHeader.UpdatingRecord += new System.ComponentModel.CancelEventHandler(this.gridHeader_UpdatingRecord);
            this.gridHeader.AddingRecord += new System.ComponentModel.CancelEventHandler(this.gridHeader_AddingRecord);
            this.gridHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridHeader_MouseDown);
            // 
            // gridRows
            // 
            this.gridRows.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True;
            this.gridRows.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True;
            this.gridRows.CellSelectionMode = Janus.Windows.GridEX.CellSelectionMode.SingleCell;
            gridRows_DesignTimeLayout.LayoutString = resources.GetString("gridRows_DesignTimeLayout.LayoutString");
            this.gridRows.DesignTimeLayout = gridRows_DesignTimeLayout;
            this.gridRows.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridRows.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.gridRows.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.gridRows.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.gridRows.HideSelection = Janus.Windows.GridEX.HideSelection.HighlightInactive;
            this.gridRows.KeepRowSettings = true;
            this.gridRows.Location = new System.Drawing.Point(0, 143);
            this.gridRows.Name = "gridRows";
            this.gridRows.NewRowPosition = Janus.Windows.GridEX.NewRowPosition.BottomRow;
            this.gridRows.RepeatHeaders = Janus.Windows.GridEX.InheritableBoolean.False;
            this.gridRows.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.gridRows.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleCellSelection;
            this.gridRows.Size = new System.Drawing.Size(596, 373);
            this.gridRows.TabIndex = 2;
            this.gridRows.GetNewRow += new Janus.Windows.GridEX.GetNewRowEventHandler(this.gridRows_GetNewRow);
            this.gridRows.DeletingRecords += new System.ComponentModel.CancelEventHandler(this.gridRows_DeletingRecords);
            this.gridRows.RecordsDeleted += new System.EventHandler(this.gridRows_RecordsDeleted);
            this.gridRows.RecordUpdated += new System.EventHandler(this.gridRows_RecordUpdated);
            this.gridRows.RecordAdded += new System.EventHandler(this.gridRows_RecordAdded);
            this.gridRows.UpdatingRecord += new System.ComponentModel.CancelEventHandler(this.gridRows_UpdatingRecord);
            this.gridRows.AddingRecord += new System.ComponentModel.CancelEventHandler(this.gridRows_AddingRecord);
            this.gridRows.SelectionChanged += new System.EventHandler(this.gridRows_SelectionChanged);
            this.gridRows.RegionChanged += new System.EventHandler(this.gridRows_RegionChanged);
            this.gridRows.DoubleClick += new System.EventHandler(this.gridRows_DoubleClick);
            this.gridRows.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridRows_KeyDown);
            this.gridRows.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.gridRows_MouseDoubleClick);
            this.gridRows.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridRows_MouseDown);
            // 
            // tabReports
            // 
            this.tabReports.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabReports.FirstTabOffset = 3;
            this.tabReports.Location = new System.Drawing.Point(0, 0);
            this.tabReports.Name = "tabReports";
            this.tabReports.Size = new System.Drawing.Size(596, 20);
            this.tabReports.TabIndex = 3;
            this.tabReports.TabPages.AddRange(new Janus.Windows.UI.Tab.UITabPage[] {
            this.uiTabPage1});
            this.tabReports.SelectedTabChanged += new Janus.Windows.UI.Tab.TabEventHandler(this.tabReports_SelectedTabChanged);
            // 
            // uiTabPage1
            // 
            this.uiTabPage1.Location = new System.Drawing.Point(1, 21);
            this.uiTabPage1.Name = "uiTabPage1";
            this.uiTabPage1.Size = new System.Drawing.Size(592, 0);
            this.uiTabPage1.TabStop = true;
            this.uiTabPage1.Text = "New Tab";
            // 
            // EditReportSheetCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridRows);
            this.Controls.Add(this.gridHeader);
            this.Controls.Add(this.tabReports);
            this.DoubleBuffered = true;
            this.Name = "EditReportSheetCtrl";
            this.Size = new System.Drawing.Size(596, 516);
            ((System.ComponentModel.ISupportInitialize)(this.gridHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabReports)).EndInit();
            this.tabReports.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.GridEX.GridEX gridHeader;
        private Janus.Windows.GridEX.GridEX gridRows;
        private Janus.Windows.UI.Tab.UITab tabReports;
        private Janus.Windows.UI.Tab.UITabPage uiTabPage1;

    }
}
