using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AtlasReportToolkit;

namespace AtlasReportToolkit
{
    [Serializable]
    public class XMLReport
    {
        public XMLReport()
        {
        }

        public XMLReport(Report report)
        {
            this.Name = report.Name;
            this.ReportingPeriod = report.ReportingPeriod;
            this.Sheets = new List<XMLReportSheet>();
            foreach (ReportSheet sheet in report.Sheets)
            {
                XMLReportSheet xmlSheet = new XMLReportSheet { Name = sheet.Template.Name, Header = sheet.Header, Rows = sheet.Rows };
                this.Sheets.Add(xmlSheet);
            }
        }

        public String Name { get; set; }
        public DateTime ReportingPeriod { get; set; }
        public List<XMLReportSheet> Sheets { get; set; }
    }
}
