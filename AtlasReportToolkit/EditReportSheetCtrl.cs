using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Janus.Windows.GridEX;
using USP.Express.Pro;
using Janus.Windows.UI.Tab;

namespace AtlasReportToolkit
{
    public partial class EditReportSheetCtrl : UserControl
    {
        public static String ReportOperationField = "{reportOperation}";
        public static String ReportPeriodField = "{reportPeriod}";

        public EditReportSheetCtrl()
        {
            InitializeComponent();
        }

        private Report m_activeReport;
        public Report ActiveReport
        {
            get
            {
                return m_activeReport;
            }
            set
            {
                m_activeReport = value;
                if (m_activeReport != null)
                {
                    m_activeReport.GodMode = (m_activeReport.Status == ReportStatus.Created);
                    m_activeReportSheet = m_activeReport.Sheets[0];
                }
                else
                    m_activeReportSheet = null;

                BindToReport();
            }
        }

        private ReportSheet m_activeReportSheet;
        public ReportSheet ActiveReportSheet
        {
            get
            {
                return m_activeReportSheet;
            }
            set
            {
                m_activeReportSheet = value;

                BindToReport();

                WidenColumns();
            }
        }

        public void EnterGodMode()
        {
            if (m_activeReport == null)
                return;

            m_activeReport.GodMode = true;

            BindToReport();
        }

        private void BindToReport()
        {
            this.SuspendLayout();
            gridHeader.SuspendLayout();
            gridRows.SuspendLayout();
            tabReports.SuspendLayout();

            m_ignoreTabChanged = true;
            tabReports.TabPages.Clear();
            if (this.ActiveReport != null && this.ActiveReport.Sheets.Count > 0 && this.ActiveReportSheet != null)
            {
                m_ignoreTabChanged = true;
                UITabPage selectedPage = null;
                foreach (ReportSheet sheet in this.ActiveReport.Sheets)
                {
                    UITabPage page = new UITabPage(sheet.Template.Name);
                    page.Tag = sheet;
                    if (sheet == this.ActiveReportSheet)
                        selectedPage = page;

                    tabReports.TabPages.Add(page);
                }
                if (selectedPage != null)
                    tabReports.SelectedTab = selectedPage;

                ConfigureHeaderEdit(this.ActiveReport, this.ActiveReportSheet, gridHeader, 3);

                ConfigureRowEdit(this.ActiveReport, this.ActiveReportSheet, gridRows);

                ValidateReport();

                foreach (GridEXColumn column in gridRows.RootTable.Columns)
                {
                    column.AutoSizeMode = ColumnAutoSizeMode.AllCellsAndHeader;
                    column.AutoSize();
                }
                if (m_activeReport.GodMode)
                    gridHeader.BackColor = Color.Yellow;
                else
                    gridHeader.BackColor = System.Drawing.SystemColors.Window;
            }
            else
            {
                gridHeader.SetDataBinding(null, null);
                gridHeader.RetrieveStructure();
                gridRows.SetDataBinding(null, null);
                gridRows.RetrieveStructure();
                gridHeader.BackColor = System.Drawing.SystemColors.Window;
            }
            m_ignoreTabChanged = false;

            gridHeader.ResumeLayout();
            gridRows.ResumeLayout();
            tabReports.ResumeLayout();
            this.ResumeLayout();
        }

        public void ValidateReport()
        {
            this.ValidationParser = PrepareForValidation(this.ActiveReportSheet);
            this.ValidationExprTrees = PrepareForValidationExpressions(this.ActiveReportSheet, this.ValidationParser);

            ValidateDataEntry(gridHeader, gridHeader.GetRow(), false, true);
            foreach (GridEXRow row in gridRows.GetRows())
                ValidateDataEntry(gridRows, row, false, true);
        }

        public void AutoSizeColumns()
        {
            if (this.ActiveReport == null)
                return;

            gridRows.AutoSizeColumns();
        }

        public static void ConfigureRowEdit(Report report, ReportSheet sheet, GridEX gridRows)
        {
            if (report == null || report.Sheets.Count == 0)
            {
                gridRows.SetDataBinding(null, null);
                return;
            }
            gridRows.SetDataBinding(sheet.Rows, null);
            gridRows.RetrieveStructure();

            gridRows.AllowAddNew = sheet.Template.AllowAddNew || report.GodMode ? InheritableBoolean.True : InheritableBoolean.False;
            gridRows.AllowDelete = sheet.Template.AllowDelete || report.GodMode ? InheritableBoolean.True : InheritableBoolean.False;
            gridRows.TotalRow = sheet.Template.ShowTotals || report.GodMode ? InheritableBoolean.True : InheritableBoolean.False;
            gridRows.FilterMode =  sheet.Template.AllowFilter || report.GodMode ? FilterMode.Automatic : FilterMode.None;
            gridRows.GroupTotals = sheet.Template.ShowSubTotals || report.GodMode ? GroupTotals.Always : GroupTotals.Default;
            //gridRows.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection;
            gridRows.RootTable.Columns.Clear();
            foreach (ReportColumnPropertyDescriptor propDescriptor in sheet.Rows.GetItemProperties(null))
            {
                GridEXColumn column = gridRows.RootTable.Columns.Add();
                ConfigureEditColumn(report, propDescriptor, column);
                column.AutoSize();
            }
        }

        public static void ConfigureHeaderEdit(Report report,ReportSheet sheet, GridEX gridHeader, int columnSetColumns = 3, bool shrinkToFit = true)
        {
            if (report == null)
            {
                gridHeader.SetDataBinding(null, null);
                gridHeader.ResumeLayout();
                return;
            }

            if (report.Sheets.Count > 0)
            {
                gridHeader.SetDataBinding(new List<ReportHeader>() { sheet.Header }, null);
                gridHeader.RetrieveStructure();
            }

            gridHeader.RootTable.Columns.Clear();
            gridHeader.RootTable.ColumnSets.Clear();

            if (report.Sheets.Count > 0)
            {
                foreach (ReportColumnPropertyDescriptor propDescriptor in sheet.Header.GetProperties())
                {
                    GridEXColumn column = gridHeader.RootTable.Columns.Add();
                    ConfigureEditColumn(report, propDescriptor, column);
                }
            }

            gridHeader.RootTable.CellLayoutMode = CellLayoutMode.UseColumnSets;
            gridHeader.RootTable.ColumnSetRowCount = gridHeader.RootTable.Columns.Count / columnSetColumns + 1;

            GridEXColumnSet columnSet = gridHeader.RootTable.ColumnSets.Add();
            columnSet.ColumnCount = columnSetColumns;
            for (int i = 0; i < gridHeader.RootTable.Columns.Count; i++)
                columnSet.Add(gridHeader.RootTable.Columns[i], i / columnSetColumns, i % columnSetColumns);

            gridHeader.CardCaptionPrefix = report.ToString();

            if (shrinkToFit)
            {
                Rectangle r = gridHeader.GetCardBounds(0);
                gridHeader.Size = new Size(r.Width, r.Height + gridHeader.CardSpacing * 2);
            }
        }

        public static void ConfigureEditColumn(Report report, ReportColumnPropertyDescriptor propDescriptor, GridEXColumn column)
        {
            column.DataMember = propDescriptor.Name;
            column.Caption = propDescriptor.DisplayName;
            column.Key = propDescriptor.Name;
            column.ColumnType = ColumnType.Text;
            column.DefaultValue = propDescriptor.Column.DefaultValue;
            column.Visible = true;
            if (propDescriptor.Column.Type == ReportTemplateColumnType.DateTime)
            {
                column.EditType = EditType.CalendarCombo;
                column.InputMask = "00/00/0000 00:00:00";
                column.FormatString = "dd/MM/yyyy HH:mm:ss";
            }
            if (propDescriptor.Column.Type == ReportTemplateColumnType.Date)
            {
                column.EditType = EditType.CalendarCombo;
                column.InputMask = "00/00/0000";
                column.FormatString = "dd/MM/yyyy";
                column.AggregateFunction = AggregateFunction.Count;
            }
            if (propDescriptor.Column.Type == ReportTemplateColumnType.Numeric)
            {
                column.EditType = EditType.TextBox;
                column.AggregateFunction = AggregateFunction.Sum;
            }
            if (propDescriptor.Column.Type == ReportTemplateColumnType.Text)
            {
                column.EditType = EditType.TextBox;
                column.AggregateFunction = AggregateFunction.Count;
            }
            if (propDescriptor.Column.ForceLowercase)
            {
                column.CharacterCasing = CharacterCasing.Lower;
            }
            if (propDescriptor.Column.ForceUppercase)
            {
                column.CharacterCasing = CharacterCasing.Upper;
            }
            if (propDescriptor.Column.Hidden)
                column.Visible = false;

            if (propDescriptor.Column.ReferenceList != null)
            {
                column.HasValueList = true;
                column.ValueList.Clear();
                foreach (ReportReferenceListValue item in propDescriptor.Column.ReferenceList.Items.OrderBy(a => a.Value))
                    if (item.Activated)
                        column.ValueList.Add(new GridEXValueListItem { Text = item.Value, Value = item.Value });

                column.LimitToList = propDescriptor.Column.LimitToReferenceList && !report.GodMode;
                if (column.LimitToList && !report.GodMode)
                    column.EditType = EditType.DropDownList;
                else
                    column.EditType = EditType.Combo;
            }

            if (String.IsNullOrWhiteSpace(propDescriptor.Column.Help))
            {
                column.CellToolTip = CellToolTip.UseCellToolTipText;
                column.CellToolTipText = propDescriptor.Column.Help;
            }

            if (propDescriptor.Column.ReadOnly || report.Status == ReportStatus.Approved)
            {
                if (!report.GodMode)
                    column.EditType = EditType.NoEdit;
                column.CellStyle.BackColor = Color.LightGray;
            }

            column.FilterEditType = FilterEditType.TextBox;
        }

        private void gridRows_GetNewRow(object sender, Janus.Windows.GridEX.GetNewRowEventArgs e)
        {
            e.NewRow = new ReportRow(this.ActiveReportSheet);
        }

        private void gridRows_UpdatingRecord(object sender, CancelEventArgs e)
        {
            if (this.ActiveReport == null || this.ActiveReportSheet == null|| this.ActiveReportSheet.Template == null)
                return;

            this.ValidationParser = PrepareForValidation(this.ActiveReportSheet);
            this.ValidationExprTrees = PrepareForValidationExpressions(this.ActiveReportSheet, this.ValidationParser);
            e.Cancel = !ValidateDataEntry(gridRows, gridRows.CurrentRow, true, false);
            if (this.ActiveReport.GodMode)
                e.Cancel = false;

            this.ActiveReport.Modified = true;
        }

        private void gridHeader_UpdatingRecord(object sender, CancelEventArgs e)
        {
        }

        private void gridHeader_AddingRecord(object sender, CancelEventArgs e)
        {
            
        }

        private void gridRows_AddingRecord(object sender, CancelEventArgs e)
        {
            if (this.ActiveReport == null || this.ActiveReportSheet == null|| this.ActiveReportSheet.Template == null)
                return;

            this.ValidationParser = PrepareForValidation(this.ActiveReportSheet);
            this.ValidationExprTrees = PrepareForValidationExpressions(this.ActiveReportSheet, this.ValidationParser);
            e.Cancel = !ValidateDataEntry(gridRows, gridRows.CurrentRow, true, false);
            if (this.ActiveReport.GodMode)
                e.Cancel = false;

            this.ActiveReport.Modified = true;
        }

        private Parser ValidationParser { get; set; }
        private Dictionary<String, ExpressionTree> ValidationExprTrees { get; set; }
        public static Parser PrepareForValidation(ReportSheet sheet)
        {
            Parser parser = new Parser();
            parser.Functions.Add(new RLookupFunction(sheet));

            foreach (ReportTemplateColumn rowColumn in sheet.Template.Rows)
            {
                try
                {
                    parser.Variables.Add(new Variable("" + rowColumn.Name.Replace(" ", "_") + "", rowColumn.GetSystemType()));
                }
                catch (System.Exception exc)
                {
                }
                try
                {
                    parser.Variables.Add(new Variable("{" + rowColumn.Name.Replace(" ", "_") + "}", rowColumn.GetSystemType()));
                }
                catch (System.Exception exc)
                {
                }
                try
                {
                    parser.Variables.Add(new Variable("prev" + rowColumn.Name.Replace(" ", "_") + "", rowColumn.GetSystemType()));
                }
                catch (System.Exception exc)
                {
                }
                try
                {
                    parser.Variables.Add(new Variable("{prev" + rowColumn.Name.Replace(" ", "_") + "}", rowColumn.GetSystemType()));
                }
                catch (System.Exception exc)
                {
                }
            }
            foreach (ReportTemplateColumn headerColumn in sheet.Template.Header)
            {
                try
                {
                    parser.Variables.Add(new Variable("header" + headerColumn.Name.Replace(" ", "_") + "", headerColumn.GetSystemType()));
                }
                catch (System.Exception exc)
                {
                }
                try
                {
                    parser.Variables.Add(new Variable("{header" + headerColumn.Name.Replace(" ", "_") + "}", headerColumn.GetSystemType()));
                }
                catch (System.Exception exc)
                {
                }
            }
            try
            {
                parser.Variables.Add(new Variable(ReportOperationField, typeof(String)));
            }
            catch (System.Exception exc)
            {
            }
            try
            {
                parser.Variables.Add(new Variable(ReportPeriodField,typeof(DateTime)));
            }
            catch (System.Exception exc)
            {
            }

            return parser;
        }

        public static Dictionary<String, ExpressionTree> PrepareForValidationExpressions(ReportSheet sheet, Parser parser)
        {
            Dictionary<String, ExpressionTree> trees = new Dictionary<string, ExpressionTree>();
            foreach (ReportTemplateColumn rowColumn in sheet.Template.Rows)
            {
                if (!String.IsNullOrWhiteSpace(rowColumn.Validation))
                    trees[rowColumn.Name] = parser.Parse(rowColumn.Validation);
            }
            return trees;
        }

        private Boolean ValidateDataEntry(GridEX grid, GridEXRow row, bool displayErrors, bool explicitEditing)
        {
            foreach (GridEXCell cell in row.Cells)
                cell.FormatStyle = null;

            if (row == null)
                return true;

            Object dataItem = row.DataRow;
            if (dataItem == null)
                return true;

            PropertyDescriptorCollection propDescriptors = TypeDescriptor.GetProperties(dataItem.GetType());
            if (dataItem is ReportDataItems)
                propDescriptors = (dataItem as ReportDataItems).GetProperties();

            GridEXRow prevRow = row.Position > 0 ? prevRow = grid.GetRow(row.Position - 1) : null;

            Object prevDataItem = (prevRow == null ? null : prevRow.DataRow);

            PropertyDescriptorCollection headerDescriptors = this.ActiveReportSheet.Header.GetProperties();

            PerformCalculations(row, propDescriptors, prevDataItem, headerDescriptors, explicitEditing);

            Object[] values = GetRowValues(this.ValidationParser, this.ActiveReport, this.ActiveReportSheet, row, prevDataItem);

            if (this.ActiveReport.GodMode)
                displayErrors = false;

            bool validationResult = true;
            for(int i=0; i < propDescriptors.Count; i++)
            {
                try
                {
                    ReportColumnPropertyDescriptor columnDescriptor = propDescriptors[i] as ReportColumnPropertyDescriptor;
                    GridEXCell curCell = null;
                    foreach (GridEXCell cell in row.Cells)
                        if (cell.Column.Key.Equals(propDescriptors[i].Name))
                            curCell = cell;

                    if (curCell == null)
                        continue;

                    if (columnDescriptor.Column.Mandatory)
                    {
                        if (String.IsNullOrWhiteSpace(curCell.Text))
                        {
                            if (displayErrors)
                            {
                                MessageBox.Show("You must enter a value into the " + columnDescriptor.Column.Name + " column.", "Invalid Imformation", MessageBoxButtons.OK);
                                displayErrors = false;
                            }

                            curCell.FormatStyle = new GridEXFormatStyle() { BackColor = Color.Red, FontBold = TriState.True };
                            validationResult = false;
                        }
                    }

                    if (columnDescriptor.Column.LimitToReferenceList)
                    {
                        if (columnDescriptor.Column.ReferenceList != null)
                        {
                            Boolean inList = false;
                            foreach (ReportReferenceListValue item in columnDescriptor.Column.ReferenceList.Items)
                                if (item.Value.Equals(curCell.Text))
                                    inList = true;

                            if (!inList && !String.IsNullOrWhiteSpace(curCell.Text))
                            {
                                if (displayErrors)
                                {
                                    MessageBox.Show((String.IsNullOrWhiteSpace(curCell.Text) ? "<empty value>" : curCell.Text) + " is not allowed in the " + columnDescriptor.Column.Name + " column. You must pick from the available list.", "Invalid Imformation", MessageBoxButtons.OK);
                                    displayErrors = false;
                                }

                                gridRows.KeepRowSettings = true;
                                curCell.FormatStyle = new GridEXFormatStyle() { BackColor = Color.Red, FontBold = TriState.True };
                                validationResult = false;
                            }
                        }
                    }

                    if (!String.IsNullOrWhiteSpace(columnDescriptor.Column.Validation))
                    {
                        ExpressionTree expression = null;
                        if (this.ValidationExprTrees != null && this.ValidationExprTrees.ContainsKey(columnDescriptor.Column.Name))
                            expression = this.ValidationExprTrees[columnDescriptor.Column.Name];
                        else
                            expression = this.ValidationParser.Parse(columnDescriptor.Column.Validation);
                        Object result = expression.Evaluate(values);
                        if (!String.Equals(result.ToString(), "true", StringComparison.OrdinalIgnoreCase))
                        {
                            if (displayErrors)
                            {
                                if (!String.IsNullOrWhiteSpace(columnDescriptor.Column.ErrorMessage))
                                    MessageBox.Show(columnDescriptor.Column.Name + " : " + columnDescriptor.Column.ErrorMessage, "Invalid Imformation", MessageBoxButtons.OK);
                                else
                                    MessageBox.Show("You have entered an invalid value for the " + columnDescriptor.Column.Name + " column.", "Invalid Imformation", MessageBoxButtons.OK);
                                displayErrors = false;
                            }

                            gridRows.KeepRowSettings = true;
                            curCell.FormatStyle = new GridEXFormatStyle() { BackColor = Color.Red, FontBold = TriState.True };
                            validationResult = false;
                        }
                    }
                }
                catch(System.Exception exc)
                {
                }
            }
            return validationResult;
        }

        private Object[] GetRowValues(Parser parser, Report report, ReportSheet sheet, GridEXRow gridRow, Object prevDataItem)
        {

            ReportRow row = new ReportRow(sheet);
            foreach (GridEXCell cell in gridRow.Cells)
                if (cell.Value != null && cell.Value != DBNull.Value && !String.IsNullOrWhiteSpace(cell.Text))
                    row.Values[cell.Column.Key] = cell.Value;

            return GetRowValues(parser, report, sheet, row, prevDataItem);
        }

        public static Object[] GetRowValues(Parser parser, Report report, ReportSheet sheet, ReportRow row, Object prevDataItem)
        {
            PropertyDescriptorCollection rowDescriptors = null;
            if (row != null)
                rowDescriptors = row.GetProperties();
            PropertyDescriptorCollection headerDescriptors = sheet.Header.GetProperties();

            Object[] values = new Object[parser.Variables.Count];
            for (int l = 0; l < parser.Variables.Count; l++)
            {
                values[l] = DBNull.Value;
                if (parser.Variables[l].Name.Equals(ReportOperationField))
                    values[l] = report.Operation;
                else if (parser.Variables[l].Name.Equals(ReportPeriodField))
                    values[l] = report.ReportingPeriod;
                else
                {
                    if (row != null)
                    {
                        for (int j = 0; j < rowDescriptors.Count; j++)
                        {
                            if (parser.Variables[l].Name.Equals((rowDescriptors[j].Name).Replace(" ", "_")) || (parser.Variables[l].Name.Equals(("{" + rowDescriptors[j].Name + "}").Replace(" ", "_"))))
                            {
                                if (row.Values.ContainsKey(rowDescriptors[j].Name))
                                    values[l] = row.Values[rowDescriptors[j].Name];

                                if (values[l] == null)
                                    values[l] = (rowDescriptors[j] as ReportColumnPropertyDescriptor).DefaultNullValue;
                            }
                            if (parser.Variables[l].Name.Equals(("prev" + rowDescriptors[j].Name).Replace(" ", "_")) || (parser.Variables[l].Name.Equals(("{prev" + rowDescriptors[j].Name + "}").Replace(" ", "_"))))
                            {
                                values[l] = rowDescriptors[j].GetValue(prevDataItem);
                                if (values[l] == null)
                                    values[l] = (rowDescriptors[j] as ReportColumnPropertyDescriptor).DefaultNullValue;
                            }
                        }
                    }

                    for (int j = 0; j < headerDescriptors.Count; j++)
                    {
                        if (parser.Variables[l].Name.Equals(("header" + headerDescriptors[j].Name).Replace(" ", "_")) || parser.Variables[l].Name.Equals(("{header" + headerDescriptors[j].Name + "}").Replace(" ", "_")))
                        {
                            values[l] = headerDescriptors[j].GetValue(sheet.Header);
                            if (values[l] == null)
                                values[l] = (headerDescriptors[j] as ReportColumnPropertyDescriptor).DefaultNullValue;
                        }
                    }
                }
            }
            return values;
        }

        private void PerformCalculations(GridEXRow row, PropertyDescriptorCollection propDescriptors, Object prevDataItem, PropertyDescriptorCollection headerDescriptors, bool explicitEditing)
        {
            Dictionary<String, ExpressionTree> calcTrees = new Dictionary<string, ExpressionTree>();
            Boolean anythingCalculated = true;
            for (int j = 0; j < propDescriptors.Count && anythingCalculated; j++)
            {
                Object[] values = GetRowValues(this.ValidationParser, this.ActiveReport, this.ActiveReportSheet, row, prevDataItem);

                anythingCalculated = false;
                for (int i = 0; i < propDescriptors.Count; i++)
                {
                    try
                    {
                        ReportColumnPropertyDescriptor columnDescriptor = propDescriptors[i] as ReportColumnPropertyDescriptor;
                        if (String.IsNullOrWhiteSpace(columnDescriptor.Column.Calculation))
                            continue;

                        GridEXCell curCell = null;
                        foreach (GridEXCell cell in row.Cells)
                            if (cell.Column.Key.Equals(propDescriptors[i].Name))
                                curCell = cell;

                        if (curCell == null)
                            continue;

                        Parser parser = this.ValidationParser;
                        if (parser == null)
                            continue;

                        ExpressionTree expression = null;
                        if (!calcTrees.ContainsKey(columnDescriptor.Column.Name))
                            calcTrees[columnDescriptor.Column.Name] = parser.Parse(columnDescriptor.Column.Calculation);
                        expression = calcTrees[columnDescriptor.Column.Name];
                        try
                        {
                            Object result = expression.Evaluate(values);
                            if ((result != null && result != DBNull.Value) && (curCell.Value == null || !result.ToString().Equals(curCell.Value.ToString())))
                            {
                                if (row.GridEX.EditMode == EditMode.EditOff)
                                {
                                    if (explicitEditing)
                                        row.BeginEdit();
                                    curCell.Value = result;
                                    if (explicitEditing)
                                        row.EndEdit();
                                }
                                else
                                    curCell.Value = result;

                                anythingCalculated = true;
                            }
                        }
                        catch (System.Exception exc)
                        {
                        }
                    }
                    catch (System.Exception exc)
                    {
                    }
                }
            }
        }


        private void gridHeader_RecordAdded(object sender, EventArgs e)
        {
            if (this.ActiveReport == null)
                return;

            this.ActiveReport.Modified = true;
        }

        private void gridHeader_RecordsDeleted(object sender, EventArgs e)
        {
            if (this.ActiveReport == null)
                return;

            this.ActiveReport.Modified = true;
        }

        private void gridHeader_RecordUpdated(object sender, EventArgs e)
        {
            if (this.ActiveReport == null)
                return;

            this.ActiveReport.Modified = true;
            GridEXRow curRow = gridHeader.GetRow();
            if (curRow != null)
            {
                this.ValidationParser = PrepareForValidation(this.ActiveReportSheet);
                this.ValidationExprTrees = PrepareForValidationExpressions(this.ActiveReportSheet, this.ValidationParser);

                ValidateDataEntry(gridHeader, curRow, true, false);
            }
        }

        private void gridRows_RecordsDeleted(object sender, EventArgs e)
        {
            if (this.ActiveReport == null)
                return;

            this.ActiveReport.Modified = true;
        }

        private void gridRows_RecordUpdated(object sender, EventArgs e)
        {
            if (this.ActiveReport == null)
                return;

            gridRows.Col = 0;

            this.ActiveReport.Modified = true;
        }

        private void gridRows_RegionChanged(object sender, EventArgs e)
        {
            if (this.ActiveReport == null)
                return;

            this.ActiveReport.Modified = true;
        }

        private void gridRows_EditingCell(object sender, EditingCellEventArgs e)
        {

        }

        private void gridRows_MouseDown(object sender, MouseEventArgs e)
        {
            if (gridRows.HitTest(e.X, e.Y) == Janus.Windows.GridEX.GridArea.Cell)
            {
                //gridRows.CurrentCellDroppedDown = true;
            }
        }

        private void gridHeader_MouseDown(object sender, MouseEventArgs e)
        {
            if (gridRows.HitTest(e.X, e.Y) == Janus.Windows.GridEX.GridArea.Cell)
            {
               // gridHeader.CurrentCellDroppedDown = true;
            }
        }

        private void gridHeader_FormattingRow(object sender, RowLoadEventArgs e)
        {

        }

        private void gridRows_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //GridArea loc = gridRows.HitTest(e.X,e.Y);
            //if (loc == GridArea.Cell)
            //{
            //    int row = gridRows.RowPositionFromPoint(e.X, e.Y);
            //    GridEXColumn col = gridRows.ColumnFromPoint(e.X, e.Y);
            //    GridEXCell cell = gridRows.GetRow(row).Cells[col];
            //    if (row > 1)
            //    {
            //        GridEXCell prevCell = gridRows.GetRow(row-1).Cells[col];
            //        gridRows.GetRow(row).BeginEdit();
            //        gridRows.GetRow(row).Cells[col].Value = prevCell.Value;
            //        gridRows.GetRow(row).EndEdit();
            //    }
            //}
        }

        private void gridRows_DoubleClick(object sender, EventArgs e)
        {
            //GridArea loc = gridRows.HitTest();
            //if (loc == GridArea.Cell)
            //{
            //    int row = gridRows.RowPositionFromPoint();
            //    GridEXColumn col = gridRows.ColumnFromPoint();
            //    GridEXCell cell = gridRows.GetRow(row).Cells[col];
            //    if (row > 1)
            //    {
            //        GridEXCell prevCell = gridRows.GetRow(row - 1).Cells[col];
            //        gridRows.GetRow(row).BeginEdit();
            //        gridRows.GetRow(row).Cells[col].Value = prevCell.Value;
            //        gridRows.GetRow(row).EndEdit();
            //    }
            //}
        }

        private void gridRows_DeletingRecords(object sender, CancelEventArgs e)
        {
            int numVisibleColumns = 0;
            foreach(GridEXColumn column in gridRows.RootTable.Columns)
                if (column.Visible)
                    numVisibleColumns ++;

            foreach (GridEXSelectedItem rowItem in gridRows.SelectedItems)
            {
                if (rowItem.SelectedCells.Count != numVisibleColumns)
                {
                    gridRows.GetRow(rowItem.Position).BeginEdit();
                    foreach (GridEXCell cell in rowItem.SelectedCells)
                        cell.Value = DBNull.Value;
                    gridRows.GetRow(rowItem.Position).EndEdit();
                    e.Cancel = true;
                }
            }

            this.ActiveReport.Modified = true;
        }

        private void gridRows_SelectionChanged(object sender, EventArgs e)
        {
            GridEXRow row = gridRows.GetRow();
            if (row == null || row.RowType != RowType.Record)
                return;

            foreach (ReportTemplateColumn column in this.ActiveReportSheet.Template.Rows)
            {
                if (!column.AllowAutoDefault)
                    continue;

                for (int i = 0; i < gridRows.RootTable.Columns.Count; i++)
                    if (gridRows.RootTable.Columns[i].Key.Equals(column.Name))
                        gridRows.RootTable.Columns[i].DefaultValue = row.Cells[i].Value;
            }
        }

        private void gridRows_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.C && e.Control && this.ActiveReport.AllowCopyAndPaste)
            {
                CopySelectedCells();
            }
            if (e.KeyCode == Keys.V && e.Control && this.ActiveReportSheet.Template.AllowInsertFromPaste)
            {
                IDataObject dataObject = Clipboard.GetDataObject();
                if (dataObject.GetDataPresent(DataFormats.CommaSeparatedValue))
                {
                    MemoryStream table = dataObject.GetData(DataFormats.CommaSeparatedValue) as MemoryStream;
                    UTF8Encoding enc = new System.Text.UTF8Encoding();
                    StreamReader reader = new System.IO.StreamReader(table, enc);
                    CreateNewReportDlg.ImportStream(this.ActiveReport, this.ActiveReportSheet, reader);
                    reader.Close();

                    this.ActiveReport.Modified = true;
                    this.ValidateReport();
                }
            }
        }

        public void WidenColumns()
        {
            gridRows.AutoSizeColumns();
        }

        public void CopySelectedCells(Boolean selectAll=false)
        {
            if (selectAll)
            {
                gridRows.SelectedItems.Clear();
                foreach (GridEXRow row in gridRows.GetRows())
                    if (row.RowType == RowType.Record)
                        gridRows.SelectedItems.Add(row.Position);
            }
            Clipboard.SetDataObject(gridRows.GetClipString(false,false,'\t','\n'));
        }

        public void FillSelectedCells(int numToRepeat = 1)
        {
            if (this.ActiveReport == null)
                return;

            if (gridRows.RootTable == null)
                return;

            if (numToRepeat <= 0)
                return;

            foreach (GridEXColumn column in gridRows.RootTable.Columns)
            {
                int firstCellPos = -1;
                int firstCellAbsPos = -1;
                foreach (GridEXCell cell in gridRows.SelectedCells)
                {
                    if (cell.Column == column && (firstCellPos > cell.Row.Position || firstCellPos < 0) && cell.Row.RowType == RowType.Record)
                    {
                        firstCellPos = cell.Row.Position;
                        firstCellAbsPos = cell.Row.AbsolutePosition;
                    }
                }
                if (firstCellPos < 0)
                    continue;

                int repeatCellIdx = 0;
                int curCellIdx = 0;
                foreach (GridEXCell cell in gridRows.SelectedCells)
                {
                    if (cell.Column == column && column.EditType != EditType.NoEdit && cell.Row.RowType == RowType.Record)
                    {
                        if (curCellIdx >= numToRepeat && firstCellPos + repeatCellIdx < gridRows.RowCount)
                        {
                            cell.Row.BeginEdit();
                            cell.Value = gridRows.GetRow(firstCellPos + repeatCellIdx).Cells[column].Value;
                            cell.Row.EndEdit();
                        }

                        repeatCellIdx = (repeatCellIdx+1)%numToRepeat;
                        curCellIdx++;
                    }
                }
            }
        }

        private void gridRows_RecordAdded(object sender, EventArgs e)
        {
            gridRows.Col = 0;
        }

        private Boolean m_ignoreTabChanged = true;
        private void tabReports_SelectedTabChanged(object sender, TabEventArgs e)
        {
            if (!m_ignoreTabChanged)
                this.ActiveReportSheet = e.Page.Tag as ReportSheet;
        }
    }
}
