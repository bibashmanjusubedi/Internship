using Microsoft.AspNetCore.Mvc;
using Internship.DAL.Repositories;
using Internship.Models;
using Microsoft.AspNetCore.Authorization;

namespace Internship.Controllers
{

    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AssetOutController : ControllerBase
    {
        private readonly AssetOutRepository _assetOutRepository;

        // Constructor to initialize the repository
        public AssetOutController()
        {
            _assetOutRepository = new AssetOutRepository();
        }
        
        [HttpGet("")]
        [HttpGet("Index")]
        public IActionResult Index()
        {
            List<AssetOut> assetouts = _assetOutRepository.GetAllAssetOuts();
            return Ok(assetouts);
        }

        // Other actions like Create, Edit, Delete can be added here

        // GET: AssetOut/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return Ok();
        }

        // POST: AssetOut/Create
        [HttpPost("Create")]
        public IActionResult Create(AssetOut assetOut)
        {
            if (ModelState.IsValid)
            {
                _assetOutRepository.CreateAssetOut(assetOut);
                return RedirectToAction(nameof(Index)); // Redirect to the list after creation
            }

            return Ok(assetOut); // Return to the form with validation errors if any
        }

        [HttpGet("Details/{Sn}")]
        public IActionResult Details(int Sn)
        {
            // Get the asset by AssetId from the repository
            AssetOut assetOut = _assetOutRepository.GetAssetOutById(Sn);//line 53

            if (assetOut == null)
            {
                return NotFound($"No Asset Out Detail found with Sn {Sn}");
            }
            return Ok(assetOut);
        }

        // GET: AssetOut/Delete/5
        [HttpGet("Delete/{Sn}")]
        public IActionResult Delete(int Sn)
        {
            AssetOut assetOut = _assetOutRepository.GetAssetOutById(Sn);
            if (assetOut == null)
            {
                return NotFound($"No Asset Out found with Sn {Sn}");
            }
            return Ok(assetOut);
        }

        // POST: AssetOut/Delete/5
        [HttpDelete("Delete/{Sn}")]
        public IActionResult DeleteConfirmed(int Sn)
        {
            _assetOutRepository.DeleteAssetOut(Sn);
            return RedirectToAction(nameof(Index));
        }
        
        [HttpGet("Edit/{Sn}")]
        public IActionResult Edit(int Sn)
        {
            var assetOut = _assetOutRepository.GetAssetOutById(Sn);
            if (assetOut == null)
            {
                return NotFound($"No asset out record found with SN {Sn}");
            }
            return Ok(assetOut); // Pass data to edit form
        }

        // POST: AssetOut/Edit/5
        [HttpPut("Edit/{Sn}")]
        public IActionResult Edit(AssetOut assetOut)
        {
            if (ModelState.IsValid)
            {
                _assetOutRepository.UpdateAssetOut(assetOut);
                return Ok(assetOut);
            }

            return Ok(assetOut); // Return form if validation fails
        }

    }
}
