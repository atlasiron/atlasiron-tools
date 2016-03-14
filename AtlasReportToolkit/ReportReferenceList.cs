using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;
using System.Text;

namespace AtlasReportToolkit
{
    [Serializable]
    public class ReportReferenceList
    {
        private static String GetListFileName(String directory, String name)
        {
            return directory + "\\" + name + ".xml";
        }

        public ReportReferenceList()
        {
            this.Identifier = Guid.NewGuid();
            this.Items = new List<ReportReferenceListValue>();
        }

        public void Update(String directory, DateTime? filterDate = null)
        {
            try
            {
                List<ReportReferenceListValue> newValues = new List<ReportReferenceListValue>();

                if (!String.IsNullOrWhiteSpace(this.DBConnectionString) && !String.IsNullOrWhiteSpace(this.DBSqlQuery))
                {
                    SqlConnection connection = new SqlConnection(this.DBConnectionString);
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(this.DBSqlQuery,connection);
                    if (filterDate.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@1", filterDate.Value);
                        cmd.Parameters.AddWithValue("@2", filterDate.Value);
                    }
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0))
                        {
                            Boolean activated = true;
                            String newValue = reader.GetValue(0).ToString();
                            foreach(ReportReferenceListValue oldValue in this.Items)
                                if (oldValue.Value.Equals(newValue))
                                    activated = oldValue.Activated;

                            newValues.Add(new ReportReferenceListValue { Value = newValue, Description = (reader.FieldCount < 2 || reader.IsDBNull(1) ? null : reader.GetValue(1).ToString()), Activated = activated });
                        }
                    }
                    reader.Close();
                    connection.Close();
                }
                else
                {
                    ReportReferenceList curList = Extensions.Load<ReportReferenceList>(ReportReferenceList.GetListFileName(directory, this.Name));
                    if (curList != null)
                    {
                        foreach (ReportReferenceListValue item in curList.Items)
                        {
                            Boolean activated = true;
                            String newValue = item.Value;
                            foreach (ReportReferenceListValue oldValue in this.Items)
                                if (oldValue.Value.Equals(newValue))
                                    activated = oldValue.Activated;
                            newValues.Add(item);
                        }
                    }
                }

                this.Items = newValues;
            }
            catch (System.Exception exc)
            {
            }
        }

        public void Save(String directory)
        {
            try
            {
                Extensions.Save<ReportReferenceList>(this, ReportReferenceList.GetListFileName(directory, this.Name));
            }
            catch (System.Exception exc)
            {
            }
        }

        public override string ToString()
        {
            return this.Name;
        }
        
        [XmlIgnore]
        public String FileName { get; set; }

        public Guid Identifier { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public List<ReportReferenceListValue> Items { get; set; }
        public String DBConnectionString { get; set; }
        public String DBSqlQuery { get; set; }
        public String LinkedListName { get; set; }
        public String LinkedListCalculation { get; set; }
    }
}
