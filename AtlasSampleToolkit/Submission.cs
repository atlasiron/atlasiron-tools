using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace AtlasSampleToolkit
{
    public class Submission
    {
        public Submission()
        {
        }

        public enum SubmissionSampleType { Blastholes, Product, Shot };

        public static String SubmissionFileSuffix = "AtlasSubmission_";

        public String Company { get; set; }
        public String Operation { get; set; }
        public String Laboratory { get; set; }
        public String Name { get; set; }
        public String FileName { get; set; }
        public List<Sample> AssaySamples { get; set; }
        public List<Sample> SizingsSamples { get; set; }
        public SubmissionSampleType SubmissionType { get; set; }

        public void LoadSubmission(Boolean reportErrors = true)
        {
            this.AssaySamples = new List<Sample>();
            StreamReader submissionReader = null;
            try
            {
                submissionReader = new StreamReader(this.FileName);

                int lineNo = 0;
                while (!submissionReader.EndOfStream)
                {
                    String line = submissionReader.ReadLine();
                    lineNo++;

                    String[] items = line.Split('|');
                    if (items[0].Equals("Company") && items.Length == 2)
                    {
                        this.Company = items[1];
                        continue;
                    }
                    if (items[0].Equals("Laboratory") && items.Length == 2)
                    {
                        this.Laboratory = items[1];
                        continue;
                    }
                    if (items[0].Equals("Operation") && items.Length == 2)
                    {
                        this.Operation = items[1];
                        continue;
                    }
                    if (items[0].Equals("Type") && items.Length == 2)
                    {
                        SubmissionSampleType type = SubmissionSampleType.Blastholes;
                        Enum.TryParse(items[1], out type);
                        this.SubmissionType = type;
                        continue;
                    }

                    if (items.Length != 13 || String.IsNullOrWhiteSpace(items[0]))
                        continue;
                    if (items[0].Equals("Sample"))
                    {
                        Sample newSample = new Sample();
                        if (String.IsNullOrWhiteSpace(items[1]) && this.SubmissionType != SubmissionSampleType.Shot)
                        {
                            if (reportErrors)
                                MessageBox.Show(String.Format("Possible corrupt submission register at line {0}. Missing sample identifier.", lineNo));
                            continue;
                        }

                        newSample.SampleId = items[1];
                        if (items.Length >= 3 && !String.IsNullOrWhiteSpace(items[2]) && this.SubmissionType != SubmissionSampleType.Shot)
                        {
                            try
                            {
                                newSample.DespatchedFromMine = Convert.ToDateTime(items[2]);
                            }
                            catch (System.Exception exc)
                            {
                                if (reportErrors)
                                    MessageBox.Show(String.Format("Possible corrupt submission register at line {0}. Unrecognised date for <Despatached From Mine>.", lineNo));
                            }
                        }
                        if (items.Length >= 4 && !String.IsNullOrWhiteSpace(items[3]) && this.SubmissionType != SubmissionSampleType.Shot)
                        {
                            try
                            {
                                newSample.ArrivedForPreparation = Convert.ToDateTime(items[3]);
                            }
                            catch (System.Exception exc)
                            {
                                if (reportErrors)
                                    MessageBox.Show(String.Format("Possible corrupt submission register at line {0}. Unrecognised date for <Arrived For Preparation>.", lineNo));
                            }
                        }
                        if (items.Length >= 5 && !String.IsNullOrWhiteSpace(items[4]) && this.SubmissionType != SubmissionSampleType.Shot)
                        {
                            try
                            {
                                newSample.ArrivedForAnalysis = Convert.ToDateTime(items[4]);
                            }
                            catch (System.Exception exc)
                            {
                                if (reportErrors)
                                    MessageBox.Show(String.Format("Possible corrupt submission register at line {0}. Unrecognised date for <Arrived For Analysis>.", lineNo));
                            }
                        }
                        if (items.Length >= 6 && !String.IsNullOrWhiteSpace(items[5]))
                        {
                            newSample.Pit = items[5];
                        }
                        if (items.Length >= 7 && !String.IsNullOrWhiteSpace(items[6]))
                        {
                            newSample.Bench = items[6];
                        }
                        if (items.Length >= 8 && !String.IsNullOrWhiteSpace(items[7]))
                        {
                            newSample.ShotNo = items[7];
                        }
                        if (items.Length >= 9 && !String.IsNullOrWhiteSpace(items[8]))
                        {
                            newSample.HoleNo = items[8];
                        }
                        if (items.Length >= 10 && !String.IsNullOrWhiteSpace(items[9]))
                        {
                            try
                            {
                                newSample.East = Convert.ToDouble(items[9]);
                            }
                            catch (System.Exception exc)
                            {
                                if (reportErrors)
                                    MessageBox.Show(String.Format("Possible corrupt submission register at line {0}. Unrecognised easting.", lineNo));
                            }
                        }
                        if (items.Length >= 11 && !String.IsNullOrWhiteSpace(items[10]))
                        {
                            try
                            {
                                newSample.North = Convert.ToDouble(items[10]);
                            }
                            catch (System.Exception exc)
                            {
                                if (reportErrors)
                                    MessageBox.Show(String.Format("Possible corrupt submission register at line {0}. Unrecognised northing.", lineNo));
                            }
                        }
                        if (items.Length >= 12 && !String.IsNullOrWhiteSpace(items[11]))
                        {
                            try
                            {
                                newSample.Elevation = Convert.ToDouble(items[11]);
                            }
                            catch (System.Exception exc)
                            {
                                if (reportErrors)
                                    MessageBox.Show(String.Format("Possible corrupt submission register at line {0}. Unrecognised elevation.", lineNo));
                            }
                        }
                        if (items.Length >= 13 && !String.IsNullOrWhiteSpace(items[12]))
                        {
                            try
                            {
                                newSample.SampledOn = Convert.ToDateTime(items[12]);
                            }
                            catch (System.Exception exc)
                            {
                                if (reportErrors)
                                    MessageBox.Show(String.Format("Possible corrupt submission register at line {0}. Unrecognised date for <Sampled On>.", lineNo));
                            }
                        } this.AssaySamples.Add(newSample);
                    }
                }
            }
            catch (System.Exception exc)
            {
                if (reportErrors)
                    MessageBox.Show(String.Format("The list of samples are not available for the selected submision [{0}]", exc.Message), "Chain of Custody Error", MessageBoxButtons.OK);
            }
            finally
            {
                if (submissionReader != null)
                    submissionReader.Close();
            }
        }

        public void WriteSubmission(String destinationDirectory, Boolean submit = false)
        {
            try
            {
                String destinationName = String.Format("{0}\\" + SubmissionFileSuffix + "{1}.csv", destinationDirectory, this.Name);
                if (!submit)
                    destinationName = this.FileName;

                StreamWriter submissionWriter = new StreamWriter(destinationName, false);
                submissionWriter.WriteLine("Company|" + this.Company ?? "?");
                submissionWriter.WriteLine("Type|" + this.SubmissionType ?? "?");
                submissionWriter.WriteLine("Laboratory|" + this.Laboratory ?? "?");
                submissionWriter.WriteLine("Operation|" + this.Operation ?? "?");
                submissionWriter.WriteLine("Submission|" + this.Name ?? "?");
                submissionWriter.WriteLine("Columns|SampleId|DespatchedFromMine|ArrivedForPreparation|ArrivedForAnalysis|Pit|Bench|ShotNo|HoleNo|East|North|Elevation|SampledOn");
                foreach (Sample sample in this.AssaySamples)
                    submissionWriter.WriteLine("Sample|" + sample.SampleId + "|" + sample.DespatchedFromMine + "|" + sample.ArrivedForPreparation + "|" + sample.ArrivedForAnalysis + "|" + sample.Pit + "|" + sample.Bench + "|" + sample.ShotNo + "|" + sample.HoleNo + "|" + sample.East + "|" + sample.North + "|" + sample.Elevation + "|" + sample.SampledOn);
                submissionWriter.Close();

                if (submit)
                    if (!String.IsNullOrWhiteSpace(this.FileName))
                        File.Delete(this.FileName);
            }
            catch (System.Exception exc)
            {
                MessageBox.Show(String.Format("The registered samples cannot be saved for the selected submision [{0}]", exc.Message), "Chain of Custody Error", MessageBoxButtons.OK);
                throw exc;
            }
        }
    }
}
