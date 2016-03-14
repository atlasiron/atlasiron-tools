using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using USP.Express.Pro;

namespace AtlasReportToolkit
{
    public partial class EditReportCtrl : UserControl
    {
        public EditReportCtrl()
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
                    m_activeReport.GodMode = (m_activeReport.Status == ReportStatus.Created);

                BindToReport();
            }
        }

        private void BindToReport()
        {
            ConfigureHeaderEdit(this.ActiveReport,gridHeader,3);

            ConfigureRowEdit(this.ActiveReport,gridRows);

            PrepareForValidation();
            foreach (GridEXRow row in gridRows.GetRows())
                ValidateDataEntry(gridRows, row, false);

            foreach (GridEXColumn column in gridRows.RootTable.Columns)
            {
                column.AutoSizeMode = ColumnAutoSizeMode.AllCellsAndHeader;
                column.AutoSize();
            }
        }

        public static void ConfigureRowEdit(Report report, GridEX gridRows)
        {
            if (report == null)
            {
                gridRows.SetDataBinding(null, null);
                return;
            }
            gridRows.SetDataBinding(report.Rows, null);

            gridRows.AllowAddNew = report.Template.AllowAddNew || report.GodMode ? InheritableBoolean.True : InheritableBoolean.False;
            gridRows.AllowDelete = report.Template.AllowDelete || report.GodMode ? InheritableBoolean.True : InheritableBoolean.False;

            gridRows.RootTable.Columns.Clear();
            foreach (ReportColumnPropertyDescriptor propDescriptor in report.Rows.GetItemProperties(null))
            {
                GridEXColumn column = gridRows.RootTable.Columns.Add();
                ConfigureEditColumn(report, propDescriptor, column);
                column.AutoSize();
            }
        }

        public static void ConfigureHeaderEdit(Report report, GridEX gridHeader, int columnSetColumns = 3, bool shrinkToFit = true)
        {
            if (report == null)
            {
                gridHeader.SetDataBinding(null, null);
                return;
            }

            gridHeader.SetDataBinding(new List<ReportHeader>() { report.Header }, null);

            gridHeader.RootTable.Columns.Clear();
            gridHeader.RootTable.ColumnSets.Clear();

            foreach (ReportColumnPropertyDescriptor propDescriptor in report.Header.GetProperties())
            {
                GridEXColumn column = gridHeader.RootTable.Columns.Add();
                ConfigureEditColumn(report, propDescriptor, column);
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
            }
            if (propDescriptor.Column.Type == ReportTemplateColumnType.Numeric)
            {
                column.EditType = EditType.TextBox;
            }
            if (propDescriptor.Column.Type == ReportTemplateColumnType.Text)
            {
                column.EditType = EditType.TextBox;
            }

            if (propDescriptor.Column.ReferenceList != null)
            {

                column.HasValueList = true;
                column.ValueList.Clear();
                foreach (ReportReferenceListValue item in propDescriptor.Column.ReferenceList.Items)
                    if (item.Activated)
                        column.ValueList.Add(new GridEXValueListItem { Text = item.Description, Value = item.Value });

                column.LimitToList = propDescriptor.Column.LimitToReferenceList;
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

            if (propDescriptor.Column.ReadOnly)
            {
                if (!report.GodMode)
                    column.EditType = EditType.NoEdit;
                column.CellStyle.BackColor = Color.LightGray;
            }
        }

        private void gridRows_GetNewRow(object sender, Janus.Windows.GridEX.GetNewRowEventArgs e)
        {
            e.NewRow = new ReportRow(this.ActiveReport);
        }

        private void gridRows_UpdatingRecord(object sender, CancelEventArgs e)
        {
            PrepareForValidation();
            e.Cancel = !ValidateDataEntry(gridRows, gridRows.CurrentRow, true) && !this.ActiveReport.GodMode;
        }

        private void gridHeader_UpdatingRecord(object sender, CancelEventArgs e)
        {
        }

        private void gridHeader_AddingRecord(object sender, CancelEventArgs e)
        {
            
        }

        private void gridRows_AddingRecord(object sender, CancelEventArgs e)
        {
            PrepareForValidation();
            e.Cancel = !ValidateDataEntry(gridRows, gridRows.CurrentRow, true) && !this.ActiveReport.GodMode;
        }

        private Dictionary<String, Parser> Parsers { get; set; }
        private void PrepareForValidation()
        {
            this.Parsers = new Dictionary<string, Parser>();
            if (this.ActiveReport == null || this.ActiveReport.Template == null)
                return;

            foreach (ReportTemplateColumn column in this.ActiveReport.Template.Rows)
            {
                if (String.IsNullOrWhiteSpace(column.Validation))
                    continue;

                Parser parser = new Parser();
                foreach (ReportTemplateColumn rowColumn in this.ActiveReport.Template.Rows)
                {
                    try
                    {
                        parser.Variables.Add(new Variable("" + rowColumn.Name + "", rowColumn.GetSystemType()));
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
                foreach (ReportTemplateColumn headerColumn in this.ActiveReport.Template.Header)
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
                
                this.Parsers[column.Name] = parser;
            }
        }

        private Boolean ValidateDataEntry(GridEX grid, GridEXRow row, bool displayErrors)
        {
            foreach (GridEXCell cell in row.Cells)
                cell.FormatStyle = null;

            if (row == null)
                return true;

            Object dataItem = row.DataRow;
            if (dataItem == null)
                return true;

            PropertyDescriptorCollection propDescriptors = TypeDescriptor.GetProperties(dataItem.GetType());
            if (dataItem is ReportData)
                propDescriptors = (dataItem as ReportData).GetProperties();

            GridEXRow prevRow = row.Position > 0 ? prevRow = grid.GetRow(row.Position - 1) : null;

            Object prevDataItem = (prevRow == null ? null : prevRow.DataRow);

            PropertyDescriptorCollection headerDescriptors = this.ActiveReport.Header.GetProperties();

            PerformCalculations(row, propDescriptors, prevDataItem, headerDescriptors);

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
                                MessageBox.Show("You must enter a value into the " + columnDescriptor.Column.Name + " column.", "Invalid Imformation", MessageBoxButtons.OK);

                            curCell.FormatStyle = new GridEXFormatStyle() { BackColor = Color.Red, FontBold = TriState.True };
                            validationResult = false;
                        }
                    }

                    if (columnDescriptor.Column.LimitToReferenceList)
                    {
                        Boolean inList = false;
                        foreach(ReportReferenceListValue item in columnDescriptor.Column.ReferenceList.Items)
                            if (item.Value.Equals(curCell.Text))
                                inList = true;

                        if (!inList)
                        {
                            if (displayErrors)
                                MessageBox.Show((curCell.Text ?? "?") + " is not allowed in the " + columnDescriptor.Column.Name + " column. You must pick from the available list.", "Invalid Imformation", MessageBoxButtons.OK);

                            curCell.FormatStyle = new GridEXFormatStyle() { BackColor = Color.Red, FontBold = TriState.True };
                            validationResult = false;
                        }
                    }

                    if (!String.IsNullOrWhiteSpace(columnDescriptor.Column.Validation))
                    {
                        if (!this.Parsers.ContainsKey(columnDescriptor.Column.Name))
                            continue;

                        Parser parser = this.Parsers[columnDescriptor.Column.Name];
                        if (parser == null)
                            continue;

                        Object[] values = new Object[parser.Variables.Count];
                        for (int l = 0; l < parser.Variables.Count; l++)
                        {
                            values[l] = columnDescriptor.DefaultNullValue;
                            for (int j = 0; j < propDescriptors.Count; j++)
                            {
                                if (parser.Variables[l].Name.Equals((propDescriptors[j].Name).Replace(" ", "_")) || (parser.Variables[l].Name.Equals(("{" + propDescriptors[j].Name + "}").Replace(" ", "_"))))
                                {
                                    foreach (GridEXCell cell in row.Cells)
                                        if (cell.Column.Key.Equals(propDescriptors[j].Name))
                                            values[l] = cell.Value;

                                    if (values[l] == null)
                                        values[l] = (propDescriptors[j] as ReportColumnPropertyDescriptor).DefaultNullValue;
                                }
                                if (parser.Variables[l].Name.Equals(("prev" + propDescriptors[j].Name).Replace(" ", "_")) || (parser.Variables[l].Name.Equals(("{prev" + propDescriptors[j].Name + "}").Replace(" ", "_"))))
                                {
                                    values[l] = propDescriptors[j].GetValue(prevDataItem);
                                    if (values[l] == null)
                                        values[l] = (propDescriptors[j] as ReportColumnPropertyDescriptor).DefaultNullValue;
                                }
                            }

                            for (int j = 0; j < headerDescriptors.Count; j++)
                            {
                                if (parser.Variables[l].Name.Equals(("header" + headerDescriptors[j].Name).Replace(" ", "_")) || parser.Variables[l].Name.Equals(("{header" + headerDescriptors[j].Name + "}").Replace(" ", "_")))
                                {
                                    values[l] = headerDescriptors[j].GetValue(this.ActiveReport.Header);
                                    if (values[l] == null)
                                        values[l] = (headerDescriptors[j] as ReportColumnPropertyDescriptor).DefaultNullValue;
                                }
                            }
                        }

                        ExpressionTree expression = parser.Parse(columnDescriptor.Column.Validation);
                        Object result = expression.Evaluate(values);
                        if (!String.Equals(result.ToString(), "true", StringComparison.OrdinalIgnoreCase))
                        {
                            if (displayErrors)
                            {
                                if (!String.IsNullOrWhiteSpace(columnDescriptor.Column.ErrorMessage))
                                    MessageBox.Show(columnDescriptor.Column.Name + " : " + columnDescriptor.Column.ErrorMessage, "Invalid Imformation", MessageBoxButtons.OK);
                                else
                                    MessageBox.Show("You have entered an invalid value for the " + columnDescriptor.Column.Name + " column.", "Invalid Imformation", MessageBoxButtons.OK);
                            }

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

        private void PerformCalculations(GridEXRow row, PropertyDescriptorCollection propDescriptors, Object prevDataItem, PropertyDescriptorCollection headerDescriptors)
        {
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

                    if (!this.Parsers.ContainsKey(columnDescriptor.Column.Name))
                        continue;

                    Parser parser = this.Parsers[columnDescriptor.Column.Name];
                    if (parser == null)
                        continue;

                    Object[] values = new Object[parser.Variables.Count];
                    for (int l = 0; l < parser.Variables.Count; l++)
                    {
                        values[l] = columnDescriptor.DefaultNullValue;
                        for (int j = 0; j < propDescriptors.Count; j++)
                        {
                            if (parser.Variables[l].Name.Equals((propDescriptors[j].Name).Replace(" ", "_")) || (parser.Variables[l].Name.Equals(("{" + propDescriptors[j].Name + "}").Replace(" ", "_"))))
                            {
                                foreach (GridEXCell cell in row.Cells)
                                    if (cell.Column.Key.Equals(propDescriptors[j].Name))
                                        values[l] = cell.Value;

                                if (values[l] == null)
                                    values[l] = (propDescriptors[j] as ReportColumnPropertyDescriptor).DefaultNullValue;
                            }
                            if (parser.Variables[l].Name.Equals(("prev" + propDescriptors[j].Name).Replace(" ", "_")) || (parser.Variables[l].Name.Equals(("{prev" + propDescriptors[j].Name + "}").Replace(" ", "_"))))
                            {
                                values[l] = propDescriptors[j].GetValue(prevDataItem);
                                if (values[l] == null)
                                    values[l] = (propDescriptors[j] as ReportColumnPropertyDescriptor).DefaultNullValue;
                            }
                        }

                        for (int j = 0; j < headerDescriptors.Count; j++)
                        {
                            if (parser.Variables[l].Name.Equals(("header" + headerDescriptors[j].Name).Replace(" ", "_")) || parser.Variables[l].Name.Equals(("{header" + headerDescriptors[j].Name + "}").Replace(" ", "_")))
                            {
                                values[l] = headerDescriptors[j].GetValue(this.ActiveReport.Header);
                                if (values[l] == null)
                                    values[l] = (headerDescriptors[j] as ReportColumnPropertyDescriptor).DefaultNullValue;
                            }
                        }
                    }

                    ExpressionTree expression = parser.Parse(columnDescriptor.Column.Calculation);
                    try
                    {
                        Object result = expression.Evaluate(values);
                        if (result != null)
                        {
                            row.BeginEdit();
                            curCell.Value = result;
                            row.EndEdit();
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

        private void gridRows_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //if (gridRows.HitTest(e.X, e.Y) == GridArea.Cell)
            //{
            //    GridEXColumn colClicked = gridRows.ColumnFromPoint();
            //    if (colClicked.Key == "<Key Here>")
            //    {
            //        cellValue = gridRows.GetValue(colClicked);
            //    }
            //}
        }

        private void gridHeader_RecordAdded(object sender, EventArgs e)
        {
            this.ActiveReport.Modified = true;
        }

        private void gridHeader_RecordsDeleted(object sender, EventArgs e)
        {
            this.ActiveReport.Modified = true;
        }

        private void gridHeader_RecordUpdated(object sender, EventArgs e)
        {
            this.ActiveReport.Modified = true;
        }

        private void gridRows_RecordsDeleted(object sender, EventArgs e)
        {
            this.ActiveReport.Modified = true;
        }

        private void gridRows_RecordUpdated(object sender, EventArgs e)
        {
            this.ActiveReport.Modified = true;
        }

        private void gridRows_RegionChanged(object sender, EventArgs e)
        {
            this.ActiveReport.Modified = true;
        }
    }
}
