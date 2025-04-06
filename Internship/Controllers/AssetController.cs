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


    }
}
