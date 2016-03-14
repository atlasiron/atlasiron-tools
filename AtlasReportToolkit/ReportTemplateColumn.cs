using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AtlasReportToolkit
{
    public enum ReportTemplateColumnSubTotalType { None, Count, Sum };

    public enum ReportTemplateColumnType { Numeric, Text, DateTime, Date };

    [Serializable]
    public class ReportTemplateColumn
    {
        public String Name { get; set; }
        public Boolean Mandatory { get; set; }
        public Boolean ReadOnly { get; set; }
        public Boolean ForceUppercase { get; set; }
        public Boolean ForceLowercase { get; set; }
        public ReportTemplateColumnSubTotalType SubTotalType { get; set; }
        public ReportTemplateColumnType Type { get; set; }
        public String ReferenceListName { get; set; }
        public Boolean LimitToReferenceList { get; set; }
        public Boolean ConfigureReferenceListOnCreateReport { get; set; }
        public String DefaultValue { get; set; }
        public String Calculation { get; set; }
        public Boolean Hidden { get; set; }
        public Boolean AllowAutoDefault { get; set; }
        private String m_importAlias;
        public String ImportAlias 
        {
            get
            {
                return this.m_importAlias;
            }
            set
            {
                m_importAlias = value;
            }
        }
        public String Validation { get; set; }
        public String ErrorMessage { get; set; }
        public String Help { get; set; }
        public ReportReferenceList ReferenceList { get; set; }

        public String AURColumn { get; set; }
        public String AURValue { get; set; }

        public Type GetSystemType()
        {
            if (this.Type == ReportTemplateColumnType.DateTime)
                return typeof(DateTime);
            if (this.Type == ReportTemplateColumnType.Date)
                return typeof(DateTime);
            if (this.Type == ReportTemplateColumnType.Numeric)
                return typeof(Double);
            return typeof(String);
        }

        public Object GetDefaultValue()
        {
            try
            {
                if (String.IsNullOrWhiteSpace(this.DefaultValue))
                    return null;

                if (this.Type == ReportTemplateColumnType.DateTime)
                    return DateTime.Parse(this.DefaultValue);
                if (this.Type == ReportTemplateColumnType.Date)
                    return DateTime.Parse(this.DefaultValue);
                if (this.Type == ReportTemplateColumnType.Numeric)
                    return Double.Parse(this.DefaultValue);
                return this.DefaultValue;
            }
            catch (System.Exception exc)
            {
                return null;
            }
        }
    }
}
