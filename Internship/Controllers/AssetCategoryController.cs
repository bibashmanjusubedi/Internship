using Internship.DAL.Repositories;
using Internship.Models;
using Microsoft.AspNetCore.Mvc;

namespace Internship.Controllers
{
    public class AssetCategoryController: Controller
    {
        private readonly AssetCategoryRepository _assetCategoryRepository;

        // Constructor to initialize the repository
        public AssetCategoryController()
        {
            _assetCategoryRepository = new AssetCategoryRepository(); //line 21 // You could also use Dependency Injection for better testing and maintainability
        }

        // Action to display all asset details
        public IActionResult Index()
        {
            // Get all asset details from the repository
            List<AssetCategory> assetCategories = _assetCategoryRepository.GetAllAssetCategories();

            // Pass the asset details to the view
            return View(assetCategories);
        }

        // Other actions like Create, Edit, Delete can be added here

        // GET: AssetCategory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AssetCategory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AssetCategory assetCategory)
        {
            if (ModelState.IsValid)
            {
                _assetCategoryRepository.CreateAssetCategory(assetCategory);
                return RedirectToAction(nameof(Index)); // Redirect to the list after creation
            }

            return View(assetCategory); // Return to the form with validation errors if any
        }

        public IActionResult Details(int CatID)
        {
            // Get the asset category  by CatID from the repository
            AssetCategory assetCategory = _assetCategoryRepository.GetAssetCategoryById(CatID);

            if (assetCategory == null)
            {
                return NotFound($"No asset category found with CatID {CatID}");
            }
            return View(assetCategory);
        }

        // GET: AssetCategory/Delete/5
        public IActionResult Delete(int CatID)
        {
            AssetCategory assetCategory = _assetCategoryRepository.GetAssetCategoryById(CatID);

            if (assetCategory == null)
            {
                return NotFound($"No asset category found with CatID {CatID}");
            }

            return View(assetCategory); // Show confirmation view
        }

        // POST: AssetCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int CatID)
        {
            _assetCategoryRepository.DeleteAssetCategory(CatID);
            return RedirectToAction(nameof(Index));
        }



    }
}
