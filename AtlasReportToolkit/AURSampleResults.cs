using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace AtlasReportToolkit
{
    public class AURSampleResults
    {
        public const String c_AURiHubFileNamePrefix = "ATL_AURSampleResults";
        public AURSampleResults()
        {
            this.Assays = new Dictionary<string, string>();
        }

        public static void Save(String fileName, List<AURSampleResults> samples)
        {
            try
            {
                samples = samples.OrderBy<AURSampleResults, DateTime>((sample) => {
                    DateTime sampleDate = DateTime.Now;
                    try
                    {
                        if (DateTime.TryParse(sample.DateAnalysed, out sampleDate))
                        {
                        }
                    }
                    catch (System.Exception exc)
                    {
                    }
                    return sampleDate;
                }).ToList();

                StringBuilder line = new StringBuilder();

                StreamWriter writer = new StreamWriter(fileName);
                line.Append("Sample Name,Date Analysed,[Analyte]");

                List<String> allAssayResults = new List<string>();
                foreach (AURSampleResults s in samples)
                    foreach(String key in s.Assays.Keys)
                        if (!allAssayResults.Contains(key))
                            allAssayResults.Add(key);

                foreach(String key in  allAssayResults)
                {
                    line.Append(",");
                    line.Append(key);
                }
                writer.WriteLine(line);
                foreach (AURSampleResults s in samples)
                {
                    line = new StringBuilder();
                    foreach(String key in  allAssayResults)
                    {
                        line.Append(",");
                        if (s.Assays.ContainsKey(key))
                            line.Append(s.Assays[key]);
                    }

                    writer.WriteLine
                        (
                            (String.IsNullOrWhiteSpace(s.SampleId) ? "" : s.SampleId) + "," +
                            (String.IsNullOrWhiteSpace(s.DateAnalysed) ? "" : AURDateFormat(s.DateAnalysed)) + "," +
                            line.ToString()
                        );
                }
                writer.Close();
            }
            catch (System.Exception exc)
            {
            }
        }

        public Object this[String lkey]
        {
            set
            {
                String key = lkey.ToUpper();
                if (key.Equals("Trans History Date".ToUpper()))
                    this.DateAnalysed = value.ToString();
                else if (key.Equals("Sample Prefix".ToUpper()))
                    this.SampleId = value.ToString();
            }
        }

        public static String AURDateFormat(String dateTime)
        {
            try
            {
                DateTime t;
                if (DateTime.TryParse(dateTime, out t))
                {
                    return String.Format("{0:00}/{1:00}/{2:0000} {3:00}:{4:00}", new object[] { t.Day, t.Month, t.Year, t.Hour, t.Minute });
                }
            }
            catch (System.Exception exc)
            {
            }
            return "";
        }

        public String SampleId { get; set; }
        public String DateAnalysed { get; set; }
        public Dictionary<String, String> Assays { get; set; }
    }
}
