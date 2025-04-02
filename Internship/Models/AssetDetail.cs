using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Internship.Models
{
    public class AssetDetail
    {
        public int Sn { get; set; }
        [ForeignKey("Asset")]
        public int AssetId { get; set; }
        [Required]
        public int AssetCode { get; set; }
        public int Price { get; set; }
        public DateOnly PurchaseDate { get; set; }

        [StringLength(255)]
        public string Remark { get; set}
        [StringLength(255)]
        public string Status { get; set; }

    }
}
