using Internship.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Internship.DAL;

namespace Internship.DAL.Repositories
{
    public class AssetCategoryRepository
    {
        public List<AssetCategory> GetAllAssetCategories()
        {
            List<AssetCategory> assetCategories = new List<AssetCategory>();
            using (var connection = DatabaseHelper.GetConnection())
            {
                string query = "SELECT * FROM AssetCategory";
                SqlCommand command = new SqlCommand(query,connection);
                connection.Open();
                SqlDataReader reader= command.ExecuteReader();

                while(reader.Read())
                {
                    var assetCategory = new AssetCategory
                    {
                        CatID = (int)reader["CatID"],
                        CatName = reader["CatName"].ToString(),

                    };
                    assetCategories.Add(assetCategory);
                }
               
            }
            return assetCategories;
        }
        
        // CRUD Operations: Create Read Update(Edit) and Delete for AssetCategory
        // Create operations for AssetCategory
        public void CreateAssetCategory(AssetCategory assetCategory)
        {
            using(var connection = DatabaseHelper.GetConnection())
            {
                // SQL insert query to add a new record to the AssetDetail table
                string query = @"INSERT INTO AssetCategory (CatID,CatName) VALUES (@CatID,@CatName)";
                SqlCommand command = new SqlCommand(query,connection);

                command.Parameters.AddWithValue("@CatID",assetCategory.CatID);
                command.Parameters.AddWithValue("@CatName", assetCategory.CatName);


                connection.Open();
                command.ExecuteNonQuery();

            }
        }

        public AssetCategory GetAssetCategoryById(int CatID)
        {
            AssetCategory assetCategory = null;
            using ( var connection = DatabaseHelper.GetConnection())
            {
                string query = "SELECT * FROM AssetCategory WHERE CatID = @CatID";
                SqlCommand command = new SqlCommand(query,connection);
                command.Parameters.AddWithValue("@CatID", CatID);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.Read())
                {
                    assetCategory = new AssetCategory
                    {
                        CatID = (int)reader["CatID"],
                        CatName = reader["CatName"].ToString()
                    };
                }
                
            }
            return assetCategory;

        }


        public void DeleteAssetCategory(int CatID)
        {
            using (var  connection = DatabaseHelper.GetConnection())
            {
                string query = "DELETE FROM AssetCategory WHERE CatID=@CatID";
                SqlCommand command = new SqlCommand(query,connection);
                command.Parameters.AddWithValue("@CatID", CatID);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }


    }
}
