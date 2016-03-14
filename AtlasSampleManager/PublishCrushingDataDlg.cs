using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Janus.Windows.GridEX;
using AtlasReportToolkit;

namespace AtlasSampleManager
{
    public partial class PublishCrushingDataDlg : Form
    {
        static public String c_ActionSkipTons = "Skip Tons";
        static public String c_ActionSkipGrade = "Skip Grade";
        static public List<String> c_sampleSuffix = new List<string>() { "", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public String TargetCrusher { get; set; }

        private class StringWrapper
        {
            public string text { get; set; }
        }

        public SqlConnection Connection
        {
            get;
            set;
        }

        public PublishCrushingDataDlg()
        {
            InitializeComponent();
        }

        private void PublishCrushingDataDlg_Load(object sender, EventArgs e)
        {
            calDate.Value = DateTime.Now;
            List<StringWrapper> streams = new List<StringWrapper>();
            foreach (string s in Properties.Settings.Default.CrusherStreams)
                streams.Add(new StringWrapper { text = s });
            gridStreams.SetDataBinding(streams, "");
            foreach(GridEXRow row in gridStreams.GetRows())
                if (row.Cells[1].Text.Equals(this.TargetCrusher))
                    row.IsChecked = true;
        }

        private void calDate_ValueChanged(object sender, EventArgs e)
        {
            this.StartDateTime = new DateTime(calDate.Value.Year, calDate.Value.Month, calDate.Value.Day, 6, 0, 0).AddDays(-1);
            this.EndDateTime = new DateTime(calDate.Value.Year, calDate.Value.Month, calDate.Value.Day, 6, 0, 0).AddDays(1);

            calEndDate.Value = this.EndDateTime;         

            txtStartDateTime.Text = this.StartDateTime.ToString("dd/MM/yyyy HH:mm");
            txtEndDateTime.Text = this.EndDateTime.ToString("dd/MM/yyyy HH:mm");
        }

        private void calEndDate_ValueChanged(object sender, EventArgs e)
        {
            this.EndDateTime = new DateTime(calEndDate.Value.Year, calEndDate.Value.Month, calEndDate.Value.Day, 6, 0, 0).AddDays(1);

            txtStartDateTime.Text = this.StartDateTime.ToString("dd/MM/yyyy HH:mm");
            txtEndDateTime.Text = this.EndDateTime.ToString("dd/MM/yyyy HH:mm");
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            ExportProductSamples(this.TargetCrusher);
            this.Close();
        }

        private void ExportProductSamples(string targetCrusher)
        {
            try
            {
                List<AURTransaction> AllMovements = new List<AURTransaction>();
                List<AURSampleResults> AllAssays = new List<AURSampleResults>();

                foreach(GridEXRow row in gridStreams.GetRows())
                {
                    if (!row.IsChecked)
                        continue;

                    targetCrusher = row.Cells[1].Text;

                    List<AURTransaction> movements = new List<AURTransaction>();
                    List<AURSampleResults> assays = new List<AURSampleResults>();
                    DateTime startBufferDate = this.StartDateTime.AddDays(-30);

                    String sampleFilter = "Crusher = '" + targetCrusher + "' and SampledOn >= '" + startBufferDate.Month.ToString("00") + "-" + startBufferDate.Day.ToString("00") + "-" + startBufferDate.Year.ToString("0000") + " " + startBufferDate.Hour.ToString("00") + ":" + startBufferDate.Minute.ToString("00") + "'";

                    SqlCommand cmd = new SqlCommand("select Crusher,SampledOn,SampleId,Weightometer,PrimaryFrom,PrimaryScoops,SecondaryFrom,SecondaryScoops,[To],Comments,Fe,Al2O3,SiO2,P,S,Mn,LOI,Moisture,ReceivedOn,Submission,StackerPosition,SampleBucket, [Size12.5mm], [Size10.0_12.5mm], [Size9.5_10.0mm], [Size6.7_9.5mm], [Size0.5_6.7mm], [Size0.0_0.5mm], Action,Finger3From,Finger3Scoops,Finger4From,Finger4Scoops,Finger5From,Finger5Scoops,Finger6From,Finger6Scoops,[Size40mm],[Size6.3_40mm],[Size0.0_6.3mm] from psm_Product_Samples where " + sampleFilter + " order by SampledOn", this.Connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    Double prevWeightometer = 0.0;
                    List<String> prevFromStockpile = new List<string>();
                    String prevToStockpile = "";
                    Boolean foundFirstSampleAfterEOD = false;
                    DateTime? lastSampledOnTime = null;
                    List<String> publishedSamples = new List<string>();
                    while (reader.Read() && !foundFirstSampleAfterEOD)
                    {
                        String crusher = (reader.IsDBNull(0) ? "" : reader.GetString(0));
                        DateTime SampledOn = (reader.IsDBNull(1) ? new DateTime(1900,1,1,0,0,0) : reader.GetDateTime(1));
                        String SampleId = (reader.IsDBNull(2) ? "" : reader.GetString(2).Replace(",", ""));
                        Double Weightometer = (reader.IsDBNull(3) ? -1.0 : reader.GetDouble(3));

                        List<String> fingerFrom = new List<string>();
                        List<int> fingerFromScoops = new List<int>();

                        String oneFrom = (reader.IsDBNull(4) ? "" : reader.GetString(4).Replace(",", ""));
                        int oneScoops = (reader.IsDBNull(5) ? 1 : reader.GetInt32(5));
                        if (!String.IsNullOrWhiteSpace(oneFrom))
                        {
                            fingerFrom.Add(oneFrom);
                            fingerFromScoops.Add(oneScoops > 0 ? oneScoops : 1);
                        }
                        oneFrom = (reader.IsDBNull(6) ? "" : reader.GetString(6).Replace(",", ""));
                        oneScoops = (reader.IsDBNull(7) ? 0 : reader.GetInt32(7));
                        if (!String.IsNullOrWhiteSpace(oneFrom))
                        {
                            fingerFrom.Add(oneFrom);
                            fingerFromScoops.Add(oneScoops > 0 ? oneScoops : 1);
                        }

                        String To = (reader.IsDBNull(8) ? "" : reader.GetString(8).Replace(",", ""));
                        String Comments = (reader.IsDBNull(9) ? "" : reader.GetString(9).Replace(",", ""));

                        Double Fe = (reader.IsDBNull(10) ? -1.0 : reader.GetFloat(10));
                        Double Al2O3 = (reader.IsDBNull(11) ? -1.0 : reader.GetFloat(11));
                        Double SiO2 = (reader.IsDBNull(12) ? -1.0 : reader.GetFloat(12));
                        Double P = (reader.IsDBNull(13) ? -1.0 : reader.GetFloat(13));
                        Double S = (reader.IsDBNull(14) ? -1.0 : reader.GetFloat(14));
                        Double Mn = (reader.IsDBNull(15) ? -1.0 : reader.GetFloat(15));
                        Double LOI = (reader.IsDBNull(16) ? -1.0 : reader.GetFloat(16));
                        Double Moisture = (reader.IsDBNull(17) ? -1.0 : reader.GetFloat(17));
                        DateTime receivedOn = (reader.IsDBNull(18) ? new DateTime(1900,1,1,0,0,0) : reader.GetDateTime(18));
                        String Submission = (reader.IsDBNull(19) ? "" : reader.GetString(19).Replace(",",""));
                        String StackerPosition = (reader.IsDBNull(20) ? "" : reader.GetInt32(20).ToString());
                        String SampleBucket = (reader.IsDBNull(21) ? "" : reader.GetInt32(21).ToString());

                        Double Size12o5mm = (reader.IsDBNull(22) ? -1.0 : reader.GetFloat(22));
                        Double Size10o0_12o5mm = (reader.IsDBNull(23) ? -1.0 : reader.GetFloat(23));
                        Double Size9o5_10o0mm = (reader.IsDBNull(24) ? -1.0 : reader.GetFloat(24));
                        Double Size6o7_9o5mm = (reader.IsDBNull(25) ? -1.0 : reader.GetFloat(25));
                        Double Size0o5_6o7mm = (reader.IsDBNull(26) ? -1.0 : reader.GetFloat(26));
                        Double Size0o0_0o5mm = (reader.IsDBNull(27) ? -1.0 : reader.GetFloat(27));
                        Double Size40mm = (reader.IsDBNull(37) ? -1.0 : reader.GetFloat(37));
                        Double Size6_3_40mm = (reader.IsDBNull(38) ? -1.0 : reader.GetFloat(38));
                        Double Size0_0_6_3mm = (reader.IsDBNull(39) ? -1.0 : reader.GetFloat(39));

                        String action = (reader.IsDBNull(28) ? "" : reader.GetString(28));

                        oneFrom = (reader.IsDBNull(29) ? "" : reader.GetString(29).Replace(",", ""));
                        oneScoops = (reader.IsDBNull(30) ? 0 : reader.GetInt32(30));
                        if (!String.IsNullOrWhiteSpace(oneFrom))
                        {
                            fingerFrom.Add(oneFrom);
                            fingerFromScoops.Add(oneScoops > 0 ? oneScoops : 1);
                        }

                        oneFrom = (reader.IsDBNull(31) ? "" : reader.GetString(31).Replace(",", ""));
                        oneScoops = (reader.IsDBNull(32) ? 0 : reader.GetInt32(32));
                        if (!String.IsNullOrWhiteSpace(oneFrom))
                        {
                            fingerFrom.Add(oneFrom);
                            fingerFromScoops.Add(oneScoops > 0 ? oneScoops : 1);
                        }

                        oneFrom = (reader.IsDBNull(33) ? "" : reader.GetString(33).Replace(",", ""));
                        oneScoops = (reader.IsDBNull(34) ? 0 : reader.GetInt32(34));
                        if (!String.IsNullOrWhiteSpace(oneFrom))
                        {
                            fingerFrom.Add(oneFrom);
                            fingerFromScoops.Add(oneScoops > 0 ? oneScoops : 1);
                        }

                        oneFrom = (reader.IsDBNull(35) ? "" : reader.GetString(35).Replace(",", ""));
                        oneScoops = (reader.IsDBNull(36) ? 0 : reader.GetInt32(36));
                        if (!String.IsNullOrWhiteSpace(oneFrom))
                        {
                            fingerFrom.Add(oneFrom);
                            fingerFromScoops.Add(oneScoops > 0 ? oneScoops : 1);
                        }
                        if (SampledOn >= this.StartDateTime && SampledOn < this.EndDateTime)
                            publishedSamples.Add(SampleId);

                        if (Weightometer <= -0.0001)
                            continue;

                        SampledOn = SampledOn.AddHours(Properties.Settings.Default.PSShiftTimeOffset);
                        Double tonnes = Weightometer - prevWeightometer;
                        prevWeightometer = Weightometer;

                        if (tonnes <= 0.0001 || action.Equals(c_ActionSkipTons))
                            continue;

                        if (prevWeightometer == 0.0)
                            tonnes = 1000.0;

                        Boolean differentFeed = (fingerFrom.Count != prevFromStockpile.Count);
                        foreach(String finger in fingerFrom)
                            if (!prevFromStockpile.Contains(finger))
                            {
                                differentFeed = true;
                                break;
                            }

                        if (SampledOn.Hour == 6 && SampledOn.Minute == 0)
                            SampledOn = new DateTime(SampledOn.Year,SampledOn.Month,SampledOn.Day,5,59,0);

                        Boolean differentProduct = !prevToStockpile.Equals(To);

                        Boolean isSample = SampleId.Substring(3, 2).Equals("PS") || SampleId.Substring(3, 2).Equals("LS");
                        Boolean noResults = Fe < 0.0 && Al2O3 < 0.0 && SiO2 < 0.0 && P < 0.0 && S < 0.0 && Mn < 0.0 && LOI < 0.0 && Size12o5mm < 0.0 && Size10o0_12o5mm < 0.0 && Size9o5_10o0mm < 0.0 && Size6o7_9o5mm < 0.0 && Size0o5_6o7mm < 0.0 && Size0o0_0o5mm < 0.0;
                        if (action.Equals(c_ActionSkipGrade))
                        {
                            Fe = -1.0;
                            Al2O3 = -1.0;
                            SiO2 = -1.0;
                            P = -1.0;
                            S = -1.0;
                            Mn = -1.0;
                            LOI = -1.0;
                        }

                        prevFromStockpile = new List<string>(fingerFrom);
                        prevToStockpile = To;

                        if (SampledOn < this.StartDateTime)
                            continue;

                        foundFirstSampleAfterEOD = (SampledOn >= this.EndDateTime && isSample);
                        if (SampledOn >= this.EndDateTime && !foundFirstSampleAfterEOD)
                            continue;

                        if (String.IsNullOrWhiteSpace(crusher))
                            crusher = targetCrusher;

                        DateTime shiftDate;
                        if (SampledOn.Hour >= 6 || SampledOn.Hour < 18)
                            shiftDate = new DateTime(SampledOn.AddHours(-6).Year, SampledOn.AddHours(-6).Month, SampledOn.AddHours(-6).Day, 6, 0, 0);
                        else
                            shiftDate = new DateTime(SampledOn.AddHours(-6).Year, SampledOn.AddHours(-6).Month, SampledOn.AddHours(-6).Day, 18, 0, 0);

                        if (!foundFirstSampleAfterEOD)
                            lastSampledOnTime = SampledOn;

                        List<Double> fingerTonnes = new List<double>();
                        int totalScoops = (from a in fingerFromScoops select a).Sum();
                        for(int i=0; i < fingerFromScoops.Count; i++)
                            fingerTonnes.Add(tonnes * (double)fingerFromScoops[i] / (double)totalScoops);

                        if (fingerFromScoops.Count > 1 && (SampledOn.Minute == 59 && (SampledOn.Hour == 5 || SampledOn.Hour == 17)))
                            SampledOn = SampledOn.AddMinutes(-fingerFromScoops.Count+1);

                        AURSampleResults sampleResults = null;
                        for(int i=0; i < fingerFrom.Count; i++)
                        {
                            AURTransaction fingerMovement = new AURTransaction
                            {
                                STDTruckType = "STD",
                                SamplePrefix = noResults && (SampleId.Substring(3, 2).Equals("PS") || SampleId.Substring(3, 2).Equals("LS")) ? "" : (SampleId + c_sampleSuffix[i]),
                                MassVolTrips = fingerTonnes[i].ToString(),
                                RowComment = Comments,
                                Source = fingerFrom[i],
                                Destination = To,
                                TransactionDate = AURTransaction.AURDateFormat(SampledOn.ToString()),
                                TransHistoryDate = SampledOn.Hour < 6 || SampledOn.Hour >= 18 ? AURTransaction.AURDateFormat(shiftDate.AddHours(12).ToString()) : AURTransaction.AURDateFormat(shiftDate.ToString()),
                                RowID = "TE_ROM_CRSH (" + Properties.Settings.Default.AtlasOperation + ")",
                                TransactionEditor = "TE_CRSH_" + Properties.Settings.Default.AtlasOperation,
                                TransactionGUID = Properties.Settings.Default.AtlasOperation + "Crushing_" + shiftDate.ToString("dd-MM-yyyy")
                            };
                            fingerMovement.ExtendedData["Carousel"] = SampleBucket;
                            fingerMovement.ExtendedData["Weightometer"] = Weightometer.ToString("#########0");
                            fingerMovement.ExtendedData["StackerPosition"] = StackerPosition;
                            fingerMovement.ExtendedData["Submission"] = Submission;
                            fingerMovement.ExtendedData["BlendRatio"] = fingerFromScoops[i].ToString();

                            if (!foundFirstSampleAfterEOD)
                                movements.Add(fingerMovement);

                            sampleResults = new AURSampleResults
                            {
                                SampleId = SampleId + c_sampleSuffix[i],
                                DateAnalysed = receivedOn < this.StartDateTime ? DateTime.Now.ToString() : receivedOn.ToString(),
                            };
                            sampleResults.Assays["Fe"] = Fe < 0.0 ? "" : Fe.ToString("####.0000");
                            sampleResults.Assays["Al2O3"] = Al2O3 < 0.0 ? "" : Al2O3.ToString("####.0000");
                            sampleResults.Assays["SiO2"] = SiO2 < 0.0 ? "" : SiO2.ToString("####.0000");
                            sampleResults.Assays["P"] = P < 0.0 ? "" : P.ToString("####.0000");
                            sampleResults.Assays["S"] = S < 0.0 ? "" : S.ToString("####.0000");
                            sampleResults.Assays["Mn"] = Mn < 0.0 ? "" : Mn.ToString("####.0000");
                            sampleResults.Assays["LOI1000"] = LOI < 0.0 ? "" : LOI.ToString("####.0000");
                            sampleResults.Assays["H2O"] = Moisture < 0.0 ? "" : Moisture.ToString("####.0000");
                            sampleResults.Assays["+12.5mm"] = Size12o5mm < 0.0 ? "" : Size12o5mm.ToString("####.0000");
                            sampleResults.Assays["+10_12mm"] = Size10o0_12o5mm < 0.0 ? "" : Size10o0_12o5mm.ToString("####.0000");
                            sampleResults.Assays["+9.5_10mm"] = Size9o5_10o0mm < 0.0 ? "" : Size9o5_10o0mm.ToString("####.0000");
                            sampleResults.Assays["+6.7_9.5mm"] = Size6o7_9o5mm < 0.0 ? "" : Size6o7_9o5mm.ToString("####.0000");
                            sampleResults.Assays["+0.5_6.7mm"] = Size0o5_6o7mm < 0.0 ? "" : Size0o5_6o7mm.ToString("####.0000");
                            sampleResults.Assays["+0_0.5mm"] = Size0o0_0o5mm < 0.0 ? "" : Size0o0_0o5mm.ToString("####.0000");
                            //sampleResults.Assays["+40mm"] = Size40mm < 0.0 ? "" : Size40mm.ToString("####.0000");
                            //sampleResults.Assays["+6_3_40mm"] = Size6_3_40mm < 0.0 ? "" : Size6_3_40mm.ToString("####.0000");
                            //sampleResults.Assays["+0_0_6_3mm"] = Size0_0_6_3mm < 0.0 ? "" : Size0_0_6_3mm.ToString("####.0000");

                            if (!foundFirstSampleAfterEOD && !(noResults && (SampleId.Substring(3, 2).Equals("PS") || SampleId.Substring(3, 2).Equals("LS"))))
                                assays.Add(sampleResults);                        

                            SampledOn = SampledOn.AddMinutes(1);
                        }

                        if (assays.Count > 1 && !differentFeed)
                        {
                            if (foundFirstSampleAfterEOD)
                            {
                                for (int i = assays.Count - 1; i >= 0 && !(assays[i].SampleId.Substring(3, 2).Equals("PS") || assays[i].SampleId.Substring(3, 2).Equals("LS")); i--)
                                    assays[i].Assays = sampleResults.Assays;
                            }
                            else
                            {
                                for (int i = assays.Count - 2; i >= 0 && !(assays[i].SampleId.Substring(3, 2).Equals("PS") || assays[i].SampleId.Substring(3, 2).Equals("LS")); i--)
                                    assays[i].Assays = sampleResults.Assays;
                            }
                        }
                    }

                    reader.Close();

                    if (lastSampledOnTime != null && lastSampledOnTime.Value.Hour != 5 && lastSampledOnTime.Value.Minute != 59)
                    {
                        DateTime endOfShiftDate = new DateTime(2000, 1, 1, 5, 59, 0);
                        MessageBox.Show("There must be a weightometer reading (sample or end of shift) at " + endOfShiftDate.AddHours(-Properties.Settings.Default.PSShiftTimeOffset).Hour.ToString() + ":" + endOfShiftDate.AddHours(-Properties.Settings.Default.PSShiftTimeOffset).Minute.ToString("00") + ".", "Error", MessageBoxButtons.OK);
                        return;
                    }

                    foreach(String sampleId in publishedSamples)
                    {       
                        try
                        {
                            SqlCommand cmdPublished = new SqlCommand("update psm_Product_Samples set Published = NULL where SampleId = @1", this.Connection);
                            cmdPublished.Parameters.Add(new SqlParameter { ParameterName = "@1", Value = sampleId, SqlDbType = SqlDbType.NVarChar });
                            cmdPublished.ExecuteNonQuery();
                        }
                        catch (System.Exception exc)
                        {
                        }
                    }

                    AllMovements.AddRange(movements);
                    AllAssays.AddRange(assays);
                }


                String movementsFileName = Properties.Settings.Default.TempDirectory + Properties.Settings.Default.AtlasOperation + "Crushing_" + this.StartDateTime.ToString("dd-MM-yyyy") + ".csv";
                AURTransaction.Save(movementsFileName, AllMovements);
                String assayFileName = Properties.Settings.Default.TempDirectory + Properties.Settings.Default.AtlasOperation + "CrushingAssays_" + this.StartDateTime.ToString("dd-MM-yyyy") + ".csv";
                AURSampleResults.Save(assayFileName, AllAssays);

                TransferMovementsDlg transfer = new TransferMovementsDlg
                {
                    TransferFiles = new List<AURTransferFile>() { new AURTransferFile { AURFileName = movementsFileName, AURFileNamePrefix = AURTransaction.c_AURiHubFileNamePrefix }, new AURTransferFile { AURFileName = assayFileName, AURFileNamePrefix = AURSampleResults.c_AURiHubFileNamePrefix } },
                    iHubErrorDirectory = Properties.Settings.Default.iHubErrorDirectory,
                    iHubProcessedDirectory = Properties.Settings.Default.iHubProcessedDirectory,
                    iHubProcessingDirectory = Properties.Settings.Default.iHubProcessingDirectory,
                    iHubPendingDirectory = Properties.Settings.Default.iHubPendingDirectory,
                    
                };
                transfer.ShowDialog();
            }
            catch (System.Exception exc)
            {
                MessageBox.Show("Internal error detected - please report to evans@endevea.com : " + exc.Message, "Error", MessageBoxButtons.OK);
            }
        }
    }
}
