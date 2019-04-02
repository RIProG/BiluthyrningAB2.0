using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiluthyrningAB.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Bilstorlek")]
        [Required]
        public CarSize CarSize { get; set; }

        [RegularExpression(@"^[A-Z]{3}[0-9]{3}$", ErrorMessage = "Ange ett korrekt registreringsnummer (ex. ABC123)")]
        [Display(Name = "Registreringsnummer")]
        [Required]
        public string RegNr { get; set; }

        [Display(Name = "Körda kilometer")]
        [RegularExpression(@"^?\d+(\,\d+)?$", ErrorMessage = "Ange ett korrekt format på kilometer (ex. 32,3)")]
        public decimal DistanceInKm { get; set; }

        [Display(Name ="Bokningar för denna bil")]
        public List<Booking> Bookings { get; set; }

        [Display(Name ="Bil tillgänglig")]
        public bool Available { get; set; } = true;
    }

    public enum CarSize {
        Liten = 0,
        Van = 1,
        Minibuss = 2
    }
}
