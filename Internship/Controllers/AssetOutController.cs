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

    }
}
