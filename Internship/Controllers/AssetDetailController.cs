using Microsoft.AspNetCore.Mvc;
using Internship.DAL.Repositories;
using Internship.Models;
using Microsoft.AspNetCore.Authorization;

namespace Internship.Controllers
{

    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AssetDetailController : Controller
    {
        private readonly AssetDetailRepository _assetDetailRepository;

        // Constructor to initialize the repository
        public AssetDetailController()
        {
            _assetDetailRepository = new AssetDetailRepository();  // You could also use Dependency Injection for better testing and maintainability
        }

        // Action to display all asset details
        [HttpGet("")]
        [HttpGet("Index")]
        public IActionResult Index()
        {
            // Get all asset details from the repository
            List<AssetDetail> assetDetails = _assetDetailRepository.GetAllAssetDetails();

            // Pass the asset details to the view
            return Ok(assetDetails);
        }

        // Other actions like Create, Edit, Delete can be added here

        // GET: AssetDetail/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return Ok();
        }

        // POST: AssetDetail/Create
        [HttpPost("Create")]
        // [ValidateAntiForgeryToken] only for MVC
        public IActionResult Create(AssetDetail assetDetail)
        {
            if (ModelState.IsValid)
            {
                _assetDetailRepository.CreateAssetDetail(assetDetail);
                return RedirectToAction(nameof(Index)); // Redirect to the list after creation
            }

            return Ok(assetDetail); // Return to the form with validation errors if any
        }

        //Get : AssetDetails/Details
        [HttpGet("Details/{Sn}")]
        public IActionResult Details(int Sn)
        {
            // Get the asset by AssetId from the repository
            AssetDetail assetDetail = _assetDetailRepository.GetAssetDetailById(Sn);//line 53

            if (assetDetail == null)
            {
                return NotFound($"No Asset Detail found with Sn {Sn}");
            }
            return Ok(assetDetail);
        }

        // GET: AssetDetail/Delete/5
        [HttpGet("Delete/{Sn}")]
        public IActionResult Delete(int Sn)
        {
            AssetDetail assetDetail = _assetDetailRepository.GetAssetDetailById(Sn);

            if (assetDetail == null)
            {
                return NotFound($"No Asset Detail found with Sn {Sn}");
            }

            return View(assetDetail);
        }

        // POST: AssetDetail/Delete/5
        //[HttpPost, ActionName("Delete")]
        [HttpDelete("Delete/{Sn}")]
        public IActionResult DeleteConfirmed(int Sn)
        {
            _assetDetailRepository.DeleteAssetDetail(Sn);
            return RedirectToAction(nameof(Index));
        }

        // GET: AssetDetail/Edit/5
        [HttpGet("Edit/{Sn}")]
        public IActionResult Edit(int Sn)
        {
            AssetDetail assetDetail = _assetDetailRepository.GetAssetDetailById(Sn);
            if (assetDetail == null)
            {
                return NotFound($"No Asset Detail found with Sn {Sn}");
            }

            return View(assetDetail);
        }

        // POST: AssetDetail/Edit/5
        [HttpPut["Edit/{Sn}"]
        public IActionResult Edit(int Sn, AssetDetail assetDetail)
        {
            if (Sn != assetDetail.Sn)
            {
                return BadRequest("Asset identifier mismatch.");
            }

            if (ModelState.IsValid)
            {
                _assetDetailRepository.UpdateAssetDetail(assetDetail); // This is the PUT-style update you added earlier
                return RedirectToAction(nameof(Index));
            }

            return View(assetDetail); // Return form with validation errors
        }




    }
}
