using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Internship.Models
{
    public class AssetOut
    {
        
        public int Sn { get; set; }
        [ForeignKey("AssetDetail")]
        public int AssetCode { get; set; }
        [ForeignKey("AssetDetail")]
        public DateOnly OutDate { get; set; }
        [ForeignKey("Person")]
        [Display(Name="Assigned To")]
        public int PId { get; set; }
        public DateOnly DateToReturn { get; set; }
        public DateOnly ReturnDate { get; set; }
        public string Remarks { get; set; }
    }
}
