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
    public partial class ImportAssaysDlg : Form
    {
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

        public ImportAssaysDlg()
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
                if (fileName.EndsWith(".xls"))
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

            String submission = lstResults.Text;
            String resultsFile = this.ResultsDirectory + "\\" + submission + ".xls";
            String acceptedFile = this.AcceptedResultsDirectory + "\\" + submission + ".xls";
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
            if (!String.Equals(worksheet.Name, submission))
            {
                this.Errors.Add(new ErrorMessage("The results file's first worksheet does not have the name of the submission (" + submission + ")."));
                return;
            }
            if (!CheckCellContents(worksheet, 6, 4, "INTERTEK JOB NUMBER :"))
                return;
            String labJobNo = (worksheet.Cells[6, 7].Value != null ? worksheet.Cells[6, 7].Value.ToString() : null);

            if (!CheckCellContents(worksheet, 4, 4, "DATE REPORTED :"))
                return;
            if (!CheckCellContents(worksheet, 8, 1, "Fe"))
                return;
            if (!CheckCellContents(worksheet,8,2,"SiO2"))
                return;
            if (!CheckCellContents(worksheet, 8, 3, "Al2O3"))
                return;
            if (!CheckCellContents(worksheet,8,4,"P"))
                return;
            if (!CheckCellContents(worksheet,8,5,"S"))
                return;
            if (!CheckCellContents(worksheet,8,6,"Mn"))
                return;
            if (!CheckCellContents(worksheet,8,7,"LOI"))
                return;
            if (!CheckCellContents(worksheet,13,0,"SAMPLE NUMBERS"))
                return;

            String reportDateText = (worksheet.Cells[4,7].Value != null ? worksheet.Cells[4,7].Value.ToString() : null);
            DateTime reportDate = DateTime.Now;
            try
            {
                reportDate = DateTime.Parse(reportDateText);
            }
            catch (System.Exception exc)
            {
                MessageBox.Show("The report date [" + reportDateText + "] is causing an error.", "Error", MessageBoxButtons.OK);
                return;
            }

            double d;
            int imported = 0, total = 0;
            bool ok = true;
            for (int row = 14; row < worksheet.Rows.Count; row++)
            {
                String sampleId = (worksheet.Cells[row, 0].Value != null ? worksheet.Cells[row, 0].Value.ToString() : null);
                Double? Fe = (worksheet.Cells[row, 1].Value != null && Double.TryParse(worksheet.Cells[row, 1].Value.ToString(), out d) ? Double.Parse(worksheet.Cells[row, 1].Value.ToString()) : (double?)null);
                Double? SiO2 = (worksheet.Cells[row, 2].Value != null && Double.TryParse(worksheet.Cells[row, 2].Value.ToString(), out d) ? Double.Parse(worksheet.Cells[row, 2].Value.ToString()) : (double?)null);
                Double? Al2O3 = (worksheet.Cells[row, 3].Value != null && Double.TryParse(worksheet.Cells[row, 3].Value.ToString(), out d) ? Double.Parse(worksheet.Cells[row, 3].Value.ToString()) : (double?)null);
                Double? P = (worksheet.Cells[row, 4].Value != null && Double.TryParse(worksheet.Cells[row, 4].Value.ToString(), out d) ? Double.Parse(worksheet.Cells[row, 4].Value.ToString()) : (double?)null);
                Double? S = (worksheet.Cells[row, 5].Value != null && Double.TryParse(worksheet.Cells[row, 5].Value.ToString(), out d) ? Double.Parse(worksheet.Cells[row, 5].Value.ToString()) : (double?)null);
                Double? Mn = (worksheet.Cells[row, 6].Value != null && Double.TryParse(worksheet.Cells[row, 6].Value.ToString(), out d) ? Double.Parse(worksheet.Cells[row, 6].Value.ToString()) : (double?)null);
                Double? LOI = (worksheet.Cells[row, 7].Value != null && Double.TryParse(worksheet.Cells[row, 7].Value.ToString(), out d) ? Double.Parse(worksheet.Cells[row, 7].Value.ToString()) : (double?)null);

                if (String.IsNullOrWhiteSpace(sampleId) && Fe == null && SiO2 == null && Al2O3 == null && P == null && S == null && Mn == null && LOI == null)
                    continue;

                total++;

                if (!InSubmission(submission, sampleId))
                {
                    this.Errors.Add(new ErrorMessage("A sample (" + sampleId + ") was found that is not in submission (" + submission + ")."));
                    ok = false;
                    continue;
                }
                bool import = true;
                if (Fe == null)
                {
                    this.Errors.Add(new ErrorMessage("Sample (" + sampleId + ") does not have a Fe assay."));
                    ok = false;
                    import = false;
                }
                if (Al2O3 == null)
                {
                    this.Errors.Add(new ErrorMessage("Sample (" + sampleId + ") does not have a Al2O3 assay."));
                    ok = false;
                    import = false;
                }
                if (SiO2 == null)
                {
                    this.Errors.Add(new ErrorMessage("Sample (" + sampleId + ") does not have a SiO2 assay."));
                    ok = false;
                    import = false;
                }
                if (P == null)
                {
                    this.Errors.Add(new ErrorMessage("Sample (" + sampleId + ") does not have a P assay."));
                    ok = false;
                    import = false;
                }
                if (S == null)
                {
                    this.Errors.Add(new ErrorMessage("Sample (" + sampleId + ") does not have a S assay."));
                    ok = false;
                    import = false;
                }
                if (Mn == null)
                {
                    this.Errors.Add(new ErrorMessage("Sample (" + sampleId + ") does not have a Mn assay."));
                    ok = false;
                    import = false;
                }
                if (LOI == null)
                {
                    this.Errors.Add(new ErrorMessage("Sample (" + sampleId + ") does not have a LOI assay."));
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
                    UpdateResults(submission, sampleId, Fe, Al2O3, SiO2, P, S, Mn, LOI, labJobNo, reportDate);
                    imported++;
                }
                catch (System.Exception exc)
                {
                    this.Errors.Add(new ErrorMessage("Database error occurred when updating sample (" + sampleId + ") results."));
                    ok = false;
                }
            }
            if (!CheckMissingResults(submission))
                ok = false;
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
                SqlCommand cmd = new SqlCommand("select SampleId from " + this.SampleTableName + " where Submission = @1 and (Fe is null or Al2O3 is null or SiO2 is null or P is null or S is null or Mn is null or LOI is null)", this.Connection);
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

        private bool CheckCellContents(ExcelWorksheet worksheet, int row, int col, String text)
        {
            if (worksheet.Cells[row, col].Value == null || !String.Equals(worksheet.Cells[row, col].Value.ToString(), text))
            {
                this.Errors.Add(new ErrorMessage("The results file format does appear too match the submission. Cell (" + row.ToString() + "," + col.ToString() + ") doesn't contain \"" + text + "\"."));
                return false;
            }
            return true;
        }

        private void UpdateResults(String submission, String sampleId, double? Fe, double? Al2O3, double? SiO2, double? P, double? S, double? Mn, double? LOI, String labJobNo, DateTime reportDate)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("update " + this.SampleTableName + " set Fe = @1,Al2O3 = @2, SiO2 = @3, P = @4, S = @5, Mn = @6, LOI = @7, LabJobNo = @8, ReceivedOn = @10, ReceivedUser = SUSER_SNAME()" +
                (this.SubmissionType == AtlasSampleToolkit.Submission.SubmissionSampleType.Product ? ", Published = 'NO' " : "") +
                " where SampleId = @9", this.Connection);
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@1", Value = Fe, SqlDbType = SqlDbType.Real });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@2", Value = Al2O3, SqlDbType = SqlDbType.Real });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@3", Value = SiO2, SqlDbType = SqlDbType.Real });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@4", Value = P, SqlDbType = SqlDbType.Real });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@5", Value = S, SqlDbType = SqlDbType.Real });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@6", Value = Mn, SqlDbType = SqlDbType.Real });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@7", Value = LOI, SqlDbType = SqlDbType.Real });
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
