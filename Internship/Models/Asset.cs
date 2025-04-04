using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Internship.Models
{
    public class Asset
    {
        [Key]
        public int AssetId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string ShortName {  get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [StringLength(50)]
        public string Unit { get; set; }

        [ForeignKey("AssetCategory")]
        public int CatID { get; set; }
    }
}
