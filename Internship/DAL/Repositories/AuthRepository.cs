using Microsoft.Data.SqlClient;

namespace Internship.DAL.Repositories
{
    public class AuthRepository
    {
        public bool ValidateUser(string name,string password)
        {
            using(var connection = DatabaseHelper.GetConnection())
            {
                string query = "SELECT COUNT(*) FROM Person WHERE Name = @Name AND Password=@Password";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Password", password);
                connection.Open();//line15
                int count = (int)command.ExecuteScalar();

                // Console.WriteLine(name, password);
                return count > 0;
            }
        }
    }
}
