using Microsoft.AspNetCore.Mvc;
using Internship.DAL.Repositories;
using Internship.Models;
using Internship.DAL;

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

        // GET: Asset/Delete/5
        public IActionResult Delete(int AssetId)
        {
            Asset asset = _assetRepository.GetAssetById(AssetId);

            if (asset == null)
            {
                return NotFound($"No asset found with AssetId {AssetId}");
            }

            return View(asset); // Shows confirmation page
        }

        // POST: Asset/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int AssetId)
        {
            _assetRepository.DeleteAsset(AssetId);
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Edit(int AssetId)
        {
            var asset = _assetRepository.GetAssetById(AssetId);
            if (asset == null)
            {
                return NotFound();
            }
            return View(asset);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Asset asset)
        {
            if (ModelState.IsValid)
            {
                _assetRepository.UpdateAsset(asset);
                return RedirectToAction(nameof(Index));
            }
            return View(asset);
        }



    }
}
