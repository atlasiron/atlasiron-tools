using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;
using System.Text;

namespace AtlasReportToolkit
{
    [Serializable]
    public abstract class ReportDataItems : ICustomTypeDescriptor, IXmlSerializable
    {
        public ReportDataItems()
        {
        }

        public ReportDataItems(ReportSheet sheet)
        {
            this.Sheet = sheet;
            this.Values = new Dictionary<string, object>();
        }

        public ReportSheet Sheet { get; set; }

        protected abstract BindingList<ReportTemplateColumn> Columns { get; }
        public Dictionary<String, Object> Values { get; set; }

        #region ICustomTypeDescriptor Members

        public AttributeCollection GetAttributes()
        {
            throw new NotImplementedException();
        }

        public string GetClassName()
        {
            throw new NotImplementedException();
        }

        public string GetComponentName()
        {
            throw new NotImplementedException();
        }

        public TypeConverter GetConverter()
        {
            throw new NotImplementedException();
        }

        public EventDescriptor GetDefaultEvent()
        {
            throw new NotImplementedException();
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            throw new NotImplementedException();
        }

        public object GetEditor(Type editorBaseType)
        {
            throw new NotImplementedException();
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            throw new NotImplementedException();
        }

        public EventDescriptorCollection GetEvents()
        {
            throw new NotImplementedException();
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return this.GetProperties();
        }

        protected PropertyDescriptorCollection m_properties;
        public abstract PropertyDescriptorCollection GetProperties();

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }

        #endregion

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public Object this[String name]
        {
            get
            {
                if (this.Values.ContainsKey(name))
                    return this.Values[name];
                return null;
            }
        }

        public void ReadXml(XmlReader reader)
        {
            this.Values = new Dictionary<string, object>();

            reader.MoveToContent();
            if (reader.IsEmptyElement)
                return;
            if (!reader.IsStartElement())
                return;
            reader.Read();

            while (true || !reader.EOF)
            {
                if (reader.IsEmptyElement)
                    continue;
                if (!reader.IsStartElement())
                    break;

                String propertyName = XmlConvert.DecodeName(reader.Name);
                String propertyTypeText = reader.GetAttribute("DataType");
                ReportTemplateColumnType propertyType = ReportTemplateColumnType.Text;
                try
                {
                    if (!String.IsNullOrWhiteSpace(propertyTypeText))
                        propertyType = (ReportTemplateColumnType)Enum.Parse(typeof(ReportTemplateColumnType), propertyTypeText);
                }
                catch (System.Exception exc)
                {
                }
                try
                {
                    Object reportValue = null;
                    if (propertyType == ReportTemplateColumnType.Text)
                        reportValue = reader.ReadElementContentAsString();
                    if (propertyType == ReportTemplateColumnType.Date)
                        reportValue = reader.ReadElementContentAsDateTime();
                    if (propertyType == ReportTemplateColumnType.DateTime)
                        reportValue = reader.ReadElementContentAsDateTime();
                    if (propertyType == ReportTemplateColumnType.Numeric)
                        reportValue = reader.ReadElementContentAsDouble();

                    if (reportValue != null && !String.IsNullOrWhiteSpace(reportValue.ToString()))
                        this.Values[propertyName] = reportValue;
                }
                catch (System.Exception exc)
                {
                }
            }
            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            foreach(String key in this.Values.Keys)
            {
                ReportTemplateColumn column = (from a in this.Columns where a.Name.Equals(key) select a).FirstOrDefault();
                if (column == null || column.Hidden)
                    continue;

                writer.WriteStartElement(XmlConvert.EncodeName(key));
                writer.WriteAttributeString("DataType", column.Type.ToString());
                writer.WriteValue(this.Values[key]);
                writer.WriteEndElement();
            }
            //writer.WriteAttributeString("Name", Name);
            //if (Birthday != DateTime.MinValue)
            //    writer.WriteElementString("Birthday",
            //        Birthday.ToString("yyyy-MM-dd"));
        }
    }
}
