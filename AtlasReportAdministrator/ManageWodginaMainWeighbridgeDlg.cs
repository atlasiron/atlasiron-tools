using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Net;
using Janus.Windows.GridEX;
using FTP;

namespace AtlasReportAdministrator
{
    public partial class ManageWodginaMainWeighbridgeDlg : Form
    {
        private const String c_ProductStockpiles = "Product Stockpiles";
        private const String c_Assets = "Assets";
        private const String c_PayrollID = "Payrolls";

        private class ProductStockpile
        {
            public String Name { get; set; }
            [Browsable(false)]
            public String[] Tokens { get; set; }
        }

        private class AssetDetails
        {
            public String AssetId { get; set; }
            public String TareWeight { get; set; }
            public String ProductStockpile { get; set; }
            [Browsable(false)]
            public String[] Tokens { get; set; }
        }

        private class PayrollDetails
        {
            public String PayrollID { get; set; }
            public String Name { get; set; }
            [Browsable(false)]
            public String[] Tokens { get; set; }
        }



        private List<ProductStockpile> ProductStockpiles { get; set; }
        private List<AssetDetails> Assets { get; set; }
        private List<PayrollDetails> Payroll { get; set; }

        public ManageWodginaMainWeighbridgeDlg()
        {
            InitializeComponent();
        }

        private void DownloadLists()
        {
            FTPclient ftp = null;

            String line = null;
            StreamReader reader = null;

            try
            {
                String localFile = Path.GetTempFileName();
                File.Delete(localFile);

                ftp = new FTPclient("10.10.24.119", "admin", "admin");

                ftp.Download("/Terminal/TABLES/Standard_A0.csv", localFile, true);
                this.ProductStockpiles = new List<ProductStockpile>();
                try
                {
                    reader = new StreamReader(localFile);
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] tokens = line.Split(',');
                        if (tokens.Length >= 2 && !String.IsNullOrWhiteSpace(tokens[0]))
                            this.ProductStockpiles.Add(new ProductStockpile { Name = tokens[0].Replace("\"", ""), Tokens = tokens });
                    }
                    reader.Close();
                }
                catch (System.Exception exc)
                {
                    MessageBox.Show("Error when reading product stockpile list from weighbridge. [" + exc.Message + "]");
                }

                ftp.Download("/Terminal/TABLES/Standard_A5.csv", localFile, true);
                this.Assets = new List<AssetDetails>();
                try
                {
                    reader = new StreamReader(localFile);
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] tokens = line.Split(',');
                        if (tokens.Length >= 15 && !String.IsNullOrWhiteSpace(tokens[0]))
                        {
                            this.Assets.Add(new AssetDetails { AssetId = tokens[0].Replace("\"", ""), TareWeight = tokens[3].Replace("\"", ""), ProductStockpile = tokens[16].Replace("\"", ""), Tokens = tokens });
                        }
                    }
                    reader.Close();
                }
                catch (System.Exception exc)
                {
                    MessageBox.Show("Error when reading asset list from weighbridge. [" + exc.Message + "]");
                }

                ftp.Download("/Terminal/TABLES/Standard_A4.csv", localFile, true);
                this.Payroll = new List<PayrollDetails>();
                try
                {
                    reader = new StreamReader(localFile);
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] tokens = line.Split(',');
                        if (tokens.Length >= 15 && !String.IsNullOrWhiteSpace(tokens[0]))
                        {
                            this.Payroll.Add(new PayrollDetails { PayrollID = tokens[0].Replace("\"", ""), Name = tokens[1].Replace("\"", ""), Tokens = tokens });
                        }
                    }
                    reader.Close();
                }
                catch (System.Exception exc)
                {
                    MessageBox.Show("Error when reading payroll list from weighbridge. [" + exc.Message + "]");
                }
            }
            catch (System.Exception exc)
            {
                MessageBox.Show("Cannot login the with weighbridge [" + exc.Message + "]");
            }
        }

        private void lstWBLists_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                butSetDefault.Enabled = false;

                if (lstWBLists.Text.Equals(c_ProductStockpiles))
                {
                    gridList.SetDataBinding(this.ProductStockpiles, null);
                    gridList.RetrieveStructure();
                }

                if (lstWBLists.Text.Equals(c_Assets))
                {
                    butSetDefault.Enabled = true;
                    gridList.SetDataBinding(this.Assets, null);
                    gridList.RetrieveStructure();

                    gridList.RootTable.Columns["ProductStockpile"].EditType = EditType.Combo;
                    gridList.RootTable.Columns["ProductStockpile"].HasValueList = true;
                    gridList.RootTable.Columns["ProductStockpile"].ValueList.Clear();
                    gridList.RootTable.Columns["ProductStockpile"].LimitToList = true;
                    foreach (ProductStockpile product in this.ProductStockpiles)
                        gridList.RootTable.Columns["ProductStockpile"].ValueList.Add(product.Name, product.Name);
                }

                if (lstWBLists.Text.Equals(c_PayrollID))
                {
                    gridList.SetDataBinding(this.Payroll, null);
                    gridList.RetrieveStructure();
                }
            }
            catch (System.Exception exc)
            {
                MessageBox.Show("Cannot login the with weighbridge [" + exc.Message + "]");
            }
        }

        private void ManageWodginaMainWeighbridgeDlg_Load(object sender, EventArgs e)
        {
            lstWBLists.Items.Clear();
            lstWBLists.Items.Add(c_ProductStockpiles, c_ProductStockpiles);
            lstWBLists.Items.Add(c_Assets, c_Assets);
            lstWBLists.Items.Add(c_PayrollID, c_PayrollID);

            DownloadLists();
        }

        private void butSaveList_Click(object sender, EventArgs e)
        {
            FTPclient ftp = null;

            String line = null;
            StreamReader reader = null;

            try
            {
                String localFile = Path.GetTempFileName();
                File.Delete(localFile);

                ftp = new FTPclient("10.10.24.119", "admin", "admin");

                if (lstWBLists.Text.Equals(c_ProductStockpiles))
                {
                    try
                    {
                        String[] defTokens = null;
                        foreach (ProductStockpile product in this.ProductStockpiles)
                            if (product.Tokens != null)
                                defTokens = product.Tokens.ToArray();
                        if (defTokens == null)
                            defTokens = new string[19];

                        StreamWriter writer = new StreamWriter(localFile, false);
                        foreach (ProductStockpile product in this.ProductStockpiles)
                        {
                            String[] theseTokens = product.Tokens;
                            if (theseTokens == null)
                                theseTokens = defTokens;

                            theseTokens[0] = product.Name;
                            theseTokens[1] = "\"" + product.Name + "\"";
                            for (int i = 0; i < theseTokens.Length; i++)
                                writer.Write(String.Format("{0}{1}", theseTokens[i], (i != theseTokens.Length - 1 ? "," : "")));
                            writer.Write("\n");
                        }
                        writer.Close();

                        //ftp.Upload(localFile, "/Terminal/TABLES/Standard_A0.csv");

                        MessageBox.Show("Product Stockpiles have been updated on the weighbridge.");
                    }
                    catch (System.Exception exc)
                    {
                        MessageBox.Show("Error while updating the product stockpile list on the weighbridge. [" + exc.Message + "]");
                    }
                }
                if (lstWBLists.Text.Equals(c_Assets))
                {
                    try
                    {
                        String[] defTokens = null;
                        foreach (AssetDetails asset in this.Assets)
                            if (asset.Tokens != null)
                                defTokens = asset.Tokens.ToArray();
                        if (defTokens == null)
                            defTokens = new string[19];

                        StreamWriter writer = new StreamWriter(localFile, false);
                        foreach (AssetDetails asset in this.Assets)
                        {
                            String[] theseTokens = asset.Tokens;
                            if (theseTokens == null)
                                theseTokens = defTokens;

                            theseTokens[0] = asset.AssetId;
                            theseTokens[1] = "\"" + asset.AssetId + "\"";
                            theseTokens[2] =  asset.TareWeight;
                            theseTokens[15] = asset.ProductStockpile;

                            for (int i = 0; i < theseTokens.Length; i++)
                                writer.Write(String.Format("{0}{1}", theseTokens[i], (i != theseTokens.Length - 1 ? "," : "")));
                            writer.Write("\n");
                        }
                        writer.Close();

                        //ftp.Upload(localFile, "/Terminal/TABLES/Standard_A5.csv");

                        MessageBox.Show("Assets have been updated on the weighbridge.");
                    }
                    catch (System.Exception exc)
                    {
                        MessageBox.Show("Error while updating the assets list on the weighbridge. [" + exc.Message + "]");
                    }
                }
                if (lstWBLists.Text.Equals(c_PayrollID))
                {
                    try
                    {
                        String[] defTokens = null;
                        foreach (PayrollDetails payroll in this.Payroll)
                            if (payroll.Tokens != null)
                                defTokens = payroll.Tokens.ToArray();
                        if (defTokens == null)
                            defTokens = new string[19];

                        StreamWriter writer = new StreamWriter(localFile, false);
                        foreach (PayrollDetails payroll in this.Payroll)
                        {
                            String[] theseTokens = payroll.Tokens;
                            if (theseTokens == null)
                                theseTokens = defTokens;

                            theseTokens[0] = payroll.PayrollID;
                            theseTokens[1] = "\"" + payroll.Name + "\"";

                            for (int i = 0; i < theseTokens.Length; i++)
                                writer.Write(String.Format("{0}{1}", theseTokens[i], (i != theseTokens.Length - 1 ? "," : "")));
                            writer.Write("\n");
                        }
                        writer.Close();

                        //ftp.Upload(localFile, "/Terminal/TABLES/Standard_A4.csv");

                        MessageBox.Show("Payrolls have been updated on the weighbridge.");
                    }
                    catch (System.Exception exc)
                    {
                        MessageBox.Show("Error while updating the payrolls list on the weighbridge. [" + exc.Message + "]");
                    }
                }
            }
            catch (System.Exception exc)
            {
                MessageBox.Show("Cannot login the with weighbridge [" + exc.Message + "]");
            }
        }

        private void butClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void butSetDefault_Click(object sender, EventArgs e)
        {
            if (lstWBLists.Text.Equals(c_Assets))
            {
                GridEXRow curRow = gridList.GetRow();
                if (curRow == null)
                    return;
                AssetDetails defProduct = curRow.DataRow as AssetDetails;
                foreach (AssetDetails asset in this.Assets)
                    asset.ProductStockpile = defProduct.ProductStockpile;

                gridList.Refetch();
            }
        }

        private void gridList_GetNewRow(object sender, GetNewRowEventArgs e)
        {
            if (lstWBLists.Text.Equals(c_ProductStockpiles))
            {
                e.NewRow = new ProductStockpile();
            }
            if (lstWBLists.Text.Equals(c_Assets))
            {
                e.NewRow = new AssetDetails();
            }
            if (lstWBLists.Text.Equals(c_PayrollID))
            {
                e.NewRow = new PayrollDetails();
            }
        }
    }
}
