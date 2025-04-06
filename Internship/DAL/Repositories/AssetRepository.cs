using Internship.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Internship.DAL;


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
                SqlDataReader reader= command.ExecuteReader();

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

        public void CreateAsset(Asset asset)
        {
            using(var connection = DatabaseHelper.GetConnection())
            {
                // SQL insert query to add a new record to the AssetDetail table
                string query = @"INSERT INTO Asset (AssetId,Name,ShortName,Description,Unit,CatID) VALUES (@AssetId,@Name,@ShortName,@Description,@Unit,@CatID)";
                SqlCommand command = new SqlCommand(query,connection);

                command.Parameters.AddWithValue("@AssetId",asset.AssetId);
                command.Parameters.AddWithValue("@Name", asset.Name);
                command.Parameters.AddWithValue("@ShortName", asset.ShortName);
                command.Parameters.AddWithValue("@Description", asset.Description);
                command.Parameters.AddWithValue("@Unit", asset.Unit);
                command.Parameters.AddWithValue("@CatID", asset.CatID);


                connection.Open();
                command.ExecuteNonQuery();

            }
        }
    }
}
