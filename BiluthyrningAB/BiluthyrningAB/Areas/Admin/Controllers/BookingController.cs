using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiluthyrningAB.Data;
using BiluthyrningAB.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BiluthyrningAB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BookingController(ApplicationDbContext db)
        {
            _db = db;
        }

        //GET
        public async Task<IActionResult> Index()
        {

            var bookings = await _db.Booking.Include(m => m.Customer).Include(m => m.Car).ToListAsync();

            return View(bookings);
        }

        //GET - CREATE
        public IActionResult Create()
        {

            BookingViewModel bookingVM = new BookingViewModel();

            bookingVM.Car = _db.Car.Select(c => new SelectListItem()
            { Text = c.RegNr, Value = c.Id.ToString() });
            bookingVM.Customer = _db.Customer.Select(c => new SelectListItem()
            { Text = c.FirstName + " " + c.LastName, Value = c.Id.ToString() });
            

            return View(bookingVM);
        }

    }
}