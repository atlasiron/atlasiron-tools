using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AtlasReportToolkit;

namespace AtlasReportToolkit
{
    public partial class DefineReportReferenceListsDlg : Form
    {
        public DefineReportReferenceListsDlg()
        {
            InitializeComponent();
        }

        public ReportReferenceList DefaultList { get; set; }
        public ReportDefinitions Definitions { get; set; }

        private void lstReportReferenceLists_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDBConnectionString.DataBindings.Clear();
            txtDBSqlQuery.DataBindings.Clear();
            lstLinkedListName.DataBindings.Clear();
            txtLinkedListCalc.DataBindings.Clear();

            if (lstReportReferenceLists.SelectedValue == null)
            {
                gridItems.SetDataBinding(null,null);
                return;
            }

            ReportReferenceList list = lstReportReferenceLists.SelectedValue as ReportReferenceList;
            gridItems.SetDataBinding(list.Items, null);
            txtDBConnectionString.DataBindings.Add("Text", list, "DBConnectionString", false, DataSourceUpdateMode.OnPropertyChanged);
            txtDBSqlQuery.DataBindings.Add("Text", list, "DBSqlQuery", false, DataSourceUpdateMode.OnPropertyChanged);
            lstLinkedListName.DataBindings.Add("Text", list, "LinkedListName", false, DataSourceUpdateMode.OnPropertyChanged);
            txtLinkedListCalc.DataBindings.Add("Text", list, "LinkedListCalculation", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void DefineReportReferenceListsDlg_Load(object sender, EventArgs e)
        {
            lstReportReferenceLists.DisplayMember = "Name";
            lstReportReferenceLists.DataSource = this.Definitions.ReferenceLists;
            if (this.DefaultList != null)
                lstReportReferenceLists.SelectedValue = this.DefaultList;

            lstLinkedListName.DisplayMember = "Name";
            lstLinkedListName.DataSource = this.Definitions.ReferenceLists;
            if (this.DefaultList != null)
                lstLinkedListName.SelectedValue = this.DefaultList;
        }

        private void butAddList_Click(object sender, EventArgs e)
        {
            PromptForTextDlg prompt = new PromptForTextDlg { Prompt = "Report Reference List" };
            if (prompt.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ReportReferenceList list = new ReportReferenceList();
                list.Name = prompt.Answer;
                this.Definitions.ReferenceLists.Add(list);

                lstReportReferenceLists.SelectedValue = list;
            }
        }

        private void butRemoveList_Click(object sender, EventArgs e)
        {
            if (lstReportReferenceLists.SelectedValue == null)
                return;
            ReportReferenceList list = lstReportReferenceLists.SelectedValue as ReportReferenceList;
            this.Definitions.ReferenceLists.Remove(list);
            if (this.Definitions.ReferenceLists.Count > 0)
                lstReportReferenceLists.SelectedValue = this.Definitions.ReferenceLists[0];
            else
                lstReportReferenceLists.SelectedValue = null;
        }

        private void gridItems_GetNewRow(object sender, Janus.Windows.GridEX.GetNewRowEventArgs e)
        {
            e.NewRow = new ReportReferenceListValue();
        }

        private void butClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lstLinkedListName_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void butCopy_Click(object sender, EventArgs e)
        {
            if (lstReportReferenceLists.SelectedValue == null)
                return;
            ReportReferenceList oldList = lstReportReferenceLists.SelectedValue as ReportReferenceList;

            PromptForTextDlg prompt = new PromptForTextDlg { Prompt = "Report Reference List" };
            if (prompt.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ReportReferenceList list = new ReportReferenceList();
                list = Extensions.BinaryClone<ReportReferenceList>(oldList);
                list.Identifier = Guid.NewGuid();
                list.Name = prompt.Answer;
                this.Definitions.ReferenceLists.Add(list);

                lstReportReferenceLists.SelectedValue = list;
            }
        }
    }
}
