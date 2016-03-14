using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;
using System.Diagnostics;

namespace AtlasReportToolkit
{
    public class iHubMonitor
    {
        public String iHubPendingDirectory { get; set; }
        public String iHubProcessingDirectory { get; set; }
        public String iHubProcessedDirectory { get; set; }
        public String iHubErrorDirectory { get; set; }

        public List<AURTransferFile> TransferFiles { get; set; }

        private Thread MonitorWorker { get; set; }
        private String AURCSVFileName { get; set; }
        private String AURLogFileName { get; set; }

        public delegate void LogChangedEventHndler(String msg);

        public LogChangedEventHndler LogChanged { get; set; }

        public void Start()
        {
            foreach (AURTransferFile transferFile in this.TransferFiles)
            {
                this.AURLogFileName = transferFile.AURFileNamePrefix + Path.GetFileNameWithoutExtension(transferFile.AURFileName) + ".log";
                this.AURCSVFileName = transferFile.AURFileNamePrefix + Path.GetFileNameWithoutExtension(transferFile.AURFileName) + ".csv";

                try
                {
                    if (File.Exists(this.iHubPendingDirectory + "\\" + this.AURCSVFileName))
                        File.Delete(this.iHubPendingDirectory + "\\" + this.AURCSVFileName);
                }
                catch (System.Exception exc)
                {
                }
                try
                {
                    if (File.Exists(this.iHubProcessingDirectory + "\\" + this.AURCSVFileName))
                        File.Delete(this.iHubProcessingDirectory + "\\" + this.AURCSVFileName);
                }
                catch (System.Exception exc)
                {
                }
                try
                {
                    if (File.Exists(this.iHubProcessedDirectory + "\\" + this.AURCSVFileName))
                        File.Delete(this.iHubProcessedDirectory + "\\" + this.AURCSVFileName);
                }
                catch (System.Exception exc)
                {
                }
                try
                {
                    if (File.Exists(this.iHubErrorDirectory + "\\" + this.AURCSVFileName))
                        File.Delete(this.iHubErrorDirectory + "\\" + this.AURCSVFileName);
                }
                catch (System.Exception exc)
                {
                }
                try
                {
                    if (File.Exists(this.iHubPendingDirectory + "\\" + this.AURLogFileName))
                        File.Delete(this.iHubPendingDirectory + "\\" + this.AURLogFileName);
                }
                catch (System.Exception exc)
                {
                }
                try
                {
                    if (File.Exists(this.iHubProcessingDirectory + "\\" + this.AURLogFileName))
                        File.Delete(this.iHubProcessingDirectory + "\\" + this.AURLogFileName);
                }
                catch (System.Exception exc)
                {
                }
                try
                {
                    if (File.Exists(this.iHubProcessedDirectory + "\\" + this.AURLogFileName))
                        File.Delete(this.iHubProcessedDirectory + "\\" + this.AURLogFileName);
                }
                catch (System.Exception exc)
                {
                }

                try
                {
                    if (File.Exists(this.iHubErrorDirectory + "\\" + this.AURLogFileName))
                        File.Delete(this.iHubErrorDirectory + "\\" + this.AURLogFileName);
                }
                catch (System.Exception exc)
                {
                }
            }

            try
            {
                this.MonitorWorker = new Thread(new ThreadStart(this.MonitorDirectories));
                this.MonitorWorker.Start();
            }
            catch (System.Exception exc)
            {
            }
        }

        public void Stop()
        {
            try
            {
                if (this.MonitorWorker.IsAlive)
                    this.MonitorWorker.Abort();
            }
            catch (System.Exception exc)
            {
            }
        }

        private void MonitorDirectories()
        {
            foreach (AURTransferFile transferFile in this.TransferFiles)
            {
                this.AURLogFileName = transferFile.AURFileNamePrefix + Path.GetFileNameWithoutExtension(transferFile.AURFileName) + ".log";
                this.AURCSVFileName = transferFile.AURFileNamePrefix + Path.GetFileNameWithoutExtension(transferFile.AURFileName) + ".csv";

                this.LogChanged("");
                this.LogChanged("Attempting to send data file [" + transferFile.AURFileName + "] to the Minemarket iHub pending directory [" + this.iHubPendingDirectory  + "]");
                this.LogChanged("");
                try
                {
                    DirectoryInfo info = new DirectoryInfo(this.iHubPendingDirectory);
                    if (info.Exists)
                    {
                        FileInfo source = new FileInfo(transferFile.AURFileName);
                        source.CopyTo(this.iHubPendingDirectory + "\\" + this.AURCSVFileName);
                    }
                }
                catch (System.Exception exc)
                {
                    this.LogChanged("");
                    this.LogChanged("An error occurred when sending the data file to the Minemarket iHub pending directory.");
                    this.LogChanged("Internal message: " + exc.Message);
                    this.LogChanged("");
                    return;
                }

                Boolean awaitingProcessing = true;
                Boolean awaitingProcessed = true;
                while (true)
                {
                    if (File.Exists(this.iHubPendingDirectory + "\\" + this.AURCSVFileName) && awaitingProcessing)
                    {
                        this.LogChanged("");
                        this.LogChanged("Waiting for transfer to commence " + this.AURCSVFileName + " ......");
                        this.LogChanged("");
                        awaitingProcessing = false;
                    }
                    if (File.Exists(this.iHubProcessingDirectory + "\\" + this.AURCSVFileName) && awaitingProcessed)
                    {
                        this.LogChanged("Transfer has started ......" );
                        this.LogChanged("");
                        awaitingProcessed = false;
                    }
                    if (File.Exists(this.iHubProcessedDirectory + "\\" + this.AURCSVFileName))
                    {
                        this.LogChanged("Data have been successfully transferred into Minemarket and are awaiting confirmation.");
                        this.LogChanged("");
                        break;
                    }
                    if (File.Exists(this.iHubErrorDirectory + "\\" + this.AURLogFileName))
                    {
                        this.LogChanged("Errors have been found during the transfer into Minemarket. No data has been saved.");
                        this.LogChanged("");
                        Thread.Sleep(1000);
                        try
                        {
                            this.LogChanged("The error log reqports the following:");
                            this.LogChanged("");
                            StreamReader reader = new StreamReader(this.iHubErrorDirectory + "\\" + this.AURLogFileName);
                            while (!reader.EndOfStream)
                            {
                                String line = reader.ReadLine();
                                this.LogChanged(line);
                            }
                            reader.Close();
                            this.LogChanged("");
                            this.LogChanged("Also check that all stockpiles are open during this report's period. Material cannot be loaded from or stacked onto an unopened stockpile.");
                            this.LogChanged("");
                            break;
                        }
                        catch (System.Exception exc)
                        {
                        }
                    }
                }
            }
            this.LogChanged("");
            this.LogChanged("PUBLISHING HAS COMPLETED.");
            this.LogChanged("");
        }
    }
}
