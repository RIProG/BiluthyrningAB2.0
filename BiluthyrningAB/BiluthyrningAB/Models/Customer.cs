using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiluthyrningAB.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [RegularExpression(@"^[0-9]{6}-[0-9]{4}$")]
        [Display(Name = "Personnummer")]
        [Required]
        public string SocialSecurityNumber { get; set; }

        public List<Booking> Bookings { get; set; }
    }
}
