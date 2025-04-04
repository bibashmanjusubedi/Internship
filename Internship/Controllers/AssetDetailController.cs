using Microsoft.AspNetCore.Mvc;
using Internship.DAL.Repositories;
using Internship.Models;

namespace Internship.Controllers
{
    public class AssetDetailController : Controller
    {
        private readonly AssetDetailRepository _assetDetailRepository;

        // Constructor to initialize the repository
        public AssetDetailController()
        {
            _assetDetailRepository = new AssetDetailRepository();  // You could also use Dependency Injection for better testing and maintainability
        }

        // Action to display all asset details
        public IActionResult Index()
        {
            // Get all asset details from the repository
            List<AssetDetail> assetDetails = _assetDetailRepository.GetAllAssetDetails();

            // Pass the asset details to the view
            return View(assetDetails);
        }

        // Other actions like Create, Edit, Delete can be added here

        // GET: AssetDetail/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AssetDetail/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AssetDetail assetDetail)
        {
            if (ModelState.IsValid)
            {
                _assetDetailRepository.CreateAssetDetail(assetDetail);
                return RedirectToAction(nameof(Index)); // Redirect to the list after creation
            }

            return View(assetDetail); // Return to the form with validation errors if any
        }

    }
}
