using System.ComponentModel.DataAnnotations;

namespace Internship.Models
{
    public class AssetOut
    {
        public int Sn { get; set; }
        public int AssetCode { get; set; }
        public DateOnly Date { get; set; }
        public int PId { get; set; }
        public DateOnly DateToReturn { get; set; }
        public DateOnly Date { get; set; }
        [StringLength(255)]
        public string Remarks { get; set; }
    }
}
