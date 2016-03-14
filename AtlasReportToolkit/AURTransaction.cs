using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace AtlasReportToolkit
{
    public class AURTransaction
    {
        public  const String c_AURiHubFileNamePrefix = "ATL_AURTransaction";
        public AURTransaction()
        {
            this.ExtendedData = new Dictionary<string, string>();
        }

        public static void Save(String fileName, List<AURTransaction> transactions)
        {
            try
            {
                transactions = transactions.OrderBy<AURTransaction, DateTime>((trans) => {
                    DateTime transactionDate = DateTime.Now;
                    try
                    {
                        if (DateTime.TryParse(trans.TransactionDate, out transactionDate))
                        {
                        }
                    }
                    catch (System.Exception exc)
                    {
                    }
                    return transactionDate;
                 }).ToList();
                StringBuilder line = new StringBuilder();

                StreamWriter writer = new StreamWriter(fileName);
                line.Append("Transaction GUID,Transaction Editor,Trans History Date,Row ID,Transaction Date,Source,Destination,Product,STD/Truck/Ttype,Mass/Vol/#Trips,UOM,Truck/TType,Overall Comment,Row Comment,Sample Prefix,Equipment,[Extended],ImportUser,ImportDate");

                List<String> allExtendedData = new List<string>();
                foreach(AURTransaction t in transactions)
                    foreach(String key in t.ExtendedData.Keys)
                        if (!allExtendedData.Contains(key))
                            allExtendedData.Add(key);

                foreach(String key in  allExtendedData)
                {
                    line.Append(",");
                    line.Append(key);
                }
                writer.WriteLine(line);
                foreach(AURTransaction t in transactions)
                {
                    if (String.IsNullOrWhiteSpace(t.TransactionGUID))
                        continue;

                    line = new StringBuilder();
                    line.Append("," + Environment.UserName + "," + DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                    foreach (String key in allExtendedData)
                    {
                        line.Append(",");
                        if (t.ExtendedData.ContainsKey(key))
                            line.Append(t.ExtendedData[key]);
                    }

                    writer.WriteLine
                        (
                            (String.IsNullOrWhiteSpace(t.TransactionGUID) ? "" :t.TransactionGUID) + "," +
                            (String.IsNullOrWhiteSpace(t.TransactionEditor) ? "" : t.TransactionEditor) + "," +
                            (String.IsNullOrWhiteSpace(t.TransHistoryDate) ? "" : AURDateFormat(t.TransHistoryDate)) + "," +
                            (String.IsNullOrWhiteSpace(t.RowID) ? "" : t.RowID) + "," +
                            (String.IsNullOrWhiteSpace(t.TransactionDate) ? "" : AURDateFormat(t.TransactionDate)) + "," +
                            (String.IsNullOrWhiteSpace(t.Source) ? "" : t.Source) + "," +
                            (String.IsNullOrWhiteSpace(t.Destination) ? "" : t.Destination) + "," +
                            (String.IsNullOrWhiteSpace(t.Product) ? "" : t.Product) + "," +
                            (String.IsNullOrWhiteSpace(t.STDTruckType) ? "" : t.STDTruckType) + "," +
                            (String.IsNullOrWhiteSpace(t.MassVolTrips) ? "" : t.MassVolTrips) + "," +
                            (String.IsNullOrWhiteSpace(t.UOM) ? "" : t.UOM) + "," +
                            (String.IsNullOrWhiteSpace(t.TruckTType) ? "" : t.TruckTType) + "," +
                            (String.IsNullOrWhiteSpace(t.OverallComment) ? "" : t.OverallComment) + "," +
                            (String.IsNullOrWhiteSpace(t.RowComment) ? "" : t.RowComment) + "," +
                            (String.IsNullOrWhiteSpace(t.SamplePrefix) ? "" : t.SamplePrefix) + "," +
                            (String.IsNullOrWhiteSpace(t.Equipment) ? "" : t.Equipment) + "," +
                            line.ToString()
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

        public Object this[String lkey]
        {
            set
            {
                String key = lkey.ToUpper();
                if (key.Equals("Transaction GUID".ToUpper()))
                    this.TransactionGUID = value.ToString();
                else if (key.Equals("Transaction Editor".ToUpper()))
                    this.TransactionEditor = value.ToString();
                else if (key.Equals("Trans History Date".ToUpper()))
                    this.TransHistoryDate = value.ToString();
                else if (key.Equals("Row ID".ToUpper()))
                    this.RowID = value.ToString();
                else if (key.Equals("Transaction Date".ToUpper()))
                    this.TransactionDate = value.ToString();
                else if (key.Equals("Source".ToUpper()))
                    this.Source = value.ToString().Replace(","," ");
                else if (key.Equals("Destination".ToUpper()))
                    this.Destination = value.ToString().Replace(",", " ");
                else if (key.Equals("Product".ToUpper()))
                    this.Product = value.ToString().Replace(",", " ");
                else if (key.Equals("UOM".ToUpper()))
                    this.UOM = value.ToString().Replace(",", " ");
                else if (key.Equals("STD/Truck/Ttype".ToUpper()))
                    this.STDTruckType = value.ToString().Replace(",", " ");
                else if (key.Equals("Mass/Vol/#Trips".ToUpper()))
                    this.MassVolTrips = value.ToString().Replace(",", " ");
                else if (key.Equals("Truck/TType".ToUpper()))
                    this.TruckTType = value.ToString().Replace(",", " ");
                else if (key.Equals("Overall Comment".ToUpper()))
                    this.OverallComment = value.ToString().Replace(",", " ");
                else if (key.Equals("Row Comment".ToUpper()))
                    this.RowComment = value.ToString().Replace(",", " ");
                else if (key.Equals("Sample Prefix".ToUpper()))
                    this.SamplePrefix = value.ToString();
                else if (key.Equals("Equipment".ToUpper()))
                    this.Equipment = value.ToString();
                else
                    ExtendedData[key] = value.ToString().Replace(",", " ");
            }
        }

        public String TransactionGUID { get; set; }
        public String TransactionEditor { get; set; }
        public String TransHistoryDate { get; set; }
        public String RowID { get; set; }
        public String TransactionDate { get; set; }
        public String Source { get; set; }
        public String Destination { get; set; }
        public String Product { get; set; }
        public String STDTruckType{ get; set; }
        public String MassVolTrips { get; set; }
        public String UOM { get; set; }
        public String TruckTType { get; set; }
        public String OverallComment { get; set; }
        public String RowComment { get; set; }
        public String SamplePrefix { get; set; }
        public String Equipment { get; set; }
        public Dictionary<String, String> ExtendedData { get; set; }
    }
}
