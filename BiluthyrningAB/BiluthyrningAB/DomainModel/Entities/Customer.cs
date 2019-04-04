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
        //[Key]
        public Guid Id { get; set; }

        [RegularExpression(@"^[0-9]{6}-[0-9]{4}$")]
        [Display(Name = "Personnummer")]
        [Required]
        public string SocialSecurityNumber { get; set; }

        [RegularExpression(@"^[A-ZÅÄÖ]{1}[a-zåäö]+$", ErrorMessage = "Fel format på förnamn.")]
        [Display(Name = "Förnamn")]
        [Required(ErrorMessage = "Du måste ange ett förnamn")]
        public string FirstName { get; set; }

        [RegularExpression(@"^[A-ZÅÄÖ]{1}[a-zåäö]+$", ErrorMessage = "Fel format på efternamn.")]
        [Display(Name = "Efternamn")]
        [Required(ErrorMessage = "Du måste ange ett efternamn")]
        public string LastName { get; set; }

        [Display(Name = "Bokningar")]
        public List<Booking> Bookings { get; set; }
    }
}
