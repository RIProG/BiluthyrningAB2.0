using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiluthyrningAB.Data;
using BiluthyrningAB.Models;
using BiluthyrningAB.Models.ViewModels;
using BiluthyrningAB.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BiluthyrningAB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookingController : Controller
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ICarRepository _carRepository;
        private readonly ICustomerRepository _customerRepository;



        public BookingController(IBookingRepository bookingRepository, ICarRepository carRepository, ICustomerRepository customerRepository)
        {
            _bookingRepository = bookingRepository;
            _carRepository = carRepository;
            _customerRepository = customerRepository;
        }

        public async Task<IActionResult> Index()
        {
            var myTask = Task.Run(() => _bookingRepository.GetAllBookings());
            return View(await myTask);
        }
        public async Task<IActionResult> CustomerBookings(Guid? Id)
        {
            var myTask = Task.Run(() => _bookingRepository.GetCustomerBookings(Id));
            return View(await myTask);
        }


        public async Task<IActionResult> ActiveBookings()
        {
            var myTask = Task.Run(() => _bookingRepository.GetBookingsDependingOnStatus(true));
            return View(await myTask);
        }


        public async Task<IActionResult> FinishedBookings()
        {
            var myTask = Task.Run(() => _bookingRepository.GetBookingsDependingOnStatus(false));
            return View(await myTask);
        }

        //GET - CREATE
        public IActionResult Create()
        {

            BookingViewModel bookingVM = new BookingViewModel();

            bookingVM.Car = FillCarListOfSelectListItems();

            bookingVM.Customer = FillCustomerListOfSelectListItems();

            return View(bookingVM);
        }

        public List<SelectListItem> FillCustomerListOfSelectListItems()
        {
            var customers = _customerRepository.GetAllCustomers();

            List<SelectListItem> listOfCustomers = new List<SelectListItem>();

            foreach (var customer in customers)
            {
                string wholeName = $"{customer.FirstName} {customer.LastName}";
                var x = new SelectListItem() { Text = wholeName, Value = customer.Id.ToString() };
                listOfCustomers.Add(x);
            }
            return listOfCustomers;
        }


        public List<SelectListItem> FillCarListOfSelectListItems()
        {
            var cars = _carRepository.GetAllCars();

            List<SelectListItem> listOfCars = new List<SelectListItem>();

            foreach (var car in cars)
            {
                var y = new SelectListItem() { Text = car.RegNr, Value = car.Id.ToString(), Group = new SelectListGroup { Name = car.CarSize.ToString() } };
                listOfCars.Add(y);
            }

            return listOfCars;
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePOST([Bind("Id,CustomerId,CarId,StartDate")] Booking booking)
        {
            booking.BookingTime = booking.BookingTime.AddDays(1);
            booking.IsActive = true;

            booking.Car = _carRepository.GetCarById(booking.CarId);

            if (booking.Car.Available == true)
            {
                booking.Car.Available = false;
            }
            else
            {
                ViewBag.Message = "Bilen är tyvärr redan bokad.";
                BookingViewModel error_bookingVm = new BookingViewModel();
                error_bookingVm.Car = FillCarListOfSelectListItems();
                error_bookingVm.Customer = FillCustomerListOfSelectListItems();
                return View(error_bookingVm);
            }

            if (ModelState.IsValid)
            {
                booking.Id = Guid.NewGuid();
                _bookingRepository.AddBooking(booking);

                _carRepository.UpdateCar(booking.Car);

                return RedirectToAction(nameof(Index));
            }

            BookingViewModel bookingVM = new BookingViewModel();

            bookingVM.Car = FillCarListOfSelectListItems();

            bookingVM.Customer = FillCustomerListOfSelectListItems();

            return View(bookingVM);
        }


        public async Task<IActionResult> FinishBooking(Guid? id)
        {
            if (id == null)
                return NotFound();

            var booking = _bookingRepository.GetBookingById(id);

            if (booking == null)
                return NotFound();

            return View(booking);
        }

    }
}