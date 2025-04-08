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
    }
}
