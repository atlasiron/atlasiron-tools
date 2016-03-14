using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Xml.Serialization;
using System.Linq;
using System.Text;

namespace AtlasReportToolkit
{
    [Serializable]
    public class ReportPolicy 
    {
        public enum ReportPublishFormat { CSV, XML, AURTransaction,SQL };
        public enum ImportFormatType { CSV, iGantt, iGanttActivityLoadHaul, iGanttActivityDrillBlast };
        public ReportPolicy()
        {
            this.Identifier = Guid.NewGuid();
            this.HeaderDefaults = new BindingList<ReportPolicyColumnDefault>();
            this.RowDefaults = new BindingList<ReportPolicyColumnDefault>();
            this.PublishFormat = ReportPublishFormat.AURTransaction;
            this.RelativeArchiveDirectory = "Archive";
            this.TemplateIds = new List<Guid>();
            this.ImportFileFormat = ImportFormatType.CSV;
        }

        public override string ToString()
        {
            return this.Name;
        }

        public Guid Identifier { get; set; }
        public String Name { get; set; }
        public String Operation { get; set; }
        public String Contractor { get; set; }
        public String SendToEmailList { get; set; }
        public String SendCCEmailList { get; set; }
        public String ReplyToEmailList { get; set; }

        public List<Guid> TemplateIds { get; set; }
        public BindingList<ReportPolicyColumnDefault> HeaderDefaults { get; set; }
        public BindingList<ReportPolicyColumnDefault> RowDefaults { get; set; }
        public Boolean IncludePreviousReports { get; set; }
        public ImportFormatType ImportFileFormat { get; set; }
        public Boolean RemoveReportData { get; set; }
        public String RemoveReportDataDateColumn { get; set; }
        public int RemoveReportDataDays { get; set; }

        public ReportPublishFormat PublishFormat { get; set; }
        public String PublishedDirectory { get; set; }
        public String DBConnectionString { get; set; }
        public String ReportsDirectory { get; set; }
        public String RelativePendingDirectory { get; set; }
        public String RelativeApprovedDirectory { get; set; }
        public String RelativeArchiveDirectory { get; set; }

        public Guid TemplateId 
        {
            get
            {
                if (this.TemplateIds.Count == 0)
                    return Guid.Empty;
                return this.TemplateIds[0];
            }
            set
            {
                if (this.TemplateIds.Count == 0)
                {
                    this.TemplateIds.Add(value);
                    return;
                }
                this.TemplateIds[0] = value;
            }
        }
    }
}
