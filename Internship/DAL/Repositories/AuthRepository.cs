using Microsoft.Data.SqlClient;

namespace Internship.DAL.Repositories
{
    public class AuthRepository
    {
        public bool ValidateUser(string username,string password)
        {
            using(var connection = DatabaseHelper.GetConnection())
            {
                string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password=@Password";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);
                connection.Open();
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }
    }
}
