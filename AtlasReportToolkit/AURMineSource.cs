using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace AtlasReportToolkit
{
    public class AURMineSource
    {
        public  const String c_AURiHubFileNamePrefix = "ATL_AURMineSource";
        public AURMineSource()
        {
            this.Assays = new Dictionary<string, string>();
        }

        public static void Save(String fileName, List<AURMineSource> mineSources)
        {
            try
            {
                StringBuilder line = new StringBuilder();

                StreamWriter writer = new StreamWriter(fileName);
                line.Append("Location,Category,Mine Source,Alias,Open Date,Close Date,Survey Date,Tonnage,Ore Type,[Analyte]");

                List<String> allAssayResults = new List<string>();
                foreach (AURMineSource s in mineSources)
                    foreach (String key in s.Assays.Keys)
                        if (!allAssayResults.Contains(key))
                            allAssayResults.Add(key);

                foreach (String key in allAssayResults)
                {
                    line.Append(",");
                    line.Append(key);
                }
                line.Append(",[GeoAtt],Pit,Bench,Shot,Flitch,[Contrib],[Waste]");

                writer.WriteLine(line);
                foreach (AURMineSource t in mineSources)
                {
                    line = new StringBuilder();
                    foreach(String key in  allAssayResults)
                    {
                        line.Append(",");
                        if (t.Assays.ContainsKey(key))
                            line.Append(t.Assays[key]);
                    }

                    writer.WriteLine
                        (
                            (String.IsNullOrWhiteSpace(t.Location) ? "" :t.Location) + "," +
                            (String.IsNullOrWhiteSpace(t.Category) ? "" : t.Category) + "," +
                            (String.IsNullOrWhiteSpace(t.MineSource) ? "" : t.MineSource) + "," +
                            (String.IsNullOrWhiteSpace(t.Alias) ? "" : t.Alias) + "," +
                            (String.IsNullOrWhiteSpace(t.OpenDate) ? "" : AURDateFormat(t.OpenDate)) + "," +
                            (String.IsNullOrWhiteSpace(t.CloseDate) ? "" : AURDateFormat(t.CloseDate)) + "," +
                            (String.IsNullOrWhiteSpace(t.SurveyDate) ? "" : AURDateFormat(t.SurveyDate)) + "," +
                            (String.IsNullOrWhiteSpace(t.Tonnage) ? "" : t.Tonnage) + "," +
                            (String.IsNullOrWhiteSpace(t.OreType) ? "" : t.OreType) + "," +
                            line.ToString() + "," +
                            "," +
                            (String.IsNullOrWhiteSpace(t.Pit) ? "" : t.Pit) + "," +
                            (String.IsNullOrWhiteSpace(t.Bench) ? "" : t.Bench) + "," +
                            (String.IsNullOrWhiteSpace(t.ShotNo) ? "" : t.ShotNo) + "," +
                            (String.IsNullOrWhiteSpace(t.Flitch) ? "" : t.Flitch) + "," +
                            ","                      
                        );
                }
                writer.Close();
            }
            catch (System.Exception exc)
            {
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

        public String Location { get; set; }
        public String Category { get; set; }
        public String MineSource { get; set; }
        public String Alias { get; set; }
        public String OpenDate { get; set; }
        public String CloseDate { get; set; }
        public String SurveyDate { get; set; }
        public String Tonnage { get; set; }
        public String OreType { get; set; }
        public String Pit { get; set; }
        public String Bench { get; set; }
        public String ShotNo { get; set; }
        public String Flitch { get; set; }
        public Dictionary<String, String> Assays { get; set; }
    }
}
