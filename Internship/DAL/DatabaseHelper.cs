using System;
using Microsoft.Data.SqlClient;
namespace Internship.DAL // DAL for Data Acess Layer
{
    public class DatabaseHelper
    {
        private static string connectionString = "my_connection_string";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);

        }
    }
}
