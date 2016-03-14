using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Security.Principal;
using System.Diagnostics;
using System.Data.SqlClient;
using System.IO;
using Janus.Windows.GridEX;
using Janus.Windows.EditControls;
using System.Windows.Forms.DataVisualization.Charting;
using AtlasSampleToolkit;
using AtlasReportToolkit;

namespace AtlasSampleManager
{
    public partial class MainForm : Form
    {
        private Timer scanChainOfCustody = new Timer();

        public List<String> LoggingCodes = new List<String> 
        {
            "HG","LG","W","Shl","Cly","Dol"
        };

        private List<List<String>> ColourCodes = new List<List<String>> 
        {
            new List<String> { "Bn","Brown" },
            new List<String> { "Or","Orange" },
            new List<String> { "Rd","Red" },
            new List<String> { "Yl","Yellow" },
            new List<String> { "Bl","Blue" },
            new List<String> { "Bk","Black" },
            new List<String> { "Wh","White" },
            new List<String> { "Gn","Green" },
        };

        private const String PlantWeightmeterMaterialChange = "Finger/Stockpile Change";
        private const String PlantWeightmeterStartOfShift = "Shift Change";
        //private const String PlantWeightmeterEndOfShift = "End Of Shift";

        private const String SampledOnColumn = "SelectedOn";
        private String BlastholesSampleFilter { get; set; }
        private String ProductSampleFilter { get; set; }
        private AtlasSampleManager.SamplesTableAdapters.psm_Product_SamplesTableAdapter psm_Product_SamplesTableAdapter;
        private AtlasSampleManager.SamplesTableAdapters.gcv_Hole_CollarsTableAdapter gcv_Hole_CollarsTableAdapter;
        public MainForm()
        {
            InitializeComponent();
        }

        [Conditional("DEBUG")]
        private void SetDebugConnection()
        {
            Properties.Settings.Default.AtlasOperation = "WEB";
            Properties.Settings.Default.ConfigDirectory = @"D:\home\Clients\ati004\data\SampleManager";

            Properties.Settings.Default.AtlasGradeControlDatabase = "Server=.;Database=Atlas_PRODGCV_WOD;Trusted_Connection=True;";
            Properties.Settings.Default.AtlasMinemarketLinkDatabase = "Server=.;Database=MinemarketLink;Trusted_Connection=True;";
            Properties.Settings.Default.AtlasSubmissionBSNameSuffix = "ATLAS IRON WOD_BS_";
            Properties.Settings.Default.AtlasSubmissionPSNameSuffix = "ATLAS IRON WOD_PS_";
            Properties.Settings.Default.LaboratoryEmailList = "evans@endevea.com";

            Properties.Settings.Default.BSPendingShotSampling = @"D:\home\Clients\ati004\data\samples\pending";

            Properties.Settings.Default.BSSubmissionDirectory = @"D:\home\Clients\ati004\data\samples\pending";
            Properties.Settings.Default.BSResultsDirectory = @"D:\home\Clients\ati004\data\samples\pending";
            Properties.Settings.Default.BSAcceptedResultsDirectory = @"D:\home\Clients\ati004\data\samples\accepted";

            Properties.Settings.Default.PSSubmissionDirectory = @"D:\home\Clients\ati004\data\samples\pending";
            Properties.Settings.Default.PSResultsDirectory = @"D:\home\Clients\ati004\data\samples\pending";
            Properties.Settings.Default.PSAcceptedResultsDirectory = @"D:\home\Clients\ati004\data\samples\accepted";
            Properties.Settings.Default.PSShiftTimeOffset = 1.0;

            Properties.Settings.Default.iHubPendingDirectory = @"D:\home\Clients\ati004\data\samples\ihub";
            Properties.Settings.Default.iHubProcessedDirectory = @"D:\home\Clients\ati004\data\samples\ihub";
            Properties.Settings.Default.iHubErrorDirectory = @"D:\home\Clients\ati004\data\samples\ihub";
            Properties.Settings.Default.iHubProcessingDirectory = @"D:\home\Clients\ati004\data\samples\ihub";
            Properties.Settings.Default.CrusherStreams.Clear();
            Properties.Settings.Default.CrusherStreams.Add("Main");
            Properties.Settings.Default.CrusherStreams.Add("Auxiliary");
            Properties.Settings.Default.CrusherStreams.Add("Tertiary");
        }

        [Conditional("DEBUG")]
        private void PreDebugSetup()
        {
          //  Properties.Settings.Default.ConfigDirectory = @"D:\home\Clients\ati004\data\SampleManager";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Version v = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            PreDebugSetup();

            ConfigureSettingsDlg.UpdateConfiguration(Properties.Settings.Default.AtlasOperation);

            this.Text = "Atlas Sample Manager V" + v.ToString();

            //SetDebugConnection();
           // MessageBox.Show(global::AtlasSampleManager.Properties.Settings.Default.AtlasGradeControlDatabase);
            if (Properties.Settings.Default.AtlasOperation.Equals("???"))
            {
                ConfigureSettingsDlg configure = new ConfigureSettingsDlg();
                configure.ShowDialog();
            }


            if (Properties.Settings.Default.CrusherStreams == null)
                Properties.Settings.Default.CrusherStreams = new System.Collections.Specialized.StringCollection();
            if (Properties.Settings.Default.CrusherStreams.Count == 0)
            {
                Properties.Settings.Default.CrusherStreams.Add("Main");
                Properties.Settings.Default.CrusherStreams.Add("Auxiliary");
                Properties.Settings.Default.CrusherStreams.Add("Tertiary");
            }
            try
            {
                gcv_Hole_CollarsTableAdapter = new SamplesTableAdapters.gcv_Hole_CollarsTableAdapter();
                psm_Product_SamplesTableAdapter = new SamplesTableAdapters.psm_Product_SamplesTableAdapter();

                calStartDate.Value = new DateTime(DateTime.Now.AddDays(-1).Year, DateTime.Now.AddDays(-1).Month, DateTime.Now.AddDays(-1).Day, 6, 0, 0);
                calEndDate.Value = calStartDate.Value.AddDays(1);
                calBSEndDate.Value = new DateTime(DateTime.Now.AddDays(1).Year, DateTime.Now.AddDays(1).Month, DateTime.Now.AddDays(1).Day, 6, 0, 0);
                calBSStartDate.Value = calBSEndDate.Value.AddDays(-14);

                FilterBlastholesGrid();

                UpdateBlastholeFilterLists();

                FilterProductSamplesGrid();

                UpdateProductSamplesFilterLists();

                gridBlastholes.RootTable.Columns["HoleNo"].SortComparer = new CompareHoleNo();

                lstDateFilter.Items.Clear();
                lstDateFilter.Items.Add("","None");
                lstDateFilter.Items.Add("SampledOn");
                lstDateFilter.Items.Add("SubmittedOn");
                lstDateFilter.Items.Add("DespatchedOn");
                lstDateFilter.Items.Add("ArrivedForPreparation");
                lstDateFilter.Items.Add("ArrivedForAnalysis");
                lstDateFilter.Items.Add("ReceivedOn");

                lstPerformanceDateFilter.Items.Clear();
                lstPerformanceDateFilter.Items.Add("SampledOn");
                lstPerformanceDateFilter.Items.Add("SubmittedOn");
                lstPerformanceDateFilter.Items.Add("DespatchedOn");
                lstPerformanceDateFilter.Items.Add("ArrivedForPreparation");
                lstPerformanceDateFilter.Items.Add("ArrivedForAnalysis");
                lstPerformanceDateFilter.Items.Add("ReceivedOn");

                lstPerformanceYDateField.Items.Clear();
                lstPerformanceYDateField.Items.Add("SampledOn");
                lstPerformanceYDateField.Items.Add("SubmittedOn");
                lstPerformanceYDateField.Items.Add("DespatchedOn");
                lstPerformanceYDateField.Items.Add("ArrivedForPreparation");
                lstPerformanceYDateField.Items.Add("ArrivedForAnalysis");
                lstPerformanceYDateField.Items.Add("ReceivedOn");

                gridProductSamples.RootTable.Columns["SampleId"].ValueList.Clear();
                gridProductSamples.RootTable.Columns["SampleId"].ValueList.Add(null,"");
                gridProductSamples.RootTable.Columns["SampleId"].ValueList.Add(PlantWeightmeterMaterialChange, PlantWeightmeterMaterialChange);
                gridProductSamples.RootTable.Columns["SampleId"].ValueList.Add(PlantWeightmeterStartOfShift, PlantWeightmeterStartOfShift);
//                gridProductSamples.RootTable.Columns["SampleId"].ValueList.Add(PlantWeightmeterEndOfShift, PlantWeightmeterEndOfShift);

                gridProductSamples.RootTable.Columns["SampledOn"].InputMask = "00/00/0000 00:00:00";
                gridProductSamples.RootTable.Columns["SampledOn"].FormatString = "dd/MM/yyyy HH:mm:ss";

                gridProductSamples.RootTable.Columns["PrimaryScoops"].ValueList.Clear();
                gridProductSamples.RootTable.Columns["SecondaryScoops"].ValueList.Clear();
                gridProductSamples.RootTable.Columns["Finger3Scoops"].ValueList.Clear();
                gridProductSamples.RootTable.Columns["Finger4Scoops"].ValueList.Clear();
                gridProductSamples.RootTable.Columns["Finger5Scoops"].ValueList.Clear();
                gridProductSamples.RootTable.Columns["Finger6Scoops"].ValueList.Clear();
                gridProductSamples.RootTable.Columns["PrimaryScoops"].ValueList.Add(null, "");
                gridProductSamples.RootTable.Columns["SecondaryScoops"].ValueList.Add(null,"");
                gridProductSamples.RootTable.Columns["Finger3Scoops"].ValueList.Add(null, "");
                gridProductSamples.RootTable.Columns["Finger4Scoops"].ValueList.Add(null, "");
                gridProductSamples.RootTable.Columns["Finger5Scoops"].ValueList.Add(null, "");
                gridProductSamples.RootTable.Columns["Finger6Scoops"].ValueList.Add(null, "");
                for (int i = 1; i < 10; i++)
                {
                    gridProductSamples.RootTable.Columns["PrimaryScoops"].ValueList.Add(i, i.ToString());
                    gridProductSamples.RootTable.Columns["SecondaryScoops"].ValueList.Add(i, i.ToString());
                    gridProductSamples.RootTable.Columns["Finger3Scoops"].ValueList.Add(i, i.ToString());
                    gridProductSamples.RootTable.Columns["Finger4Scoops"].ValueList.Add(i, i.ToString());
                    gridProductSamples.RootTable.Columns["Finger5Scoops"].ValueList.Add(i, i.ToString());
                    gridProductSamples.RootTable.Columns["Finger6Scoops"].ValueList.Add(i, i.ToString());
                }

                foreach (String code in LoggingCodes)
                {
                    gridBlastholes.RootTable.Columns["LogMatType"].ValueList.Add(code, code);
                    gridBlastholes.RootTable.Columns["LogOtherMatType"].ValueList.Add(code, code);
                    gridBlastholes.RootTable.Columns["LogTopMatType"].ValueList.Add(code, code);
                    gridBlastholes.RootTable.Columns["LogBottomMatType"].ValueList.Add(code, code);
                }

                foreach (List<String> code in ColourCodes)
                {
                    gridBlastholes.RootTable.Columns["LogTopColour"].ValueList.Add(code[0], code[1]);
                    gridBlastholes.RootTable.Columns["LogBottomColour"].ValueList.Add(code[0], code[1]);            
                }

                gridProductSamples.RootTable.Columns["Action"].ValueList.Add(null, "");
                gridProductSamples.RootTable.Columns["Action"].ValueList.Add(PublishCrushingDataDlg.c_ActionSkipTons, PublishCrushingDataDlg.c_ActionSkipTons);
                gridProductSamples.RootTable.Columns["Action"].ValueList.Add(PublishCrushingDataDlg.c_ActionSkipGrade, PublishCrushingDataDlg.c_ActionSkipGrade);

                gridProductSamples.RootTable.Columns["Crusher"].DefaultValue = "Main";
                gridProductSamples.RootTable.Columns["Crusher"].ValueList.Add("Main", "Main");
                gridProductSamples.RootTable.Columns["Crusher"].ValueList.Add("Auxiliary", "Auxiliary");
                gridProductSamples.RootTable.Columns["Crusher"].ValueList.Add("Tertiary", "Tertiary");

                if (Properties.Settings.Default.CrusherStreams != null && Properties.Settings.Default.CrusherStreams.Count > 0)
                {
                    lstCrushers.SelectedValue = Properties.Settings.Default.CrusherStreams[0] ?? "Main";
                }

                foreach (string s in Properties.Settings.Default.CrusherStreams)
                    lstCrushers.Items.Add(new UIComboBoxItem(s, s));

                gridProductSamples.RootTable.Columns["Shift"].ValueList.Add("Day","Day");
                gridProductSamples.RootTable.Columns["Shift"].ValueList.Add("Night", "Night");

                //scanChainOfCustody.Interval = 1000 * 120;
                //scanChainOfCustody.Tick += new EventHandler(this.ScanChainOfCustody);
                //scanChainOfCustody.Start();

                uiTabPage2.TabVisible = true;

                CheckForNonPublishedCrushingData();

            }
            catch (System.Exception exc)
            {
                MessageBox.Show("There is an error connecting to the database. Click OK to check your configuration. System message : [" + exc.Message + ":" + exc.StackTrace.ToString() + "]");
                MessageBox.Show("There is an error connecting to the database. Click OK to check your configuration. Connection Data is : [" + global::AtlasSampleManager.Properties.Settings.Default.AtlasGradeControlDatabase + "]");
                MessageBox.Show("There is an error connecting to the database. Click OK to check your configuration. Connection Data is : [" + global::AtlasSampleManager.Properties.Settings.Default.AtlasGradeControlDatabase + "]");
                ConfigureSettingsDlg config = new ConfigureSettingsDlg();
                config.ShowDialog();
                MessageBox.Show("You must restart sample manager before any configuration changes have an effect.", "Sample Management", MessageBoxButtons.OK);
                this.Close();
            }
        }


        private void CheckForNonPublishedCrushingData()
        {
            if (psm_Product_SamplesTableAdapter.Connection.State != ConnectionState.Open)
                psm_Product_SamplesTableAdapter.Connection.Open();

            SqlCommand cmd = new SqlCommand("select distinct 1 from psm_Product_Samples where Published is not null", psm_Product_SamplesTableAdapter.Connection);
            Object result = cmd.ExecuteScalar();
            if (result != null && result != DBNull.Value)
                MessageBox.Show("There are unpublished crushing data. This may seriously effect stockpile tonnes and grades.", "Error", MessageBoxButtons.OK);

            psm_Product_SamplesTableAdapter.Connection.Close();
        }

        private void UpdateBlastholeFilterLists()
        {
            if (gcv_Hole_CollarsTableAdapter.Connection.State != ConnectionState.Open)
                gcv_Hole_CollarsTableAdapter.Connection.Open();

            SqlCommand cmd = new SqlCommand("select distinct Pit from gcv_Hole_Collars order by Pit", gcv_Hole_CollarsTableAdapter.Connection);
            SqlDataReader reader = cmd.ExecuteReader();
            lstPits.Items.Clear();
            lstPits.Items.Add(new Janus.Windows.EditControls.UIComboBoxItem(null));
            while (reader.Read())
                lstPits.Items.Add(new Janus.Windows.EditControls.UIComboBoxItem(reader[0]));
            reader.Close();

            cmd = new SqlCommand("select distinct Bench from gcv_Hole_Collars order by Bench desc", gcv_Hole_CollarsTableAdapter.Connection);
            reader = cmd.ExecuteReader();
            lstBench.Items.Clear();
            lstBench.Items.Add(new Janus.Windows.EditControls.UIComboBoxItem(null));
            while (reader.Read())
                lstBench.Items.Add(new Janus.Windows.EditControls.UIComboBoxItem(reader[0]));
            reader.Close();

            cmd = new SqlCommand("select distinct ShotNo from gcv_Hole_Collars order by ShotNo desc", gcv_Hole_CollarsTableAdapter.Connection);
            reader = cmd.ExecuteReader();
            lstShotNos.Items.Clear();
            lstShotNos.Items.Add(new Janus.Windows.EditControls.UIComboBoxItem(null));
            while (reader.Read())
                lstShotNos.Items.Add(new Janus.Windows.EditControls.UIComboBoxItem(reader[0]));
            reader.Close();

            cmd = new SqlCommand("select distinct Submission from gcv_Hole_Collars order by Submission desc", gcv_Hole_CollarsTableAdapter.Connection);
            reader = cmd.ExecuteReader();
            lstSubmissions.Items.Clear();
            lstSubmissions.Items.Add(new Janus.Windows.EditControls.UIComboBoxItem(null));
            while (reader.Read())
            {
                String submission = (reader[0] == DBNull.Value ? null : reader[0].ToString());
                if (!String.IsNullOrWhiteSpace(submission))
                    lstSubmissions.Items.Add(new Janus.Windows.EditControls.UIComboBoxItem(submission, submission));
            }
            reader.Close();

            gcv_Hole_CollarsTableAdapter.Connection.Close();
        }

        private void butClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridBlastholes_RecordUpdated(object sender, EventArgs e)
        {
            try
            {
                if (gcv_Hole_CollarsTableAdapter.Connection.State != ConnectionState.Open)
                    gcv_Hole_CollarsTableAdapter.Connection.Open();
                gcv_Hole_CollarsTableAdapter.Update(samples.gcv_Hole_Collars);
                gcv_Hole_CollarsTableAdapter.Connection.Close();
            }
            catch (SystemException exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void gridBlastholes_UpdatingRecord(object sender, CancelEventArgs e)
        {
            e.Cancel = false;

            GridEXRow row = gridBlastholes.GetRow();
            int sampleIdLength = row.Cells["SampleId"].Value == null ? 0 : row.Cells["SampleId"].Value.ToString().Length;
            if (sampleIdLength == 0)
                return;

            String sampleId = row.Cells["SampleId"].Value.ToString();

            if (gcv_Hole_CollarsTableAdapter.Connection.State != ConnectionState.Open)
                gcv_Hole_CollarsTableAdapter.Connection.Open();

            String pit = row.Cells["Pit"].Value.ToString();
            String bench = row.Cells["Bench"].Value.ToString();
            String shotno = row.Cells["ShotNo"].Value.ToString();
            String holeno = row.Cells["HoleNo"].Value.ToString();
            SqlCommand cmd = new SqlCommand("select Pit,Bench,ShotNo,HoleNo from gcv_Hole_Collars where SampleId = @1 and not (Pit = @2 and Bench = @3 and ShotNo = @4 and HoleNo = @5)", gcv_Hole_CollarsTableAdapter.Connection);
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@1", Value = sampleId });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@2", Value = pit });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@3", Value = bench });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@4", Value = shotno });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@5", Value = holeno });
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                MessageBox.Show("The sample id <" +  sampleId + "> is already assigned to blasthole <" + reader[0].ToString() + ">,<" + reader[1].ToString() + ">,<" + reader[2].ToString() + ">,<" + reader[3].ToString() + ">", "Error", MessageBoxButtons.OK);
                e.Cancel = true;
            }
            reader.Close();

            if (((!sampleId.Substring(0, 3).Equals(Properties.Settings.Default.AtlasOperation)) && !(Properties.Settings.Default.AtlasOperation.Equals("WEB") && sampleId.Substring(0, 3).Equals("DOV"))) ||
                !sampleId.Substring(3, 2).Equals("BS") ||
                sampleId.Length < 10 ||
                sampleId.Length > 11)
            {
                MessageBox.Show("The sample id <" + sampleId + "> does not have the correct format (" + Properties.Settings.Default.AtlasOperation + "BSNNNNN).", "Error", MessageBoxButtons.OK);
                e.Cancel = true;
            }

            gcv_Hole_CollarsTableAdapter.Connection.Close();
        }

        private void FilterBlastholesGrid()
        {
            int curRowNo = gridBlastholes.FirstRow;

            this.BlastholesSampleFilter = "1 = 1";
            if (lstPits.SelectedValue != null && !String.IsNullOrWhiteSpace(lstPits.SelectedValue.ToString()))
            {
                this.BlastholesSampleFilter += " and Pit = '" + lstPits.SelectedValue.ToString() + "'";
            }
            if (lstBench.SelectedValue != null && !String.IsNullOrWhiteSpace(lstBench.SelectedValue.ToString()))
            {
                this.BlastholesSampleFilter += " and Bench = '" + lstBench.SelectedValue.ToString() + "'";
            }
            if (lstShotNos.SelectedValue != null && !String.IsNullOrWhiteSpace(lstShotNos.SelectedValue.ToString()))
            {
                this.BlastholesSampleFilter += " and ShotNo = '" + lstShotNos.SelectedValue.ToString() + "'";
            }
            if (lstSubmissions.SelectedValue != null && !String.IsNullOrWhiteSpace(lstSubmissions.SelectedValue.ToString()))
            {
                this.BlastholesSampleFilter += " and Submission = '" + lstSubmissions.SelectedValue.ToString() + "'";
            }
            if (lstDateFilter.SelectedValue != null && !String.IsNullOrWhiteSpace(lstDateFilter.SelectedValue.ToString()) && !lstDateFilter.SelectedValue.ToString().Equals("None"))
            {
                if (calBSStartDate.Value != null)
                {
                    this.BlastholesSampleFilter += " and " + lstDateFilter.SelectedValue.ToString() + " >= '" + calBSStartDate.Value.Month.ToString("00") + "-" + calBSStartDate.Value.Day.ToString("00") + "-" + calBSStartDate.Value.Year.ToString("0000") + "'";
                }
                if (calBSEndDate.Value != null)
                {
                    this.BlastholesSampleFilter += " and " + lstDateFilter.SelectedValue.ToString() + " <= '" + calBSEndDate.Value.Month.ToString("00") + "-" + calBSEndDate.Value.Day.ToString("00") + "-" + calBSEndDate.Value.Year.ToString("0000") + "'";
                }
            }
            if (String.Equals(this.BlastholesSampleFilter, "1 = 1"))
                this.BlastholesSampleFilter = "1 = 2";

            if (gcv_Hole_CollarsTableAdapter.Connection.State != ConnectionState.Open)
                gcv_Hole_CollarsTableAdapter.Connection.Open();

            gridBlastholes.SuspendBinding();
            if (this.gcv_Hole_CollarsTableAdapter.Adapter.SelectCommand == null)
                this.gcv_Hole_CollarsTableAdapter.Fill(this.samples.gcv_Hole_Collars);

            if (this.gcv_Hole_CollarsTableAdapter.Adapter.SelectCommand.CommandText.IndexOf(" WHERE ") >= 0)
                this.gcv_Hole_CollarsTableAdapter.Adapter.SelectCommand.CommandText = this.gcv_Hole_CollarsTableAdapter.Adapter.SelectCommand.CommandText.Substring(0, this.gcv_Hole_CollarsTableAdapter.Adapter.SelectCommand.CommandText.IndexOf(" WHERE "));

            this.gcv_Hole_CollarsTableAdapter.Adapter.SelectCommand.CommandText += " WHERE " + this.BlastholesSampleFilter;
            this.gcv_Hole_CollarsTableAdapter.Fill(this.samples.gcv_Hole_Collars);
            gridBlastholes.ResumeBinding();
            
            if (gridBlastholes.FirstRow >= 0 && gridBlastholes.FirstRow < this.samples.gcv_Hole_Collars.Count)
                gridBlastholes.FirstRow = curRowNo;

            gcv_Hole_CollarsTableAdapter.Connection.Close();

            gridBlastholes.SetDataBinding(this.samples.gcv_Hole_Collars, null);

            FilterPerformanceGraph();
        }


        private void FilterPerformanceGraph()
        {
            if (String.IsNullOrWhiteSpace(lstPerformanceDateFilter.Text))
            {
                chartPerformance.DataSource = null;
                chartPerformance.DataBind();
                return;
            }
            if (String.IsNullOrWhiteSpace(lstPerformanceYDateField.Text))
                lstPerformanceYDateField.Text = lstPerformanceDateFilter.Text;

            if (gcv_Hole_CollarsTableAdapter.Connection.State != ConnectionState.Open)
                gcv_Hole_CollarsTableAdapter.Connection.Open();

            chartPerformance.Series.Clear();
            String cmdSql = "select min(" + lstPerformanceDateFilter.Text + ") " + lstPerformanceDateFilter.Text + ",";
            if (lstPerformanceFunction.Text.Equals("Count"))
                cmdSql += " count(" + lstPerformanceYDateField.Text + ") [Count]";
            else
                cmdSql += " round(avg(datediff(hh," + lstPerformanceDateFilter.Text + "," + lstPerformanceYDateField.Text + ")/24.0),1) [Average Days]";

            if (radGaphBlastholes.Checked)
                cmdSql += " from gcv_Hole_Collars where (" + lstPerformanceDateFilter.Text + " is not null and " + lstPerformanceYDateField.Text + " is not null) and " + this.BlastholesSampleFilter + " group by year(" + lstPerformanceDateFilter.Text + "),month(" + lstPerformanceDateFilter.Text + "),day(" + lstPerformanceDateFilter.Text + ") order by " + lstPerformanceDateFilter.Text;
            else
                cmdSql += " from psm_Product_Samples  where (" + lstPerformanceDateFilter.Text + " is not null and " + lstPerformanceYDateField.Text + " is not null) and " + this.ProductSampleFilter + " group by year(" + lstPerformanceDateFilter.Text + "),month(" + lstPerformanceDateFilter.Text + "),day(" + lstPerformanceDateFilter.Text + ") order by " + lstPerformanceDateFilter.Text;

            SqlCommand cmd = new SqlCommand(cmdSql,gcv_Hole_CollarsTableAdapter.Connection);

            SqlDataReader reader = cmd.ExecuteReader();
            chartPerformance.DataBindTable(reader, lstPerformanceDateFilter.Text);

            reader.Close();

            chartPerformance.ChartAreas[0].AxisX.IsStartedFromZero = false;
            chartPerformance.Series[0].IsValueShownAsLabel = true;
            chartPerformance.Series[0].SmartLabelStyle.Enabled = true;
            chartPerformance.Series[0].ChartType = SeriesChartType.Column;
            chartPerformance.ChartAreas[0].AxisY.IntervalType = DateTimeIntervalType.Number;
            if (chartPerformance.Series[0].Points.Count > 5)
            {
                chartPerformance.DataManipulator.FinancialFormula(FinancialFormula.MovingAverage, "5", chartPerformance.Series[0].Name + ":Y", "Trending:Y");
                chartPerformance.Series[1].ChartType = SeriesChartType.Line;
            }
            gcv_Hole_CollarsTableAdapter.Connection.Close();
                       
            //chartPerformance.Series.Clear();
            //chartPerformance.Series.Add("Performance");
            //chartPerformance.Series["Performance"].XValueMember = lstPerformanceDateFilter.Text;
            //chartPerformance.Series["Performance"].YValueMembers = lstPerformanceYDateField.Text;
            //chartPerformance.Series["Performance"].ChartType = SeriesChartType.Line;
            //chartPerformance.DataBind();
        }

        private void lstPits_TextChanged(object sender, EventArgs e)
        {
            FilterBlastholesGrid();
            gridBlastholes.Focus();
        }

        private void lstBench_TextChanged(object sender, EventArgs e)
        {
            FilterBlastholesGrid();
            gridBlastholes.Focus();
        }

        private void lstShotNos_TextChanged(object sender, EventArgs e)
        {
            FilterBlastholesGrid();
            gridBlastholes.Focus();
        }

        private void lstSubmissions_TextChanged(object sender, EventArgs e)
        {
            FilterBlastholesGrid();
            gridBlastholes.Focus();
        }

        private void butSubmission_Click(object sender, EventArgs e)
        {
            if (gcv_Hole_CollarsTableAdapter.Connection.State != ConnectionState.Open)
                gcv_Hole_CollarsTableAdapter.Connection.Open();
            
            ManageSubmissionsDlg submissions = new ManageSubmissionsDlg { Connection = gcv_Hole_CollarsTableAdapter.Connection, SampleFilter = this.BlastholesSampleFilter };
            submissions.ShowDialog();

            UpdateBlastholeFilterLists();
            FilterBlastholesGrid();
            gridBlastholes.Focus();

            gcv_Hole_CollarsTableAdapter.Connection.Close();
        }


        private void butSampling_Click(object sender, EventArgs e)
        {
            try
            {
                if (gcv_Hole_CollarsTableAdapter.Connection.State != ConnectionState.Open)
                    gcv_Hole_CollarsTableAdapter.Connection.Open();

                SqlCommand cmd = new SqlCommand("select distinct Pit,Bench,ShotNo,HoleNo,SampleId,PlanEasting,PlanNorthing,PlanElevation from gcv_Hole_Collars where SampleId is null and SelectedOn is not null and " + this.BlastholesSampleFilter + " order by Pit,Bench,ShotNo,HoleNo", gcv_Hole_CollarsTableAdapter.Connection);
                SqlDataReader reader = cmd.ExecuteReader();
                List<Sample> samples = new List<Sample>();
                String shotName = null;
                String prevShotName = null;
                Boolean multipleShots = false;
                while (reader.Read())
                {
                    Sample sample = new Sample { Pit = reader[0].ToString(), Bench = reader[1].ToString(), ShotNo = reader[2].ToString(), HoleNo = reader[3].ToString(), SampleId = reader[4].ToString(), East = reader.GetDouble(5), North = reader.GetDouble(6), Elevation = reader.GetDouble(7) };
                    samples.Add(sample);

                    shotName = sample.Pit + "_" + sample.Bench + "_" + sample.ShotNo;
                    if (prevShotName != null && !String.Equals(prevShotName, shotName))
                        multipleShots = true;
                    prevShotName = shotName;
                }
                reader.Close();

                if (samples.Count == 0)
                {
                    MessageBox.Show("You must display the holes to export and ensure that the are selected for sampling.", "Error", MessageBoxButtons.OK);
                    return;
                }
                if (multipleShots)
                {
                    MessageBox.Show("You can only export one shot for sampling at one time.", "Error", MessageBoxButtons.OK);
                    return;
                }

                Submission submission = new Submission { Company = Properties.Settings.Default.AtlasCompany, Operation = Properties.Settings.Default.AtlasOperation, Laboratory = Properties.Settings.Default.Laboratory, Name = lstSubmissions.Text, FileName = "", AssaySamples = samples, SubmissionType = Submission.SubmissionSampleType.Shot };
                submission.Name = shotName;
                submission.WriteSubmission(Properties.Settings.Default.BSPendingShotSampling, true);
                
                gcv_Hole_CollarsTableAdapter.Connection.Close();

                Process.Start(Properties.Settings.Default.BSPendingShotSampling);
                
            }
            catch (System.Exception exc)
            {
                MessageBox.Show("Unable to save the shot for sampling. Internal message [" + exc.Message + "]", "Error", MessageBoxButtons.OK);
            }
        }

        private void gridBlastholes_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            GridEXColumn colClicked = gridBlastholes.ColumnFromPoint(e.X, e.Y);
            int rowClicked = gridBlastholes.RowPositionFromPoint(e.X, e.Y);
            if (colClicked != null && rowClicked >= 0 && colClicked.Key.Equals(SampledOnColumn))
            {
                GridEXRow row = gridBlastholes.GetRow(rowClicked);
                GridEXCell cell = row.Cells[colClicked];

                AtlasSampleManager.Samples.gcv_Hole_CollarsRow blasthole = (row.DataRow as DataRowView).Row as AtlasSampleManager.Samples.gcv_Hole_CollarsRow;
                if (blasthole.IsSelectedOnNull())
                    blasthole.SelectedOn = DateTime.Now;
                else
                    blasthole.SetSelectedOnNull();

                try
                {
                    if (gcv_Hole_CollarsTableAdapter.Connection.State != ConnectionState.Open)
                        gcv_Hole_CollarsTableAdapter.Connection.Open();
                    gcv_Hole_CollarsTableAdapter.Update(this.samples.gcv_Hole_Collars);
                    gcv_Hole_CollarsTableAdapter.Connection.Close();
                }
                catch (SystemException exc)
                {
                    MessageBox.Show(exc.Message);
                }
                gridBlastholes.Refresh();
            }
        }

        private void butChainOfCustody_Click(object sender, EventArgs e)
        {
            gridBlastholes.RootTable.Columns["SelectedOn"].Visible = !gridBlastholes.RootTable.Columns["SelectedOn"].Visible;
            gridBlastholes.RootTable.Columns["SampleId"].Visible = !gridBlastholes.RootTable.Columns["SampleId"].Visible;
            gridBlastholes.RootTable.Columns["SampledOn"].Visible = !gridBlastholes.RootTable.Columns["SampledOn"].Visible;
            gridBlastholes.RootTable.Columns["SampledUser"].Visible = !gridBlastholes.RootTable.Columns["SampledUser"].Visible;
            gridBlastholes.RootTable.Columns["SubmittedOn"].Visible = !gridBlastholes.RootTable.Columns["SubmittedOn"].Visible;
            gridBlastholes.RootTable.Columns["SubmittedUser"].Visible = !gridBlastholes.RootTable.Columns["SubmittedUser"].Visible;
            gridBlastholes.RootTable.Columns["DespatchedOn"].Visible = !gridBlastholes.RootTable.Columns["DespatchedOn"].Visible;
            gridBlastholes.RootTable.Columns["DespatchedUser"].Visible = !gridBlastholes.RootTable.Columns["DespatchedUser"].Visible;
            gridBlastholes.RootTable.Columns["ArrivedForPreparation"].Visible = !gridBlastholes.RootTable.Columns["ArrivedForPreparation"].Visible;
            gridBlastholes.RootTable.Columns["ArrivedForAnalysis"].Visible = !gridBlastholes.RootTable.Columns["ArrivedForAnalysis"].Visible;
            gridBlastholes.RootTable.Columns["ReceivedOn"].Visible = !gridBlastholes.RootTable.Columns["ReceivedOn"].Visible;
            gridBlastholes.RootTable.Columns["Submission"].Visible = gridBlastholes.RootTable.Columns["LabJobNo"].Visible || gridBlastholes.RootTable.Columns["SelectedOn"].Visible;
        }

        private void butLogging_Click(object sender, EventArgs e)
        {
            gridBlastholes.RootTable.Columns["LogMatType"].Visible = !gridBlastholes.RootTable.Columns["LogMatType"].Visible;
            gridBlastholes.RootTable.Columns["LogOtherMatType"].Visible = !gridBlastholes.RootTable.Columns["LogOtherMatType"].Visible;
            gridBlastholes.RootTable.Columns["LogTopMatType"].Visible = !gridBlastholes.RootTable.Columns["LogTopMatType"].Visible;
            gridBlastholes.RootTable.Columns["LogBottomMatType"].Visible = !gridBlastholes.RootTable.Columns["LogBottomMatType"].Visible;
            gridBlastholes.RootTable.Columns["LogTopColour"].Visible = !gridBlastholes.RootTable.Columns["LogTopColour"].Visible;
            gridBlastholes.RootTable.Columns["LogBottomColour"].Visible = !gridBlastholes.RootTable.Columns["LogBottomColour"].Visible;
            gridBlastholes.RootTable.Columns["LogReference"].Visible = !gridBlastholes.RootTable.Columns["LogReference"].Visible;
            gridBlastholes.RootTable.Columns["LogComments"].Visible = !gridBlastholes.RootTable.Columns["LogComments"].Visible;
            gridBlastholes.RootTable.Columns["LoggedOn"].Visible = !gridBlastholes.RootTable.Columns["LoggedOn"].Visible;
            gridBlastholes.RootTable.Columns["LogValidatedOn"].Visible = !gridBlastholes.RootTable.Columns["LogValidatedOn"].Visible;
            gridBlastholes.RootTable.Columns["LoggedUser"].Visible = !gridBlastholes.RootTable.Columns["LoggedUser"].Visible;
        }

        private void butAssays_Click(object sender, EventArgs e)
        {
            gridBlastholes.RootTable.Columns["Fe"].Visible = !gridBlastholes.RootTable.Columns["Fe"].Visible;
            gridBlastholes.RootTable.Columns["Al2O3"].Visible = !gridBlastholes.RootTable.Columns["Al2O3"].Visible;
            gridBlastholes.RootTable.Columns["SiO2"].Visible = !gridBlastholes.RootTable.Columns["SiO2"].Visible;
            gridBlastholes.RootTable.Columns["P"].Visible = !gridBlastholes.RootTable.Columns["P"].Visible;
            gridBlastholes.RootTable.Columns["S"].Visible = !gridBlastholes.RootTable.Columns["S"].Visible;
            gridBlastholes.RootTable.Columns["Mn"].Visible = !gridBlastholes.RootTable.Columns["Mn"].Visible;
            gridBlastholes.RootTable.Columns["LOI"].Visible = !gridBlastholes.RootTable.Columns["LOI"].Visible;
            gridBlastholes.RootTable.Columns["LabJobNo"].Visible = !gridBlastholes.RootTable.Columns["LabJobNo"].Visible;
            gridBlastholes.RootTable.Columns["Submission"].Visible = gridBlastholes.RootTable.Columns["LabJobNo"].Visible || gridBlastholes.RootTable.Columns["SelectedOn"].Visible;
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            gridBlastholes.RootTable.Columns["Pit"].Visible = !gridBlastholes.RootTable.Columns["Pit"].Visible;
            gridBlastholes.RootTable.Columns["Bench"].Visible = !gridBlastholes.RootTable.Columns["Bench"].Visible;
            gridBlastholes.RootTable.Columns["ShotNo"].Visible = !gridBlastholes.RootTable.Columns["ShotNo"].Visible;
        }

        private void butRefresh_Click(object sender, EventArgs e)
        {
            UpdateBlastholeFilterLists();
            FilterBlastholesGrid();
            UpdateProductSamplesFilterLists();
            FilterProductSamplesGrid();
        }

        private void butImportAssays_Click(object sender, EventArgs e)
        {
            if (gcv_Hole_CollarsTableAdapter.Connection.State != ConnectionState.Open)
                gcv_Hole_CollarsTableAdapter.Connection.Open();

            ImportAssaysDlg import = new ImportAssaysDlg() { Connection = gcv_Hole_CollarsTableAdapter.Connection };
            import.SubmissionType = Submission.SubmissionSampleType.Blastholes;
            import.ResultsDirectory = Properties.Settings.Default.BSResultsDirectory;
            import.AcceptedResultsDirectory = Properties.Settings.Default.BSAcceptedResultsDirectory;
            import.ShowDialog();
            
            gcv_Hole_CollarsTableAdapter.Connection.Close();

            UpdateBlastholeFilterLists();
            FilterBlastholesGrid();
        }

        private void butConfigure_Click(object sender, EventArgs e)
        {
            ConfigureSettingsDlg config = new ConfigureSettingsDlg();
            config.ShowDialog();
            MessageBox.Show("You must restart sample manager before any configuration changes have an effect.", "Sample Management", MessageBoxButtons.OK);
            this.Close();
        }

        private void calStartDate_ValueChanged(object sender, EventArgs e)
        {
            if (calEndDate.Value.Year == calStartDate.Value.Year && calEndDate.Value.Month == calStartDate.Value.Month && calEndDate.Value.Day == calStartDate.Value.Day)
            {
                calStartDate.Value = calEndDate.Value.AddDays(-1);
                MessageBox.Show("The start and end dates cannot be the same.", "Error", MessageBoxButtons.OK);
                return;
            }

            if (calEndDate.Value <= calStartDate.Value)
                calEndDate.Value = calStartDate.Value.AddDays(1);

            gridProductSamples.RootTable.Columns["SampledOn"].DefaultValue = new DateTime(calStartDate.Value.Year, calStartDate.Value.Month, calStartDate.Value.Day, 6, 0, 0);
            gridProductSamples.RootTable.Columns["SampleDate"].DefaultValue = new DateTime(calStartDate.Value.Year, calStartDate.Value.Month, calStartDate.Value.Day, 6, 0, 0);
            gridProductSamples.RootTable.Columns["SampleTime"].DefaultValue = "06:00";

            FilterProductSamplesGrid();
            gridProductSamples.Focus();
        }

        private void calEndDate_ValueChanged(object sender, EventArgs e)
        {
            if (calEndDate.Value.Year == calStartDate.Value.Year && calEndDate.Value.Month == calStartDate.Value.Month && calEndDate.Value.Day == calStartDate.Value.Day)
            {
                calEndDate.Value = calStartDate.Value.AddDays(1);
                MessageBox.Show("The start and end dates cannot be the same.", "Error", MessageBoxButtons.OK);
                return;
            }

            FilterProductSamplesGrid();
            gridProductSamples.Focus();
        }

        private void butProductId_Click(object sender, EventArgs e)
        {
//            gridProductSamples.RootTable.Columns["Shift"].Visible = !gridProductSamples.RootTable.Columns["Shift"].Visible;
            gridProductSamples.RootTable.Columns["Weightometer"].Visible = !gridProductSamples.RootTable.Columns["Weightometer"].Visible;
//            gridProductSamples.RootTable.Columns["SampleId"].Visible = !gridProductSamples.RootTable.Columns["SampleId"].Visible;
            gridProductSamples.RootTable.Columns["SampleBucket"].Visible = !gridProductSamples.RootTable.Columns["SampleBucket"].Visible;
            gridProductSamples.RootTable.Columns["StackerPosition"].Visible = !gridProductSamples.RootTable.Columns["StackerPosition"].Visible;
            gridProductSamples.RootTable.Columns["SampledOn"].Visible = !gridProductSamples.RootTable.Columns["SampledOn"].Visible;
            gridProductSamples.RootTable.Columns["SampleDate"].Visible = !gridProductSamples.RootTable.Columns["SampleDate"].Visible;
            gridProductSamples.RootTable.Columns["SampleTime"].Visible = !gridProductSamples.RootTable.Columns["SampleTime"].Visible;
            gridProductSamples.RootTable.Columns["Tonnes"].Visible = !gridProductSamples.RootTable.Columns["Tonnes"].Visible;
            //gridProductSamples.RootTable.Columns["PrimaryFrom"].Visible = !gridProductSamples.RootTable.Columns["PrimaryFrom"].Visible;
            gridProductSamples.RootTable.Columns["PrimaryScoops"].Visible = !gridProductSamples.RootTable.Columns["PrimaryScoops"].Visible;
            //gridProductSamples.RootTable.Columns["SecondaryFrom"].Visible = !gridProductSamples.RootTable.Columns["SecondaryFrom"].Visible;
            gridProductSamples.RootTable.Columns["SecondaryScoops"].Visible = !gridProductSamples.RootTable.Columns["SecondaryScoops"].Visible;
            gridProductSamples.RootTable.Columns["Finger3Scoops"].Visible = !gridProductSamples.RootTable.Columns["Finger3Scoops"].Visible;
            gridProductSamples.RootTable.Columns["Finger4Scoops"].Visible = !gridProductSamples.RootTable.Columns["Finger4Scoops"].Visible;
            gridProductSamples.RootTable.Columns["Finger5Scoops"].Visible = !gridProductSamples.RootTable.Columns["Finger5Scoops"].Visible;
            gridProductSamples.RootTable.Columns["Finger6Scoops"].Visible = !gridProductSamples.RootTable.Columns["Finger6Scoops"].Visible;
            //gridProductSamples.RootTable.Columns["To"].Visible = !gridProductSamples.RootTable.Columns["To"].Visible;
            gridProductSamples.RootTable.Columns["Comments"].Visible = !gridProductSamples.RootTable.Columns["Comments"].Visible;
        }

        private void butProductChainOfCustofy_Click(object sender, EventArgs e)
        {
            gridProductSamples.RootTable.Columns["Instructions"].Visible = !gridProductSamples.RootTable.Columns["Instructions"].Visible;
            gridProductSamples.RootTable.Columns["SubmittedOn"].Visible = !gridProductSamples.RootTable.Columns["SubmittedOn"].Visible;
            gridProductSamples.RootTable.Columns["SubmittedUser"].Visible = !gridProductSamples.RootTable.Columns["SubmittedUser"].Visible;
            gridProductSamples.RootTable.Columns["DespatchedOn"].Visible = !gridProductSamples.RootTable.Columns["DespatchedOn"].Visible;
            gridProductSamples.RootTable.Columns["DespatchedUser"].Visible = !gridProductSamples.RootTable.Columns["DespatchedUser"].Visible;
            gridProductSamples.RootTable.Columns["ArrivedForPreparation"].Visible = !gridProductSamples.RootTable.Columns["ArrivedForPreparation"].Visible;
            gridProductSamples.RootTable.Columns["ArrivedForAnalysis"].Visible = !gridProductSamples.RootTable.Columns["ArrivedForAnalysis"].Visible;
            gridProductSamples.RootTable.Columns["ReceivedOn"].Visible = !gridProductSamples.RootTable.Columns["ReceivedOn"].Visible;
            gridProductSamples.RootTable.Columns["Submission"].Visible = (gridProductSamples.RootTable.Columns["LabJobNo"].Visible || gridProductSamples.RootTable.Columns["Moisture"].Visible || gridProductSamples.RootTable.Columns["Instructions"].Visible);
        }

        private void butProductAssays_Click(object sender, EventArgs e)
        {
            gridProductSamples.RootTable.Columns["Fe"].Visible = !gridProductSamples.RootTable.Columns["Fe"].Visible;
            gridProductSamples.RootTable.Columns["Al2O3"].Visible = !gridProductSamples.RootTable.Columns["Al2O3"].Visible;
            gridProductSamples.RootTable.Columns["SiO2"].Visible = !gridProductSamples.RootTable.Columns["SiO2"].Visible;
            gridProductSamples.RootTable.Columns["P"].Visible = !gridProductSamples.RootTable.Columns["P"].Visible;
            gridProductSamples.RootTable.Columns["S"].Visible = !gridProductSamples.RootTable.Columns["S"].Visible;
            gridProductSamples.RootTable.Columns["Mn"].Visible = !gridProductSamples.RootTable.Columns["Mn"].Visible;
            gridProductSamples.RootTable.Columns["LOI"].Visible = !gridProductSamples.RootTable.Columns["LOI"].Visible;
            gridProductSamples.RootTable.Columns["LabJobNo"].Visible = !gridProductSamples.RootTable.Columns["LabJobNo"].Visible;
            gridProductSamples.RootTable.Columns["Submission"].Visible = (gridProductSamples.RootTable.Columns["LabJobNo"].Visible || gridProductSamples.RootTable.Columns["Moisture"].Visible || gridProductSamples.RootTable.Columns["Instructions"].Visible);
        }

        private void butSizings_Click(object sender, EventArgs e)
        {
            gridProductSamples.RootTable.Columns["TotalMass"].Visible = !gridProductSamples.RootTable.Columns["TotalMass"].Visible;
            gridProductSamples.RootTable.Columns["12_5mm"].Visible = !gridProductSamples.RootTable.Columns["12_5mm"].Visible;
            gridProductSamples.RootTable.Columns["10_0_12_5mm"].Visible = !gridProductSamples.RootTable.Columns["10_0_12_5mm"].Visible;
            gridProductSamples.RootTable.Columns["9_5_10_0mm"].Visible = !gridProductSamples.RootTable.Columns["9_5_10_0mm"].Visible;
            gridProductSamples.RootTable.Columns["6_7_9_5mm"].Visible = !gridProductSamples.RootTable.Columns["6_7_9_5mm"].Visible;
            gridProductSamples.RootTable.Columns["0_5_6_7mm"].Visible = !gridProductSamples.RootTable.Columns["0_5_6_7mm"].Visible;
            gridProductSamples.RootTable.Columns["0_0_0_5mm"].Visible = !gridProductSamples.RootTable.Columns["0_0_0_5mm"].Visible;

            gridProductSamples.RootTable.Columns["40mm"].Visible = !gridProductSamples.RootTable.Columns["40mm"].Visible;
            gridProductSamples.RootTable.Columns["6_3_40mm"].Visible = !gridProductSamples.RootTable.Columns["6_3_40mm"].Visible;
            gridProductSamples.RootTable.Columns["0_0_6_3mm"].Visible = !gridProductSamples.RootTable.Columns["0_0_6_3mm"].Visible;

            gridProductSamples.RootTable.Columns["Moisture"].Visible = !gridProductSamples.RootTable.Columns["Moisture"].Visible;
            gridProductSamples.RootTable.Columns["Submission"].Visible = (gridProductSamples.RootTable.Columns["LabJobNo"].Visible || gridProductSamples.RootTable.Columns["Moisture"].Visible || gridProductSamples.RootTable.Columns["Instructions"].Visible);
        }

        private void butProductSubmissions_Click(object sender, EventArgs e)
        {
            if (psm_Product_SamplesTableAdapter.Connection.State != ConnectionState.Open)
                psm_Product_SamplesTableAdapter.Connection.Open();

            ManageSubmissionsDlg submissions = new ManageSubmissionsDlg { Connection = psm_Product_SamplesTableAdapter.Connection, SampleFilter = this.ProductSampleFilter, SubmissionType = Submission.SubmissionSampleType.Product };
            submissions.ShowDialog();
            UpdateProductSamplesFilterLists();
            FilterProductSamplesGrid();

            psm_Product_SamplesTableAdapter.Connection.Close();
        }

        private void butImportProductSizings_Click(object sender, EventArgs e)
        {
            if (psm_Product_SamplesTableAdapter.Connection.State != ConnectionState.Open)
                psm_Product_SamplesTableAdapter.Connection.Open();

            ImportSizingsDlg import = new ImportSizingsDlg() { Connection = psm_Product_SamplesTableAdapter.Connection };
            import.SubmissionType = Submission.SubmissionSampleType.Product;
            import.ResultsDirectory = Properties.Settings.Default.PSResultsDirectory;
            import.AcceptedResultsDirectory = Properties.Settings.Default.PSAcceptedResultsDirectory;
            import.ShowDialog();

            psm_Product_SamplesTableAdapter.Connection.Close();

            UpdateProductSamplesFilterLists();
            FilterProductSamplesGrid();

        }

        private void butImportProductAssays_Click(object sender, EventArgs e)
        {
            if (psm_Product_SamplesTableAdapter.Connection.State != ConnectionState.Open)
                psm_Product_SamplesTableAdapter.Connection.Open();

            ImportAssaysDlg import = new ImportAssaysDlg() { Connection = psm_Product_SamplesTableAdapter.Connection };
            import.SubmissionType = Submission.SubmissionSampleType.Product;
            import.ResultsDirectory = Properties.Settings.Default.PSResultsDirectory;
            import.AcceptedResultsDirectory = Properties.Settings.Default.PSAcceptedResultsDirectory;
            import.ShowDialog();

            psm_Product_SamplesTableAdapter.Connection.Close();

            UpdateProductSamplesFilterLists();
            FilterProductSamplesGrid();
        }


        private void UpdateProductSamplesFilterLists()
        {
            if (psm_Product_SamplesTableAdapter.Connection.State != ConnectionState.Open)
                psm_Product_SamplesTableAdapter.Connection.Open();

            SqlCommand cmd = new SqlCommand("select distinct Submission from psm_Product_Samples order by Submission desc", psm_Product_SamplesTableAdapter.Connection);
            SqlDataReader reader = cmd.ExecuteReader();
            lstProductSubmissions.Items.Clear();
            lstProductSubmissions.Items.Add(new Janus.Windows.EditControls.UIComboBoxItem(null));
            while (reader.Read())
            {
                String submission = (reader[0] == DBNull.Value ? null : reader[0].ToString());
                if (!String.IsNullOrWhiteSpace(submission))
                    lstProductSubmissions.Items.Add(new Janus.Windows.EditControls.UIComboBoxItem(submission,submission));
            }
            reader.Close();

            psm_Product_SamplesTableAdapter.Connection.Close();
        }

        private void FilterProductSamplesGrid(Boolean sortBySampleDate=true)
        {
            if (psm_Product_SamplesTableAdapter.Connection.State != ConnectionState.Open)
                psm_Product_SamplesTableAdapter.Connection.Open();

            int curRowNo = gridProductSamples.FirstRow;

            if (this.psm_Product_SamplesTableAdapter.Adapter.SelectCommand != null)
                this.psm_Product_SamplesTableAdapter.Adapter.SelectCommand.Parameters.Clear();

            this.ProductSampleFilter = "(1 = 1";
            if (calStartDate.Value != null)
            {
                this.ProductSampleFilter += " and SampledOn >= '" + calStartDate.Value.Month.ToString("00") + "-" + calStartDate.Value.Day.ToString("00") + "-" + calStartDate.Value.Year.ToString("0000") + " " + calStartDate.Value.Hour.ToString("00") + ":" + calStartDate.Value.Minute.ToString("00") + "'";
                //if (this.psm_Product_SamplesTableAdapter.Adapter.SelectCommand != null)
                //{
                //    this.ProductSampleFilter += " and SampledOn >= @startDate";
                //    this.psm_Product_SamplesTableAdapter.Adapter.SelectCommand.Parameters.Add(new SqlParameter("@startDate", calStartDate.Value));
                //}
            }
            if (calEndDate.Value != null)
            {
                this.ProductSampleFilter += " and SampledOn < '" + calEndDate.Value.Month.ToString("00") + "-" + calEndDate.Value.Day.ToString("00") + "-" + calEndDate.Value.Year.ToString("0000") + " " + calEndDate.Value.Hour.ToString("00") + ":" + calEndDate.Value.Minute.ToString("00") + "'";
                //if (this.psm_Product_SamplesTableAdapter.Adapter.SelectCommand != null)
                //{
                //    this.ProductSampleFilter += " and SampledOn >= @endDate";
                //    this.psm_Product_SamplesTableAdapter.Adapter.SelectCommand.Parameters.Add(new SqlParameter("@endDate", calStartDate.Value));
                //}
            }
            if (lstCrushers.SelectedValue != null && !String.IsNullOrWhiteSpace(lstCrushers.SelectedValue.ToString()))
            {
                this.ProductSampleFilter += " and Crusher = '" + lstCrushers.SelectedValue.ToString() + "'";
            }
            if (lstProductSubmissions.SelectedValue != null && !String.IsNullOrWhiteSpace(lstProductSubmissions.SelectedValue.ToString()))
            {
                this.ProductSampleFilter += " and Submission = '" + lstProductSubmissions.SelectedValue.ToString() + "'";
            }
            if (String.Equals(this.ProductSampleFilter, "( 1 = 1"))
                this.ProductSampleFilter = "( 1 = 2";
            this.ProductSampleFilter += ") or SampledOn is null";

            if (psm_Product_SamplesTableAdapter.Connection.State != ConnectionState.Open)
                psm_Product_SamplesTableAdapter.Connection.Open();

            gridProductSamples.SuspendBinding();
            if (this.psm_Product_SamplesTableAdapter.Adapter.SelectCommand == null)
                this.psm_Product_SamplesTableAdapter.Fill(this.samples.psm_Product_Samples);

            if (this.psm_Product_SamplesTableAdapter.Adapter.SelectCommand.CommandText.IndexOf(" WHERE ") >= 0)
                this.psm_Product_SamplesTableAdapter.Adapter.SelectCommand.CommandText = this.psm_Product_SamplesTableAdapter.Adapter.SelectCommand.CommandText.Substring(0, this.psm_Product_SamplesTableAdapter.Adapter.SelectCommand.CommandText.IndexOf(" WHERE "));

            this.psm_Product_SamplesTableAdapter.Adapter.SelectCommand.CommandText += " WHERE " + this.ProductSampleFilter;
            this.psm_Product_SamplesTableAdapter.Fill(this.samples.psm_Product_Samples);

            gridProductSamples.ResumeBinding();

            if (gridProductSamples.FirstRow >= 0 && gridProductSamples.FirstRow < this.samples.psm_Product_Samples.Count)
                gridProductSamples.FirstRow = curRowNo;

            gridProductSamples.RootTable.SortKeys.Clear();
            gridProductSamples.RootTable.SortKeys.Add("SampledOn", Janus.Windows.GridEX.SortOrder.Ascending);
            gridProductSamples.SetDataBinding(this.samples.psm_Product_Samples, null);

            FilterPerformanceGraph();
        }

        private void gridProductSamples_UpdatingRecord(object sender, CancelEventArgs e)
        {
            e.Cancel = false;

            GridEXRow row = gridProductSamples.GetRow();

            DateTime newSsampledOn = row.Cells["SampledOn"].Value == null || !(row.Cells["SampledOn"].Value is DateTime) ? new DateTime(1900, 1, 1) : (DateTime)row.Cells["SampledOn"].Value;
            if (gridProductSamples.GetValue("SampleDate") is DateTime)
            {
                DateTime sampledOnDate = (DateTime)gridProductSamples.GetValue("SampleDate");
                try
                {
                    Object v = gridProductSamples.GetValue("SampleTime");
                    DateTime sampledOnTime;
                    if (v != null && DateTime.TryParse(v.ToString(), out sampledOnTime))
                        newSsampledOn = new DateTime(sampledOnDate.Year, sampledOnDate.Month, sampledOnDate.Day, sampledOnTime.Hour, sampledOnTime.Minute, 0);
                }
                catch (System.Exception exc)
                {
                }
            }
            if (newSsampledOn < calStartDate.Value || newSsampledOn > calEndDate.Value)
            {
                MessageBox.Show("You must enter a \"Sampled on\" date that is between the filter start and dates.", "Error", MessageBoxButtons.OK);
                e.Cancel = true;
                return;
            }
            int newSampleIdLength = row.Cells["SampleId"].Value == null ? 0 : row.Cells["SampleId"].Value.ToString().Length;
            if (newSampleIdLength == 0)
            {
                int newCommentsLength = row.Cells["Comments"].Value == null ? 0 : row.Cells["Comments"].Value.ToString().Length;
                if (newCommentsLength == 0)
                {
                    MessageBox.Show("You must enter a sample id or a comment to exlain why it is blank.", "Error", MessageBoxButtons.OK);
                    e.Cancel = true;
                    return;
                }
            }

            int newCrusherLength = row.Cells["Crusher"].Value == null ? 0 : row.Cells["Crusher"].Value.ToString().Length;
            if (newCrusherLength == 0)
            {
                MessageBox.Show("You must enter a crusher.", "Error", MessageBoxButtons.OK);
                e.Cancel = true;
                return;
            }

            //int newSampleBucket = row.Cells["SampleBucket"].Value == null ? 0 : row.Cells["SampleBucket"].Value.ToString().Length;
            //if (newSampleBucket == 0)
            //{
            //    MessageBox.Show("You must enter a sample bucket.", "Error", MessageBoxButtons.OK);
            //    e.Cancel = true;
            //    return;
            //}

            //int newFromLength = row.Cells["PrimaryFrom"].Value == null ? 0 : row.Cells["PrimaryFrom"].Value.ToString().Length;
            //if (newFromLength == 0)
            //{
            //    MessageBox.Show("You must enter a primary finger.", "Error", MessageBoxButtons.OK);
            //    e.Cancel = true;
            //    return;
            //}

            //int newToLength = row.Cells["To"].Value == null ? 0 : row.Cells["To"].Value.ToString().Length;
            //if (newToLength == 0)
            //{
            //    MessageBox.Show("You must enter a product stockpile.", "Error", MessageBoxButtons.OK);
            //    e.Cancel = true;
            //    return;
            //}

            int newShiftLength = row.Cells["Shift"].Value == null ? 0 : row.Cells["Shift"].Value.ToString().Length;
            //int newWeightometerLength = row.Cells["Weightometer"].Text == null ? 0 : row.Cells["Weightometer"].Text.ToString().Length;
            Boolean hasSampledOn = (row.Cells["SampledOn"].Value is DateTime);

            if (!hasSampledOn)
            {
                MessageBox.Show("You must enter the sampling date/time.", "Error", MessageBoxButtons.OK);
                e.Cancel = true;
                return;
            }

            int rowIndex = row.Position < 0 ? gridProductSamples.GetRows().Count() : row.Position;
            if (rowIndex >= 1)
            {
                DateTime prevTime = gridProductSamples.GetRow(rowIndex - 1).Cells["SampledOn"].Value == null ? new DateTime(1900,1,1) : (DateTime)gridProductSamples.GetRow(rowIndex - 1).Cells["SampledOn"].Value;
                if (newSsampledOn < prevTime)
                {
                    MessageBox.Show("WARNING: You should enter a sample date that is greater than the previous sample date.", "Error", MessageBoxButtons.OK);
                }
            }

            if (newShiftLength >  0)
            {
                if (newShiftLength == 0)
                {
                    MessageBox.Show("You must enter a shift.", "Error", MessageBoxButtons.OK);
                    e.Cancel = true;
                    return;
                }

                String shift = row.Cells["Shift"].Value == null ? "" : row.Cells["Shift"].Value.ToString().ToString();
                if (shift == "Day" && (newSsampledOn.Hour < 6 || newSsampledOn.Hour >= 18))
                {
                    MessageBox.Show("You must enter a shift that matches the sample time.", "Error", MessageBoxButtons.OK);
                    e.Cancel = true;
                    return;
                }
                if (shift == "Night" && (newSsampledOn.Hour >= 6 && newSsampledOn.Hour < 18))
                {
                    MessageBox.Show("You must enter a shift that matches the sample time.", "Error", MessageBoxButtons.OK);
                    e.Cancel = true;
                    return;
                }
            }


            //if (newWeightometerLength > 0)
            //{
            //    if (newWeightometerLength == 0)
            //    {
            //        MessageBox.Show("You must enter a weightometer reading.", "Error", MessageBoxButtons.OK);
            //        e.Cancel = true;
            //        return;
            //    }
            //    if (rowIndex >= 1)
            //    {
            //        double weightometer = row.Cells["Weightometer"].Value == null ? 0.0 : (double)row.Cells["Weightometer"].Value;
            //        double prevWeightometer = gridProductSamples.GetRow(rowIndex - 1).Cells["Weightometer"].Value == null ? 0.0 : (double)gridProductSamples.GetRow(rowIndex - 1).Cells["Weightometer"].Value;
            //        if (weightometer < prevWeightometer)
            //        {
            //            MessageBox.Show("WARNING: You should enter a weightometer reading that is greater than the previous reading.", "Error", MessageBoxButtons.OK);
            //        }
            //    }
            //}

            String sampleId = row.Cells["SampleId"].Value.ToString();

            if (psm_Product_SamplesTableAdapter.Connection.State != ConnectionState.Open)
                psm_Product_SamplesTableAdapter.Connection.Open();


            if (sampleId.Equals(PlantWeightmeterMaterialChange, StringComparison.OrdinalIgnoreCase) || String.IsNullOrWhiteSpace(sampleId))
            {
                row.Cells["SampleId"].Value = GetNewChangeStockpileSampleId(Properties.Settings.Default.PSChangeStockpileSampleType);
            }
            else if (sampleId.Equals(PlantWeightmeterStartOfShift, StringComparison.OrdinalIgnoreCase) || String.IsNullOrWhiteSpace(sampleId))
            {
                row.Cells["SampleId"].Value = GetNewChangeStockpileSampleId(Properties.Settings.Default.PSSttartOfShiftStockpileSampleType);
            }
            else if (((!sampleId.Substring(0, 3).Equals(Properties.Settings.Default.AtlasOperation)) && !(Properties.Settings.Default.AtlasOperation.Equals("WEB") && sampleId.Substring(0, 3).Equals("DOV"))) ||
                (!sampleId.Substring(3, 2).Equals("PS") && !sampleId.Substring(3, 2).Equals("LS") && !sampleId.Substring(3, 2).Equals("CS")) ||
                sampleId.Length != 10)
            {
                MessageBox.Show("The sample id <" + sampleId + "> does not have the correct format (e.g. " + Properties.Settings.Default.AtlasOperation + "PSNNNNN).", "Error", MessageBoxButtons.OK);
                e.Cancel = true;
            }
            //if (sampleId.Equals(PlantWeightmeterEndOfShift, StringComparison.OrdinalIgnoreCase) || String.IsNullOrWhiteSpace(sampleId))
            //{
            //    row.Cells["SampleId"].Value = GetNewChangeStockpileSampleId(Properties.Settings.Default.PSEndOfShiftStockpileSampleType);
            //}

            SqlCommand cmd;
            SqlDataReader reader;

            if (!String.IsNullOrWhiteSpace(sampleId))
            {
                DataRowView dataRowView = row.DataRow as DataRowView;
                DateTime origSampledOn = new DateTime(1900, 1, 1);
                if (dataRowView.Row["SampledOn"] != DBNull.Value)
                    origSampledOn = (DateTime)dataRowView.Row["SampledOn"];

                cmd = new SqlCommand("select SampledOn from psm_Product_Samples where SampleId = @1 and not (SampledOn = @2)", psm_Product_SamplesTableAdapter.Connection);
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@1", Value = sampleId });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@2", Value = origSampledOn });
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    MessageBox.Show("WARNING: The sample id <" + sampleId + "> may have been already assigned to another sample taken on  <" + reader[0].ToString() + ">", "Error", MessageBoxButtons.OK);
                    e.Cancel = true;
                    break;
                }
                reader.Close();

                cmd = new SqlCommand("select SampledOn from psm_Product_Samples where not (SampleId = @1) and (SampledOn = @2 and SampledOn <> @3)", psm_Product_SamplesTableAdapter.Connection);
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@1", Value = sampleId });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@2", Value = newSsampledOn });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@3", Value = origSampledOn });
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    MessageBox.Show("WARNING: Another sample  <" + sampleId + ">  may have already been taken at the specified date and time.", "Error", MessageBoxButtons.OK);
                    break;
                }
                reader.Close();
            }
            psm_Product_SamplesTableAdapter.Connection.Close();

            if (!e.Cancel)
            {
                row.Cells["SampledOn"].Value = newSsampledOn;

                gridProductSamples.Refresh();
            }
        }

        private String GetNewChangeStockpileSampleId(String sampleCode)
        {
            String sampleId = null;
            SqlCommand cmd = new SqlCommand("select max(cast(substring(SampleId,6,20) as int))+1 from psm_Product_Samples where SampleId like @1", psm_Product_SamplesTableAdapter.Connection);
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@1", Value = Properties.Settings.Default.AtlasOperation + sampleCode + "%" });
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                if (reader.IsDBNull(0))
                    sampleId = Properties.Settings.Default.AtlasOperation + sampleCode + "000001";
                else
                {
                    int maxId = reader.GetInt32(0);
                    if (maxId > 99999)
                        sampleId = Properties.Settings.Default.AtlasOperation + sampleCode + maxId.ToString("000000");
                    else
                        sampleId = Properties.Settings.Default.AtlasOperation + sampleCode + maxId.ToString("00000");
                }
            }
            else
                sampleId = Properties.Settings.Default.AtlasOperation + sampleCode + "000001";

            reader.Close();

            return sampleId;
        }

        private void gridProductSamples_RecordUpdated(object sender, EventArgs e)
        {
            try
            {
                if (psm_Product_SamplesTableAdapter.Connection.State != ConnectionState.Open)
                    psm_Product_SamplesTableAdapter.Connection.Open();

                psm_Product_SamplesTableAdapter.Update(samples.psm_Product_Samples);

                psm_Product_SamplesTableAdapter.Connection.Close();
            }
            catch (SystemException exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void gridBlastholes_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            if (e.Column.Key.Equals("SampleId"))
            {
                gridBlastholes.GetRow().Cells["SampledOn"].Value = DateTime.Now;
                gridBlastholes.GetRow().Cells["SampledUser"].Value = WindowsIdentity.GetCurrent().Name;
            }
        }

        private void gridProductSamples_FormattingRow(object sender, RowLoadEventArgs e)
        {
            if (e.Row.RowType == RowType.Record)
            {
                if (e.Row.Cells["Weightometer"].Value != null)
                {
                    double prevW = 0.0;
                    DateTime prevDateTime = new DateTime(1900, 1, 1);
                    DateTime curSampledOn = (DateTime)e.Row.Cells["SampledOn"].Value;

                    foreach(Samples.psm_Product_SamplesRow row in this.samples.psm_Product_Samples)
                    {
                        if (row.RowState == DataRowState.Deleted)
                            continue;

                        if (row["Weightometer"] == DBNull.Value)
                            continue;

                        if (row == (e.Row.DataRow as DataRowView).Row)
                            continue;
                        if (row["Weightometer"] == null || row["Weightometer"] == DBNull.Value)
                            continue;
                        if (row["Crusher"] == null || row["Crusher"] == DBNull.Value)
                            continue;
                        if (row["SampledOn"] == null || row["SampledOn"] == DBNull.Value)
                            continue;
                        if (!row["Crusher"].ToString().Equals(e.Row.Cells["Crusher"].Text))
                            continue;

                        DateTime rDate = (DateTime)row["SampledOn"];
                        if (rDate > curSampledOn)
                            continue;
                        if (rDate <= prevDateTime)
                            continue;

                        prevW = (double)row["Weightometer"];
                        prevDateTime = rDate;
                    }
                    if (e.Row.Cells["Weightometer"].Value != DBNull.Value && e.Row.Cells["Weightometer"].Value != null)
                    {
                        double w = (double)e.Row.Cells["Weightometer"].Value;
                        e.Row.Cells["Weightometer"].Text = String.Format("{0:###,###,###,###,###}", (Int64)w);
                        if (prevW > 0.0)
                        {
                            //                        e.Row.Cells["Weightometer"].Text = String.Format("{0:D}", (Int64)w);
                            e.Row.Cells["Tonnes"].Text = prevW > 0.0 ? (w - prevW).ToString() : "";
                        }
                    }
                }
                if (e.Row.Cells["SampledOn"].Value != null)
                {
                    DateTime d = (DateTime)e.Row.Cells["SampledOn"].Value;
                    e.Row.Cells["SampleDate"].Text = String.Format("{0:d}",d);
                    e.Row.Cells["SampleTime"].Text = String.Format("{0:D2}:{1:D2}", d.Hour,d.Minute);
                }
                if (e.Row.Cells["SampleId"].Value != null)
                {
                    String sampleId = e.Row.Cells["SampleId"].Value.ToString();
                    if (sampleId.Length > 6 && sampleId.Substring(4, 2) == Properties.Settings.Default.PSChangeStockpileSampleType)
                        e.Row.Cells["SampleId"].Text = PlantWeightmeterMaterialChange;
                    if (sampleId.Length > 6 && sampleId.Substring(4, 2) == Properties.Settings.Default.PSSttartOfShiftStockpileSampleType)
                        e.Row.Cells["SampleId"].Text = PlantWeightmeterStartOfShift;
                    //if (sampleId.Length > 6 && sampleId.Substring(4, 2) == Properties.Settings.Default.PSEndOfShiftStockpileSampleType)
                    //    e.Row.Cells["SampleId"].Text = PlantWeightmeterEndOfShift;
                }
            }
        }

        private void lstProductSubmissions_TextChanged(object sender, EventArgs e)
        {
            FilterProductSamplesGrid();
            gridProductSamples.Focus();
        }

        private void lstCrushers_TextChanged(object sender, EventArgs e)
        {
            gridProductSamples.RootTable.Columns["Crusher"].DefaultValue = lstCrushers.Text;
            FilterProductSamplesGrid();
            gridProductSamples.Focus();
        }

        private void gridProductSamples_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.C && e.Control)
            {
                Clipboard.SetDataObject(gridProductSamples.GetClipString());
            }
            else if (e.KeyCode == Keys.V && e.Control)
            {
                try
                {
                    IDataObject dataObject = Clipboard.GetDataObject();
                    if (dataObject.GetDataPresent(DataFormats.CommaSeparatedValue))
                    {
                        if (psm_Product_SamplesTableAdapter.Connection.State != ConnectionState.Open)
                            psm_Product_SamplesTableAdapter.Connection.Open();

                        MemoryStream table = dataObject.GetData(DataFormats.CommaSeparatedValue) as MemoryStream;
                        UTF8Encoding enc = new System.Text.UTF8Encoding();
                        StreamReader reader = new System.IO.StreamReader(table, enc);

                        String line = reader.ReadLine();
                        String[] fields = line.Split(',');
                        while (!reader.EndOfStream)
                        {
                            line = reader.ReadLine();
                            String[] values = line.Split(',');
                            if (values.Length != fields.Length)
                                continue;

                            Dictionary<String, String> row = new Dictionary<string, string>();
                            for (int i = 0; i < fields.Length && i < values.Length; i++)
                                row[fields[i].Trim()] = values[i];

                            try
                            {
                                Samples.psm_Product_SamplesRow dataRow = samples.psm_Product_Samples.NewRow() as Samples.psm_Product_SamplesRow;

                                dataRow.BeginEdit();
                                DateTime d = DateTime.Parse(row["Date"]);
                                DateTime t = DateTime.Parse(row["Time"].Replace('.',':'));
                                dataRow.Crusher = lstCrushers.Text;
                                dataRow.SampleId = GetNewChangeStockpileSampleId(Properties.Settings.Default.PSChangeStockpileSampleType);
                                dataRow.SampledOn = new DateTime(d.Year, d.Month, d.Day, t.Hour, t.Minute, 0);
                                dataRow.Weightometer = Double.Parse(row["Weightometer"]);
                                dataRow.StackerPosition = Int32.Parse(row["Stacker Position"]);
                                dataRow.SampleBucket = Int32.Parse(row["Bucket #"]);
                                dataRow.EndEdit();
                                samples.psm_Product_Samples.Rows.Add(dataRow);

                                psm_Product_SamplesTableAdapter.Update(samples.psm_Product_Samples);
                            }
                            catch (System.Exception exc)
                            {
                            }
                        }
                        reader.Close();

                        psm_Product_SamplesTableAdapter.Connection.Close();

                        FilterProductSamplesGrid();
                        gridProductSamples.Focus();
                    }
                }
                catch (System.Exception exc)
                {
                }
            }
        }        

        private void gridBlastholes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.C && e.Control)
            {
                Clipboard.SetDataObject(gridBlastholes.GetClipString());
            }
        }

        private class CompareHoleNo : IComparer
        {
            #region IComparer Members

            public int Compare(object a, object b)
            {
                String aText = (a == null || String.IsNullOrWhiteSpace(a.ToString()) ? "0" : a.ToString());
                String bText = (b == null || String.IsNullOrWhiteSpace(b.ToString()) ? "0" : b.ToString());

                Double r = 0.0;
                Boolean aIsNum = Double.TryParse(aText, out r);
                Double aNum = (aIsNum ? r : 0.0);
                Boolean bIsNum = Double.TryParse(bText, out r);
                Double bNum = (bIsNum ? r : 0.0);
                if (aIsNum && bIsNum)
                    return aNum < bNum ? -1 : (aNum > bNum ? 1 : 0);
                if (aIsNum && !bIsNum)
                    return -1;
                if (!aIsNum && bIsNum)
                    return 1;

                return String.Compare(aText, bText);
            }

            #endregion
        }

        private void lstDateFilter_TextChanged(object sender, EventArgs e)
        {
            FilterBlastholesGrid();
            gridBlastholes.Focus();
        }

        private void calBSStartDate_ValueChanged(object sender, EventArgs e)
        {
            FilterBlastholesGrid();
            gridBlastholes.Focus();
        }

        private void calBSEndDate_ValueChanged(object sender, EventArgs e)
        {
            FilterBlastholesGrid();
            gridBlastholes.Focus();
        }

        private void gridProductSamples_CellValueChanged(object sender, ColumnActionEventArgs e)
        {
            GridEXRow curRow = gridProductSamples.GetRow();
            if (curRow == null)
                return;
            
            //if (e.Column.Key.Equals("SampleDate"))
            //{
            //    if (curRow.RowType != RowType.NewRecord)
            //        curRow.BeginEdit();
            //    DateTime sampledOnDateTime = DateTime.Now;
            //    if (curRow.Cells["SampledOn"].Value != null)
            //        sampledOnDateTime = (DateTime)curRow.Cells["SampledOn"].Value;
            //    if (gridProductSamples.GetValue(e.Column) is DateTime)
            //    {
            //        DateTime sampledOnDate = (DateTime)gridProductSamples.GetValue(e.Column);
            //        curRow.Cells["SampledOn"].Value = new DateTime(sampledOnDate.Year, sampledOnDate.Month, sampledOnDate.Day, sampledOnDateTime.Hour, sampledOnDateTime.Minute, 0);
            //    }

            //    DateTime d = (DateTime)curRow.Cells["SampledOn"].Value;
            //    curRow.Cells["SampleTime"].Value = String.Format("{0:D2}:{1:D2}", d.Hour, d.Minute);

            //    if (curRow.RowType != RowType.NewRecord)
            //        curRow.EndEdit();

            //    gridProductSamples.Refresh();
            //}

            //if (e.Column.Key.Equals("SampleTime"))
            //{
            //    if (curRow.RowType != RowType.NewRecord)
            //        curRow.BeginEdit();
            //    DateTime sampledOnDateTime = DateTime.Now;
            //    if (curRow.Cells["SampledOn"].Value != null)
            //        sampledOnDateTime = (DateTime)curRow.Cells["SampledOn"].Value;
            //    try
            //    {
            //        Object v = gridProductSamples.GetValue(e.Column);
            //        DateTime sampledOnTime;
            //        if (v != null && DateTime.TryParse(v.ToString(),out sampledOnTime))
            //            curRow.Cells["SampledOn"].Value = new DateTime(sampledOnDateTime.Year, sampledOnDateTime.Month, sampledOnDateTime.Day, sampledOnTime.Hour, sampledOnTime.Minute, 0);
            //    }
            //    catch (System.Exception exc)
            //    {
            //    }
            //    DateTime d = (DateTime)curRow.Cells["SampledOn"].Value;
            //    curRow.Cells["SampleDate"].Value = String.Format("{0:d}", d);

            //    if (curRow.RowType != RowType.NewRecord)
            //        curRow.EndEdit();

            //    gridProductSamples.Refresh();
            //}
        }

        private void gridProductSamples_LoadingRow(object sender, RowLoadEventArgs e)
        {
            if (e.Row.Cells["SampledOn"].Value == null || e.Row.Cells["SampledOn"].Value == DBNull.Value)
                return;

            DateTime d = (DateTime)e.Row.Cells["SampledOn"].Value; 
            e.Row.Cells["SampleDate"].Value = new DateTime(d.Year, d.Month, d.Day, 6, 0, 0);
            e.Row.Cells["SampleTime"].Value = String.Format("{0:D2}:{1:D2}",d.Hour,d.Minute);
        }

        private void gridProductSamples_GetNewRow(object sender, GetNewRowEventArgs e)
        {
            gridProductSamples.CurrentColumn = gridProductSamples.RootTable.Columns["Crusher"];

            GridEXRow row = gridProductSamples.GetRow();
            if (row == null || row.Position == 0)
                return;
            GridEXRow prevRow = gridProductSamples.GetRow(row.Position - 1);
            if (prevRow == null)
                return;
        }

        private void gridProductSamples_EditingCell(object sender, EditingCellEventArgs e)
        {
            GridEXRow row = gridProductSamples.GetRow();
            if (row == null)
                return;


            //GridEXRow prevRow = gridProductSamples.GetRow(row.Position - 1);
            //if (prevRow != null)
            //{
            //    gridProductSamples.RootTable.Columns["SampledOn"].DefaultValue = ((DateTime)prevRow.Cells["SampledOn"].Value).AddMinutes(15);
            //    gridProductSamples.RootTable.Columns["SampleDate"].DefaultValue = prevRow.Cells["SampledOn"].Value;
            //    gridProductSamples.RootTable.Columns["SampleTime"].DefaultValue = ((DateTime)prevRow.Cells["SampledOn"].Value).AddMinutes(15);
            //    gridProductSamples.RootTable.Columns["PrimaryFrom"].DefaultValue = prevRow.Cells["PrimaryFrom"].Value;
            //    gridProductSamples.RootTable.Columns["PrimaryScoops"].DefaultValue = prevRow.Cells["PrimaryScoops"].Value;
            //    gridProductSamples.RootTable.Columns["SecondaryFrom"].DefaultValue = prevRow.Cells["SecondaryFrom"].Value;
            //    gridProductSamples.RootTable.Columns["SecondaryScoops"].DefaultValue = prevRow.Cells["SecondaryScoops"].Value;
            //    gridProductSamples.RootTable.Columns["To"].DefaultValue = prevRow.Cells["To"].Value;

            //    if (!String.IsNullOrWhiteSpace(prevRow.Cells["SampleId"].Text))
            //    {
            //        String sampleId = prevRow.Cells["SampleId"].Text;
            //        try
            //        {
            //            gridProductSamples.RootTable.Columns["SampleId"].DefaultValue = sampleId.Substring(0, 5) + ((Int32.Parse(sampleId.Substring(5, 5)) + 1).ToString("00000"));
            //        }
            //        catch (System.Exception exc)
            //        {
            //        }
            //    }
            //}

        }

        private void butPublish_Click(object sender, EventArgs e)
        {
            if (psm_Product_SamplesTableAdapter.Connection.State != ConnectionState.Open)
                psm_Product_SamplesTableAdapter.Connection.Open();

            PublishCrushingDataDlg publish = new PublishCrushingDataDlg() { TargetCrusher = lstCrushers.SelectedValue.ToString(), Connection = psm_Product_SamplesTableAdapter.Connection };
            publish.ShowDialog();

            psm_Product_SamplesTableAdapter.Connection.Close();

            FilterProductSamplesGrid(true);
        }

        private void gridBlastholes_RecordsDeleted(object sender, EventArgs e)
        {
            try
            {
                if (gcv_Hole_CollarsTableAdapter.Connection.State != ConnectionState.Open)
                    gcv_Hole_CollarsTableAdapter.Connection.Open();
                gcv_Hole_CollarsTableAdapter.Update(samples.gcv_Hole_Collars);
                gcv_Hole_CollarsTableAdapter.Connection.Close();
            }
            catch (SystemException exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void lstPerformanceDateFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterPerformanceGraph();
        }

        private void lstPerformanceYDateField_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterPerformanceGraph();
        }

        private void radGaphBlastholes_CheckedChanged(object sender, EventArgs e)
        {
            FilterPerformanceGraph();
        }

        private void radGraphPlantSamples_CheckedChanged(object sender, EventArgs e)
        {
            FilterPerformanceGraph();
        }

        private void lstPerformanceFunction_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterPerformanceGraph();
        }

        private void gridProductSamples_SelectionChanged(object sender, EventArgs e)
        {
            for (int i = gridProductSamples.GetRows().Length; i >= 1; i--)
            {
                GridEXRow prevRow = gridProductSamples.GetRow(i);
                if (prevRow != null && prevRow.RowType == RowType.Record)
                {
                    gridProductSamples.RootTable.Columns["SampledOn"].DefaultValue = prevRow.Cells["SampledOn"].Value;
                    gridProductSamples.RootTable.Columns["SampleDate"].DefaultValue = prevRow.Cells["SampledOn"].Value;
                    gridProductSamples.RootTable.Columns["SampleTime"].DefaultValue = prevRow.Cells["SampledOn"].Value;
                    gridProductSamples.RootTable.Columns["PrimaryFrom"].DefaultValue = prevRow.Cells["PrimaryFrom"].Value;
                    gridProductSamples.RootTable.Columns["PrimaryScoops"].DefaultValue = prevRow.Cells["PrimaryScoops"].Value;
                    gridProductSamples.RootTable.Columns["SecondaryFrom"].DefaultValue = prevRow.Cells["SecondaryFrom"].Value;
                    gridProductSamples.RootTable.Columns["SecondaryScoops"].DefaultValue = prevRow.Cells["SecondaryScoops"].Value;
                    gridProductSamples.RootTable.Columns["Finger3From"].DefaultValue = prevRow.Cells["Finger3From"].Value;
                    gridProductSamples.RootTable.Columns["Finger3Scoops"].DefaultValue = prevRow.Cells["Finger3Scoops"].Value;
                    gridProductSamples.RootTable.Columns["Finger4From"].DefaultValue = prevRow.Cells["Finger4From"].Value;
                    gridProductSamples.RootTable.Columns["Finger4Scoops"].DefaultValue = prevRow.Cells["Finger4Scoops"].Value;
                    gridProductSamples.RootTable.Columns["Finger5From"].DefaultValue = prevRow.Cells["Finger5From"].Value;
                    gridProductSamples.RootTable.Columns["Finger5Scoops"].DefaultValue = prevRow.Cells["Finger5Scoops"].Value;
                    gridProductSamples.RootTable.Columns["Finger6From"].DefaultValue = prevRow.Cells["Finger6From"].Value;
                    gridProductSamples.RootTable.Columns["Finger6Scoops"].DefaultValue = prevRow.Cells["Finger6Scoops"].Value;
                    gridProductSamples.RootTable.Columns["To"].DefaultValue = prevRow.Cells["To"].Value;

                    if (!String.IsNullOrWhiteSpace(prevRow.Cells["SampleId"].Text))
                    {
                        String sampleId = prevRow.Cells["SampleId"].Text;
                        try
                        {
                            if (sampleId.Length == 10)
                                sampleId = sampleId.Substring(0, 5) + ((Int32.Parse(sampleId.Substring(5, 5)) + 1).ToString("00000"));
                            else if (sampleId.Length == 11)
                                sampleId = sampleId.Substring(0, 5) + ((Int32.Parse(sampleId.Substring(5, 6)) + 1).ToString("000000"));

                            gridProductSamples.RootTable.Columns["SampleId"].DefaultValue = sampleId;
                        }
                        catch (System.Exception exc)
                        {
                        }
                    }
                    break;
                }
            }

            GridEXRow row = gridProductSamples.GetRow();

            gridProductSamples.RootTable.Columns["PrimaryFrom"].ValueList.Clear();
            gridProductSamples.RootTable.Columns["SecondaryFrom"].ValueList.Clear();
            gridProductSamples.RootTable.Columns["Finger3From"].ValueList.Clear();
            gridProductSamples.RootTable.Columns["Finger4From"].ValueList.Clear();
            gridProductSamples.RootTable.Columns["Finger5From"].ValueList.Clear();
            gridProductSamples.RootTable.Columns["Finger6From"].ValueList.Clear();
            gridProductSamples.RootTable.Columns["To"].ValueList.Clear();
            if (row.Cells["SampledOn"].Value != null && row.Cells["SampledOn"].Value != DBNull.Value)
            {
                SqlConnection connection = null;
                try
                {
                    connection = new SqlConnection(Properties.Settings.Default.AtlasMinemarketLinkDatabase);
                    connection.Open();

                    DateTime d = (DateTime)row.Cells["SampledOn"].Value;

                    SqlCommand cmd = new SqlCommand("select Stockpile from atlasStockpileActivity where Operation = '"  + Properties.Settings.Default.AtlasOperation + "' and Activity = 'ROM' and OpenDate <= @1 and IsNull(CloseDate,'1-1-2100') >= @2 order by Stockpile ", connection);
                    cmd.Parameters.AddWithValue("@1", d);
                    cmd.Parameters.AddWithValue("@2", d);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        String stockpile = (reader[0] == DBNull.Value ? null : reader[0].ToString());
                        if (stockpile != null)
                        {
                            gridProductSamples.RootTable.Columns["PrimaryFrom"].ValueList.Add(stockpile, stockpile);
                            gridProductSamples.RootTable.Columns["SecondaryFrom"].ValueList.Add(stockpile, stockpile);
                            gridProductSamples.RootTable.Columns["Finger3From"].ValueList.Add(stockpile, stockpile);
                            gridProductSamples.RootTable.Columns["Finger4From"].ValueList.Add(stockpile, stockpile);
                            gridProductSamples.RootTable.Columns["Finger5From"].ValueList.Add(stockpile, stockpile);
                            gridProductSamples.RootTable.Columns["Finger6From"].ValueList.Add(stockpile, stockpile);
                        }
                    }
                    reader.Close();

                    cmd = new SqlCommand("select Stockpile from atlasStockpileActivity where Operation = '" + Properties.Settings.Default.AtlasOperation + "' and Activity = 'PRODUCT' and OpenDate <= @1 and IsNull(CloseDate,'1-1-2100') >= @2 order by Stockpile ", connection);
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@1", d);
                    cmd.Parameters.AddWithValue("@2", d);

                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        String stockpile = (reader[0] == DBNull.Value ? null : reader[0].ToString());
                        if (stockpile != null)
                            gridProductSamples.RootTable.Columns["To"].ValueList.Add(stockpile, stockpile);
                    }
                    reader.Close();
                }
                catch (System.Exception exc)
                {
                }
                finally
                {
                    connection.Close();
                }
            }

        }

        private void butAutoSample_Click(object sender, EventArgs e)
        {         
            if (gridBlastholes.RootTable == null)
                return;

            String firstSampleId = null;
            String firstHoleNo = null;
            String sampleId = null;
            String holeNo = null;
            gridBlastholes.SelectedItems.Sort();
            foreach(GridEXSelectedItem item in gridBlastholes.SelectedItems)
            {
                if (item.RowType != RowType.Record)
                    continue;

                if (sampleId == null)
                {
                    sampleId = item.GetRow().Cells["SampleId"].Text;
                    if (String.IsNullOrWhiteSpace(sampleId))
                        return;
                    firstSampleId = sampleId;
                    firstHoleNo = item.GetRow().Cells["HoleNo"].Text;
                }
                else
                {
                    try
                    {
                        if (sampleId.Length == 10)
                            sampleId = sampleId.Substring(0, 5) + ((Int32.Parse(sampleId.Substring(5, 5)) + 1).ToString("00000"));
                        else if (sampleId.Length == 11)
                            sampleId = sampleId.Substring(0, 5) + ((Int32.Parse(sampleId.Substring(5, 6)) + 1).ToString("000000"));

                        holeNo = item.GetRow().Cells["HoleNo"].Text;
                    }
                    catch (System.Exception exc)
                    {
                    }
                }
            }

            if (MessageBox.Show("Auto create samples - Hole " + firstHoleNo + " [" + firstSampleId + "] to Hole " + holeNo + " [" + sampleId + "] ?", "Auto Sample Ids", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                sampleId = null;
                foreach (GridEXSelectedItem item in gridBlastholes.SelectedItems)
                {
                    if (item.RowType != RowType.Record)
                        continue;

                    if (sampleId == null)
                    {
                        sampleId = item.GetRow().Cells["SampleId"].Text;
                        if (String.IsNullOrWhiteSpace(sampleId))
                            return;
                    }
                    else
                    {
                        try
                        {
                            if (sampleId.Length == 10)
                                sampleId = sampleId.Substring(0, 5) + ((Int32.Parse(sampleId.Substring(5, 5)) + 1).ToString("00000"));
                            else if (sampleId.Length == 11)
                                sampleId = sampleId.Substring(0, 5) + ((Int32.Parse(sampleId.Substring(5, 6)) + 1).ToString("000000"));
                            item.GetRow().BeginEdit();
                            item.GetRow().Cells["SampleId"].Value = sampleId;
                            item.GetRow().Cells["SampledOn"].Value = DateTime.Now;
                            item.GetRow().Cells["SampledUser"].Value = WindowsIdentity.GetCurrent().Name;
                            item.GetRow().EndEdit();
                        }
                        catch (System.Exception exc)
                        {
                        }
                    }

                    gridBlastholes_RecordUpdated(null, null);
                }
            }
        }

        private void butPublishOreBlocks_Click(object sender, EventArgs e)
        {
            PublishOreBlocksDlg publish = new PublishOreBlocksDlg() { Connection = gcv_Hole_CollarsTableAdapter.Connection, globalPitsList = lstPits, globalBenchList = lstBench, globalShotsList = lstShotNos };
            publish.ShowDialog();
        }

        private void butAutoSelect_Click(object sender, EventArgs e)
        {
            if (gridBlastholes.RootTable == null)
                return;

            String firstHoleNo = null;
            String holeNo = null;
            gridBlastholes.SelectedItems.Sort();
            foreach (GridEXSelectedItem item in gridBlastholes.SelectedItems)
            {
                if (item.RowType != RowType.Record)
                    continue;

                if (firstHoleNo == null)
                {
                    firstHoleNo = item.GetRow().Cells["HoleNo"].Text;
                }
                else
                {
                    try
                    {
                        holeNo = item.GetRow().Cells["HoleNo"].Text;
                    }
                    catch (System.Exception exc)
                    {
                    }
                }
            }

            if (MessageBox.Show("Toggle blastholes for sampling - Hole " + firstHoleNo + " to Hole " + holeNo + " ?", "Auto select Ids", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                foreach (GridEXSelectedItem item in gridBlastholes.SelectedItems)
                {
                    if (item.RowType != RowType.Record)
                        continue;

                    try
                    {
                        item.GetRow().BeginEdit();
                        if (String.IsNullOrWhiteSpace(item.GetRow().Cells["SelectedOn"].Text))
                            item.GetRow().Cells["SelectedOn"].Value = DateTime.Now;
                        else
                            item.GetRow().Cells["SelectedOn"].Value = DBNull.Value;
                        item.GetRow().EndEdit();
                    }
                    catch (System.Exception exc)
                    {
                    }

                    gridBlastholes_RecordUpdated(null, null);
                }
            }
        }

        private void lstPits_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lstBench_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void lstShotNos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void gridProductSamples_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            GridEXRow row = gridProductSamples.GetRow();
            if (row == null)
                return;

            row.Cells["Published"].Value = "NO";
        }

    }
}
