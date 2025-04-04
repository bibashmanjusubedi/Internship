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
                SqlCommand = new SqlCommand(query,connection);
                connection.Open();
                SqlDataReader = command.ExecuteReader();

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

                }
            }
        }
    }
}
