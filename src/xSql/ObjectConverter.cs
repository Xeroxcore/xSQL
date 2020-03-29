using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Components
{
    public class ObjectConverter
    {
        public static int ConvertStringToInt(string number)
        {
            return int.Parse(number);
        }

        private static T assignValueToObject<T>(PropertyInfo pro, DataColumn column, DataRow dr, T obj)
        {
            if (pro.PropertyType.Name == "Boolean")
                pro.SetValue(obj, Convert.ToBoolean(dr[column.ColumnName]), null);
            else
                pro.SetValue(obj, dr[column.ColumnName], null);
            return obj;
        }

        private static bool ObjectAssignmentIsValid(string name, string columnName, object obj)
            => name == columnName && obj != null;

        private static T IterateColumns<T>(T obj, DataColumn column, DataRow dr)
        {
            var properties = obj.GetType().GetProperties();
            foreach (PropertyInfo pro in properties)
            {
                if (ObjectAssignmentIsValid(pro.Name, column.ColumnName, dr[column.ColumnName]))
                {
                    obj = assignValueToObject(pro, column, dr, obj);
                    break;
                }
                continue;
            }
            return obj;
        }

        private static T GetItem<T>(DataRow dr)
        {
            T obj = Activator.CreateInstance<T>();
            foreach (DataColumn column in dr.Table.Columns)
            {
                obj = IterateColumns<T>(obj, column, dr);
            }
            return obj;
        }

        public static List<T> ConvertDataTableToList<T>(DataTable dt)
        {
            try
            {
                var data = new List<T>();
                foreach (DataRow row in dt.Rows)
                {
                    T item = GetItem<T>(row);
                    data.Add(item);
                }
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static T ConvertDataTableRowToObject<T>(DataTable table, int index)
        {
            try
            {
                T data = GetItem<T>(table.Rows[index]);
                return data;
            }
            catch (Exception error)
            {
                throw error;
            }
        }
    }
}