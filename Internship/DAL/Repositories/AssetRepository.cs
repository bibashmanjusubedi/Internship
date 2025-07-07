using Internship.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Internship.DAL;
using System.Linq;
using System.Threading.Tasks;


namespace Internship.DAL.Repositories
{
    public class AssetRepository
    {
        public List<Asset> GetAllAssets()
        {
            List<Asset> assets = new List<Asset>();
            using (var connection = DatabaseHelper.GetConnection())
            {
                string query = "SELECT * FROM Asset";
                SqlCommand command = new SqlCommand(query,connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while(reader.Read())
                {
                    var asset = new Asset
                    {
                        AssetId = (int)reader["AssetId"],
                        Name = reader["Name"].ToString(),
                        ShortName = reader["ShortName"].ToString(),
                        Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null,
                        Unit = reader["Unit"] != DBNull.Value ? reader["Unit"].ToString() : null,
                        CatID = (int)reader["CatID"]

                    };
                    assets.Add(asset);
                }
               
            }
            return assets;
        }

        public async Task<int> GetAssetCountAsync()
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                string query = "SELECT COUNT(*) FROM Asset";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                // ExecuteScalar returns the first column of the first row
                var count = (int)await command.ExecuteScalarAsync();
                return count;
            }
        }

        public void CreateAsset(Asset asset)
        {
            using(var connection = DatabaseHelper.GetConnection())
            {
                // SQL insert query to add a new record to the AssetDetail table
                string query = @"INSERT INTO Asset (Name, ShortName, Description, Unit, CatID) 
                         VALUES (@Name, @ShortName, @Description, @Unit, @CatID)";
                SqlCommand command = new SqlCommand(query,connection);

                // command.Parameters.AddWithValue("@AssetId",asset.AssetId);
                command.Parameters.AddWithValue("@Name", asset.Name);
                command.Parameters.AddWithValue("@ShortName", asset.ShortName);
                command.Parameters.AddWithValue("@Description", asset.Description);
                command.Parameters.AddWithValue("@Unit", asset.Unit);
                command.Parameters.AddWithValue("@CatID", asset.CatID);


                connection.Open();
                command.ExecuteNonQuery();

            }
        }

        public Asset GetAssetById(int AssetId)
        {
            Asset asset = null;
            using (var connection = DatabaseHelper.GetConnection())
            {
                string query = "SELECT * FROM Asset WHERE AssetId = @AssetId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AssetId", AssetId);//line 69
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    asset = new Asset
                    {
                        AssetId = (int)reader["AssetId"],
                        Name = reader["Name"].ToString(),
                        ShortName = reader["ShortName"].ToString(),
                        Description = reader["Description"].ToString(),
                        Unit = reader["Unit"].ToString(),
                        CatID = (int)reader["CatID"]
                    };
                }

            }
            return asset;

        }

        public void DeleteAsset(int AssetId)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                string query = "DELETE FROM Asset WHERE AssetId = @AssetId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AssetId", AssetId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }


        public void UpdateAsset(Asset asset)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                string query = @"UPDATE Asset 
                         SET Name = @Name, 
                             ShortName = @ShortName, 
                             Description = @Description, 
                             Unit = @Unit, 
                             CatID = @CatID 
                         WHERE AssetId = @AssetId";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AssetId", asset.AssetId);
                command.Parameters.AddWithValue("@Name", asset.Name);
                command.Parameters.AddWithValue("@ShortName", asset.ShortName);
                command.Parameters.AddWithValue("@Description", (object)asset.Description ?? DBNull.Value);
                command.Parameters.AddWithValue("@Unit", (object)asset.Unit ?? DBNull.Value);
                command.Parameters.AddWithValue("@CatID", asset.CatID);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }



    }
}
