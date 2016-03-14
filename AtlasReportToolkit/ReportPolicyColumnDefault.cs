using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtlasReportToolkit
{
    [Serializable]
    public class ReportPolicyColumnDefault
    {
        public ReportPolicyColumnDefault()
        {
        }

        public String ColumnName { get; set; }
        public String DefaultValue { get; set; }
    }
}
