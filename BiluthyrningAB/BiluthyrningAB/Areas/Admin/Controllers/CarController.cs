using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiluthyrningAB.Data;
using BiluthyrningAB.Models;
using BiluthyrningAB.Persistence.Repositories;
using BiluthyrningAB.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BiluthyrningAB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CarController : Controller
    {
        private readonly ICarRepository _carRepository;

        public CarController(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<IActionResult> Index()
        {
            var myTask = Task.Run(() => _carRepository.GetAllCars());
            return View(await myTask);
        }

        public async Task<IActionResult> CarsAvailable()
        {
            var myTask = Task.Run(() => _carRepository.GetCarsDependingOnBookingStatus(true));
            return View(await myTask);
        }


        public async Task<IActionResult> CarsBooked()
        {
            var myTask = Task.Run(() => _carRepository.GetCarsDependingOnBookingStatus(false));
            return View(await myTask);
        }


        public IActionResult Create()
        {
            return View();
        }

        private List<SelectListItem> GetCarSizesToSelectList()
        {
            string[] arr = Enum.GetNames(typeof(CarSize));
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (var item in arr)
            {
                var y = new SelectListItem() { Text = item, Value = item };
                list.Add(y);
            }

            return list;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RegNr,CarSize,DistanceInKm")] Car car)
        {
            if (ModelState.IsValid)
            {
                car.Id = Guid.NewGuid();
                _carRepository.AddCar(car);

                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        //GET - EDIT
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
                return NotFound();

            var car = _carRepository.GetCarById(id);

            if (car == null)
                return NotFound();

            CarViewModel carSizeVM = new CarViewModel()
            {
            CarSize = GetCarSizesToSelectList(),
            Car = car
            };


            return View(carSizeVM);
        }

        //POST - EDIT
        [HttpPost,ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,RegNr,CarSize,DistanceInKm")] Car car)
        {
            if (id != car.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _carRepository.UpdateCar(car);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            CarViewModel carSizeVM = new CarViewModel();

            carSizeVM.CarSize = GetCarSizesToSelectList();
            carSizeVM.Car = car;

            return View(carSizeVM);

        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
                return NotFound();

            var car = _carRepository.GetCarById(id);

            if (car == null)
                return NotFound();

            return View(car);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var car = _carRepository.GetCarById(id);
            _carRepository.RemoveCar(car);
            return RedirectToAction(nameof(Index));
        }


        private bool CarExists(Guid id)
        {
            return _carRepository.CarExists(id);
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
                return NotFound();

            var car = _carRepository.GetCarById(id);

            if (car == null)
                return NotFound();

            return View(car);
        }
    }
}