using Internship.Models;
using Internship.DAL;
using Microsoft.Data.SqlClient;


namespace Internship.DAL.Repositories
{
    public class AssetOutRepository
    {
        public List<AssetOut> GetAllAssetOuts()
        {
            List<AssetOut> assetOuts = new List<AssetOut>();

            using (var connection = DatabaseHelper.GetConnection())
            {
                string query = "SELECT * FROM AssetOut";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    // Create a new AssetDetail object and map values from the SQL result
                    var assetOut = new AssetOut
                    {
                        Sn = (int)reader["Sn"], // Sn is the primary key
                        AssetCode = (int)reader["AssetCode"], // AssetCode
                        PId = (int)reader["PId"],//assigned to
                        OutDate = reader["OutDate"] != DBNull.Value ? DateOnly.FromDateTime(Convert.ToDateTime(reader["OutDate"])) : default,
                        DateToReturn = reader["DateToReturn"] != DBNull.Value ? DateOnly.FromDateTime(Convert.ToDateTime(reader["DateToReturn"])) : default,
                        ReturnDate = reader["ReturnDate"] != DBNull.Value ? DateOnly.FromDateTime(Convert.ToDateTime(reader["ReturnDate"])) : default,
                        Remarks = reader["Remarks"]?.ToString(), // Handle string field
                    };

                    assetOuts.Add(assetOut);
                }
            }


            return assetOuts;
        }
    }
}
