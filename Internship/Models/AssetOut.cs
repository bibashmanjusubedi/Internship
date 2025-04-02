using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Internship.Models
{
    public class AssetOut
    {
        public int Sn { get; set; }
        [ForeignKey("AssetDetail")]
        public int AssetCode { get; set; }
        public DateOnly OutDate { get; set; }
        public int PId { get; set; }
        public DateOnly DateToReturn { get; set; }
        public DateOnly ReturnDate { get; set; }
        [StringLength(255)]
        public string Remarks { get; set; }
    }
}
