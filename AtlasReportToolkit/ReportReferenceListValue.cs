using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtlasReportToolkit
{
    [Serializable]
    public class ReportReferenceListValue
    {
        public ReportReferenceListValue()
        {
            this.Activated = true;
        }

        public Boolean Activated { get; set; }
        public String Value { get; set; }
        private String m_description;
        public String Description
        { 
            get 
            {
                return String.IsNullOrWhiteSpace(m_description) ? (this.Value == null ? "?" : this.Value.ToString()) : m_description;
            } 
            set 
            {
                m_description = value;
            } 
        }
    }
}
