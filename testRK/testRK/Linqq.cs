using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;

namespace RK3
{
    class RK
    {
        private static string connectionString = @"Data Source = .\SQLEXPRESS; Database = RK3TEST; Integrated Security = true";

        public void Ado1()
        {
            const string queryString = @"SELECT e.DateOfUpgrade, e.FIO
                                         FROM Employees AS e";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand(queryString, connection);

            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
                Console.WriteLine(dataReader[0].ToString());

            dataReader.Close();
            connection.Close();
        }

        public void linq1()
        {
            DataContext db = new DataContext(@"Data Source = .\SQLEXPRESS; Database = RK3TEST; Integrated Security = true");
            var moreThenThree = from a in db.GetTable<Employees>()
                                where (DateTime.Today - a.DateOfUpgrade).TotalDays > 365 * 3
                                select new { date = a.DateOfUpgrade, fio = a.FIO };
            

            foreach (var m in moreThenThree)
                Console.WriteLine(m.fio + ' ' + m.date);
        }

        
        public void linq2()
        {
            DataContext db = new DataContext(@"Data Source = .\SQLEXPRESS; Database = RK3TEST; Integrated Security = true");
            var maxValue = (from e in db.GetTable<Employees>()
                            join a in db.GetTable<Accounting>() on e.Specialty equals a.Specialty
                            orderby e.DateOfUpgrade
                            select new { fio = e.FIO, name = a.CourseName }).Take(1);

            foreach (var m in maxValue)
                Console.WriteLine(m.fio + ' ' + m.name);
        }

        /*
        public void linq3()
        {
            DataContext db = new DataContext(@"Data Source = .\SQLEXPRESS; Database = RK3TEST; Integrated Security = true");
            var maxValue = (from a in db.GetTable<Accounting>()
                            join e in db.GetTable<Employees>() on a.Specialty equals e.Specialty
                            select new { count = ???????COUNT??? });

            foreach (var m in maxValue)
                Console.WriteLine(m.count);
        }
        */

        public RK()
        {
            Ado1();
            linq1();
            linq2();
            //linq3();
        }
    }
}
