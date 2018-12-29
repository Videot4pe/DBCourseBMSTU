using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Lab8
{
    class Program
    {
        private static string connectionString = @"Data Source = .\SQLEXPRESS; Database = MusicDB; Integrated Security = true";
        static void Main(string[] args)
        {
            Program lab = new Program();
            //lab.sqlConnectionString();
            //lab.selection();
            //lab.sqlDataReader();
            //lab.sqlCommandWithParameters();
            //lab.dataSetFromTable();
            //lab.filterSort();
            //lab.insert();
            //lab.toXml();
            lab.delete();
            //lab.storedProcedure();
            //lab.update();
        }
        public void sqlConnectionString()
        {
            Console.WriteLine("1");

            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("Connection has been opened.");
                Console.WriteLine("Connection properties:");
                Console.WriteLine("\tConnection string: {0}", connection.ConnectionString);
                Console.WriteLine("\tDatabase:          {0}", connection.Database);
                Console.WriteLine("\tData Source:       {0}", connection.DataSource);
                Console.WriteLine("\tServer version:    {0}", connection.ServerVersion);
                Console.WriteLine("\tConnection state:  {0}", connection.State);
                Console.WriteLine("\tWorkstation id:    {0}", connection.WorkstationId);
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            finally
            {
                connection.Close();
                Console.WriteLine("Connection has been closed.");
            }
            Console.ReadLine();
        }

        public void selection()
        {
            Console.WriteLine("2");

            string queryString = @"select count(*) from Groups";
            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand scalarQueryCommand = new SqlCommand(queryString, connection);
            try
            {
                connection.Open();
                Console.WriteLine("Groups count: {0}", scalarQueryCommand.ExecuteScalar());
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            finally
            {
                connection.Close();
            }
            Console.ReadLine();
        }

        public void sqlDataReader()
        {
            Console.WriteLine("3");

            string queryString = @"select AlbumId, GroupId from Albums where AlbumId > 60";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand dataQueryCommand = new SqlCommand(queryString, connection);
            try
            {
                connection.Open();
                SqlDataReader dataReader = dataQueryCommand.ExecuteReader();

                Console.WriteLine("AlbumId - GroupId: ");
                while (dataReader.Read())
                {
                    Console.WriteLine("{0} - {1}", dataReader.GetValue(0), dataReader.GetValue(1));
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            finally
            {
                connection.Close();
            }
            Console.ReadLine();
        }

        public void sqlCommandWithParameters()
        {
            Console.WriteLine("4");

            string selectWithDuration = @"select Title from Albums where Albums.duration > @duration";

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand selectCommand = new SqlCommand(selectWithDuration, connection);

            selectCommand.Parameters.Add("@duration", SqlDbType.Int);

            try
            {
                connection.Open();

                int duration = Convert.ToInt32(Console.ReadLine());
                selectCommand.Parameters["@duration"].Value = duration;
                var dataReader = selectCommand.ExecuteReader();
                while (dataReader.Read())
                    Console.WriteLine("{0}", dataReader.GetValue(0));
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            Console.ReadLine();
        }

        public void storedProcedure()
        {
            Console.WriteLine("5");

            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand storedProcedureCommand = connection.CreateCommand();
            storedProcedureCommand.CommandType = CommandType.StoredProcedure;
            storedProcedureCommand.CommandText = "CalculateFactorial1";
            try
            {
                connection.Open();

                Console.Write("Factorial. Input the number: ");
                int number = Convert.ToInt32(Console.ReadLine());
                storedProcedureCommand.Parameters.Add("n", SqlDbType.Int).Value = number;

                var returnParameter = storedProcedureCommand.Parameters.Add("@ReturnVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                storedProcedureCommand.ExecuteNonQuery();
                var result = returnParameter.Value;

                Console.WriteLine("{0}! = {1}", number, result);
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            finally
            {
                connection.Close();
                Console.ReadLine();
            }
        }

        public void dataSetFromTable()
        {
            Console.WriteLine("6");

            string query = @"select Title, Duration from Albums where TopOfTheYear > 10";

            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "AlbumsWithTop");
                DataTable table = dataSet.Tables["AlbumsWithTop"];

                Console.WriteLine("Albums with top > 90:");
                foreach (DataRow row in table.Rows)
                {
                    Console.Write("{0} ", row["Title"]);
                    Console.Write("{0}\n", row["Duration"]);
                }
                Console.WriteLine();
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            finally
            {
                connection.Close();
            }
            Console.ReadLine();
        }

        public void filterSort()
        {
            Console.WriteLine("7");

            string query = @"select * from Albums";
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "Albums");
                DataTableCollection tables = dataSet.Tables;

                Console.Write("Input part of album title: ");
                string partOfName = Console.ReadLine();
                Console.WriteLine();

                string filter = "Title like '%" + partOfName + "%'";
                string sort = "Title asc";
                Console.WriteLine("Titles: ");
                foreach (DataRow row in tables["Albums"].Select(filter, sort))
                {
                    Console.Write("{0} ", row["AlbumId"]);
                    Console.Write("{0} ", row["Title"]);
                    Console.Write("{0}\n", row["Duration"]);
                }
                Console.WriteLine();
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Bad input! Message: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            Console.ReadLine();
        }

        public void insert()
        {
            Console.WriteLine("8");

            string dataCommand = @"select * from Musicians";
            string insertQueryString = @"insert into Musicians (Name, Surname, BirthDate, Instrument) values (@name, @surname, @birthdate, @instrument)";

            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                Console.WriteLine("Inserting a new Musician. Input: ");
                Console.Write("name: ");
                string name = Console.ReadLine();
                Console.Write("surname: ");
                string surname = Console.ReadLine();
                Console.Write("birthdate: ");
                DateTime birthdate = Convert.ToDateTime(Console.ReadLine());
                Console.Write("instrument: ");
                string instrument = Console.ReadLine();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(new SqlCommand(dataCommand, connection));
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "Musicians");
                DataTable table = dataSet.Tables["Musicians"];

                DataRow insertingRow = table.NewRow();
                insertingRow["Name"] = name;
                insertingRow["Surname"] = surname;
                insertingRow["BirthDate"] = birthdate;
                insertingRow["Instrument"] = instrument;


                table.Rows.InsertAt(insertingRow, 2);

                Console.WriteLine("Musicians");
                foreach (DataRow row in table.Rows)
                {
                    Console.Write("{0} ", row["Name"]);
                    Console.Write("{0} ", row["Surname"]);
                    Console.Write("{0} ", row["BirthDate"]);
                    Console.Write("{0}\n", row["Instrument"]);
                }

                SqlCommand insertQueryCommand = new SqlCommand(insertQueryString, connection);
                insertQueryCommand.Parameters.Add("@name", SqlDbType.VarChar, 85, "Name");
                insertQueryCommand.Parameters.Add("@surname", SqlDbType.VarChar, 20, "Surname");
                insertQueryCommand.Parameters.Add("@birthdate", SqlDbType.DateTime, 4, "BirthDate");
                insertQueryCommand.Parameters.Add("@instrument", SqlDbType.VarChar, 4, "Instrument");

                dataAdapter.InsertCommand = insertQueryCommand;
                dataAdapter.Update(dataSet, "Musicians");
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Bad input! " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            Console.ReadLine();
        }

        public void delete()
        {
            Console.WriteLine("9");

            string dataCommand = @"select * from Albums where Duration > 50";
            string deleteQueryString = @"delete from Albums where Title = @title";

            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                Console.WriteLine("Deleting album. Input: ");
                Console.Write("title: ");
                string title = Console.ReadLine();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(new SqlCommand(dataCommand, connection));
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "Albums");
                DataTable table = dataSet.Tables["Albums"];

                string filter = "Title = '"+title+"'";
                if (table.Select(filter).Count() == 0)
                    Console.Write("No elements \n");
                else
                    foreach (DataRow row in table.Select(filter))
                    {
                        row.Delete();
                    }

                SqlCommand deleteQueryCommand = new SqlCommand(deleteQueryString, connection);
                deleteQueryCommand.Parameters.Add("@title", SqlDbType.VarChar, 85, "Title");

                dataAdapter.DeleteCommand = deleteQueryCommand;
                dataAdapter.Update(dataSet, "Albums");

                
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Bad input! " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            Console.ReadLine();
        }

        public void toXml()
        {
            Console.WriteLine("10");

            string query = @"select * from Albums";

            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "Albums");
                DataTable table = dataSet.Tables["Albums"];

                dataSet.WriteXml("Albums.xml");
                Console.WriteLine("Albums.xml.");
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            finally
            {
                connection.Close();
            }
            Console.ReadLine();
        }


        public void update()
        {
            Console.WriteLine("11");

            string dataCommand = @"select * from Musicians";
            string updateQueryString = @"update Musicians set Name = 'Colya' where Name = @name";

            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                Console.WriteLine("Updating a new Musician. Input: ");
                Console.Write("name: ");
                string name = Console.ReadLine();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(new SqlCommand(dataCommand, connection));
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "Musician");
                DataTable table = dataSet.Tables["Musician"];

                string filter = "Name = '" + name + "'";
                if (table.Select(filter).Count() == 0)
                    Console.Write("No elements \n");
                else
                    foreach (DataRow row in table.Select(filter))
                        row["Name"] = "NENICOLAY!!!";

                SqlCommand deleteQueryCommand = new SqlCommand(updateQueryString, connection);
                deleteQueryCommand.Parameters.Add("@name", SqlDbType.VarChar, 85, "Name");

                dataAdapter.DeleteCommand = deleteQueryCommand;
                dataAdapter.Update(dataSet, "Musicians");
            }


            catch (SqlException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Bad input! " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            Console.ReadLine();
        }

    }
}
