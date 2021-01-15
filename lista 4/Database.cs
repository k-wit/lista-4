using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace lista_4
{
    class Database
    {
            public static string connetionString;
            public static SqlConnection cnn;
            public static SqlCommand command;
            public static SqlDataAdapter adapter = new SqlDataAdapter();
            public static DataTable dataTable = new DataTable();


            public static void openConnection()
            {
                connetionString = @"Data source=LAPTOP-0V4R6QN9; database=NobelPrizeWinners; Integrated Security=SSPI;";
                cnn = new SqlConnection(connetionString);
                cnn.Open();

            }

            public static void closeConnection()
            {
                cnn.Close();
            }

            public static void readBase()
            {
                command = new SqlCommand();

                command.CommandText = "Select [Index], [Surname], [Name], [Age], [Pesel] from List";
                command.CommandType = CommandType.Text;
                command.Connection = cnn;

                adapter = new SqlDataAdapter(command);
                dataTable = new DataTable("List");
                adapter.Fill(dataTable);

            }
    }
}
