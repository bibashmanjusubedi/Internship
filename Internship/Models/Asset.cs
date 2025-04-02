using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Internship.Models
{
    public class Asset
    {
        public int AssetId { get; set; }
        [StringLength[100]
        public string Name { get; set; }

        [Required]
        public string ShortName {  get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [StringLength(50)]
        public string Unit { get; set; }

        [ForeignKey("AssetCategory")]
        public int CatID { get; set; }
    }
}
