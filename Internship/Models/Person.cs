using System.ComponentModel.DataAnnotations;

namespace Internship.Models
{
    public class Person
    {
        public int PId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Address { get; set; }
        [StringLength(15)]
        public string Phone { get; set; }
        public int LoginID { get; set; }
        [StringLength(100)]

        public bool LoginStatus { get; set; } = false;
        public string Password { get; set; }



        [StringLength(100)]
        public string Remarks { get; set; }
    }
}
