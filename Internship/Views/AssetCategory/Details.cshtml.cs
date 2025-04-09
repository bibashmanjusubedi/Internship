using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Internship.Models;
using Internship.Services;


namespace Internship.Views.AssetCategory
{
    [Route("AssetCategory/Details")]
    public class DetailsModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        [BindProperty(SupportsGet = true)]
        public int CatID { get; set; }

        public Internship.Models.AssetCategory Category { get; set; }


        public DetailsModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult OnGet()
        {
            // Fetch the category by CatID from query string
            Category = _categoryService.GetAssetCategoryById(CatID);

            if (Category == null)
            {
                return NotFound(); // Handle if category not found
            }

            return Page();
        }
    }
}
