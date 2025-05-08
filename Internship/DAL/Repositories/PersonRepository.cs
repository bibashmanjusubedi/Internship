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
                        LoginStatus = reader["LoginStatus"] != DBNull.Value ? bool.Parse(reader["LoginStatus"].ToString()) : false,//line 30
                        Password = reader["Password"]?.ToString(),
                        Remarks = reader["remarks"]?.ToString()
                    };

                    persons.Add(person);
                }
            }


            return persons;
        }


        public void CreatePerson(Person person)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                // SQL insert query to add a new record to the Person table
                string query = @"INSERT INTO Person (PId,Name,Address,Phone,LoginID,LoginStatus,Password,Remarks) VALUES (@PId,@Name,@Address,@Phone,@LoginID,@LoginStatus,@Password,@Remarks)";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@PId", person.PId);
                command.Parameters.AddWithValue("@Name", person.Name);
                command.Parameters.AddWithValue("@Address", person.Address);
                command.Parameters.AddWithValue("@Phone", person.Phone);
                command.Parameters.AddWithValue("@LoginID", person.LoginID);
                command.Parameters.AddWithValue("@LoginStatus", person.LoginStatus);
                command.Parameters.AddWithValue("@Password", person.Password);
                command.Parameters.AddWithValue("@Remarks", person.Remarks);

                connection.Open();
                command.ExecuteNonQuery();

            }
        }

        public Person GetPersonById(int PId)
        {
            Person person = null;
            using (var connection = DatabaseHelper.GetConnection())
            {
                string query = "SELECT * FROM Person WHERE PId = @PId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("PId", PId);//line 69
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    person = new Person
                    {
                        PId = (int)reader["PId"],
                        Name = reader["Name"].ToString(),
                        Address = reader["Address"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        LoginID = (int)reader["LoginID"],
                        Password = reader["Password"].ToString(),
                        LoginStatus = reader["LoginStatus"] != DBNull.Value ? Convert.ToBoolean(reader["LoginStatus"]) : false,//line 87
                        Remarks = reader["Remarks"].ToString()
                    };
                }

            }
            return person;

        }

        public void DeletePerson(int PId)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                string query = "DELETE FROM Person WHERE PId = @PId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PId", PId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdatePerson(Person person)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                string query = @"UPDATE Person 
                         SET Name = @Name,
                             Address = @Address,
                             Phone = @Phone,
                             LoginID = @LoginID,
                             LoginStatus = @LoginStatus,
                             Password = @Password,
                             Remarks = @Remarks
                         WHERE PId = @PId";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@PId", person.PId);
                command.Parameters.AddWithValue("@Name", person.Name);
                command.Parameters.AddWithValue("@Address", person.Address);
                command.Parameters.AddWithValue("@Phone", person.Phone);
                command.Parameters.AddWithValue("@LoginID", person.LoginID);
                command.Parameters.AddWithValue("@LoginStatus", person.LoginStatus);
                command.Parameters.AddWithValue("@Password", person.Password);
                command.Parameters.AddWithValue("@Remarks", person.Remarks);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public bool RegisterPerson(Person person)
        {
            if (IsLoginIdExists(person.LoginID))
            {
                return false;// Registration - LoginID already exists
            }

            using (var connection = DatabaseHelper.GetConnection())
            {
                try
                {
                    // SQL insert query registration
                    string query = @"INSERT INTO PERSON (Name,Address,Phone, LoginID,Password,LoginStatus,Remarks) VALUES (@Name,@Address,@Phone,@LoginID,@Password,@LoginStatus,@Remarks)";
                    SqlCommand command = new SqlCommand(query, connection);
                    // Note: Removed PId parameter since it appears to be incremented in DB
                    command.Parameters.AddWithValue("@Name", person.Name ?? (object)DBNull.Value);//line 155
                    command.Parameters.AddWithValue("@Address", person.Address ?? (object)DBNull.Value);//line 156
                    command.Parameters.AddWithValue("@Phone", person.Phone ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@LoginID", person.LoginID);


                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return (rowsAffected > 0);
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool IsLoginIdExists(int loginID)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                string query = "SELECT COUNT(1) FROM PERSON Where LoginID = @LoginID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LoginID", loginID);//line 178
                connection.Open();
                int count = (int)command.ExecuteScalar();
                return (count > 0);
            }
        }

    }
}
