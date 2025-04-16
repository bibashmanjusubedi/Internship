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

        public void CreateAssetOut(AssetOut assetOut)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                // SQL insert query to add a new record to the AssetDetail table
                string query = @"INSERT INTO AssetOut (Sn,AssetCode,OutDate,PId,DateToReturn,ReturnDate) VALUES (@Sn,@AssetCode,@OutDate,@PId,@DateToReturn,@ReturnDate)";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Sn", assetOut.Sn);
                command.Parameters.AddWithValue("@AssetCode", assetOut.AssetCode);
                command.Parameters.AddWithValue("@OutDate", assetOut.OutDate);
                command.Parameters.AddWithValue("@PId", assetOut.PId);
                command.Parameters.AddWithValue("@DateToReturn", assetOut.DateToReturn);
                command.Parameters.AddWithValue("@ReturnDate", assetOut.ReturnDate);
                command.Parameters.AddWithValue("@Remarks", assetOut.Remarks);


                connection.Open();
                command.ExecuteNonQuery();

            }
        }

        public AssetOut GetAssetOutById(int Sn)
        {
            AssetOut assetOut = null;
            using (var connection = DatabaseHelper.GetConnection())
            {
                string query = "SELECT * FROM AssetOut WHERE Sn = @Sn";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Sn", Sn);//line 69
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    assetOut = new AssetOut
                    {
                        Sn = (int)reader["Sn"],
                        AssetCode = (int)reader["AssetCode"], // AssetCode
                        PId = (int)reader["PId"],//assigned to
                        OutDate = reader["OutDate"] != DBNull.Value ? DateOnly.FromDateTime(Convert.ToDateTime(reader["OutDate"])) : default,
                        DateToReturn = reader["DateToReturn"] != DBNull.Value ? DateOnly.FromDateTime(Convert.ToDateTime(reader["DateToReturn"])) : default,
                        ReturnDate = reader["ReturnDate"] != DBNull.Value ? DateOnly.FromDateTime(Convert.ToDateTime(reader["ReturnDate"])) : default,
                        Remarks = reader["Remarks"]?.ToString(), // Handle string field
                    };
                }

            }
            return assetOut;

        }
    }

    



}
