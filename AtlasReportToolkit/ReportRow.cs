using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Linq;
using System.Text;

namespace AtlasReportToolkit
{
    [Serializable]
    public class ReportRow : ReportDataItems, ICustomTypeDescriptor
    {
        public ReportRow()
        {
        }

        public ReportRow(ReportSheet sheet)
            : base(sheet)
        {
            if (sheet.Template != null)
            {
                //foreach (ReportTemplateColumn column in sheet.Template.Rows)
                //{
                //    Object defaultValue = column.GetDefaultValue();
                //    if (defaultValue != null)
                //        this.Values[column.Name] = defaultValue;
                //}
            }
        }

        protected override BindingList<ReportTemplateColumn> Columns
        {
            get 
            {
                return this.Sheet == null || this.Sheet.Template == null ? new BindingList<ReportTemplateColumn>() : this.Sheet.Template.Rows; 
            }
        }

        public override PropertyDescriptorCollection GetProperties()
        {
            if (m_properties == null && this.Sheet != null && this.Sheet.Template != null && this.Sheet.Template.Rows != null)
            {
                List<PropertyDescriptor> properties = new List<PropertyDescriptor>();
                for (int i = 0; i < this.Sheet.Template.Rows.Count; i++)
                    properties.Add(new ReportColumnPropertyDescriptor(this.Sheet, this.Sheet.Template.Rows[i]));

                m_properties = new PropertyDescriptorCollection(properties.ToArray());
            }
            return m_properties;
        }
    }
}
