using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AtlasReportToolkit;

namespace AtlasSampleManager
{
    public partial class ConfigureSettingsDlg : Form
    {
        [Serializable]
        public class ConfigSettings
        {
            public String AtlasGradeControlDatabase;
            public String AtlasMinemarketLinkDatabase;
            public String AtlasSubmissionBSNameSuffix;
            public String AtlasSubmissionPSNameSuffix;
            public String LaboratoryEmailList;

            public String BSPendingShotSampling;

            public String BSSubmissionDirectory;
            public String BSResultsDirectory;
            public String BSAcceptedResultsDirectory;

            public String PSSubmissionDirectory;
            public String PSResultsDirectory;
            public String PSAcceptedResultsDirectory;
            public Double PSShiftTimeOffset;

            public StringCollection CrusherStreams;
        }

        public ConfigureSettingsDlg()
        {
            InitializeComponent();
        }

        private void butClose_Click(object sender, EventArgs e)
        {
            this.Close();

            Properties.Settings.Default.Save();
        }

        private void ConfigureSettingsDlg_Load(object sender, EventArgs e)
        {
            ignore_lstMineSite_SelectedIndexChanged = true;
            lstMineSite.DataBindings.Add("Text", Properties.Settings.Default, "AtlasOperation", false, DataSourceUpdateMode.OnPropertyChanged);
            lstLaboratory.DataBindings.Add("Text", Properties.Settings.Default, "Laboratory", false, DataSourceUpdateMode.OnPropertyChanged);
            lstAtlasCompany.DataBindings.Add("Text", Properties.Settings.Default, "AtlasCompany", false, DataSourceUpdateMode.OnPropertyChanged);
            lstSubmissionBSPrefix.DataBindings.Add("Text", Properties.Settings.Default, "AtlasSubmissionBSNameSuffix", false, DataSourceUpdateMode.OnPropertyChanged);
            lstSubmissionPSPrefix.DataBindings.Add("Text", Properties.Settings.Default, "AtlasSubmissionPSNameSuffix", false, DataSourceUpdateMode.OnPropertyChanged);

            txtConnectionString.DataBindings.Add("Text", Properties.Settings.Default, "AtlasGradeControlDatabase", false, DataSourceUpdateMode.OnPropertyChanged);
            txtPDMSConnectionString.DataBindings.Add("Text", Properties.Settings.Default, "AtlasMinemarketLinkDatabase", false, DataSourceUpdateMode.OnPropertyChanged);
            txtSubmissionEmailList.DataBindings.Add("Text", Properties.Settings.Default, "LaboratoryEmailList", false, DataSourceUpdateMode.OnPropertyChanged);

            txtBSPendingShotSampling.DataBindings.Add("Text", Properties.Settings.Default, "BSPendingShotSampling", false, DataSourceUpdateMode.OnPropertyChanged);

            txtBSSubmissionDirectory.DataBindings.Add("Text", Properties.Settings.Default, "BSSubmissionDirectory", false, DataSourceUpdateMode.OnPropertyChanged);
            //txtBSSampleDespatchDirectory.DataBindings.Add("Text", Properties.Settings.Default, "BSSampleDespatchDirectory", false, DataSourceUpdateMode.OnPropertyChanged);
            //txtBSArrivedForGeochemicalAnalysis.DataBindings.Add("Text", Properties.Settings.Default, "BSArrivedForGeochemicalAnalysisDirectory", false, DataSourceUpdateMode.OnPropertyChanged);
            txtBSResultsDirectory.DataBindings.Add("Text", Properties.Settings.Default, "BSResultsDirectory", false, DataSourceUpdateMode.OnPropertyChanged);
            txtBSAcceptedResultsDirectory.DataBindings.Add("Text", Properties.Settings.Default, "BSAcceptedResultsDirectory", false, DataSourceUpdateMode.OnPropertyChanged);
            //txtBSPendingAnalyticalReception.DataBindings.Add("Text", Properties.Settings.Default, "BSPendingAnalyticalReceptionDirectory", false, DataSourceUpdateMode.OnPropertyChanged);
            //txtBSPendingPreparationReceptionDirectory.DataBindings.Add("Text", Properties.Settings.Default, "BSPendingPreparationReceptionDirectory", false, DataSourceUpdateMode.OnPropertyChanged);

            txtPSSubmissionDirectory.DataBindings.Add("Text", Properties.Settings.Default, "PSSubmissionDirectory", false, DataSourceUpdateMode.OnPropertyChanged);
            //txtPSSampleDespatchDirectory.DataBindings.Add("Text", Properties.Settings.Default, "PSSampleDespatchDirectory", false, DataSourceUpdateMode.OnPropertyChanged);
            //txtPSArrivedForGeochemicalAnalysis.DataBindings.Add("Text", Properties.Settings.Default, "PSArrivedForGeochemicalAnalysisDirectory", false, DataSourceUpdateMode.OnPropertyChanged);
            txtPSResultsDirectory.DataBindings.Add("Text", Properties.Settings.Default, "PSResultsDirectory", false, DataSourceUpdateMode.OnPropertyChanged);
            txtPSAcceptedResultsDirectory.DataBindings.Add("Text", Properties.Settings.Default, "PSAcceptedResultsDirectory", false, DataSourceUpdateMode.OnPropertyChanged);
            //txtPSPendingAnalyticalReception.DataBindings.Add("Text", Properties.Settings.Default, "PSPendingAnalyticalReceptionDirectory", false, DataSourceUpdateMode.OnPropertyChanged);
            //txtPSPendingPreparationReceptionDirectory.DataBindings.Add("Text", Properties.Settings.Default, "PSPendingPreparationReceptionDirectory", false, DataSourceUpdateMode.OnPropertyChanged);
            txtPSShiftTimeAdjustment.DataBindings.Add("Text", Properties.Settings.Default, "PSShiftTimeOffset", false, DataSourceUpdateMode.OnPropertyChanged);
            txtStreams.Text = Properties.Settings.Default.CrusherStreams.ToString();
            ignore_lstMineSite_SelectedIndexChanged = false;
        }

        //private void txtBSDespatchedFromMineDirectory_ButtonClick(object sender, EventArgs e)
        //{
        //    FolderBrowserDialog browse = new FolderBrowserDialog();
        //    browse.SelectedPath = Properties.Settings.Default.BSSampleDespatchDirectory;
        //    browse.ShowNewFolderButton = false;
        //    if (browse.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //        Properties.Settings.Default.BSSampleDespatchDirectory = browse.SelectedPath;
        //}

        //private void txtBSPendingForPreparationDirectory_ButtonClick(object sender, EventArgs e)
        //{
        //    FolderBrowserDialog browse = new FolderBrowserDialog();
        //    browse.SelectedPath = Properties.Settings.Default.BSPendingPreparationReceptionDirectory;
        //    browse.ShowNewFolderButton = false;
        //    if (browse.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //        Properties.Settings.Default.BSPendingPreparationReceptionDirectory = browse.SelectedPath;
        //}

        //private void txtBSArrivedAtAnalysisDirectory_ButtonClick(object sender, EventArgs e)
        //{
        //    FolderBrowserDialog browse = new FolderBrowserDialog();
        //    browse.SelectedPath = Properties.Settings.Default.BSArrivedForGeochemicalAnalysisDirectory;
        //    browse.ShowNewFolderButton = false;
        //    if (browse.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //        Properties.Settings.Default.BSArrivedForGeochemicalAnalysisDirectory = browse.SelectedPath;
        //}

        //private void txtBSPendingForAnalysisDirectory_ButtonClick(object sender, EventArgs e)
        //{
        //    FolderBrowserDialog browse = new FolderBrowserDialog();
        //    browse.SelectedPath = Properties.Settings.Default.BSPendingAnalyticalReceptionDirectory;
        //    browse.ShowNewFolderButton = false;
        //    if (browse.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //        Properties.Settings.Default.BSPendingAnalyticalReceptionDirectory = browse.SelectedPath;
        //}

        private void txtBSResultsDirectory_ButtonClick(object sender, EventArgs e)
        {
            FolderBrowserDialog browse = new FolderBrowserDialog();
            browse.SelectedPath = Properties.Settings.Default.BSResultsDirectory;
            browse.ShowNewFolderButton = false;
            if (browse.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                Properties.Settings.Default.BSResultsDirectory = browse.SelectedPath;
        }

        private void txtBSAcceptedResultsDirectory_ButtonClick(object sender, EventArgs e)
        {
            FolderBrowserDialog browse = new FolderBrowserDialog();
            browse.SelectedPath = Properties.Settings.Default.BSAcceptedResultsDirectory;
            browse.ShowNewFolderButton = false;
            if (browse.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                Properties.Settings.Default.BSAcceptedResultsDirectory = browse.SelectedPath;
        }

        private void txtBSSubmissionDirectory_ButtonClick(object sender, EventArgs e)
        {
            FolderBrowserDialog browse = new FolderBrowserDialog();
            browse.SelectedPath = Properties.Settings.Default.BSSubmissionDirectory;
            browse.ShowNewFolderButton = false;
            if (browse.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                Properties.Settings.Default.BSSubmissionDirectory = browse.SelectedPath;
        }


        //private void txtPSDespatchedFromMineDirectory_ButtonClick(object sender, EventArgs e)
        //{
        //    FolderBrowserDialog browse = new FolderBrowserDialog();
        //    browse.SelectedPath = Properties.Settings.Default.PSSampleDespatchDirectory;
        //    browse.ShowNewFolderButton = false;
        //    if (browse.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //        Properties.Settings.Default.PSSampleDespatchDirectory = browse.SelectedPath;
        //}

        //private void txtPSPendingForPreparationDirectory_ButtonClick(object sender, EventArgs e)
        //{
        //    FolderBrowserDialog browse = new FolderBrowserDialog();
        //    browse.SelectedPath = Properties.Settings.Default.PSPendingPreparationReceptionDirectory;
        //    browse.ShowNewFolderButton = false;
        //    if (browse.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //        Properties.Settings.Default.PSPendingPreparationReceptionDirectory = browse.SelectedPath;
        //}

        //private void txtPSArrivedAtAnalysisDirectory_ButtonClick(object sender, EventArgs e)
        //{
        //    FolderBrowserDialog browse = new FolderBrowserDialog();
        //    browse.SelectedPath = Properties.Settings.Default.PSArrivedForGeochemicalAnalysisDirectory;
        //    browse.ShowNewFolderButton = false;
        //    if (browse.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //        Properties.Settings.Default.PSArrivedForGeochemicalAnalysisDirectory = browse.SelectedPath;
        //}

        //private void txtPSPendingForAnalysisDirectory_ButtonClick(object sender, EventArgs e)
        //{
        //    FolderBrowserDialog browse = new FolderBrowserDialog();
        //    browse.SelectedPath = Properties.Settings.Default.PSPendingAnalyticalReceptionDirectory;
        //    browse.ShowNewFolderButton = false;
        //    if (browse.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //        Properties.Settings.Default.PSPendingAnalyticalReceptionDirectory = browse.SelectedPath;
        //}

        private void txtPSResultsDirectory_ButtonClick(object sender, EventArgs e)
        {
            FolderBrowserDialog browse = new FolderBrowserDialog();
            browse.SelectedPath = Properties.Settings.Default.PSResultsDirectory;
            browse.ShowNewFolderButton = false;
            if (browse.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                Properties.Settings.Default.PSResultsDirectory = browse.SelectedPath;
        }

        private void txtPSAcceptedResultsDirectory_ButtonClick(object sender, EventArgs e)
        {
            FolderBrowserDialog browse = new FolderBrowserDialog();
            browse.SelectedPath = Properties.Settings.Default.PSAcceptedResultsDirectory;
            browse.ShowNewFolderButton = false;
            if (browse.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                Properties.Settings.Default.PSAcceptedResultsDirectory = browse.SelectedPath;
        }

        private void txtPSSubmissionDirectory_ButtonClick(object sender, EventArgs e)
        {
            FolderBrowserDialog browse = new FolderBrowserDialog();
            browse.SelectedPath = Properties.Settings.Default.PSSubmissionDirectory;
            browse.ShowNewFolderButton = false;
            if (browse.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                Properties.Settings.Default.PSSubmissionDirectory = browse.SelectedPath;
        }

        private void txtBSPendingShotSampling_ButtonClick(object sender, EventArgs e)
        {
            FolderBrowserDialog browse = new FolderBrowserDialog();
            browse.SelectedPath = Properties.Settings.Default.BSPendingShotSampling;
            browse.ShowNewFolderButton = false;
            if (browse.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                Properties.Settings.Default.BSPendingShotSampling = browse.SelectedPath;
        }
        private void ConfigureSettingsDlg_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private Boolean ignore_lstMineSite_SelectedIndexChanged = false;
        private void lstMineSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignore_lstMineSite_SelectedIndexChanged)
                return;

            ConfigureSettingsDlg.UpdateConfiguration(lstMineSite.Text);
        }

        public static void UpdateConfiguration(String mine)
        {
            ConfigSettings savedSettings = null;

            try
            {
                savedSettings = Extensions.Load<ConfigSettings>(Properties.Settings.Default.ConfigDirectory + "\\" + mine + "_SampleManager.xml");
            }
            catch (System.Exception exc)
            {
            }
            if (savedSettings == null)
            {
                if (mine.Equals("PDO"))
                {
                    ConfigurePDO();
                }
                if (mine.Equals("WOD"))
                {
                    ConfigureWOD();
                }
                if (mine.Equals("DOV"))
                {
                    ConfigureDOV();
                }
                if (mine.Equals("WEB"))
                {
                    ConfigureWEB();
                }
            }
            else
            {
                Properties.Settings.Default.AtlasGradeControlDatabase = savedSettings.AtlasGradeControlDatabase;
                Properties.Settings.Default.AtlasMinemarketLinkDatabase = savedSettings.AtlasMinemarketLinkDatabase;
                Properties.Settings.Default.AtlasSubmissionBSNameSuffix = savedSettings.AtlasSubmissionBSNameSuffix;
                Properties.Settings.Default.AtlasSubmissionPSNameSuffix = savedSettings.AtlasSubmissionPSNameSuffix;
                Properties.Settings.Default.LaboratoryEmailList = savedSettings.LaboratoryEmailList;

                Properties.Settings.Default.BSPendingShotSampling = savedSettings.BSPendingShotSampling;

                Properties.Settings.Default.BSSubmissionDirectory = savedSettings.BSSubmissionDirectory;
                Properties.Settings.Default.BSResultsDirectory = savedSettings.BSResultsDirectory;
                Properties.Settings.Default.BSAcceptedResultsDirectory = savedSettings.BSAcceptedResultsDirectory;

                Properties.Settings.Default.PSSubmissionDirectory = savedSettings.PSSubmissionDirectory;
                Properties.Settings.Default.PSResultsDirectory = savedSettings.PSResultsDirectory;
                Properties.Settings.Default.PSAcceptedResultsDirectory = savedSettings.PSAcceptedResultsDirectory;
                Properties.Settings.Default.PSShiftTimeOffset = savedSettings.PSShiftTimeOffset;
                Properties.Settings.Default.CrusherStreams = savedSettings.CrusherStreams;
            }
            try
            {                
                savedSettings = new ConfigSettings();
                savedSettings.AtlasGradeControlDatabase = Properties.Settings.Default.AtlasGradeControlDatabase;
                savedSettings.AtlasMinemarketLinkDatabase = Properties.Settings.Default.AtlasMinemarketLinkDatabase;
                savedSettings.AtlasSubmissionBSNameSuffix = Properties.Settings.Default.AtlasSubmissionBSNameSuffix;
                savedSettings.AtlasSubmissionPSNameSuffix = Properties.Settings.Default.AtlasSubmissionPSNameSuffix;
                savedSettings.LaboratoryEmailList = Properties.Settings.Default.LaboratoryEmailList;

                savedSettings.BSPendingShotSampling = Properties.Settings.Default.BSPendingShotSampling;

                savedSettings.BSSubmissionDirectory = Properties.Settings.Default.BSSubmissionDirectory;
                savedSettings.BSResultsDirectory = Properties.Settings.Default.BSResultsDirectory;
                savedSettings.BSAcceptedResultsDirectory = Properties.Settings.Default.BSAcceptedResultsDirectory;

                savedSettings.PSSubmissionDirectory = Properties.Settings.Default.PSSubmissionDirectory;
                savedSettings.PSResultsDirectory = Properties.Settings.Default.PSResultsDirectory;
                savedSettings.PSAcceptedResultsDirectory = Properties.Settings.Default.PSAcceptedResultsDirectory;
                savedSettings.PSShiftTimeOffset = Properties.Settings.Default.PSShiftTimeOffset;
                savedSettings.CrusherStreams = Properties.Settings.Default.CrusherStreams;
                Extensions.Save<ConfigSettings>(savedSettings, Properties.Settings.Default.ConfigDirectory + "\\" + mine + "_SampleManager.xml");
            }
            catch (System.Exception exc)
            {
            }
            Properties.Settings.Default.iHubPendingDirectory = @"\\per-pdms\iHub\Pending";
            Properties.Settings.Default.iHubProcessedDirectory = @"\\per-pdms\iHub\Processed";
            Properties.Settings.Default.iHubErrorDirectory = @"\\per-pdms\iHub\Error";
            Properties.Settings.Default.iHubProcessingDirectory = @"\\per-pdms\iHub\Processing";
        } 

        private static void ConfigurePDO()
        {
            Properties.Settings.Default.AtlasGradeControlDatabase = "Server=PDO-FS01;Database=Atlas_PRODGCV_PDO;Trusted_Connection=True;";
            Properties.Settings.Default.AtlasMinemarketLinkDatabase = "Server=minemarketsql;Database=MinemarketLink;Trusted_Connection=True;";
            Properties.Settings.Default.AtlasSubmissionBSNameSuffix = "ATLAS IRON PDO_BS_";
            Properties.Settings.Default.AtlasSubmissionPSNameSuffix = "ATLAS IRON PDO_PS_";
            //Properties.Settings.Default.LaboratoryEmailList = "deirdre.crawford@intertek.com; mukit.hossain@intertek.com; elenita.tumbaga@intertek.com; Guy.Pelling@atlasiron.com.au; hayden.francis@intertek.com; cbaauspal@intertek.com; mukit.hossain@intertek.com; marites.ocampo@intertek.com; Simon.Gobbett@atlasiron.com.au; brenden.chuck@atlasiron.com.au; tom.westerhuis@atlasiron.com.au; amy.swift@atlasiron.com.au; Paula.McKinney@atlasiron.com.au; Jack.Milford@atlasiron.com.au";
            Properties.Settings.Default.LaboratoryEmailList = "porthedland.preplab@intertek.com; wodginalab@intertek.com; richard.burridge@intertek.com; Kitkeung.chan@intertek.com; Asmat.khan@intertek.com; deirdre.crawford@intertek.com; mukit.hossain@intertek.com; elenita.tumbaga@intertek.com; hayden.francis@intertek.com; marites.ocampo@intertek.com; pdo.geology@atlasiron.com.au";

            Properties.Settings.Default.BSPendingShotSampling = @"P:\PDO\ServiceProviders\Intertek\BlastholeAnalysis\ShotSampling";

            Properties.Settings.Default.BSSubmissionDirectory = @"Q:\05_Geology\05_MineGeology\2 Data\2 Pardoo\4 Samples\Blasthole\Submissions";
            Properties.Settings.Default.BSResultsDirectory = @"Q:\05_Geology\05_MineGeology\2 Data\2 Pardoo\4 Samples\Blasthole\GeochemicalResults";
            Properties.Settings.Default.BSAcceptedResultsDirectory = @"Q:\05_Geology\05_MineGeology\2 Data\2 Pardoo\4 Samples\Blasthole\ApprovedResults";

            Properties.Settings.Default.PSSubmissionDirectory = @"Q:\05_Geology\05_MineGeology\2 Data\2 Pardoo\4 Samples\Plant\Submissions";
            Properties.Settings.Default.PSResultsDirectory = @"Q:\05_Geology\05_MineGeology\2 Data\2 Pardoo\4 Samples\Plant\GeochemicalResults";
            Properties.Settings.Default.PSAcceptedResultsDirectory = @"Q:\05_Geology\05_MineGeology\2 Data\2 Pardoo\4 Samples\Plant\ApprovedResults";
            Properties.Settings.Default.PSShiftTimeOffset = 0.0;
        }

        private static void ConfigureWOD()
        {
            Properties.Settings.Default.AtlasGradeControlDatabase = "Server=WOD-FS01;Database=Atlas_PRODGCV_WOD;Trusted_Connection=True;";
            Properties.Settings.Default.AtlasMinemarketLinkDatabase = "Server=minemarketsql;Database=MinemarketLink;Trusted_Connection=True;";
            Properties.Settings.Default.AtlasSubmissionBSNameSuffix = "ATLAS IRON WOD_BS_";
            Properties.Settings.Default.AtlasSubmissionPSNameSuffix = "ATLAS IRON WOD_PS_";
            //Properties.Settings.Default.LaboratoryEmailList = "Rebecca.Turbitt@atlasiron.com.au; Titus.Mercea@atlasiron.com.au; deirdre.crawford@intertek.com; mukit.hossain@intertek.com; David.Tabrett@atlasiron.com.au; elenita.tumbaga@intertek.com; hayden.francis@intertek.com; cbaauspal@intertek.com; mukit.hossain@intertek.com; marites.ocampo@intertek.com; Peter.Laka@atlasiron.com.au; Michael.Graham@atlasiron.com.au; Warren.Carter@atlasiron.com.au; Maryanne.Stan-Bishop@atlasiron.com.au; Howard.Daley@atlasiron.com.au;  amy.swift@atlasiron.com.au; ben.riley@atlasiron.com.au";
            Properties.Settings.Default.LaboratoryEmailList = "porthedland.preplab@intertek.com; wodginalab@intertek.com; richard.burridge@intertek.com; Kitkeung.chan@intertek.com; Asmat.khan@intertek.com; deirdre.crawford@intertek.com; mukit.hossain@intertek.com; elenita.tumbaga@intertek.com; hayden.francis@intertek.com; marites.ocampo@intertek.com; wod.geology@atlasiron.com.au";

            Properties.Settings.Default.BSPendingShotSampling = @"P:\WOD\ServiceProviders\Intertek\BlastholeAnalysis\ShotSampling";

            Properties.Settings.Default.BSSubmissionDirectory = @"Q:\02_MineGeology\Revised_file_structure_Geology_V1\MineGeology\2_Data\1 Wodgina\4_Samples\1_Blast Hole\3_Submissions";
            Properties.Settings.Default.BSResultsDirectory = @"Q:\02_MineGeology\Revised_file_structure_Geology_V1\MineGeology\2_Data\1 Wodgina\4_Samples\1_Blast Hole\4_GeochemicalResults";
            Properties.Settings.Default.BSAcceptedResultsDirectory = @"Q:\02_MineGeology\Revised_file_structure_Geology_V1\MineGeology\2_Data\1 Wodgina\4_Samples\1_Blast Hole\5_ApprovedResults";

            Properties.Settings.Default.PSSubmissionDirectory = @"Q:\02_MineGeology\Revised_file_structure_Geology_V1\MineGeology\2_Data\1 Wodgina\4_Samples\3_Plant\3_Submissions";
            Properties.Settings.Default.PSResultsDirectory = @"Q:\02_MineGeology\Revised_file_structure_Geology_V1\MineGeology\2_Data\1 Wodgina\4_Samples\3_Plant\4_GeochemicalResults";
            Properties.Settings.Default.PSAcceptedResultsDirectory = @"Q:\02_MineGeology\Revised_file_structure_Geology_V1\MineGeology\2_Data\1 Wodgina\4_Samples\3_Plant\5_ApprovedResults";
            Properties.Settings.Default.PSShiftTimeOffset = 1.0;

        }

        private static void ConfigureDOV()
        {
            //Properties.Settings.Default.iHubPendingDirectory = @"c:\iHub\Pending";
            //Properties.Settings.Default.iHubProcessedDirectory = @"c:\iHub\Processed";
            //Properties.Settings.Default.iHubProcessingDirectory = @"c:\iHub\Processing";
            //Properties.Settings.Default.iHubErrorDirectory = @"c:\iHub\Error";

            //Properties.Settings.Default.AtlasGradeControlDatabase = @"Server=aby-pc034\atlasiron;Database=Atlas_PRODGCV_DOV;Trusted_Connection=True;";
            Properties.Settings.Default.AtlasGradeControlDatabase = @"Server=MTD-FS01;Database=Atlas_PRODGCV_DOV;Trusted_Connection=True;";
            Properties.Settings.Default.AtlasMinemarketLinkDatabase = @"Server=minemarketsql;Database=MinemarketLink;Trusted_Connection=True;";
            Properties.Settings.Default.AtlasSubmissionBSNameSuffix = "ATLAS IRON DOV_BS_";
            Properties.Settings.Default.AtlasSubmissionPSNameSuffix = "ATLAS IRON DOV_PS_";
            //Properties.Settings.Default.LaboratoryEmailList = "Rebecca.Turbitt@atlasiron.com.au; Titus.Mercea@atlasiron.com.au; deirdre.crawford@intertek.com; mukit.hossain@intertek.com; David.Tabrett@atlasiron.com.au; elenita.tumbaga@intertek.com; hayden.francis@intertek.com; cbaauspal@intertek.com; mukit.hossain@intertek.com; marites.ocampo@intertek.com; Peter.Laka@atlasiron.com.au; Michael.Graham@atlasiron.com.au; Warren.Carter@atlasiron.com.au; Maryanne.Stan-Bishop@atlasiron.com.au; Howard.Daley@atlasiron.com.au;  amy.swift@atlasiron.com.au; ben.riley@atlasiron.com.au";
            Properties.Settings.Default.LaboratoryEmailList = "porthedland.preplab@intertek.com; wodginalab@intertek.com; richard.burridge@intertek.com; Kitkeung.chan@intertek.com; Asmat.khan@intertek.com; deirdre.crawford@intertek.com; mukit.hossain@intertek.com; elenita.tumbaga@intertek.com; hayden.francis@intertek.com; marites.ocampo@intertek.com; wod.geology@atlasiron.com.au";

            Properties.Settings.Default.BSPendingShotSampling = @"C:\DOV\Blastholes\ShotSampling";

            Properties.Settings.Default.BSSubmissionDirectory = @"C:\DOV\Blastholes\Submissions";
            Properties.Settings.Default.BSResultsDirectory = @"C:\DOV\Blastholes\GeochemicalResults";
            Properties.Settings.Default.BSAcceptedResultsDirectory = @"C:\DOV\Blastholes\ApprovedResults";

            Properties.Settings.Default.PSSubmissionDirectory = @"C:\DOV\Plant\Submissions";
            Properties.Settings.Default.PSResultsDirectory = @"C:\DOV\Plant\GeochemicalResults";
            Properties.Settings.Default.PSAcceptedResultsDirectory = @"C:\DOV\Plant\ApprovedResults";
            Properties.Settings.Default.PSShiftTimeOffset = 0.0;
        }

        private static void ConfigureWEB()
        {
            //Properties.Settings.Default.iHubPendingDirectory = @"c:\iHub\Pending";
            //Properties.Settings.Default.iHubProcessedDirectory = @"c:\iHub\Processed";
            //Properties.Settings.Default.iHubProcessingDirectory = @"c:\iHub\Processing";
            //Properties.Settings.Default.iHubErrorDirectory = @"c:\iHub\Error";

            //Properties.Settings.Default.AtlasGradeControlDatabase = @"Server=aby-pc034\atlasiron;Database=Atlas_PRODGCV_DOV;Trusted_Connection=True;";
            Properties.Settings.Default.AtlasGradeControlDatabase = @"Server=MTW-FS01;Database=Atlas_PRODGCV_MTW;Trusted_Connection=True;";
            Properties.Settings.Default.AtlasMinemarketLinkDatabase = @"Server=minemarketsql;Database=MinemarketLink;Trusted_Connection=True;";
            Properties.Settings.Default.AtlasSubmissionBSNameSuffix = "ATLAS IRON WEB_BS_";
            Properties.Settings.Default.AtlasSubmissionPSNameSuffix = "ATLAS IRON WEB_PS_";
            //Properties.Settings.Default.LaboratoryEmailList = "Rebecca.Turbitt@atlasiron.com.au; Titus.Mercea@atlasiron.com.au; deirdre.crawford@intertek.com; mukit.hossain@intertek.com; David.Tabrett@atlasiron.com.au; elenita.tumbaga@intertek.com; hayden.francis@intertek.com; cbaauspal@intertek.com; mukit.hossain@intertek.com; marites.ocampo@intertek.com; Peter.Laka@atlasiron.com.au; Michael.Graham@atlasiron.com.au; Warren.Carter@atlasiron.com.au; Maryanne.Stan-Bishop@atlasiron.com.au; Howard.Daley@atlasiron.com.au;  amy.swift@atlasiron.com.au; ben.riley@atlasiron.com.au";
            Properties.Settings.Default.LaboratoryEmailList = "MTW-geology@atlasiron.com.au; wodginalab@intertek.com;";

            Properties.Settings.Default.BSPendingShotSampling = @"C:\DOV\Blastholes\ShotSampling";

            Properties.Settings.Default.BSSubmissionDirectory = @"G:\17 OPERATIONS\02_Geology\02 Data\05 Mt Webber\01 Sampling\01 Blast Hole\01 Submissions";
            Properties.Settings.Default.BSResultsDirectory = @"G:\17 OPERATIONS\02_Geology\02 Data\05 Mt Webber\01 Sampling\01 Blast Hole\02 Incoming Assays";
            Properties.Settings.Default.BSAcceptedResultsDirectory = @"G:\17 OPERATIONS\02_Geology\02 Data\05 Mt Webber\01 Sampling\01 Blast Hole\03 Uploaded Assays";

            Properties.Settings.Default.PSSubmissionDirectory = @"G:\17 OPERATIONS\02_Geology\02 Data\05 Mt Webber\01 Sampling\02 Plant\01 Submissions";
            Properties.Settings.Default.PSResultsDirectory = @"G:\17 OPERATIONS\02_Geology\02 Data\05 Mt Webber\01 Sampling\02 Plant\02 Incoming Assays";
//            Properties.Settings.Default.PSResultsDirectory = @"e:\temp";
            Properties.Settings.Default.PSAcceptedResultsDirectory = @"G:\17 OPERATIONS\02_Geology\02 Data\05 Mt Webber\01 Sampling\02 Plant\03 Uploaded Assays";
            Properties.Settings.Default.PSShiftTimeOffset = 0.0;
        }
    }
}
