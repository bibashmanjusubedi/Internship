using Microsoft.AspNetCore.Mvc;
using Internship.DAL.Repositories;
using Internship.Models;

namespace Internship.Controllers
{
    public class AssetController: Controller
    {
        private readonly AssetRepository _assetRepository;
        
        // Constructor to initialize the repository
        public AssetController()
        {
            _assetRepository = new AssetRepository();
        }

        // Action to display all asset details
        public IActionResult Index()
        {
            List<Asset> assets = _assetRepository.GetAllAssets();
            return View(assets);
        }

        // Other actions like Create, Edit, Delete can be added here
        // GET: Asset/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Asset/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Asset asset)
        {
            if (ModelState.IsValid)
            {
                _assetRepository.CreateAsset(asset);
                return RedirectToAction(nameof(Index)); // Redirect to the list after creation
            }

            return View(asset); // Return to the form with validation errors if any
        }


        public IActionResult Details(int AssetId)
        {
            // Get the asset by AssetId from the repository
            Asset asset = _assetRepository.GetAssetById(AssetId);

            if (asset == null)
            {
                return NotFound($"No asset category found with CatID {AssetId}");
            }
            return View(asset);
        }

    }
}
