using Microsoft.AspNetCore.Mvc;
using Internship.DAL.Repositories;
using Internship.Models;

namespace Internship.Controllers
{
    public class AssetOutController : Controller
    {
        private readonly AssetOutRepository _assetOutRepository;

        // Constructor to initialize the repository
        public AssetOutController()
        {
            _assetOutRepository = new AssetOutRepository();
        }
        public IActionResult Index()
        {
            List<AssetOut> assetouts = _assetOutRepository.GetAllAssetOuts();
            return View(assetouts);
        }

        // Other actions like Create, Edit, Delete can be added here

        // GET: AssetOut/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AssetOut/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AssetOut assetOut)
        {
            if (ModelState.IsValid)
            {
                _assetOutRepository.CreateAssetOut(assetOut);
                return RedirectToAction(nameof(Index)); // Redirect to the list after creation
            }

            return View(assetOut); // Return to the form with validation errors if any
        }

        public IActionResult Details(int Sn)
        {
            // Get the asset by AssetId from the repository
            AssetOut assetOut = _assetOutRepository.GetAssetOutById(Sn);//line 53

            if (assetOut == null)
            {
                return NotFound($"No Asset Out Detail found with Sn {Sn}");
            }
            return View(assetOut);
        }

        // GET: AssetOut/Delete/5
        public IActionResult Delete(int Sn)
        {
            AssetOut assetOut = _assetOutRepository.GetAssetOutById(Sn);
            if (assetOut == null)
            {
                return NotFound($"No Asset Out found with Sn {Sn}");
            }
            return View(assetOut);
        }

        // POST: AssetOut/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int Sn)
        {
            _assetOutRepository.DeleteAssetOut(Sn);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int Sn)
        {
            var assetOut = _assetOutRepository.GetAssetOutById(Sn);
            if (assetOut == null)
            {
                return NotFound($"No asset out record found with SN {Sn}");
            }
            return View(assetOut); // Pass data to edit form
        }

        // POST: AssetOut/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AssetOut assetOut)
        {
            if (ModelState.IsValid)
            {
                _assetOutRepository.UpdateAssetOut(assetOut);
                return RedirectToAction(nameof(Index)); // After update
            }

            return View(assetOut); // Return form if validation fails
        }

    }
}
