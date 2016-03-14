using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using System.Collections;

namespace AtlasReportToolkit
{
    [Serializable]
    public class TypedLibBindingList<T> : BindingList<T>, ITypedList where T : ICustomTypeDescriptor
    {
        public TypedLibBindingList()
        {
        }

        public void SetProperties(ReportSheet sheet)
        {
            ReportRow row = new ReportRow(sheet);
            m_properties = row.GetProperties();
        }

        #region ITypedList Members
        private PropertyDescriptorCollection m_properties;
        public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            return m_properties;
        }

        public string GetListName(PropertyDescriptor[] listAccessors)
        {
            return typeof(T).Name;
        }

        #endregion
    }
}
