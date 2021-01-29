using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace lista_4
{
    public class Database
    {
            public static string connetionString;
            public static SqlConnection cnn;
            public static SqlCommand command;
            public static SqlDataAdapter adapter = new SqlDataAdapter();
            public static DataTable dataTable = new DataTable();


            public Database()
            {
                connetionString = @"Data source=LAPTOP-0V4R6QN9; database=NobelPrizeWinners; Integrated Security=SSPI;";
                cnn = new SqlConnection(connetionString);
                cnn.Open();
            }

            public void closeConnection()
            {
                cnn.Close();
            }

            public DataTable readBase()
            {
                command = new SqlCommand();

                command.CommandText = "GetAllPersons";
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = cnn;

                adapter = new SqlDataAdapter(command);
                dataTable = new DataTable("Table_1");
                adapter.Fill(dataTable);
            return dataTable;
            }

        public void SavePerson(Nobel_Prize_winner person)
        {

            command = new SqlCommand();

            command.CommandText = "InsertPerson";
            command.CommandType = CommandType.StoredProcedure;
            command.Connection = cnn;

            command.Parameters.Add("@DB", SqlDbType.Date).Value = person.BirthD;
            command.Parameters.Add("@DD", SqlDbType.Date).Value = person.DeathD;
            command.Parameters.Add("@FN", SqlDbType.NVarChar).Value = person.FirstName;
            command.Parameters.Add("@LN", SqlDbType.NVarChar).Value = person.LastName;
            command.Parameters.Add("@N", SqlDbType.NVarChar).Value = person.Nationality;
            command.Parameters.Add("@F", SqlDbType.NVarChar).Value = person.Field;
            command.Parameters.Add("@Y", SqlDbType.Int).Value = person.Year;
            command.Parameters.Add("@PIC", SqlDbType.NVarChar).Value = Convert.ToBase64String(person.LargeIconSerialized);
            command.ExecuteNonQuery();
        }
    }
}
