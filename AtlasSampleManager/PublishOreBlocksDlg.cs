using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using AtlasReportToolkit;
using AtlasSampleToolkit;
using Janus.Windows.GridEX;
using Janus.Windows.EditControls;

namespace AtlasSampleManager
{
    public partial class PublishOreBlocksDlg : Form
    {
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public SqlConnection Connection
        {
            get;
            set;
        }

        public UIComboBox globalPitsList { get; set; }
        public UIComboBox globalBenchList { get; set; }
        public UIComboBox globalShotsList { get; set; }

        //public String BlastholesSampleFilter { get; set; }

        public PublishOreBlocksDlg()
        {
            InitializeComponent();
        }

        private void calDate_ValueChanged(object sender, EventArgs e)
        {
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            ExportGradeBlocks();
        }

        private void ExportGradeBlocks()
        {
            try
            {
                if (calOpenDate.IsNullDate)
                {
                    MessageBox.Show("You must define the opening date for the ore blocks", "Error", MessageBoxButtons.OK);
                    return;
                }

                if (this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                List<AURMineSource> gradeBlocks = new List<AURMineSource>();

                foreach (GridEXSelectedItem item in gridBlasts.SelectedItems)
                {
                    GridEXRow curRow = item.GetRow();
                    if (curRow.RowType != RowType.Record)
                        continue;

                    String Pit = curRow.Cells["Pit"].Text;
                    String Bench = curRow.Cells["Bench"].Text;
                    String ShotNo = curRow.Cells["ShotNo"].Text;
                    String Flitch = String.IsNullOrWhiteSpace(curRow.Cells["Flitch"].Text) ? null : curRow.Cells["Flitch"].Text;

                    SqlCommand cmd = new SqlCommand("select Flitch,BlockId,MaterialType,Volume,Tonnes,Density,FinalFe,FinalAl2O3,FinalSiO2,FinalP,FinalS,FinalMn,FinalLOI from gcv_Grade_Blocks where Pit = @1 and Bench = @2 and ShotNo = @3 and (Flitch = @4 or @4 is null) order by Pit,Bench,ShotNo", this.Connection);
                    cmd.Parameters.AddWithValue("@1", Pit);
                    cmd.Parameters.AddWithValue("@2", Bench);
                    cmd.Parameters.AddWithValue("@3", ShotNo);
                    cmd.Parameters.AddWithValue("@4", Flitch);

                    SqlDataReader reader = cmd.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            Flitch = (reader.IsDBNull(0) ? "" : reader.GetString(0));
                            String BlockId = (reader.IsDBNull(1) ? "" : reader.GetString(1));
                            String MaterialType = (reader.IsDBNull(2) ? "" : reader.GetString(2));

                            Double Volume = (reader.IsDBNull(3) ? -1.0 : reader.GetDouble(3));
                            Double Tonnes = (reader.IsDBNull(4) ? -1.0 : reader.GetDouble(4));
                            Double Density = (reader.IsDBNull(5) ? -1.0 : reader.GetFloat(5));
                            Double Fe = (reader.IsDBNull(6) ? -1.0 : reader.GetFloat(6));
                            Double Al2O3 = (reader.IsDBNull(7) ? -1.0 : reader.GetFloat(7));
                            Double SiO2 = (reader.IsDBNull(8) ? -1.0 : reader.GetFloat(8));
                            Double P = (reader.IsDBNull(9) ? -1.0 : reader.GetFloat(9));
                            Double S = (reader.IsDBNull(10) ? -1.0 : reader.GetFloat(10));
                            Double Mn = (reader.IsDBNull(11) ? -1.0 : reader.GetFloat(11));
                            Double LOI = (reader.IsDBNull(12) ? -1.0 : reader.GetFloat(12));

                            if (!String.IsNullOrWhiteSpace(MaterialType) && !MaterialType.Equals("W") && !MaterialType.Equals("MW") && !MaterialType.Equals("HG"))
                                MaterialType = "PR_" + Pit + "_" + MaterialType;

                            AURMineSource gradeBlock = new AURMineSource();
                            gradeBlock.Location = Properties.Settings.Default.AtlasOperation + "_OPMA_" + Pit;
                            gradeBlock.Pit = Pit;
                            gradeBlock.Bench = Bench;
                            gradeBlock.ShotNo = ShotNo;
                            gradeBlock.Flitch = Flitch;
                            gradeBlock.MineSource = Pit + "_" + Bench + "_" + ShotNo + "_" + Flitch + "_" + BlockId;
                            gradeBlock.Alias = Pit + "_" + Bench + "_" + ShotNo + "_" + Flitch + "_" + BlockId;
                            gradeBlock.OpenDate = AURMineSource.AURDateFormat(calOpenDate.Value.ToString());
                            gradeBlock.SurveyDate = AURMineSource.AURDateFormat(calOpenDate.Value.ToString());
                            gradeBlock.OreType = MaterialType;
                            gradeBlock.Tonnage = Tonnes.ToString();
                            gradeBlock.Assays["Fe"] = Fe >= 0.0 ? Fe.ToString() : "";
                            gradeBlock.Assays["Al2O3"] = Al2O3 >= 0.0 ? Al2O3.ToString() : "";
                            gradeBlock.Assays["SiO2"] = SiO2 >= 0.0 ? SiO2.ToString() : "";
                            gradeBlock.Assays["P"] = P >= 0.0 ? P.ToString() : "";
                            gradeBlock.Assays["S"] = S >= 0.0 ? S.ToString() : "";
                            gradeBlock.Assays["Mn"] = Mn >= 0.0 ? Mn.ToString() : "";
                            gradeBlock.Assays["LOI1000"] = LOI >= 0.0 ? LOI.ToString() : "";
                            gradeBlock.Assays["Density"] = Density >= 0.0 ? Density.ToString() : "";
                            gradeBlocks.Add(gradeBlock);
                        }
                    }
                    catch (System.Exception exc)
                    {
                    }

                    reader.Close();
                }
                this.Connection.Close();

                String gradeblocksFileName = Properties.Settings.Default.TempDirectory + Properties.Settings.Default.AtlasOperation + "MineSources_" + DateTime.Now.ToString("ddMMyyyyHHMM") + ".csv";
                AURMineSource.Save(gradeblocksFileName, gradeBlocks);

                TransferMovementsDlg transfer = new TransferMovementsDlg
                {
                    TransferFiles = new List<AURTransferFile>() { new AURTransferFile { AURFileName = gradeblocksFileName, AURFileNamePrefix = AURMineSource.c_AURiHubFileNamePrefix } },
                    iHubErrorDirectory = Properties.Settings.Default.iHubErrorDirectory,
                    iHubProcessedDirectory = Properties.Settings.Default.iHubProcessedDirectory,
                    iHubProcessingDirectory = Properties.Settings.Default.iHubProcessingDirectory,
                    iHubPendingDirectory = Properties.Settings.Default.iHubPendingDirectory,
                    
                };
                transfer.ShowDialog();
            }
            catch (System.Exception exc)
            {
            }

            this.Close();
        }

        private void PublishOreBlocksDlg_Load_1(object sender, EventArgs e)
        {
            lstPits.Items.Clear();
            foreach (UIComboBoxItem item in globalPitsList.Items)
                    lstPits.Items.Add(new UIComboBoxItem(item.Value));

            lstBench.Items.Clear();
            foreach (UIComboBoxItem item in globalBenchList.Items)
                lstBench.Items.Add(new UIComboBoxItem(item.Value));

            lstShotNos.Items.Clear();
            foreach (UIComboBoxItem item in globalShotsList.Items)
                lstShotNos.Items.Add(new UIComboBoxItem(item.Value));

            calOpenDate.IsNullDate = true;
//            calOpenDate.Value = DateTime.Now.AddDays(1);

            UpdateFlitchesList();
        }

        private void UpdateFlitchesList()
        {
            String gradBlocksFilter = "1 = 1";
            if (lstPits.SelectedValue != null && !String.IsNullOrWhiteSpace(lstPits.SelectedValue.ToString()))
            {
                gradBlocksFilter += " and Pit = '" + lstPits.SelectedValue.ToString() + "'";
            }
            if (lstBench.SelectedValue != null && !String.IsNullOrWhiteSpace(lstBench.SelectedValue.ToString()))
            {
                gradBlocksFilter += " and Bench = '" + lstBench.SelectedValue.ToString() + "'";
            }
            if (lstShotNos.SelectedValue != null && !String.IsNullOrWhiteSpace(lstShotNos.SelectedValue.ToString()))
            {
                gradBlocksFilter += " and ShotNo = '" + lstShotNos.SelectedValue.ToString() + "'";
            }
            if (gradBlocksFilter.Equals("1 = 1"))
            {
                gridBlasts.SetDataBinding(null, null);
                return;
            }

            if (this.Connection.State != ConnectionState.Open)
                this.Connection.Open();

            List<Shot> blocks = new List<Shot>();
            SqlCommand cmd = new SqlCommand("select distinct Pit,Bench,ShotNo,Flitch,[CreatedByUser],[CreatedByDate] from gcv_Grade_Blocks where " + gradBlocksFilter + " order by Pit,Bench,ShotNo", this.Connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                String Pit = (reader.IsDBNull(0) ? "" : reader.GetString(0));
                String Bench = (reader.IsDBNull(1) ? "" : reader.GetString(1));
                String ShotNo = (reader.IsDBNull(2) ? "" : reader.GetString(2));
                String Flitch = (reader.IsDBNull(3) ? "" : reader.GetString(3));
                String User = (reader.IsDBNull(4) ? "" : reader.GetString(4));
                String CreatedOn = (reader.IsDBNull(5) ? "" : reader.GetDateTime(5).ToString());
                blocks.Add(new Shot { Pit = Pit, Bench = Bench, ShotNo = ShotNo, Flitch = Flitch, PublishedBy = User, PublishedOn = CreatedOn });
            }
            reader.Close();
            this.Connection.Close();

            gridBlasts.SetDataBinding(blocks, null);
            gridBlasts.RetrieveStructure();          
        }

        private void lstPits_TextChanged(object sender, EventArgs e)
        {
            UpdateFlitchesList();
        }

        private void lstBench_TextChanged(object sender, EventArgs e)
        {
            UpdateFlitchesList();
        }

        private void lstShotNos_TextChanged(object sender, EventArgs e)
        {
            UpdateFlitchesList();
        }

        private void gridBlasts_SelectionChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    GridEXRow curRow = gridBlasts.GetRow();
            //    if (curRow.RowType != RowType.Record)
            //        return;

            //    if (this.Connection.State != ConnectionState.Open)
            //        this.Connection.Open();

            //    String Pit = curRow.Cells["Pit"].Text;
            //    String Bench = curRow.Cells["Bench"].Text;
            //    String ShotNo = curRow.Cells["ShotNo"].Text;
            //    String Flitch = String.IsNullOrWhiteSpace(curRow.Cells["Flitch"].Text) ? "?" : curRow.Cells["Flitch"].Text;

            //    SqlCommand cmd = new SqlCommand("select [CreatedByUser],[CreatedByDate]  from gcv_Grade_Blocks where Pit = '" + Pit + "' and Bench = '" + Bench + "' and ShotNo = '" + ShotNo + "' and Flitch = '" + Flitch + "'", this.Connection);
            //    SqlDataReader reader = cmd.ExecuteReader();
            //    while (reader.Read())
            //    {
            //        txtUserName.Text = reader.GetString(0);
            //        txtDate.Text = reader.GetDateTime(1).ToString();
            //    }
            //    reader.Close();
            //    this.Connection.Close();
            //}
            //catch (System.Exception exc)
            //{
            //}
        }
    }
}
