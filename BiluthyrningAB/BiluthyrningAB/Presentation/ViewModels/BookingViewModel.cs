using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiluthyrningAB.Models.ViewModels
{
    public class BookingViewModel
    {
        public Booking Booking { get; set; }
        public IEnumerable<SelectListItem> Car { get; set; }
        public IEnumerable<SelectListItem> Customer { get; set; }
    }
}
