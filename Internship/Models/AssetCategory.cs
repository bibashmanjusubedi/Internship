using System.ComponentModel.DataAnnotations;

namespace Internship.Models
{
    public class AssetCategory
    {
        public int CatId { get; set; }

        [StringLength(100)]
        public string CatName { get; set; }
    }
}
