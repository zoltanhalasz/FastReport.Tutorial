using FastReport.OpenSource.Web.MVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FastReport.OpenSource.Web.MVC
{
    public static class Services
    {
        public static DataSet ConvertToDataSet<T>(this IEnumerable<T> source, string name)
        {
            if (source == null)
                throw new ArgumentNullException("source ");
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            var converted = new DataSet(name);
            converted.Tables.Add(NewTable(name, source));
            return converted;
        }

        private static DataTable NewTable<T>(string name, IEnumerable<T> list)
        {
            PropertyInfo[] propInfo = typeof(T).GetProperties();
            DataTable table = Table<T>(name, list, propInfo);
            IEnumerator<T> enumerator = list.GetEnumerator();
            while (enumerator.MoveNext())
                table.Rows.Add(CreateRow<T>(table.NewRow(), enumerator.Current, propInfo));
            return table;
        }

        private static DataRow CreateRow<T>(DataRow row, T listItem, PropertyInfo[] pi)
        {
            foreach (PropertyInfo p in pi)
                row[p.Name.ToString()] = p.GetValue(listItem, null);
            return row;
        }

        private static DataTable Table<T>(string name, IEnumerable<T> list, PropertyInfo[] pi)
        {
            DataTable table = new DataTable(name);
            foreach (PropertyInfo p in pi)
                table.Columns.Add(p.Name, p.PropertyType);
            return table;
        }

        public static List<Employees> GetEmployees()
        {
            List<Employees> EmployeesList = new List<Employees>();
            var myEmp = new Employees() { Address = "Oradea", FirstName = "Adrian", LastName = "Rossini", BirthDate = new DateTime(1975, 10, 18), EmployeeID = 1 };
            EmployeesList.Add(myEmp);
            myEmp = new Employees() { Address = "Salonta", FirstName = "Arpad", LastName = "Dolgos", BirthDate = new DateTime(1960, 05, 30), EmployeeID = 2 };
            EmployeesList.Add(myEmp);
            myEmp = new Employees() { Address = "Beius", FirstName = "Ioane", LastName = "George", BirthDate = new DateTime(1980, 06, 11), EmployeeID = 3 };
            EmployeesList.Add(myEmp);
            myEmp = new Employees() { Address = "Debrecen", FirstName = "Janos", LastName = "Pista", BirthDate = new DateTime(1995, 02, 27), EmployeeID = 4 };
            EmployeesList.Add(myEmp);
            return EmployeesList;
        }

    }
}
