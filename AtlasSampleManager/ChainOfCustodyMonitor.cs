using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using AtlasSampleToolkit;

namespace AtlasSampleManager
{
    public static class ChainOfCustodyMonitor
    {
        public static void Scan(SqlConnection connection,String directory, String tableName, String register)
        {
            try
            {
                if (!Directory.Exists(directory))
                    return;

                foreach (String file in Directory.EnumerateFiles(directory))
                {
                    try
                    {
                        Submission submission = new Submission() { FileName = file };
                        submission.LoadSubmission(false);
                        foreach (Sample sample in submission.AssaySamples)
                        {
                            try
                            {
                                String registerColumn = "";
                                DateTime? registerDate = null;
                                if (register.Equals("DespatchedFromMine"))
                                {
                                    registerColumn = "DespatchedOn";
                                    registerDate = sample.DespatchedFromMine;
                                }
                                if (register.Equals("DespatchedFromPreparation"))
                                {
                                    registerColumn = "DespatchedFromPreparationOn";
                                    registerDate = sample.DespatchedFromPreparation;
                                }
                                else if (register.Equals("ArrivedForPreparation"))
                                {
                                    registerColumn = "ArrivedForPreparation";
                                    registerDate = sample.ArrivedForPreparation;
                                }
                                else if (register.Equals("ArrivedForAnalysis"))
                                {
                                    registerColumn = "ArrivedForAnalysis";
                                    registerDate = sample.ArrivedForAnalysis;
                                }
                                else
                                    continue;

                                SqlCommand cmd = new SqlCommand("update " + tableName + " set " + registerColumn + " = @1 where SampleId = @2", connection);
                                cmd.Parameters.Add(new SqlParameter { ParameterName = "@1", Value = registerDate, SqlDbType = SqlDbType.DateTime, Size = 80 });
                                cmd.Parameters.Add(new SqlParameter { ParameterName = "@2", Value = sample.SampleId, SqlDbType = SqlDbType.NVarChar, Size = 80 });
                                cmd.ExecuteNonQuery();
                            }
                            catch (System.Exception exc)
                            {
                            }
                        }
                    }
                    catch (System.Exception exc)
                    {
                    }
                }
            }
            catch (System.Exception exc)
            {
            }
        }
    }
}
