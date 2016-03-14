using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Threading;
using GemBox.Spreadsheet;
using AtlasSampleToolkit;

namespace AtlasSampleManager
{
    public partial class ImportSizingsDlg : Form
    {
        enum SizingsType {  OldFinesFormat, FinesFormat, LumpFormat };
        private class ErrorMessage
        {
            public ErrorMessage(string message)
            {
                this.Message = message;
            }
            public String Message { get; set; }
        }

        public SqlConnection Connection { get; set; }
        private BindingList<ErrorMessage> Errors = new BindingList<ErrorMessage>();
        public String ResultsDirectory { get; set; }
        public String AcceptedResultsDirectory { get; set; }
        public Submission.SubmissionSampleType SubmissionType { get; set; }

        private String SampleTableName
        {
            get
            {
                if (this.SubmissionType == AtlasSampleToolkit.Submission.SubmissionSampleType.Blastholes)
                    return "gcv_Hole_Collars";
                else
                    return "psm_Product_Samples";
            }
        }

        public ImportSizingsDlg()
        {
            InitializeComponent();
        }

        private void ImportAssaysDlg_Load(object sender, EventArgs e)
        {
            UpdateResultsList();

            gridErrors.SetDataBinding(this.Errors, null);
        }

        private void UpdateResultsList()
        {
            List<String> files = new List<string>();
            try
            {
                if (Directory.Exists(this.ResultsDirectory))
                    files = Directory.EnumerateFiles(this.ResultsDirectory).ToList();
            }
            catch (System.Exception exc)
            {
            }
            lstResults.Items.Clear();
            foreach (String fileName in files.OrderBy(a=>a).Reverse())
                if (fileName.EndsWith(".xls") && (fileName.Contains("Sizing") || fileName.Contains("Moisture")))
                    lstResults.Items.Add(Path.GetFileNameWithoutExtension(fileName));
        }

        private void butImport_Click(object sender, EventArgs e)
        {
            this.Errors.Clear();
            if (String.IsNullOrWhiteSpace(lstResults.Text))
            {
                this.Errors.Add(new ErrorMessage("Select a results file to import"));
                return;
            }

            String resultsFile = this.ResultsDirectory + "\\" + lstResults.Text + ".xls";
            String acceptedFile = this.AcceptedResultsDirectory + "\\" + lstResults.Text + ".xls";
            ExcelFile excelFile = new ExcelFile();
            try
            {
                excelFile.LoadXls(resultsFile);
            }
            catch (System.Exception exc)
            {
                MessageBox.Show("There is a problem opening the results file.", "Error", MessageBoxButtons.OK);
                return;
            }
            if (excelFile.Worksheets.Count == 0)
            {
                this.Errors.Add(new ErrorMessage("The results file does not have any worksheets."));
                return;
            }
            ExcelWorksheet worksheet = excelFile.Worksheets[0];
            if (worksheet == null)
            {
                this.Errors.Add(new ErrorMessage("There is no <results> worksheet in the results file."));
                return;
            }
            //if (worksheet.Cells[3,5].Value == null || !String.Equals(worksheet.Cells[3,5].Value.ToString(), submission))
            //{
            //    this.Errors.Add(new ErrorMessage("The results file's first worksheet does not have the name of the submission (" + submission + ")."));
            //    return;
            //}
            String labJobNo = "", submission = "";
            DateTime reportDate = DateTime.Now;
            SizingsType formatType = SizingsType.FinesFormat;
            Boolean hasMoisture = false;
            if (CheckCellContents(worksheet, 4, 3, "INTERTEK JOB NUMBER :", false))
            {
                formatType = SizingsType.OldFinesFormat;

                labJobNo = (worksheet.Cells[4, 5].Value != null ? worksheet.Cells[4, 5].Value.ToString() : null);

                if (!CheckCellContents(worksheet, 3, 3, "SUBMISSION NUMBER :"))
                    return;
                submission = (worksheet.Cells[3, 5].Value != null ? worksheet.Cells[3, 5].Value.ToString() : null);

                if (!CheckCellContents(worksheet, 2, 3, "DATE REPORTED :"))
                    return;

                if (!CheckCellContents(worksheet, 6, 1, "Total Mass"))
                    return;
                if (CheckCellContents(worksheet, 6, 2, ">12.5 mm",false))
                {
                    formatType = SizingsType.OldFinesFormat;
                    if (!CheckCellContents(worksheet, 6, 2, ">12.5 mm"))
                        return;
                    if (!CheckCellContents(worksheet, 6, 3, ">10 - 12.5mm"))
                        return;
                    if (!CheckCellContents(worksheet, 6, 4, ">9.5- 10mm"))
                        return;
                    if (!CheckCellContents(worksheet, 6, 5, ">6.7 -9.5mm"))
                        return;
                    if (!CheckCellContents(worksheet, 6, 6, ">0.5 - 6.7mm"))
                        return;
                    if (!CheckCellContents(worksheet, 6, 7, "<0.5 mm"))
                        return;
                    if (!CheckCellContents(worksheet, 6, 8, "Total"))
                        return;
                    hasMoisture = CheckCellContents(worksheet, 6, 9, "Moisture",false);
                    if (!CheckCellContents(worksheet, 6, 0, "Sample No."))
                        return;

                }
                else
                {
                    formatType = SizingsType.LumpFormat;
                    if (!CheckCellContents(worksheet, 6, 2, "+40 mm"))
                        return;
                    if (!CheckCellContents(worksheet, 6, 3, "+31.5 - 40 mm"))
                        return;
                    if (!CheckCellContents(worksheet, 6, 4, "+12.5 - 31.5 mm"))
                        return;
                    if (!CheckCellContents(worksheet, 6, 5, "+6.3 - 40 mm"))
                        return;
                    if (!CheckCellContents(worksheet, 6, 6, "-6.3 mm"))
                        return;
                    if (!CheckCellContents(worksheet, 6, 7, "Total"))
                        return;
                    hasMoisture = CheckCellContents(worksheet, 6, 8, "Moisture", false);
                    if (!CheckCellContents(worksheet, 6, 0, "Sample No."))
                        return;
                }

                String reportDateText = (worksheet.Cells[2, 5].Value != null ? worksheet.Cells[2, 5].Value.ToString() : null);
                reportDate = DateTime.Now;
                try
                {
                    reportDate = DateTime.Parse(reportDateText);
                }
                catch (System.Exception exc)
                {
                    try
                    {
                        double reportDateInt = Double.Parse(reportDateText);
                        reportDate = new DateTime(1900, 1, 1);
                        reportDate = reportDate.AddDays(reportDateInt - 2);
                    }
                    catch (System.Exception exc2)
                    {
                        MessageBox.Show("The report date [" + reportDateText + "] is causing an error.", "Error", MessageBoxButtons.OK);
                        return;
                    }
                }
            }
            else if (CheckCellContents(worksheet, 6, 4, "INTERTEK JOB NUMBER :"))
            {
                formatType = SizingsType.OldFinesFormat; ;

                labJobNo = (worksheet.Cells[6, 7].Value != null ? worksheet.Cells[6, 7].Value.ToString() : null);

                if (!CheckCellContents(worksheet, 5, 4, "SUBMISSION NUMBER :"))
                    return;
                submission = (worksheet.Cells[5, 7].Value != null ? worksheet.Cells[5, 7].Value.ToString() : null);

                if (!CheckCellContents(worksheet, 4, 4, "DATE REPORTED :"))
                    return;

                if (!CheckCellContents(worksheet, 8, 1, "Total Mass"))
                    return;
                if (!CheckCellContents(worksheet, 8, 2, ">12.5mm"))
                    return;
                if (!CheckCellContents(worksheet, 8, 3, ">10-12.5mm"))
                    return;
                if (!CheckCellContents(worksheet, 8, 4, ">9.5-10mm"))
                    return;
                if (!CheckCellContents(worksheet, 8, 5, ">6.7-9.5mm"))
                    return;
                if (!CheckCellContents(worksheet, 8, 6, ">0.5-6.7mm"))
                    return;
                if (!CheckCellContents(worksheet, 8, 7, "<0.5mm"))
                    return;
                hasMoisture = CheckCellContents(worksheet, 8, 8, "Moisture",false);
                if (!CheckCellContents(worksheet, 13, 0, "SAMPLE NUMBERS"))
                    return;

                String reportDateText = (worksheet.Cells[4, 7].Value != null ? worksheet.Cells[4, 7].Value.ToString() : null);
                reportDate = DateTime.Now;
                try
                {
                    reportDate = DateTime.Parse(reportDateText);
                }
                catch (System.Exception exc)
                {
                    try
                    {
                        double reportDateInt = Double.Parse(reportDateText);
                        reportDate = new DateTime(1900, 1, 1);
                        reportDate = reportDate.AddDays(reportDateInt - 2);
                    }
                    catch (System.Exception exc2)
                    {
                        MessageBox.Show("The report date [" + reportDateText + "] is causing an error.", "Error", MessageBoxButtons.OK);
                        return;
                    }
                }
            }
            else
                return;

            double d;
            int imported = 0, total = 0;
            bool ok = true;
            int startRow = 0;
            if (formatType == SizingsType.OldFinesFormat)
                startRow = 8;
            if (formatType == SizingsType.FinesFormat)
                startRow = 14;
            if (formatType == SizingsType.LumpFormat)
                startRow = 8;

            for (int row = startRow; row < worksheet.Rows.Count; row++)
            {
                if (formatType == SizingsType.OldFinesFormat || formatType == SizingsType.FinesFormat)
                {
                    String sampleId = (worksheet.Cells[row, 0].Value != null ? worksheet.Cells[row, 0].Value.ToString() : null);
                    Double? TotalMass = (worksheet.Cells[row, 1].Value != null && Double.TryParse(worksheet.Cells[row, 1].Value.ToString(), out d) ? Double.Parse(worksheet.Cells[row, 1].Value.ToString()) : (double?)null);
                    Double? Size12o5mm = (worksheet.Cells[row, 2].Value != null && Double.TryParse(worksheet.Cells[row, 2].Value.ToString(), out d) ? Double.Parse(worksheet.Cells[row, 2].Value.ToString()) : (double?)null);
                    Double? Size10o0_12o5mm = (worksheet.Cells[row, 3].Value != null && Double.TryParse(worksheet.Cells[row, 3].Value.ToString(), out d) ? Double.Parse(worksheet.Cells[row, 3].Value.ToString()) : (double?)null);
                    Double? Size9o5_10o0mm = (worksheet.Cells[row, 4].Value != null && Double.TryParse(worksheet.Cells[row, 4].Value.ToString(), out d) ? Double.Parse(worksheet.Cells[row, 4].Value.ToString()) : (double?)null);
                    Double? Size6o7_9o5mm = (worksheet.Cells[row, 5].Value != null && Double.TryParse(worksheet.Cells[row, 5].Value.ToString(), out d) ? Double.Parse(worksheet.Cells[row, 5].Value.ToString()) : (double?)null);
                    Double? Size0o5_6o7mm = (worksheet.Cells[row, 6].Value != null && Double.TryParse(worksheet.Cells[row, 6].Value.ToString(), out d) ? Double.Parse(worksheet.Cells[row, 6].Value.ToString()) : (double?)null);
                    Double? Size0o0_0o5mm = (worksheet.Cells[row, 7].Value != null && Double.TryParse(worksheet.Cells[row, 8].Value.ToString(), out d) ? Double.Parse(worksheet.Cells[row, 7].Value.ToString()) : (double?)null);
                    Double? Total = formatType == SizingsType.FinesFormat ? (worksheet.Cells[row, 8].Value != null && Double.TryParse(worksheet.Cells[row, 8].Value.ToString(), out d) ? Double.Parse(worksheet.Cells[row, 8].Value.ToString()) : (double?)null) : -1;
                    Double? Moisture = null;
                    if (hasMoisture)
                        Moisture = (worksheet.Cells[row, formatType == SizingsType.OldFinesFormat ? 9 : 8].Value != null && Double.TryParse(worksheet.Cells[row, formatType == SizingsType.OldFinesFormat ? 9 : 8].Value.ToString(), out d) ? Double.Parse(worksheet.Cells[row, formatType == SizingsType.OldFinesFormat ? 9 : 8].Value.ToString()) : (double?)null);

                    if (String.IsNullOrWhiteSpace(sampleId) && TotalMass == null && Size12o5mm == null && Size10o0_12o5mm == null && Size9o5_10o0mm == null && Size6o7_9o5mm == null && Size0o5_6o7mm == null && Size0o0_0o5mm == null && (Moisture == null && hasMoisture))
                        continue;

                    total++;

                    if (!InSubmission(submission, sampleId))
                    {
                        this.Errors.Add(new ErrorMessage("A sample (" + sampleId + ") was found that is not in submission (" + submission + ")."));
                        ok = false;
                        continue;
                    }
                    bool import = true;
                    if (TotalMass == null)
                    {
                        this.Errors.Add(new ErrorMessage("Sample (" + sampleId + ") does not have a [Total Mass]."));
                        ok = false;
                        import = false;
                    }
                    if (Size12o5mm == null)
                    {
                        this.Errors.Add(new ErrorMessage("Sample (" + sampleId + ") does not have a [>12.5 mm] sizing."));
                        ok = false;
                        import = false;
                    }
                    if (Size10o0_12o5mm == null)
                    {
                        this.Errors.Add(new ErrorMessage("Sample (" + sampleId + ") does not have a [>10 - 12.5mm] sizing."));
                        ok = false;
                        import = false;
                    }
                    if (Size9o5_10o0mm == null)
                    {
                        this.Errors.Add(new ErrorMessage("Sample (" + sampleId + ") does not have a [>9.5- 10mm] sizing."));
                        ok = false;
                        import = false;
                    }
                    if (Size6o7_9o5mm == null)
                    {
                        this.Errors.Add(new ErrorMessage("Sample (" + sampleId + ") does not have a [>6.7 -9.5mm] sizing."));
                        ok = false;
                        import = false;
                    }
                    if (Size0o5_6o7mm == null)
                    {
                        this.Errors.Add(new ErrorMessage("Sample (" + sampleId + ") does not have a [>0.5 - 6.7mm] sizing."));
                        ok = false;
                        import = false;
                    }
                    if (Size0o0_0o5mm == null)
                    {
                        this.Errors.Add(new ErrorMessage("Sample (" + sampleId + ") does not have a [<0.5 mm] sizing."));
                        ok = false;
                        import = false;
                    }
                    if (Total == null)
                    {
                        this.Errors.Add(new ErrorMessage("Sample (" + sampleId + ") does not have a [Total%] sizing."));
                        ok = false;
                        import = false;
                    }
                    if (Moisture == null && hasMoisture)
                    {
                        this.Errors.Add(new ErrorMessage("Sample (" + sampleId + ") does not have a [Moisture]."));
                        ok = false;
                        import = false;
                    }
                    if (!import)
                    {
                        this.Errors.Add(new ErrorMessage("IMPORTANT: Sample (" + sampleId + ") has not been imported."));
                        ok = false; 
                        return;
                    }
                    try
                    {
                        UpdateFinesResults(submission, sampleId, TotalMass,Size12o5mm,Size10o0_12o5mm,Size9o5_10o0mm,Size6o7_9o5mm,Size0o5_6o7mm,Size0o0_0o5mm,Total,Moisture, labJobNo, reportDate);
                        imported++;
                    }
                    catch (System.Exception exc)
                    {
                        this.Errors.Add(new ErrorMessage("Database error occurred when updating sample (" + sampleId + ") results."));
                        ok = false;
                    }
                }
                else if (formatType == SizingsType.LumpFormat)
                {
                    String sampleId = (worksheet.Cells[row, 0].Value != null ? worksheet.Cells[row, 0].Value.ToString() : null);
                    Double? TotalMass = (worksheet.Cells[row, 1].Value != null && Double.TryParse(worksheet.Cells[row, 1].Value.ToString(), out d) ? Double.Parse(worksheet.Cells[row, 1].Value.ToString()) : (double?)null);
                    Double? Size40mm = (worksheet.Cells[row, 2].Value != null && Double.TryParse(worksheet.Cells[row, 2].Value.ToString(), out d) ? Double.Parse(worksheet.Cells[row, 2].Value.ToString()) : (double?)null);
                    Double? Size31o5_40mm = (worksheet.Cells[row, 3].Value != null && Double.TryParse(worksheet.Cells[row, 3].Value.ToString(), out d) ? Double.Parse(worksheet.Cells[row, 3].Value.ToString()) : (double?)null);
                    Double? Size1205_31o5mm = (worksheet.Cells[row, 4].Value != null && Double.TryParse(worksheet.Cells[row, 4].Value.ToString(), out d) ? Double.Parse(worksheet.Cells[row, 4].Value.ToString()) : (double?)null);
                    Double? Size6o3_40mm = (worksheet.Cells[row, 5].Value != null && Double.TryParse(worksheet.Cells[row, 5].Value.ToString(), out d) ? Double.Parse(worksheet.Cells[row, 5].Value.ToString()) : (double?)null);
                    Double? Size6o3mm = (worksheet.Cells[row, 6].Value != null && Double.TryParse(worksheet.Cells[row, 6].Value.ToString(), out d) ? Double.Parse(worksheet.Cells[row, 6].Value.ToString()) : (double?)null);
                    Double? Total = (worksheet.Cells[row, 7].Value != null && Double.TryParse(worksheet.Cells[row, 7].Value.ToString(), out d) ? Double.Parse(worksheet.Cells[row, 7].Value.ToString()) : (double?)null);
                    Double? Moisture = null;
                    if (hasMoisture)
                        Moisture = (worksheet.Cells[row, 8].Value != null && Double.TryParse(worksheet.Cells[row, 8].Value.ToString(), out d) ? Double.Parse(worksheet.Cells[row, 8].Value.ToString()) : (double?)null);

                    if (String.IsNullOrWhiteSpace(sampleId) && TotalMass == null && Size40mm == null && Size31o5_40mm == null && Size1205_31o5mm == null && Size6o3_40mm == null && Size6o3mm == null && (Moisture == null && hasMoisture))
                        continue;

                    total++;

                    if (!InSubmission(submission, sampleId))
                    {
                        this.Errors.Add(new ErrorMessage("A sample (" + sampleId + ") was found that is not in submission (" + submission + ")."));
                        ok = false;
                        continue;
                    }
                    bool import = true;
                    if (TotalMass == null)
                    {
                        this.Errors.Add(new ErrorMessage("Sample (" + sampleId + ") does not have a [Total Mass]."));
                        ok = false;
                        import = false;
                    }
                    if (Size40mm == null)
                    {
                        this.Errors.Add(new ErrorMessage("Sample (" + sampleId + ") does not have a [>40 mm] sizing."));
                        ok = false;
                        import = false;
                    }
                    if (Size6o3_40mm == null)
                    {
                        this.Errors.Add(new ErrorMessage("Sample (" + sampleId + ") does not have a [>6.3 - 40mm] sizing."));
                        ok = false;
                        import = false;
                    }
                    if (Size6o3mm == null)
                    {
                        this.Errors.Add(new ErrorMessage("Sample (" + sampleId + ") does not have a [>6.3mm] sizing."));
                        ok = false;
                        import = false;
                    }
                    if (Total == null)
                    {
                        this.Errors.Add(new ErrorMessage("Sample (" + sampleId + ") does not have a [Total%] sizing."));
                        ok = false;
                        import = false;
                    }
                    if (Moisture == null && hasMoisture)
                    {
                        this.Errors.Add(new ErrorMessage("Sample (" + sampleId + ") does not have a [Moisture]."));
                        ok = false;
                        import = false;
                    }
                    if (!import)
                    {
                        this.Errors.Add(new ErrorMessage("IMPORTANT: Sample (" + sampleId + ") has not been imported."));
                        ok = false;
                        return;
                    }
                    try
                    {
                        UpdateLumpResults(submission, sampleId, TotalMass, Size40mm, Size6o3_40mm, Size6o3mm, Total, Moisture, labJobNo, reportDate);
                        imported++;
                    }
                    catch (System.Exception exc)
                    {
                        this.Errors.Add(new ErrorMessage("Database error occurred when updating sample (" + sampleId + ") results."));
                        ok = false;
                    }
                }
            }
            //if (!CheckMissingResults(submission))
            //    ok = false;
            MessageBox.Show("Imported samples (" + imported.ToString() + ") of (" + total.ToString() + ") results.", "Summary", MessageBoxButtons.OK);
            if (!ok)
                MessageBox.Show("The results have errors that must be corrected and imported again.");
            else
            {
                try
                {
                    File.Delete(acceptedFile);
                    File.Move(resultsFile, acceptedFile);
                    UpdateResultsList();
                    MessageBox.Show("The results were fully imported.");
                    lstResults.Text = "";
                }
                catch (System.Exception exc)
                {
                    MessageBox.Show("An error occurred when moving the results into the AcceptedResults directory (" + exc.Message + ")", "Error", MessageBoxButtons.OK);
                }
            }
        }

        private bool CheckMissingResults(String submission)
        {
            bool ok = true;
            try
            {
                SqlCommand cmd = new SqlCommand("select SampleId from " + this.SampleTableName + " where Submission = @1 and Instructions like '%Sizings%' and (Fe is null or Al2O3 is null or SiO2 is null or P is null or S is null or Mn is null or LOI is null)", this.Connection);
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@1", Value = submission, SqlDbType = SqlDbType.NVarChar, Size = 80 });
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    String sampleId = reader.GetString(0);
                    this.Errors.Add(new ErrorMessage("IMPORTANT: Submission missing results for sample (" + sampleId + ") results."));
                    ok = false;
                }
            }
            catch (System.Exception exc)
            {
            }
            return ok;
        }

        private bool CheckCellContents(ExcelWorksheet worksheet, int row, int col, String text, bool displayError=true)
        {
            if (worksheet.Cells[row, col].Value == null || !String.Equals(worksheet.Cells[row, col].Value.ToString().Trim(), text.Trim()))
            {
                if (displayError)
                    this.Errors.Add(new ErrorMessage("The results file format does appear too match the submission. Cell (" + row.ToString() + "," + col.ToString() + ") doesn't contain \"" + text.Trim() + "\"."));
                return false;
            }
            return true;
        }

        private void UpdateFinesResults(String submission, String sampleId, double? TotalMass, double? Size12o5mm, double? Size10o0_12o5mm, double? Size9o5_10o0mm, double? Size6o7_9o5mm, double? Size0o5_6o7mm, double? Size0o0_0o5mm, double? Total, double? Moisture, String labJobNo, DateTime reportDate)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("update " + this.SampleTableName + " set [TotalMass] = @1, [Size12.5mm] = @2, [Size10.0_12.5mm] = @3, [Size9.5_10.0mm] = @4, [Size6.7_9.5mm] = @5, [Size0.5_6.7mm] = @6, [Size0.0_0.5mm] = @7, LabJobNo = @8, ReceivedOn = @10, ReceivedUser = SUSER_SNAME() " +
                    (this.SubmissionType == AtlasSampleToolkit.Submission.SubmissionSampleType.Product ? ", Published = 'NO' " : "") +
                    " where SampleId = @9 and Instructions like '%Sizings%'", this.Connection);
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@1", Value = TotalMass, SqlDbType = SqlDbType.Real });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@2", Value = Size12o5mm, SqlDbType = SqlDbType.Real });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@3", Value = Size10o0_12o5mm, SqlDbType = SqlDbType.Real });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@4", Value = Size9o5_10o0mm, SqlDbType = SqlDbType.Real });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@5", Value = Size6o7_9o5mm, SqlDbType = SqlDbType.Real });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@6", Value = Size0o5_6o7mm, SqlDbType = SqlDbType.Real });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@7", Value = Size0o0_0o5mm, SqlDbType = SqlDbType.Real });
                if (!String.IsNullOrWhiteSpace(labJobNo))
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@8", Value = labJobNo, SqlDbType = SqlDbType.NVarChar, Size = 80 });
                else
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@8", Value = DBNull.Value, SqlDbType = SqlDbType.NVarChar, Size = 80 });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@9", Value = sampleId, SqlDbType = SqlDbType.NVarChar, Size = 80 });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@10", Value = reportDate, SqlDbType = SqlDbType.DateTime });
                cmd.ExecuteNonQuery();
            }
            catch (System.Exception exc)
            {
            }
            try
            {
                SqlCommand cmd = new SqlCommand("update " + this.SampleTableName + " set [Moisture] = @1, LabJobNo = @2, ReceivedOn = @4, ReceivedUser = SUSER_SNAME() where SampleId = @3 and Instructions like '%Moisture%'", this.Connection);
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@1", Value = Moisture, SqlDbType = SqlDbType.Real });
                if (!String.IsNullOrWhiteSpace(labJobNo))
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@2", Value = labJobNo, SqlDbType = SqlDbType.NVarChar, Size = 80 });
                else
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@2", Value = DBNull.Value, SqlDbType = SqlDbType.NVarChar, Size = 80 });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@3", Value = sampleId, SqlDbType = SqlDbType.NVarChar, Size = 80 });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@4", Value = reportDate, SqlDbType = SqlDbType.DateTime });
                cmd.ExecuteNonQuery();
            }
            catch (System.Exception exc)
            {
            }
        }
        private void UpdateLumpResults(String submission, String sampleId, double? TotalMass, double? Size40mm, double? Size6o3_40mm, double? Size6o3mm,  double? Total, double? Moisture, String labJobNo, DateTime reportDate)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("update " + this.SampleTableName + " set [TotalMass] = @1, [Size40mm] = @2, [Size6.3_40mm] = @3, [Size0.0_6.3mm] = @4, LabJobNo = @5, ReceivedOn = @6, ReceivedUser = SUSER_SNAME() " +
                    (this.SubmissionType == AtlasSampleToolkit.Submission.SubmissionSampleType.Product ? ", Published = 'NO' " : "") +
                    " where SampleId = @7 and Instructions like '%Sizings%'", this.Connection);
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@1", Value = TotalMass, SqlDbType = SqlDbType.Real });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@2", Value = Size40mm, SqlDbType = SqlDbType.Real });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@3", Value = Size6o3_40mm, SqlDbType = SqlDbType.Real });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@4", Value = Size6o3mm, SqlDbType = SqlDbType.Real });
                if (!String.IsNullOrWhiteSpace(labJobNo))
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@5", Value = labJobNo, SqlDbType = SqlDbType.NVarChar, Size = 80 });
                else
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@5", Value = DBNull.Value, SqlDbType = SqlDbType.NVarChar, Size = 80 });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@7", Value = sampleId, SqlDbType = SqlDbType.NVarChar, Size = 80 });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@6", Value = reportDate, SqlDbType = SqlDbType.DateTime });
                cmd.ExecuteNonQuery();
            }
            catch (System.Exception exc)
            {
            }
            try
            {
                SqlCommand cmd = new SqlCommand("update " + this.SampleTableName + " set [Moisture] = @1, LabJobNo = @2, ReceivedOn = @4, ReceivedUser = SUSER_SNAME() where SampleId = @3 and Instructions like '%Moisture%'", this.Connection);
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@1", Value = Moisture, SqlDbType = SqlDbType.Real });
                if (!String.IsNullOrWhiteSpace(labJobNo))
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@2", Value = labJobNo, SqlDbType = SqlDbType.NVarChar, Size = 80 });
                else
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@2", Value = DBNull.Value, SqlDbType = SqlDbType.NVarChar, Size = 80 });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@3", Value = sampleId, SqlDbType = SqlDbType.NVarChar, Size = 80 });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@4", Value = reportDate, SqlDbType = SqlDbType.DateTime });
                cmd.ExecuteNonQuery();
            }
            catch (System.Exception exc)
            {
            }
        }
        private bool InSubmission(String submission, String sampleId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select 1 from " + this.SampleTableName + " where Submission = @1 and SampleId = @2", this.Connection);
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@1", Value = submission, SqlDbType = SqlDbType.NVarChar, Size = 80 });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@2", Value = sampleId, SqlDbType = SqlDbType.NVarChar, Size = 80 });
                Object v = cmd.ExecuteScalar();
                if (v != null && v is int && (int)v == 1)
                    return true;
                return false;
            }
            catch (System.Exception exc)
            {
            }
            return false;
        }

        private void butClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
