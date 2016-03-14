using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;
using System.Diagnostics;
using USP.Express.Pro.Functions;
using USP.Express.Pro;
using USP.Express.Pro.Constants;

namespace AtlasReportToolkit
{
    /// <summary>
    /// IIf function.
    /// </summary>
    public class RLookupFunction : Function
    {
        private ReportSheet Sheet { get; set; }
        public RLookupFunction(ReportSheet sheet) { this.Sheet = sheet; }
        public override string Name { get { return "RLOOKUP"; } }

        public override string Syntax { get { return "RLOOKUP(column_name, search_value,)"; } }
        public override string Description
        {
            get { return ""; }
        }
        public override GroupType Group { get { return GroupType.Logical; } }

        public override bool MultArgsSupported(int nCount)
        {
            return (nCount == 2);
        }

        public override Type GetReturnType(Type[] Types)
        {
            return typeof(String);
        }

        protected override bool InputTypeSupported(Type Type, int Index)
        {
            bool bSupported = false;
            switch (Index)
            {
                case 0:
                    bSupported = Type.Equals(typeof(String));
                    break;
                case 1:
                    bSupported = Type.Equals(typeof(String));
                    break;
            }
            return bSupported;
        }

        public override object Evaluate(object[] Values)
        {
            if (Values[0] == null || Values[1] == null)
                return DBNull.Value;

            String colName = Values[0].ToString();
            String searchValue = Values[1].ToString();

            foreach (ReportTemplateColumn column in this.Sheet.Template.Rows)
            {
                if (!column.Name.Equals(colName) || column.ReferenceList == null || column.ReferenceList.Items == null)
                    continue;

                foreach (ReportReferenceListValue value in column.ReferenceList.Items)
                {
                    if (String.IsNullOrWhiteSpace(value.Description))
                        continue;

                    if (value.Description.Equals(searchValue))
                        return value.Value;
                }
            }
            return DBNull.Value;
        }

        public override bool IsNullable(object[] Values)
        {
            return false;
        }
    }
}
