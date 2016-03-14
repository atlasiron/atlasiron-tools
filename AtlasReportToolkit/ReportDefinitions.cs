using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace AtlasReportToolkit
{
    public class ReportDefinitions
    {
        public static String BypassOperationCode = "UTH";

        private static String GetConfigFilename(String directory, String fileName)
        {
            return directory + "\\" + fileName;
        }

        public static ReportDefinitions LoadFullConfig(String directory, String fileName, Boolean updateReferenceLists=true)
        {
            ReportDefinitions definition = Extensions.Load<ReportDefinitions>(ReportDefinitions.GetConfigFilename(directory, fileName));
            definition.ConfigDateTime = File.GetLastWriteTime(ReportDefinitions.GetConfigFilename(directory, fileName));
            definition.ConfigFileName = ReportDefinitions.GetConfigFilename(directory, fileName);
            if (definition != null)
                if (updateReferenceLists)
                    foreach (ReportReferenceList list in definition.ReferenceLists)
                        list.Update(directory);
            return definition;
        }

        public static void SaveFullConfig(ReportDefinitions definition, String directory, String fileName)
        {
            DateTime curConfigDateTime = File.GetLastWriteTime(ReportDefinitions.GetConfigFilename(directory, fileName));

            if (curConfigDateTime.Equals(definition.ConfigDateTime))
            {
                Extensions.Save<ReportDefinitions>(definition, ReportDefinitions.GetConfigFilename(directory, fileName));
                foreach (ReportReferenceList list in definition.ReferenceLists)
                    list.Save(directory);
            }
            definition.ConfigDateTime = File.GetLastWriteTime(ReportDefinitions.GetConfigFilename(directory, fileName));
            definition.ConfigFileName = ReportDefinitions.GetConfigFilename(directory, fileName);
        }

        public ReportDefinitions()
        {
            this.Templates = new BindingList<ReportTemplate>();
            this.Policies = new BindingList<ReportPolicy>();
            this.ReferenceLists = new BindingList<ReportReferenceList>();
        }

        [XmlIgnore]
        public DateTime ConfigDateTime { get; set; }
        [XmlIgnore]
        public String ConfigFileName { get; set; }

        //private String m_currentOperation;
        //[XmlIgnore]
        //public String CurrentOperation 
        //{
        //    get
        //    {
        //        //if (String.IsNullOrWhiteSpace(m_currentOperation))
        //        //{
        //        //    m_currentOperation = "PDO";
        //        //    if (this.ConfigFileName.ToLower().Contains("per10dc01"))
        //        //        m_currentOperation = "WOD";
        //        //}

        //        return m_currentOperation;
        //    }
        //    set
        //    {
        //        m_currentOperation = value;
        //    }
        //}
        public BindingList<ReportTemplate> Templates { get; set; }
        public BindingList<ReportPolicy> Policies { get; set; }
        public BindingList<ReportReferenceList> ReferenceLists { get; set; }
    }
}
