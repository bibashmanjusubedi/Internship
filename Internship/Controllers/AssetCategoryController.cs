using Internship.DAL.Repositories;
using Internship.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Internship.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AssetCategoryController : ControllerBase
    {
        private readonly AssetCategoryRepository _assetCategoryRepository;

        // Constructor to initialize the repository
        public AssetCategoryController()
        {
            _assetCategoryRepository = new AssetCategoryRepository(); //line 21 // You could also use Dependency Injection for better testing and maintainability
        }

        // Action to display all asset details

        [HttpGet("")]
        [HttpGet("Index")]
        public IActionResult Index()
        {
            // Get all asset details from the repository
            List<AssetCategory> assetCategories = _assetCategoryRepository.GetAllAssetCategories();

            // Pass the asset details to the view
            return Ok(assetCategories);
        }

        // Other actions like Create, Edit, Delete can be added here

        // GET: AssetCategory/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return Ok(new { message = "This would be your create form data" });
        }

        // POST: AssetCategory/Create
        [HttpPost("Create")]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(AssetCategory assetCategory)
        {
            if (ModelState.IsValid)
            {
                _assetCategoryRepository.CreateAssetCategory(assetCategory);
                return RedirectToAction(nameof(Index)); // Redirect to the list after creation
            }

            return Ok(assetCategory); // Return to the form with validation errors if any
        }

        [HttpGet("{CatID}")]
        public IActionResult Details(int CatID)
        {
            // Get the asset category  by CatID from the repository
            AssetCategory assetCategory = _assetCategoryRepository.GetAssetCategoryById(CatID);

            if (assetCategory == null)
            {
                return NotFound($"No asset category found with CatID {CatID}");
            }
            return Ok(assetCategory);
        }

        // GET: AssetCategory/Delete/5
        public IActionResult Delete(int CatID)
        {
            AssetCategory assetCategory = _assetCategoryRepository.GetAssetCategoryById(CatID);

            if (assetCategory == null)
            {
                return NotFound($"No asset category found with CatID {CatID}");
            }

            return Ok(assetCategory); // Show confirmation view
        }

        // POST: AssetCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int CatID)
        {
            _assetCategoryRepository.DeleteAssetCategory(CatID);
            return RedirectToAction(nameof(Index));
        }

        // GET: AssetCategory/Edit/5
        public IActionResult Edit(int CatID)
        {
            var assetCategory = _assetCategoryRepository.GetAssetCategoryById(CatID);

            if (assetCategory == null)
            {
                return NotFound($"No asset category found with CatID {CatID}");
            }

            return Ok(assetCategory); // Show form with current values
        }

        // POST: AssetCategory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AssetCategory assetCategory)
        {
            if (ModelState.IsValid)
            {
                _assetCategoryRepository.UpdateAssetCategory(assetCategory);
                return RedirectToAction(nameof(Index));
            }

            return Ok(assetCategory);
        }

        

    }
}
