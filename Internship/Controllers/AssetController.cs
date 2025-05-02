using Microsoft.AspNetCore.Mvc;
using Internship.DAL.Repositories;
using Internship.Models;
using Internship.DAL;
using Microsoft.AspNetCore.Authorization;

namespace Internship.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AssetController: Controller
    {
        private readonly AssetRepository _assetRepository;
        
        // Constructor to initialize the repository
        public AssetController()
        {
            _assetRepository = new AssetRepository();
        }

        // Action to display all asset details
        [HttpGet("")]
        [HttpGet("Index")]
        public IActionResult Index()
        {
            List<Asset> assets = _assetRepository.GetAllAssets();
            return Ok(assets);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return Ok();
        }

        // POST: Asset/Create
        [HttpPost("Create")]
        //[ValidateAntiForgeryToken] not required for API only for MVC
        public IActionResult Create(Asset asset)
        {
            if (ModelState.IsValid)
            {
                _assetRepository.CreateAsset(asset);
                return RedirectToAction(nameof(Index)); // Redirect to the list after creation
            }

            return Ok(asset); // Return to the form with validation errors if any
        }

        [HttpGet("Details/{AssetId}")]
        public IActionResult Details(int AssetId)
        {
            // Get the asset by AssetId from the repository
            Asset asset = _assetRepository.GetAssetById(AssetId);

            if (asset == null)
            {
                return NotFound($"No asset category found with CatID {AssetId}");
            }
            return Ok(asset);
        }

        // GET: Asset/Delete/5
        [HttpGet("Delete/{AssetId}")]
        public IActionResult Delete(int AssetId)
        {
            Asset asset = _assetRepository.GetAssetById(AssetId);

            if (asset == null)
            {
                return NotFound($"No asset found with AssetId {AssetId}");
            }

            return View(asset); // Shows confirmation page
        }

        [HttpDelete("Delete/{AssetId}")]
        public IActionResult DeleteConfirmed(int AssetId)
        {
            _assetRepository.DeleteAsset(AssetId);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Edit/{AssetId}")]
        public IActionResult Edit(int AssetId)
        {
            var asset = _assetRepository.GetAssetById(AssetId);
            if (asset == null)
            {
                return NotFound();
            }
            return View(asset);
        }

        [HttpPut("Edit/{AssetId}")]
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
