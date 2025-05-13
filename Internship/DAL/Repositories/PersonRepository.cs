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
                    var person = new Person
                    {
                        PId = (int)reader["PId"],
                        Name = reader["Name"]?.ToString(),
                        Address = reader["Address"]?.ToString(),
                        Phone = reader["Phone"]?.ToString(),
                        LoginID = (int)reader["LoginID"],
                        LoginStatus = reader["LoginStatus"] != DBNull.Value ? bool.Parse(reader["LoginStatus"].ToString()) : false,
                        Password = reader["Password"]?.ToString(),
                        Remarks = reader["Remarks"]?.ToString()
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
                string query = @"INSERT INTO Person (Name, Address, Phone, LoginID, LoginStatus, Password, Remarks) 
                                 VALUES (@Name, @Address, @Phone, @LoginID, @LoginStatus, @Password, @Remarks)";
                SqlCommand command = new SqlCommand(query, connection);

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
                command.Parameters.AddWithValue("@PId", PId);

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
                        LoginStatus = reader["LoginStatus"] != DBNull.Value ? Convert.ToBoolean(reader["LoginStatus"]) : false,
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
                return false; // Registration - LoginID already exists
            }

            using (var connection = DatabaseHelper.GetConnection())
            {
                try
                {
                    string query = @"INSERT INTO PERSON (Name, Address, Phone, LoginID, Password, LoginStatus, Remarks) 
                                     VALUES (@Name, @Address, @Phone, @LoginID, @Password, @LoginStatus, @Remarks)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Name", person.Name ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Address", person.Address ?? (object)DBNull.Value);
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
                string query = "SELECT COUNT(1) FROM PERSON WHERE LoginID = @LoginID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LoginID", loginID);

                connection.Open();
                int count = (int)command.ExecuteScalar();
                return (count > 0);
            }
        }
    }
}
