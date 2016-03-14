using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtlasReportToolkit
{
    [Serializable]
    public class XMLReportSheet
    {
        public XMLReportSheet()
        {
        }

        public String Name { get; set; }
        public ReportHeader Header { get; set; }
        public TypedLibBindingList<ReportRow> Rows { get; set; }
    }
}
