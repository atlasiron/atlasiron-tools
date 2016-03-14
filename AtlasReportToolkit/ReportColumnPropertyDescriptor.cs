using System;
using System.Collections.Generic;
using System.Reflection;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AtlasReportToolkit
{
    public class ReportColumnPropertyDescriptor : PropertyDescriptor
    {
        public ReportColumnPropertyDescriptor(ReportSheet sheet, ReportTemplateColumn column)
            : base(column.Name, null)
        {
            this.Sheet = sheet;
            this.Column = column;
        }

        public ReportSheet Sheet { get; set; }
        public ReportTemplateColumn Column { get; set; }

        public override bool CanResetValue(object component)
        {
            return true;
        }

        public override Type ComponentType
        {
            get { return typeof(ReportDataItems); }
        }

        public override object GetValue(object component)
        {
            if (component is ReportDataItems)
            {
                ReportDataItems row = component as ReportDataItems;
                if (row == null)
                    return null;
                if (row.Values.ContainsKey(this.Column.Name))
                    return row.Values[this.Column.Name];
            }
            return null;
        }

        public override bool IsReadOnly
        {
            get { return this.Column.ReadOnly && !this.Sheet.Report.GodMode && String.IsNullOrWhiteSpace(this.Column.Calculation); }
        }

        public Object DefaultNullValue
        {
            get
            {
                if (this.Column.Type == ReportTemplateColumnType.DateTime)
                    return new DateTime(1900,1,1);
                if (this.Column.Type == ReportTemplateColumnType.Date)
                    return new DateTime(1900, 1, 1);
                if (this.Column.Type == ReportTemplateColumnType.Numeric)
                    return 0.0;
                return "";
            }
        }

        public override Type PropertyType
        {
            get 
            {
                return this.Column.GetSystemType();
            }
        }

        public override void ResetValue(object component)
        {
            ReportRow row = component as ReportRow;
            if (row == null)
                return;
            if (row.Values.ContainsKey(this.Column.Name))
                row.Values.Remove(this.Column.Name);
        }

        public override void SetValue(object component, object value)
        {
            if (component is ReportDataItems)
            {
                ReportDataItems row = component as ReportDataItems;
                if (row == null)
                    return;
                if (value == null || value == DBNull.Value)
                {
                    if (row.Values.ContainsKey(this.Column.Name))
                        row.Values.Remove(this.Column.Name);
                }
                else
                {
                    try
                    {
                        if (this.Column.Type == ReportTemplateColumnType.DateTime)
                            row.Values[this.Column.Name] = DateTime.Parse(value.ToString());
                        else if (this.Column.Type == ReportTemplateColumnType.Date)
                            row.Values[this.Column.Name] = DateTime.Parse(value.ToString());
                        else if (this.Column.Type == ReportTemplateColumnType.Numeric)
                            row.Values[this.Column.Name] = Double.Parse(value.ToString());
                        else
                            row.Values[this.Column.Name] = value.ToString();
                    }
                    catch (System.Exception exc)
                    {
                    }
                }
            }
        }

        public override bool ShouldSerializeValue(object component)
        {
            return true;
        }
    }
}
