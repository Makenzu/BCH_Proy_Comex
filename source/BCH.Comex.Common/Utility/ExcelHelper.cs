using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace BCH.Comex.Common.Utility
{
    public class ExcelHelper
    {
        /// <summary>
        /// Convierte la lista de datos en un Data Table para mapearlo a un SLDocument
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">Lista de datos a convertir a DataTable</param>
        /// <returns></returns>
        public static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }

            return table;
        }
    }
}
