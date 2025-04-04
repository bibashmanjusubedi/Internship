using Internship.Models;
using Internship.DAL;
using Microsoft.Data.SqlClient;

namespace Internship.DAL.Repositories
{
    public class AssetDetailRepository
    {
        public List<AssetDetail> GetAllAssetDetails()
        {
            List<AssetDetail> assetDetails = new List<AssetDetail>();

            using (var connection = DatabaseHelper.GetConnection())
            {
                string query = "SELECT * FROM AssetDetail";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    // Create a new AssetDetail object and map values from the SQL result
                    var assetDetail = new AssetDetail
                    {
                        Sn = (int)reader["Sn"], // Sn is the primary key
                        AssetId = (int)reader["AssetId"], // AssetId (foreign key)
                        AssetCode = (int)reader["AssetCode"], // AssetCode
                        Price = reader["Price"] != DBNull.Value ? (int)reader["Price"] : 0, // Price (check for null values)
                        PurchaseDate = reader["PurchaseDate"] != DBNull.Value ? DateOnly.Parse(reader["PurchaseDate"].ToString()) : default, // Parse PurchaseDate
                        Remark = reader["Remark"]?.ToString(), // Handle string fields
                        Status = reader["Status"]?.ToString() // Handle string fields
                    };
                }
            }

            return assetDetails;
        }

        // Other CRUD methods like Create, Update, Delete
    }
}
