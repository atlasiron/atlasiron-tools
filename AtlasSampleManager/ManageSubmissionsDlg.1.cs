using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using Janus.Windows.GridEX;
using System.IO;
using System.Diagnostics;
using AtlasSampleToolkit;
using GemBox.Spreadsheet;

namespace AtlasSampleManager
{
    public partial class ManageSubmissionsDlg : Form
    {
        public String Submission { get; set; }
        public String SampleFilter { get; set; }

        public SqlConnection Connection
        {
            get;
            set;
        }

        private BindingList<Sample> Samples { get; set; }
        private BindingList<Sample> SubmissionSamples { get; set; }
        private BindingList<Sample> SizingsSamples { get; set; }
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

        private String SubmissionNameSuffix
        {
            get
            {
                if (this.SubmissionType == AtlasSampleToolkit.Submission.SubmissionSampleType.Blastholes)
                    return Properties.Settings.Default.AtlasSubmissionBSNameSuffix;
                else
                    return Properties.Settings.Default.AtlasSubmissionPSNameSuffix;
            }
        }

        public ManageSubmissionsDlg()
        {
            InitializeComponent();
            this.SubmissionType = AtlasSampleToolkit.Submission.SubmissionSampleType.Blastholes;
        }

        private void ManageSubmissionsDlg_Load(object sender, EventArgs e)
        {
            if (this.SubmissionType == AtlasSampleToolkit.Submission.SubmissionSampleType.Blastholes)
                UpdateBlastholeSamples();
            else
                UpdateProductSamples();
        }

        private void UpdateBlastholeSamples()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select distinct Pit,Bench,ShotNo,HoleNo,SampleId from gcv_Hole_Collars where Submission is null and SampleId is not null and " + SampleFilter + " order by Pit,Bench,ShotNo,HoleNo", this.Connection);
                SqlDataReader reader = cmd.ExecuteReader();
                this.Samples = new BindingList<Sample>();
                while (reader.Read())
                    this.Samples.Add(new Sample { Pit = reader[0].ToString(), Bench = reader[1].ToString(), ShotNo = reader[2].ToString(), HoleNo = reader[3].ToString(), SampleId = reader[4].ToString() });
                reader.Close();
                gridSamples.SetDataBinding(this.Samples, null);

                cmd = new SqlCommand("select distinct Submission from gcv_Hole_Collars where " + SampleFilter + " order by Submission", this.Connection);
                reader = cmd.ExecuteReader();
                m_allowSubmissions_TextChanged = false;
                lstSubmissions.Items.Clear();
                while (reader.Read())
                {
                    String item = reader[0].ToString();
                    if (!String.IsNullOrWhiteSpace(item))
                    {
                        lstSubmissions.Items.Add(new Janus.Windows.EditControls.UIComboBoxItem(item));
                        if (String.IsNullOrWhiteSpace(lstSubmissions.Text))
                            lstSubmissions.Text = item;
                    }
                }
                m_allowSubmissions_TextChanged = true;
                reader.Close();

                cmd = new SqlCommand("select distinct Pit,Bench,ShotNo,HoleNo,SampleId from gcv_Hole_Collars where Submission = '" + lstSubmissions.Text + "' and SampleId is not null and " + SampleFilter + " order by SampleId", this.Connection);
                reader = cmd.ExecuteReader();
                this.SubmissionSamples = new BindingList<Sample>();
                while (reader.Read())
                    this.SubmissionSamples.Add(new Sample { Pit = reader[0].ToString(), Bench = reader[1].ToString(), ShotNo = reader[2].ToString(), HoleNo = reader[3].ToString(), SampleId = reader[4].ToString() });
                reader.Close();

                gridSubmission.SetDataBinding(this.SubmissionSamples, null);
                butAdd.Enabled = !String.IsNullOrWhiteSpace(lstSubmissions.Text);
                butAddAll.Enabled = !String.IsNullOrWhiteSpace(lstSubmissions.Text);
                butRemove.Enabled = !String.IsNullOrWhiteSpace(lstSubmissions.Text);
                butRemoveAll.Enabled = !String.IsNullOrWhiteSpace(lstSubmissions.Text);

                gridSamples.RootTable.Columns["Pit"].Visible = true;
                gridSamples.RootTable.Columns["Bench"].Visible = true;
                gridSamples.RootTable.Columns["ShotNo"].Visible = true;
                gridSamples.RootTable.Columns["HoleNo"].Visible = true;
                gridSamples.RootTable.Columns["SampleId"].Visible = true;
                gridSamples.RootTable.Columns["SampledOn"].Visible = false;
            }
            catch (System.Exception exc)
            {
            }
        }

        private void UpdateProductSamples()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select distinct SampledOn,Shift,SampleId from " + this.SampleTableName + " where Submission is null and SampleId is not null and " + SampleFilter + " order by SampleId", this.Connection);
                SqlDataReader reader = cmd.ExecuteReader();
                this.Samples = new BindingList<Sample>();
                while (reader.Read())
                    this.Samples.Add(new Sample { SampledOn = (DateTime)reader[0], Shift = reader[1].ToString(), SampleId = reader[2].ToString() });
                reader.Close();
                gridSamples.SetDataBinding(this.Samples, null);

                cmd = new SqlCommand("select distinct Submission from " + this.SampleTableName + " where " + SampleFilter + " order by Submission", this.Connection);
                reader = cmd.ExecuteReader();
                m_allowSubmissions_TextChanged = false;
                lstSubmissions.Items.Clear();
                while (reader.Read())
                {
                    String item = reader[0].ToString();
                    if (!String.IsNullOrWhiteSpace(item))
                    {
                        lstSubmissions.Items.Add(new Janus.Windows.EditControls.UIComboBoxItem(item));
                        if (String.IsNullOrWhiteSpace(lstSubmissions.Text))
                            lstSubmissions.Text = item;
                    }
                }
                m_allowSubmissions_TextChanged = true;
                reader.Close();

                cmd = new SqlCommand("select distinct SampledOn,Shift,SampleId from " + this.SampleTableName + " where Submission = '" + lstSubmissions.Text + "' and SampleId is not null and " + SampleFilter + " order by SampleId", this.Connection);
                reader = cmd.ExecuteReader();
                this.SubmissionSamples = new BindingList<Sample>();
                while (reader.Read())
                    this.SubmissionSamples.Add(new Sample { SampledOn = (DateTime)reader[0], Shift = reader[1].ToString(), SampleId = reader[2].ToString() });
                reader.Close();

                gridSubmission.SetDataBinding(this.SubmissionSamples, null);
                butAdd.Enabled = !String.IsNullOrWhiteSpace(lstSubmissions.Text);
                butAddAll.Enabled = !String.IsNullOrWhiteSpace(lstSubmissions.Text);
                butRemove.Enabled = !String.IsNullOrWhiteSpace(lstSubmissions.Text);
                butRemoveAll.Enabled = !String.IsNullOrWhiteSpace(lstSubmissions.Text);

                gridSamples.RootTable.Columns["Pit"].Visible = false;
                gridSamples.RootTable.Columns["Bench"].Visible = false;
                gridSamples.RootTable.Columns["ShotNo"].Visible = false;
                gridSamples.RootTable.Columns["HoleNo"].Visible = false;
                gridSamples.RootTable.Columns["SampleId"].Visible = true;
                gridSamples.RootTable.Columns["SampledOn"].Visible = true;
            }
            catch (System.Exception exc)
            {
            }
        }

        private Boolean HasAssays(String sampleId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select count(*) from " + this.SampleTableName + " where SampleId = @1 and Fe is not null", this.Connection);
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@1", Value = sampleId, SqlDbType = SqlDbType.NVarChar, Size = 80 });
                return (int)cmd.ExecuteScalar() > 0;
            }
            catch (System.Exception exc)
            {
            }
            return false;
        }

        private void UpdateSubmission(String submission, String sampleId, Boolean wipeDate = false)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("update " + this.SampleTableName + " set Submission = @1 where SampleId = @2", this.Connection);
                if (wipeDate)
                    cmd = new SqlCommand("update " + this.SampleTableName +  " set Submission = @1, SubmittedOn = null  where SampleId = @2", this.Connection);
                if (String.IsNullOrWhiteSpace(submission))
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@1", Value = DBNull.Value, SqlDbType = SqlDbType.NVarChar, Size = 80 });
                else
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@1", Value = submission, SqlDbType = SqlDbType.NVarChar, Size = 80 });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@2", Value = sampleId, SqlDbType = SqlDbType.NVarChar, Size = 80 });
                cmd.ExecuteNonQuery();
            }
            catch (System.Exception exc)
            {
            }
        }

        private void UpdateSubmissionDate(String sampleId,String submission)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("update " + this.SampleTableName + " set SubmittedOn = GETDATE(), Submission = @1, SubmittedUser = SUSER_SNAME() where SampleId = @2", this.Connection);
                if (String.IsNullOrWhiteSpace(submission))
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@1", Value = DBNull.Value, SqlDbType = SqlDbType.NVarChar, Size = 80 });
                else
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@1", Value = submission, SqlDbType = SqlDbType.NVarChar, Size = 80 });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@2", Value = sampleId, SqlDbType = SqlDbType.NVarChar, Size = 80 });
                cmd.ExecuteNonQuery();
            }
            catch (System.Exception exc)
            {
            }
        }

        private void butClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void butAdd_Click(object sender, EventArgs e)
        {
            List<Sample> blastholes = new List<Sample>();
            foreach (GridEXSelectedItem item in gridSamples.SelectedItems)
                blastholes.Add(item.GetRow().DataRow as Sample);

            foreach (Sample blasthole in blastholes)
            {
                UpdateSubmission(lstSubmissions.Text, blasthole.SampleId);
                this.Samples.Remove(blasthole);
                this.SubmissionSamples.Add(blasthole);
            }
        }

        private void butAddAll_Click(object sender, EventArgs e)
        {
            foreach (Sample blasthole in this.Samples.ToList())
            {
                UpdateSubmission(lstSubmissions.Text, blasthole.SampleId);
                this.Samples.Remove(blasthole);
                this.SubmissionSamples.Add(blasthole);
            }
        }

        private void butRemove_Click(object sender, EventArgs e)
        {
            List<Sample> blastholes = new List<Sample>();
            foreach (GridEXSelectedItem item in gridSubmission.SelectedItems)
                blastholes.Add(item.GetRow().DataRow as Sample);

            Boolean hasAssays = false;
            foreach (Sample blasthole in blastholes)
                hasAssays = hasAssays || HasAssays(blasthole.SampleId);
            if (hasAssays)
            {
                MessageBox.Show("This sample (or samples) have assays loaded against them. You can not remove them from this submission.", "Error", MessageBoxButtons.OK);
                return;
            }

            foreach (Sample blasthole in blastholes)
            {
                UpdateSubmission(null, blasthole.SampleId, true);
                this.SubmissionSamples.Remove(blasthole);
                this.Samples.Add(blasthole);
            }
        }

        private void butRemoveAll_Click(object sender, EventArgs e)
        {
            Boolean hasAssays = false;
            foreach (Sample blasthole in this.SubmissionSamples.ToList())
                hasAssays = hasAssays || HasAssays(blasthole.SampleId);
            if (hasAssays)
            {
                MessageBox.Show("These samples have assays loaded against them. You can not remove them from this submission.", "Error", MessageBoxButtons.OK);
                return;
            }

            foreach (Sample blasthole in this.SubmissionSamples.ToList())
            {
                UpdateSubmission(null, blasthole.SampleId, true);
                this.SubmissionSamples.Remove(blasthole);
                this.Samples.Add(blasthole);
            }
        }

        private void butNewSubmission_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select max(cast(substring(Submission,@1,80) as int)+1) from " + this.SampleTableName + " where Submission is not null", this.Connection);
            cmd.Parameters.Add(new SqlParameter("@1", this.SubmissionNameSuffix.Length + 1));
            Object value = cmd.ExecuteScalar();
            int submissionNo = 1;
            try
            {
                if (value != null && value != DBNull.Value)
                    submissionNo = (int)value;
            }
            catch (System.Exception exc)
            {
            }
            lstSubmissions.Text = this.SubmissionNameSuffix + submissionNo.ToString();
            if (this.SubmissionType == AtlasSampleToolkit.Submission.SubmissionSampleType.Blastholes)
                UpdateBlastholeSamples();
            else
                UpdateProductSamples();
        }

        private bool m_allowSubmissions_TextChanged = false;
        private void lstSubmissions_TextChanged(object sender, EventArgs e)
        {
            if (m_allowSubmissions_TextChanged)
            {
                if (this.SubmissionType == AtlasSampleToolkit.Submission.SubmissionSampleType.Blastholes)
                    UpdateBlastholeSamples();
                else
                    UpdateProductSamples();
            }
        }

        private void butSubmit_Click(object sender, EventArgs e)
        {
            Submission submission = new Submission { Company = Properties.Settings.Default.AtlasCompany, Operation = Properties.Settings.Default.AtlasOperation, Laboratory = Properties.Settings.Default.Laboratory, Name = lstSubmissions.Text, FileName = "", AssaySamples = this.SubmissionSamples.ToList() };
            try
            {
                Boolean hasAssays = false;
                foreach (Sample blasthole in this.SubmissionSamples.ToList())
                    hasAssays = hasAssays || HasAssays(blasthole.SampleId);
                if (hasAssays)
                {
                    MessageBox.Show("This submission has samples with assays loaded against them. You can not resubmit it.", "Error", MessageBoxButtons.OK);
                    return;
                }
                
                String submissionName = lstSubmissions.Text;
                String fileName = (this.SubmissionType == AtlasSampleToolkit.Submission.SubmissionSampleType.Blastholes ? Properties.Settings.Default.BSSubmissionDirectory : Properties.Settings.Default.PSSubmissionDirectory) + "\\" + submissionName + ".xls";

                CreateSubmissionSheet(submissionName, fileName);

                SendEmail email = new SendEmail();
                email.AddRecipientTo(Properties.Settings.Default.LaboratoryEmailList);
                email.AddAttachment(fileName);
                foreach (Sample sample in submission.AssaySamples)
                    UpdateSubmissionDate(sample.SampleId, lstSubmissions.Text);

                String subject = "Submission : " + submissionName;
                String body = "";
                email.SendMailPopup(subject, body);

                if (this.SubmissionType == AtlasSampleToolkit.Submission.SubmissionSampleType.Blastholes)
                {
                    submission.WriteSubmission(Properties.Settings.Default.BSSampleDespatchDirectory, true);
                }
                else
                {
                    submission.WriteSubmission(Properties.Settings.Default.PSSampleDespatchDirectory, true);
                }
                MessageBox.Show("Submission was submitted for despatch. Saved to <" + fileName + ">", "Chain of Custody", MessageBoxButtons.OK);

                this.Close();
            }
            catch (System.Exception exc)
            {
                MessageBox.Show("Error saving submission to laboratory synchronisation directory", "Error", MessageBoxButtons.OK);
            }
        }

        private void butSubmissionSheet_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog saveAs = new FolderBrowserDialog();
            saveAs.SelectedPath = this.SubmissionType == AtlasSampleToolkit.Submission.SubmissionSampleType.Blastholes ? Properties.Settings.Default.BSSubmissionDirectory : Properties.Settings.Default.PSSubmissionDirectory;
            if (saveAs.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                String submission = lstSubmissions.Text;
                String fileName = saveAs.SelectedPath + "\\" + submission + ".xls";

                CreateSubmissionSheet(submission, fileName);

                Process.Start(fileName);
            }
        }

        private void CreateSubmissionSheet(String submission, String fileName)
        {
            ExcelFile excelFile = new ExcelFile();
            ExcelWorksheet worksheet = excelFile.Worksheets.Add(submission);
            worksheet.Columns[0].Width = 7000;
            worksheet.Columns[1].Width = 5000;
            worksheet.Columns[2].Width = 5000;
            worksheet.Columns[3].Width = 5000;
            worksheet.Columns[4].Width = 5000;

            worksheet.Cells[0, 0].Value = submission;
            worksheet.Cells[0, 0].Style.Font.Weight = 800;

            worksheet.Cells[2, 0].Value = "From:";
            worksheet.Cells[2, 0].Style.Font.Weight = 800;
            worksheet.Cells[2, 1].Value = Properties.Settings.Default.AtlasOperation;

            worksheet.Cells[4, 0].Value = "Date Dispatched:";
            worksheet.Cells[4, 0].Style.Font.Weight = 800;
            worksheet.Cells[4, 1].Value = DateTime.Now.ToString();

            worksheet.Cells[6, 0].Value = "Description of Samples:";
            worksheet.Cells[6, 0].Style.Font.Weight = 800;
            worksheet.Cells[6, 1].Value = "Blasthole Samples:";

            worksheet.Cells[8, 0].Value = "Sample Identification:";
            worksheet.Cells[8, 0].Style.Font.Weight = 800;

            int n = 0;
            for (int c = 0; c < 3 && n < this.SubmissionSamples.Count; c++)
            {
                for (int r = 0; r < this.SubmissionSamples.Count / 3 + 1 && n < this.SubmissionSamples.Count; r++)
                {
                    worksheet.Cells[10 + r, 1 + c].Value = this.SubmissionSamples[n].SampleId;
                    worksheet.Cells[10 + r, 1 + c].SetBorders(MultipleBorders.Outside, Color.Black, LineStyle.Thin);
                    n++;
                }
            }
            int row = 10 + this.SubmissionSamples.Count / 3 + 2;

            worksheet.Cells[row, 0].Value = "Total:";
            worksheet.Cells[row, 0].Style.Font.Weight = 800;
            worksheet.Cells[row, 1].Style.HorizontalAlignment = HorizontalAlignmentStyle.Left;
            worksheet.Cells[row, 1].Value = this.SubmissionSamples.Count;
            row += 2;
            worksheet.Cells[row, 0].Value = "Analysis Required:";
            worksheet.Cells[row, 0].Style.Font.Weight = 800;
            worksheet.Cells[row, 1].Value = "Standard iron ore analysis suite (Fe, Sio2, Al2o3, P, LOI, Mn, S)";
            worksheet.Cells[row, 1].Style.Font.Color = Color.Red;
            worksheet.Cells[row, 1].Style.Font.Weight = 800;
            row++;
            worksheet.Cells[row, 1].Value = "NO sizings and NO moisture.";
            worksheet.Cells[row, 1].Style.Font.Color = Color.Red;
            worksheet.Cells[row, 1].Style.Font.Weight = 800;
            worksheet.Cells[row, 1].Style.FillPattern.SetSolid(Color.Yellow);
            worksheet.Cells[row, 2].Style.FillPattern.SetSolid(Color.Yellow);
            row += 2;
            worksheet.Cells[row, 0].Value = "Special Instructions:";
            worksheet.Cells[row, 0].Style.Font.Weight = 800;
            worksheet.Cells[row, 1].Value = "These samples are normal priority.";
            worksheet.Cells[row, 1].Style.Font.Color = Color.Red;
            worksheet.Cells[row, 1].Style.Font.Weight = 800;
            row += 2;
            worksheet.Cells[row, 0].Value = "Submitted By:";
            worksheet.Cells[row, 0].Style.Font.Weight = 800;
            row += 2;
            worksheet.Cells[row, 0].Value = "Report To:";
            worksheet.Cells[row, 0].Style.Font.Weight = 800;
            worksheet.Cells[row, 1].Value = "Standard email distribution list.";
            row += 2;
            worksheet.Cells[row, 0].Value = "Invoice To:";
            worksheet.Cells[row, 0].Style.Font.Weight = 800;
            worksheet.Cells[row++, 1].Value = "Atlas Iron";
            worksheet.Cells[row++, 1].Value = "PO Box 223";
            worksheet.Cells[row++, 1].Value = "West Perth 6872";

            excelFile.SaveXls(fileName);

        }
    }
}
