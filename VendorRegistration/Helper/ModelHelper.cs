using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Registration.Helper
{
    public static class ModelHelper
    {

        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                if (row != null)
                {
                    T item = GetItem<T>(row);
                    data.Add(item);
                }
            }
            return data;
        }

        public static T ConvertDataTableToModel<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                if (row != null)
                {
                    T item = GetItem<T>(row);
                    data.Add(item);
                }
            }
            return data.FirstOrDefault();
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();
            foreach (DataColumn column in dr.Table.Columns)
            {
                if (dr != null)
                {
                    foreach (PropertyInfo pro in temp.GetProperties())
                    {
                        PropertyInfo propertyInfos = obj.GetType().GetProperty(pro.Name);
                        if (pro.Name == column.ColumnName)
                        {
                            var value = (dr[pro.Name] == DBNull.Value) ? null : dr[pro.Name]; //if database field is nullable
                            pro.SetValue(obj, value, null);
                        }
                        else
                            continue;
                    }
                }
            }
            return obj;
        }
    }
}
