using Project.Classes.Person;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.classes
{
    public static class SmartSearch
    {

        public static List<Person> Search(List<Person> list, object valueToSearsh)
        {
            DataTable table1 = ToDataTable<Person>(list);
            DataTable table = new DataTable();
            for (int i = 0; i < table1.Columns.Count; i++)
            {
                table.Columns.Add(table1.Columns[i].ColumnName);
            }
            DataRow dr1;
            for (int i = 0; i < table1.Rows.Count; i++)
            {
                dr1 = table1.Rows[i];
                if (IsFind(dr1, valueToSearsh))
                    CopyRow(table, dr1);
            }

            return (from DataRow row in table.Rows

                    select new Person
                    {
                        id = row["id"].ToString(),
                        name = row["name"].ToString(),
                        city = row["city"].ToString(),
                        status = row["status"].ToString(),
                        job = row["job"].ToString(),
                        yeshivaOrSeminar = row["yeshivaOrSeminar"].ToString(),
                        gender = row["gender"].ToString(),
                        phone = row["phone"].ToString(),
                        parents = row["parents"].ToString(),
                        imageUrl = row["imageUrl"].ToString(),
                        age = row["age"].ToString(),
                        email = row["email"].ToString(),
                        eda = row["eda"].ToString(),
                        motsa = row["gender"].ToString(),
                        friends = row["phone"].ToString(),
                        details = row["parents"].ToString(),
                        peaOrMitpachat = row["imageUrl"].ToString(),
                        learnOrWork = row["age"].ToString(),
                    }).ToList();
        }
        public static void CopyRow(DataTable table, DataRow row)
        {
            object[] values = new object[table.Columns.Count];
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = row[i];
            }
            table.Rows.Add(values);
        }
        public static bool IsFind(DataRow dr, object valueToSearsh1)
        {
            bool ok = false;
            for (int i = 0; i < dr.Table.Columns.Count; i++)
            {
                if (dr[i].ToString().Contains(valueToSearsh1.ToString()))
                {
                    ok = true;
                    break;
                }
            }
            return ok;
        }

        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
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
