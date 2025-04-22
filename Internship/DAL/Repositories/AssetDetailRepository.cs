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
                        PurchaseDate = reader["PurchaseDate"] != DBNull.Value ? DateOnly.FromDateTime(Convert.ToDateTime(reader["PurchaseDate"])): default,
                        Remark = reader["Remark"]?.ToString(), // Handle string fields
                        Status = reader["Status"]?.ToString() // Handle string fields
                    };

                    assetDetails.Add(assetDetail);
                }
            }
            

            return assetDetails;
        }

        // Other CRUD methods like Create, Update, Delete


        // Create Operation: Insert a new AssetDetail
        public void CreateAssetDetail(AssetDetail assetDetail)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                // SQL insert query to add a new record to the AssetDetail table
                string query = @"INSERT INTO AssetDetail (Sn, AssetId, AssetCode, Price, PurchaseDate, Remark, Status)
                                 VALUES (@Sn,@AssetId, @AssetCode, @Price, @PurchaseDate, @Remark, @Status)";

                SqlCommand command = new SqlCommand(query, connection);

                // Add parameters to the SQL command to prevent SQL injection
                command.Parameters.AddWithValue("@Sn", assetDetail.Sn);
                command.Parameters.AddWithValue("@AssetId", assetDetail.AssetId);
                command.Parameters.AddWithValue("@AssetCode", assetDetail.AssetCode);
                command.Parameters.AddWithValue("@Price", assetDetail.Price);
                command.Parameters.AddWithValue("@PurchaseDate", assetDetail.PurchaseDate);
                command.Parameters.AddWithValue("@Remark", assetDetail.Remark ?? (object)DBNull.Value); // Handle null for Remark
                command.Parameters.AddWithValue("@Status", assetDetail.Status ?? (object)DBNull.Value); // Handle null for Status

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public AssetDetail GetAssetDetailById(int Sn)
        {
            AssetDetail assetDetail = null;
            using (var connection = DatabaseHelper.GetConnection())
            {
                string query = "SELECT * FROM AssetDetail WHERE Sn = @Sn";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Sn", Sn);//line 69
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    assetDetail = new AssetDetail
                    {
                        Sn = (int)reader["Sn"],
                        AssetId = (int)reader["AssetId"],
                        AssetCode = (int)reader["AssetCode"],
                        Price = (int)reader["Price"],
                        PurchaseDate= reader["PurchaseDate"] != DBNull.Value ? DateOnly.FromDateTime(Convert.ToDateTime(reader["PurchaseDate"])) : default,
                        Remark = reader["Remark"].ToString(),
                        Status= reader["Status"].ToString()
                    };
                }

            }
            return assetDetail;

        }

        public void DeleteAssetDetail(int Sn)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                string query = "DELETE FROM AssetDetail WHERE Sn = @Sn";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Sn", Sn);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }



    }
}
