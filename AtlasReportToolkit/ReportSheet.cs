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
    public class ReportSheet
    {
        public ReportSheet()
        {
            this.Header = new ReportHeader(this);
            this.Rows = new TypedLibBindingList<ReportRow>();
        }

        public ReportSheet(Report report, ReportTemplate template)
        {
            this.Report = report;
            this.Template = template;
            this.Header = new ReportHeader(this);
            this.Rows = new TypedLibBindingList<ReportRow>();
        }

        [XmlIgnore]
        public Report Report { get; set; }

        public ReportTemplate Template { get; set; }

        public ReportHeader Header { get; set; }

        private TypedLibBindingList<ReportRow> m_rows;
        public TypedLibBindingList<ReportRow> Rows
        {
            get
            {
                m_rows.SetProperties(this);
                return m_rows;
            }
            set
            {
                m_rows = value;
            }
        }
    }
}
