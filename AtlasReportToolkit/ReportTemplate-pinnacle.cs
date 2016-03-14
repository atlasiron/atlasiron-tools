using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;
using System.Text;
using USP.Express.Pro;

namespace AtlasReportToolkit
{
    [Serializable]
    public class ReportTemplate
    {
        public ReportTemplate()
        {
            this.Identifier = Guid.NewGuid();
            this.Header = new BindingList<ReportTemplateColumn>();
            this.Rows = new BindingList<ReportTemplateColumn>();
            this.ShowTotals = true;
            this.AllowFilter = true;
        }

        public override string ToString()
        {
            return this.Name;
        }

        public void AssignPolicyDefaults(ReportPolicy policy)
        {
            foreach (ReportPolicyColumnDefault defaultValue in policy.HeaderDefaults)
            {
                ReportTemplateColumn column = (from a in this.Header where a.Name.Equals(defaultValue.ColumnName) select a).FirstOrDefault();
                if (column == null)
                    continue;

                column.DefaultValue = defaultValue.DefaultValue;
            }

            foreach (ReportPolicyColumnDefault defaultValue in policy.RowDefaults)
            {
                ReportTemplateColumn column = (from a in this.Rows where a.Name.Equals(defaultValue.ColumnName) select a).FirstOrDefault();
                if (column == null)
                    continue;

                column.DefaultValue = defaultValue.DefaultValue;
            }
        }

        public void UpdateReferenceLists(String directory, DateTime? filterDate=null, Boolean ignoreConfigurableLists=false)
        {
            foreach (ReportTemplateColumn column in this.Header)
            {
                if (ignoreConfigurableLists && column.ConfigureReferenceListOnCreateReport)
                    continue;

                if (column.ReferenceList != null)
                    column.ReferenceList.Update(directory, filterDate);
            }

            foreach (ReportTemplateColumn column in this.Rows)
            {
                if (ignoreConfigurableLists && column.ConfigureReferenceListOnCreateReport)
                    continue;

                if (column.ReferenceList != null)
                    column.ReferenceList.Update(directory, filterDate);
            }
        }

        public void FillReferenceLists(ReportPolicy policy, ReportDefinitions definitions)
        {
            foreach (ReportTemplateColumn column in this.Header)
            {
                if (String.IsNullOrWhiteSpace(column.ReferenceListName))
                    continue;

                String referenceListName = column.ReferenceListName.Replace(EditReportSheetCtrl.ReportOperationField, policy.Operation);
                foreach(ReportPolicyColumnDefault columnDefault in policy.HeaderDefaults)
                    referenceListName = referenceListName.Replace(EditReportSheetCtrl.GetColumndDefaultField(columnDefault.ColumnName), columnDefault.DefaultValue);

                column.ReferenceList = (from a in definitions.ReferenceLists where a.Name.Equals(referenceListName) select a).FirstOrDefault();
                if (column.ReferenceList != null)
                {
                    column.ReferenceList = Extensions.BinaryClone<ReportReferenceList>(column.ReferenceList);
                }
            }

            foreach (ReportTemplateColumn column in this.Rows)
            {
                if (String.IsNullOrWhiteSpace(column.ReferenceListName))
                    continue;

                String referenceListName = column.ReferenceListName.Replace(EditReportSheetCtrl.ReportOperationField, policy.Operation);
                foreach (ReportPolicyColumnDefault columnDefault in policy.HeaderDefaults)
                    referenceListName = referenceListName.Replace(EditReportSheetCtrl.GetColumndDefaultField(columnDefault.ColumnName), columnDefault.DefaultValue);

                column.ReferenceList = (from a in definitions.ReferenceLists where a.Name.Equals(referenceListName) select a).FirstOrDefault();
                if (column.ReferenceList != null)
                    column.ReferenceList = Extensions.BinaryClone<ReportReferenceList>(column.ReferenceList);
            }
        }

        public List<ReportReferenceList> GetReferenceLists(String operation, ReportDefinitions definitions, Boolean onlyConfigurableOnCreate=false)
        {
            List<ReportReferenceList> lists = new List<ReportReferenceList>();
            foreach (ReportTemplateColumn column in this.Header)
            {
                if (String.IsNullOrWhiteSpace(column.ReferenceListName) || column.ReferenceList == null)
                    continue;

                if (onlyConfigurableOnCreate && !column.ConfigureReferenceListOnCreateReport)
                    continue;

                lists.Add(column.ReferenceList);
            }

            foreach (ReportTemplateColumn column in this.Rows)
            {
                if (String.IsNullOrWhiteSpace(column.ReferenceListName) || column.ReferenceList == null)
                    continue;

                if (onlyConfigurableOnCreate && !column.ConfigureReferenceListOnCreateReport)
                    continue;

                lists.Add(column.ReferenceList);
            }
            return lists;
        }


        [XmlIgnore]
        public String FileName { get; set; }

        public Guid Identifier { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public Boolean AllowAddNew { get; set; }
        public Boolean AllowDelete { get; set; }
        public Boolean AllowFilter { get; set; }
        public Boolean ShowSubTotals { get; set; }
        public Boolean ShowTotals { get; set; }
        public Boolean AllowInsertFromPaste { get; set; }
        public String AURPublishTarget { get; set; }
        public BindingList<ReportTemplateColumn> Header { get; set; }
        public BindingList<ReportTemplateColumn> Rows { get; set; }
    }
}
