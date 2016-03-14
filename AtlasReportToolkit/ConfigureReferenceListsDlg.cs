using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using USP.Express.Pro;

namespace AtlasReportToolkit
{
    public partial class ConfigureReferenceListsDlg : Form
    {
        public ConfigureReferenceListsDlg()
        {
            InitializeComponent();
        }

        public ReportDefinitions Definitions { get; set; }
        public Report Report { get; set; }
        public String Operation { get; set; }
        public String ConfigurationDirectory { get; set; }

        private void CreateReportDlg_Load(object sender, EventArgs e)
        {
            txtReportName.Text = this.Report.Name;

            foreach(ReportReferenceList list in this.Report.GetAllReferenceLists(this.Operation, this.Definitions, false))
                list.Update(this.ConfigurationDirectory, this.Report.ReportingPeriod);

            Report.UpdateLinkedReferenceLists(this.Report.GetAllReferenceLists(this.Operation, this.Definitions, false));

            gridDefaultLists.SetDataBinding(this.Report.GetAllReferenceLists(this.Operation, this.Definitions, false), null);

            this.Report.Modified = true;
        }

        private void gridItems_GetNewRow(object sender, GetNewRowEventArgs e)
        {
            e.NewRow = new ReportReferenceListValue();
        }

        private void gridDefaultLists_SelectionChanged(object sender, EventArgs e)
        {
            GridEXRow row = gridDefaultLists.GetRow();
            if (row == null)
                return;

            ReportReferenceList list = row.DataRow as ReportReferenceList;
            if (list == null)
                return;

            Report.UpdateLinkedReferenceLists(gridDefaultLists.DataSource as List<ReportReferenceList>);

            gridItems.SetDataBinding(list.Items, null);
            gridItems.RootTable.ApplyFilter(gridItems.RootTable.StoredFilters[0].FilterCondition);

            gridItems.RootTable.Columns["Value"].EditType = EditType.TextBox;
            gridItems.RootTable.Columns["Description"].EditType = EditType.TextBox;
            if (!String.IsNullOrWhiteSpace(list.LinkedListName) || !String.IsNullOrWhiteSpace(list.LinkedListCalculation))
            {
                gridItems.AllowDelete = InheritableBoolean.False;
                gridItems.AllowAddNew = InheritableBoolean.False;
                gridItems.AllowEdit = InheritableBoolean.False;
            }
            else if (!String.IsNullOrWhiteSpace(list.DBConnectionString) || !String.IsNullOrWhiteSpace(list.DBSqlQuery))
            {
                gridItems.AllowDelete = InheritableBoolean.False;
                gridItems.AllowAddNew = InheritableBoolean.False;
                gridItems.AllowEdit = InheritableBoolean.True;
                gridItems.RootTable.Columns["Value"].EditType = EditType.NoEdit;
                gridItems.RootTable.Columns["Description"].EditType = EditType.NoEdit;
            }
            else
            {
                gridItems.AllowDelete = InheritableBoolean.True;
                gridItems.AllowAddNew = InheritableBoolean.True;
                gridItems.AllowEdit = InheritableBoolean.True;
            }
        }

        private void butRefresh_Click(object sender, EventArgs e)
        {
            GridEXRow row = gridDefaultLists.GetRow();
            if (row == null)
                return;

            ReportReferenceList list = row.DataRow as ReportReferenceList;
            if (list == null)
                return;

            list.Update(this.ConfigurationDirectory, this.Report.ReportingPeriod);
            Report.UpdateLinkedReferenceLists(this.Report.GetAllReferenceLists(this.Operation, this.Definitions, false));

            gridItems.SetDataBinding(list.Items, null);
            gridItems.RootTable.ApplyFilter(gridItems.RootTable.StoredFilters[0].FilterCondition);
        }

        private void ticShowActivatedValuesOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (ticShowActivatedValuesOnly.Checked)
                gridItems.RootTable.ApplyFilter(gridItems.RootTable.StoredFilters[0].FilterCondition);
            else
                gridItems.RootTable.RemoveFilter();
        }

        private void butClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
