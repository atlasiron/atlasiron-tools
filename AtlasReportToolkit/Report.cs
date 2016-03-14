using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using USP.Express.Pro;

namespace AtlasReportToolkit
{
    [Serializable]
    public enum ReportStatus { Created, Requested, PendingApproval, Approved };

    [Serializable]
    public class Report
    {
        public const String AtlasReportIdPrefix = "identifier";
        public const String AtlasReportSampleResultPrefix = "sample";
        public const String AtlasReportPrefix = "Atlas";
        public static BindingList<Report> LoadReports(String reportsDirectory)
        {
            BindingList<Report> reports = new BindingList<Report>();
            try
            {
                if (!Directory.Exists(reportsDirectory))
                    return reports;

                foreach (String fileName in Directory.EnumerateFiles(reportsDirectory, "*.xml", SearchOption.TopDirectoryOnly))
                {
                    try
                    {
                        Report report = Report.LoadReport(fileName);
                        if (report != null)
                            reports.Add(report);
                    }
                    catch (System.Exception exc)
                    {
                        MessageBox.Show("Error loading report : " + fileName + " [" + exc.Message + "]");
                    }
                }
            }
            catch (System.Exception exc)
            {
            }
            return reports;
        }

        public static Report LoadReport(String reportFileName)
        {
            Report report = new Report();
            try
            {
                if (!File.Exists(reportFileName))
                    return null;

                try
                {
                    report = Extensions.Load<Report>(reportFileName);
                    if (report != null)
                    {
                        foreach (ReportSheet sheet in report.Sheets)
                        {
                            sheet.Report = report;
                            sheet.Header.Sheet = sheet;
                            foreach (ReportRow row in sheet.Rows)
                                row.Sheet = sheet;
                        }

                        report.CurrentFileName = reportFileName;
                    }
                }
                catch (System.Exception exc)
                {
                }
            }
            catch (System.Exception exc)
            {
            }
            return report;
        }

        public List<ReportReferenceList> GetAllReferenceLists(String operation, ReportDefinitions definitions, Boolean onlyConfigurableOnCreate = false)
        {
            List<ReportReferenceList> lists = new List<ReportReferenceList>();
            foreach (ReportSheet sheet in this.Sheets)
                lists.AddRange(sheet.Template.GetReferenceLists(operation,definitions,onlyConfigurableOnCreate));

            return lists;
        }

        public static void UpdateLinkedReferenceLists(List<ReportReferenceList> lists)
        {
            foreach (ReportReferenceList linkedList in lists)
            {
                if (String.IsNullOrWhiteSpace(linkedList.LinkedListName) || String.IsNullOrWhiteSpace(linkedList.LinkedListCalculation))
                    continue;

                linkedList.Items.Clear();
                ReportReferenceList list = (from a in lists where a.Name.Equals(linkedList.LinkedListName) select a).FirstOrDefault();
                if (list == null)
                    continue;

                try
                {
                    Parser parser = new Parser();
                    parser.Variables.Add(new Variable("{Value}", typeof(String)));
                    parser.Variables.Add(new Variable("{Description}", typeof(String)));
                    parser.Variables.Add(new Variable("{Activated}", typeof(Boolean)));
                    ExpressionTree tree = parser.Parse(linkedList.LinkedListCalculation);

                    foreach (ReportReferenceListValue item in list.Items)
                    {
                        Object[] values = new object[3] { item.Value, item.Description, item.Activated };
                        Object result = tree.Evaluate(values);
                        if (result != null && result != DBNull.Value && !String.IsNullOrWhiteSpace(result.ToString()))
                        {
                            if (!(from a in linkedList.Items where a.Value.Equals(result.ToString()) select true).FirstOrDefault())
                                linkedList.Items.Add(new ReportReferenceListValue { Value = result.ToString(), Description = result.ToString(), Activated = true });
                        }
                    }
                }
                catch (System.Exception exc)
                {
                }
            }
        }

        public static void PublishReportCSV(Report report, ReportPolicy policy, String publishDirectory)
        {
            StreamWriter writer = null;
            try
            {
                String fileName = publishDirectory + "\\" + report.Name + ".csv";
                writer = new System.IO.StreamWriter(fileName, false);

                writer.WriteLine("REPORT");
                writer.WriteLine("Title,\"" + report.Name ?? "?" + "\"");
                writer.WriteLine("Contractor,\"" + policy.Contractor ?? "?" + "\"");
                writer.WriteLine("Date,\"" + report.ReportingPeriod.ToString() ?? "?" + "\"");
                writer.WriteLine("Policy,\"" + policy.Name ?? "?" + "\"");
                writer.WriteLine("Status,\"" + report.Status.ToString() ?? "?" + "\"");
                writer.WriteLine("");
                foreach (ReportSheet sheet in report.Sheets)
                {
                    writer.WriteLine("HEADER");
                    for (int i = 0; i < sheet.Template.Header.Count; i++)
                        writer.WriteLine("\"" + sheet.Template.Header[i].Name + "\",\"" + (sheet.Header.Values.ContainsKey(sheet.Template.Header[i].Name) ? sheet.Header.Values[sheet.Template.Header[i].Name] ?? "?" : "") + "\"");

                    writer.WriteLine("");
                    writer.WriteLine("DETAILS");
                    StringBuilder line = new StringBuilder();
                    for (int i = 0; i < sheet.Template.Rows.Count; i++)
                        line.Append("\"" + sheet.Template.Rows[i].Name + "\",");
                    writer.WriteLine(line);

                    for (int i = 0; i < sheet.Rows.Count; i++)
                    {
                        line = new StringBuilder();
                        for (int j = 0; j < sheet.Template.Rows.Count; j++)
                            line.Append("\"" + (sheet.Rows[i].Values.ContainsKey(sheet.Template.Rows[j].Name) ? sheet.Rows[i].Values[sheet.Template.Rows[j].Name] ?? "?" : "") + "\",");
                        writer.WriteLine(line);
                    }
                }
            }
            catch (System.Exception exc)
            {
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        public static void PublishReportXML(Report report, String fileName)
        {
            try
            {
                XMLReport xmlReport = new XMLReport(report);
                Extensions.Save<XMLReport>(xmlReport, fileName);
            }
            catch (System.Exception exc)
            {
            }
            finally
            {
            }
        }

        public static void PublishReportAUR(Report report, List<AURTransferFile> files)
        {
            List<AURTransaction> transactions = new List<AURTransaction>();
            List<AURSampleResults> sampleResults = new List<AURSampleResults>();
            foreach (ReportSheet sheet in report.Sheets)
            {
                Parser parser = EditReportSheetCtrl.PrepareForValidation(sheet);

                foreach (ReportRow row in sheet.Rows)
                {
                    Object[] values = EditReportSheetCtrl.GetRowValues(parser, report, sheet, row, null);

                    AURTransaction t = new AURTransaction();
                    AURSampleResults s = new AURSampleResults();
                    List<ReportTemplateColumn> allColumns = new List<ReportTemplateColumn>(sheet.Template.Header);
                    allColumns.AddRange(sheet.Template.Rows);

                    foreach (ReportTemplateColumn column in allColumns)
                    {
                        if (String.IsNullOrWhiteSpace(column.AURColumn))
                            continue;

                        String aurCalc = column.AURValue;
                        if (String.IsNullOrWhiteSpace(aurCalc))
                            aurCalc = "{" + column.Name.Replace(" ", "_") + "}";

                        try
                        {
                            ExpressionTree expression = parser.Parse(aurCalc);
                            Object result = expression.Evaluate(values);
                            if (result != null && result != DBNull.Value)
                            {
                                if (column.AURColumn.StartsWith(AtlasReportSampleResultPrefix))
                                {
                                    s.Assays.Add(column.AURColumn.Substring(AtlasReportSampleResultPrefix.Length), result.ToString());
                                }
                                else
                                {
                                    s[column.AURColumn] = result;
                                    t[column.AURColumn] = result;
                                }
                            }
                        }
                        catch (System.Exception exc)
                        {
                        }
                    }
                    transactions.Add(t);
                    if (!String.IsNullOrWhiteSpace(s.SampleId))
                        sampleResults.Add(s);
                }
            }

            AURTransaction.Save(files[0].AURFileName, transactions);
            if (sampleResults.Count > 0)
                AURSampleResults.Save(files[1].AURFileName, sampleResults);
            else
                files.RemoveAt(1);
        }

        public static void PublishReportSQL(Report report, ReportPolicy policy)
        {
            foreach (ReportSheet sheet in report.Sheets)
            {
                Parser parser = EditReportSheetCtrl.PrepareForValidation(sheet);

                List<Dictionary<String, Object>> records = new List<Dictionary<string, object>>();
                string keyField = null;
                object keyId = null;
                List<ReportTemplateColumn> allColumns = new List<ReportTemplateColumn>(sheet.Template.Header);
                allColumns.AddRange(sheet.Template.Rows);
                foreach (ReportTemplateColumn column in allColumns)
                    if (!String.IsNullOrWhiteSpace(column.AURColumn) && column.AURColumn.StartsWith(AtlasReportIdPrefix))
                        keyField = column.AURColumn.Substring(AtlasReportIdPrefix.Length);

                List<ReportRow> rows = sheet.Rows.ToList();
                if (rows.Count == 0)
                {
                    rows.Add(new ReportRow(sheet));
                }

                foreach (ReportRow row in rows)
                {
                    Object[] values = EditReportSheetCtrl.GetRowValues(parser, report, sheet, row, null);

                    Dictionary<String, Object> record = new Dictionary<string, object>();
                    foreach (ReportTemplateColumn column in allColumns)
                    {
                        if (String.IsNullOrWhiteSpace(column.AURColumn))
                            continue;

                        String aurCalc = column.AURValue;
                        if (String.IsNullOrWhiteSpace(aurCalc))
                            aurCalc = "{" + column.Name.Replace(" ", "_") + "}";

                        try
                        {
                            ExpressionTree expression = parser.Parse(aurCalc);
                            Object result = expression.Evaluate(values);
                            if (result != null && result != DBNull.Value)
                            {
                                if (column.AURColumn.StartsWith(AtlasReportIdPrefix))
                                    keyId = result;
                                else
                                    record[column.AURColumn] = result;
                            }
                        }
                        catch (System.Exception exc)
                        {
                        }
                    }

                    if (record.Keys.Count > 0)
                        records.Add(record);
                }

                if (String.IsNullOrWhiteSpace(keyField) || keyId == null)
                    return;

                try
                {
                    SqlConnection connection = new SqlConnection(policy.DBConnectionString);
                    connection.Open();
                    try
                    {
                        SqlCommand cmd = new SqlCommand("delete from " + sheet.Template.AURPublishTarget + " where " + keyField + " = @1", connection);
                        cmd.Parameters.AddWithValue("@1", keyId);
                        cmd.ExecuteNonQuery();

                        foreach (Dictionary<string, object> record in records)
                        {
                            StringBuilder sql = new StringBuilder();
                            sql.Append("insert into " + sheet.Template.AURPublishTarget + " (" + keyField);
                            foreach (String key in record.Keys)
                                sql.Append( "," + key);
                            sql.Append(") values (@" + keyField);
                            foreach (String key in record.Keys)
                                sql.Append(",@" + key);
                            sql.Append(")");

                            cmd = new SqlCommand(sql.ToString(), connection);
                            cmd.Parameters.AddWithValue("@" + keyField, keyId);
                            foreach (String key in record.Keys)
                                cmd.Parameters.AddWithValue("@" + key,(record[key] == null || record[key] == DBNull.Value) ? DBNull.Value : record[key]);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch(SystemException e)
                    {
                        SqlCommand cmd = new SqlCommand("delete from " + sheet.Template.AURPublishTarget + " where " + keyField + " = @1", connection);
                        cmd.Parameters.AddWithValue("@1", keyId);
                        cmd.ExecuteNonQuery();
                    }
                    connection.Close();
                }
                catch(System.Exception exc)
                {
                }
            }
        }

        public Report()
        {
            this.Sheets = new List<ReportSheet>();
            this.Status = ReportStatus.Created;
            this.GodMode = false;

            this.RelativeApprovedDirectory = "Approved";
            this.RelativePendingDirectory = "Pending";
            this.RelativeArchiveDirectory = "Archive";
        }

        public Report(ReportPolicy policy, List<ReportTemplate> templates)
        {
            this.Sheets = new List<ReportSheet>();
            foreach (ReportTemplate template in templates)
            {
                ReportSheet sheet = new ReportSheet(this, template);
                sheet.Header = new ReportHeader(sheet);
                sheet.Rows = new TypedLibBindingList<ReportRow>();
                this.Sheets.Add(sheet);
            }
            this.Status = ReportStatus.Created;
            this.GodMode = false;

            this.RelativeApprovedDirectory = policy.RelativeApprovedDirectory;
            this.RelativePendingDirectory = policy.RelativePendingDirectory;
            this.RelativeArchiveDirectory = policy.RelativeArchiveDirectory;
            this.ReportPolicyId = policy.Identifier;
            this.SendToEmailList = policy.SendToEmailList;
            this.SendCCEmailList = policy.SendCCEmailList;
            this.ReplyToEmailList = policy.ReplyToEmailList;
        }

        public override string ToString()
        {
            return this.Name;
        }

        public void Save(String fileName, Boolean forceSave=false)
        {
            if (this.Modified || forceSave)
                Extensions.Save<Report>(this, fileName);
        }

        public void Save(String reportsDirectory)
        {
            String fileName = this.GetCurrentFileName(reportsDirectory);
            Extensions.Save<Report>(this, fileName);
        }


        [XmlIgnore]
        public Boolean GodMode { get; set; }

        [XmlIgnore]
        public Boolean AllowCopyAndPaste
        {
            get
            {
                return (this.GodMode || this.Status == ReportStatus.Approved);
            }
        }
        [XmlIgnore]
        public String CurrentFileName { get; set; }

        private Boolean m_Modified = false;
        [XmlIgnore]
        public Boolean Modified
        {
            get
            {
                return m_Modified;
            }
            set
            {
                m_Modified = value;
                if (m_Modified)
                    this.ModifiedAt = DateTime.Now;
            }
        }

        public DateTime? ModifiedAt { get; set; }
        public String RelativePendingDirectory { get; set; }
        public String RelativeApprovedDirectory { get; set; }
        public String RelativeArchiveDirectory { get; set; }

        public String SendToEmailList { get; set; }
        public String SendCCEmailList { get; set; }
        public String ReplyToEmailList { get; set; }

        public String Name { get; set; }

        public String Operation { get; set; }
        public Guid ReportPolicyId { get; set; }

        private DateTime m_reportingPeriod = DateTime.Now;
        public DateTime ReportingPeriod
        {
            get
            {
                return m_reportingPeriod;
            }
            set
            {
                m_reportingPeriod = new DateTime(value.Year, value.Month, value.Day, 6, 0, 0);
            }
        }

        public ReportStatus Status { get; set; }

        public List<ReportSheet> Sheets { get; set; }

        public String GetPendingFileName(String reportsDirectory) 
        {
            return reportsDirectory + (reportsDirectory.EndsWith("\\") ? "" : "\\") + RelativePendingDirectory + (RelativePendingDirectory.EndsWith("\\") ? "" : "\\") + AtlasReportPrefix + "_" + this.Name + "_Pending.xml";
        }

        public String GetApprovedFileName(String reportsDirectory)
        {
            return reportsDirectory + (reportsDirectory.EndsWith("\\") ? "" : "\\") + RelativeApprovedDirectory + (RelativePendingDirectory.EndsWith("\\") ? "" : "\\") + AtlasReportPrefix + "_" + this.Name + "_Approved.xml";
        }

        public String GetArchiveFileName(String reportsDirectory)
        {
            return reportsDirectory + (reportsDirectory.EndsWith("\\") ? "" : "\\") + RelativeArchiveDirectory + (RelativePendingDirectory.EndsWith("\\") ? "" : "\\") + AtlasReportPrefix + "_" + this.Name + "_Approved.xml";
        }

        public String GetCurrentFileName(String reportsDirectory)
        {
            if (this.Status == ReportStatus.Created)
                return this.GetPendingFileName(reportsDirectory);
            if (this.Status == ReportStatus.PendingApproval || this.Status == ReportStatus.Requested)
                return this.GetPendingFileName(reportsDirectory);
            if (this.Status == ReportStatus.Approved)
                return this.GetApprovedFileName(reportsDirectory);
            return "";
        }

        public Boolean CheckExists(String reportsDirectory)
        {
            if (File.Exists(GetPendingFileName(reportsDirectory)))
                return true;
            if (File.Exists(GetApprovedFileName(reportsDirectory)))
                return true;
            if (File.Exists(GetArchiveFileName(reportsDirectory)))
                return true;
            return false;
        }
    }
}
