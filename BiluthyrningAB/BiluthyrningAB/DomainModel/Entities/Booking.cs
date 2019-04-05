using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BiluthyrningAB.Models
{
    public class Booking
    {
        public Guid Id { get; set; }

        [Display(Name = "Kund")]
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Display(Name = "Bil")]
        public Guid CarId { get; set; }
        public Car Car { get; set; }

        [Required]
        [Display(Name = "Bokningens Starttid")]
        public DateTime BookingTime { get; set; }

        [Display(Name = "Bokningens Sluttid")]
        public DateTime ReturnTime { get; set; }

        [Display(Name = "Antal körda kilometer")]
        public decimal NumberOfKm { get; set; }

        [Display(Name = "Pågående bokning?")]
        public bool IsActive { get; set; }


        public decimal NumberOfDays
        {
            get
            {
                return Convert.ToDecimal((ReturnTime - BookingTime).TotalDays);
            }
        }

        private readonly decimal baseDayRental = 500;
        private readonly decimal kmPrice = 15;

        public decimal Price
        {
            get
            {
                string[] carSizes = Enum.GetNames(typeof(CarSize));

                if (carSizes.Single(x => x == "Liten") == Car.CarSize.ToString())
                {
                    return (baseDayRental * NumberOfDays);
                }

                else if (carSizes.Single(x => x == "Van") == Car.CarSize.ToString())
                {
                    return (baseDayRental * NumberOfDays * 1.2m) + (kmPrice * NumberOfKm);
                }

                else if (carSizes.Single(x => x == "Minibuss") == Car.CarSize.ToString())
                {
                    return (baseDayRental * NumberOfDays * 1.7m) + (kmPrice * NumberOfKm * 1.5m);
                }

                throw new Exception("Den här bilstorleken existerar inte.");
            }
        }
    }
}
