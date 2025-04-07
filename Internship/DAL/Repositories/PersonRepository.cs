using Internship.Models;
using Internship.DAL;
using Microsoft.Data.SqlClient;

namespace Internship.DAL.Repositories
{
    public class PersonRepository
    {
        public List<Person> GetAllPersons()
        {
            List<Person> persons = new List<Person>();

            using (var connection = DatabaseHelper.GetConnection())
            {
                string query = "SELECT * FROM Person";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    // Create a new AssetDetail object and map values from the SQL result
                    var person = new Person
                    {
                        PId = (int)reader["PId"], 
                        Name = reader["Name"]?.ToString(),
                        Address = reader["Address"]?.ToString(),
                        Phone = reader["Phone"]?.ToString(),
                        LoginID = (int)reader["LoginID"],
                        LoginStatus = reader["LoginStatus"] != DBNull.Value? bool.Parse(reader["LoginStatus"].ToString()): false,//line 30
                        Password = reader["Password"]?.ToString(),
                        Remarks = reader["remarks"]?.ToString()
                    };

                    persons.Add(person);
                }
            }


            return persons;
        }

    }
}
