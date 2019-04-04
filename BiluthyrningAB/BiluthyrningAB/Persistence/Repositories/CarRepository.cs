using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiluthyrningAB.Data;
using BiluthyrningAB.Models;
using Microsoft.AspNetCore.Mvc;

namespace BiluthyrningAB.Persistence.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly AppDbContext _context;

        public CarRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddCar(Car car)
        {
            _context.Add(car);
            _context.SaveChangesAsync();
        }

        public void UpdateCar(Car car)
        {
            _context.Update(car);
        }

        public void RemoveCar(Car car)
        {
            _context.Car.Remove(car);
        }

        public IEnumerable<Car> GetAllCars()
        {
            return _context.Car.ToList();
        }

        public IEnumerable<Car> GetCarsDependingOnBookingStatus(bool status)
        {
            return _context.Car.Where(x => x.Available == status).ToList();
        }

        public Car GetCarById(Guid? id)
        {
            return _context.Car.Single(x => x.Id == id);
        }

        public bool CarExists(Guid id)
        {
            return _context.Car.Any(e => e.Id == id);
        }
    }
}