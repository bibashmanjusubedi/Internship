using Internship.Models;
using System.Collections.Generic;
using System.Linq;
using Internship.DAL.Repositories;

namespace Internship.Services
{
    public class CategoryService: ICategoryService
    {
        private readonly AssetCategoryRepository _categoryRepository;

        // Inject the repository through the constructor
        public CategoryService(AssetCategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public AssetCategory GetAssetCategoryById(int CatID)
        {
            // Use the repository to fetch the category from the database
            return _categoryRepository.GetAssetCategoryById(CatID);
        }
    }
}
