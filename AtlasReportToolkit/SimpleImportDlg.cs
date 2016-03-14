using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace AtlasReportToolkit
{
    public partial class SimpleImportDlg : Form
    {
        public SimpleImportDlg()
        {
            InitializeComponent();
        }

        public Report Report { get; set; }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void butImport_Click(object sender, EventArgs e)
        {
            try
            {
                StreamReader reader = new StreamReader(txtFileName.Text);
                String header = reader.ReadLine();
                if (String.IsNullOrWhiteSpace(header))
                {
                    reader.Close();
                    return;
                }
                String[] fields = header.Split(',');
                while (!reader.EndOfStream)
                {
                    String row = reader.ReadLine();
                    String[] values = row.Split(',');
                    ReportRow reportRow = new ReportRow(this.Report.Sheets[0]);
                    PropertyDescriptorCollection propDescriptors = reportRow.GetProperties();

                    for (int i = 0; i < fields.Length && i < values.Length; i++)
                    {
                        if (String.IsNullOrWhiteSpace(fields[i]) || String.IsNullOrWhiteSpace(values[i]))
                            continue;

                        foreach (PropertyDescriptor propDescriptor in propDescriptors)
                        {
                            ReportColumnPropertyDescriptor reportDescriptor = propDescriptor as ReportColumnPropertyDescriptor;
                            try
                            {
                                if (reportDescriptor.Column.ImportAlias.Equals(fields[i]))
                                    reportDescriptor.SetValue(reportRow, values[i].Trim());
                            }
                            catch (System.Exception exc)
                            {
                            }
                        }
                    }
                    this.Report.Sheets[0].Rows.Add(reportRow);
                }
                MessageBox.Show("Data imported successfully into the report", "Data Import", MessageBoxButtons.OK);
            }
            catch (System.Exception exc)
            {
                MessageBox.Show("Data import failed", "Data Import", MessageBoxButtons.OK);
            }
            this.Close();
        }

        private void txtFileName_ButtonClick(object sender, EventArgs e)
        {
            OpenFileDialog browse = new OpenFileDialog();
            browse.CheckPathExists = true;
            browse.CheckFileExists = true;
            browse.RestoreDirectory = true;
            browse.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*||";
            browse.Title = "Select file to import";
            browse.AddExtension = true;
            browse.DefaultExt = ".txt";
            if (browse.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                txtFileName.Text = browse.FileName;
        }
    }
}

