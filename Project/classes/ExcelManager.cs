using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.InteropServices;
using Project.classes;
using System.Windows;
using ExcelDataReader;
using System.Data;
using Microsoft.Office.Interop.Excel;
using System.Data.OleDb;

namespace Project.Classes
{
    public static class ExcelManager
    {
        public static void ReadExcel()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = File.Open("..\\..\\..\\people.xls", FileMode.Open, FileAccess.Read))
            {
                IExcelDataReader reader;
                reader = ExcelDataReader.ExcelReaderFactory.CreateReader(stream);

                var conf = new ExcelDataSetConfiguration
                {
                    ConfigureDataTable = _ => new ExcelDataTableConfiguration
                    {
                        UseHeaderRow = true
                    }
                };

                var dataSet = reader.AsDataSet(conf);
                var dataTable = dataSet.Tables[0];

                people.peopleList = new List<Person.Person>();
                people.peopleList = (from DataRow dr in dataTable.Rows
                                     select new Person.Person()
                                     {
                                         id = dr["id"].ToString(),
                                         gender = dr["gender"].ToString(),
                                         name = dr["name"].ToString(),
                                         age = dr["age"].ToString(),
                                         height = dr["height"].ToString(),
                                         status = dr["status"].ToString(),
                                         eda = dr["eda"].ToString(),
                                         migzar = dr["migzar"].ToString(),
                                         yeshivaOrSeminar = dr["yeshivaOrSeminar"].ToString(),
                                         isook = dr["isook"].ToString(),
                                         kisooyRosh = dr["kisooyRosh"].ToString(),
                                         learnOrWork = dr["learnOrWork"].ToString(),
                                         job = dr["job"].ToString(),
                                         phone = dr["phone"].ToString(),
                                         homePhone = dr["homePhone"].ToString(),
                                         email = dr["email"].ToString(),
                                         friends = dr["friends"].ToString(),
                                         city = dr["city"].ToString(),
                                         background = dr["background"].ToString(),
                                         imageUrl = dr["imageUrl"].ToString(),
                                     }).ToList();
            }
        }


        public static void WriteExcel()
        {
            DataSet ds = new DataSet("people");
            System.Data.DataTable dt = people.peopleList.ToDataTable();

            ds.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
            dt.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;

            ds.Tables.Add(dt);          

            ExcelLibrary.DataSetHelper.CreateWorkbook("..\\..\\..\\people.xls", ds);
        }
    }
}
