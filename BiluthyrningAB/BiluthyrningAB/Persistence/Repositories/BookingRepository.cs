using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiluthyrningAB.Data;
using BiluthyrningAB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BiluthyrningAB.Persistence.Repositories
{

    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _context;

        public BookingRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Booking> GetAllBookings()
        {
            return _context.Booking.Include(x => x.Car).ToList();
        }

        public IEnumerable<Booking> GetCustomerBookings(Guid? Id)
        {
            return _context.Booking.Include(x => x.Car).Include(x => x.Customer).Where(x => x.Customer.Id == Id).ToList();
        }

        public IEnumerable<Booking> GetBookingsDependingOnStatus(bool status)
        {
            return _context.Booking.Include(x => x.Car).Where(x => x.IsActive == status).ToList();
        }

        public Booking GetBookingById(Guid? id)
        {
            return _context.Booking.Include(x => x.Customer).Include(x => x.Car)
                .FirstOrDefault(m => m.Id == id);
        }

        public bool BookingExists(Guid id)
        {
            return _context.Booking.Any(e => e.Id == id);
        }

        public void AddBooking(Booking booking)
        {
            _context.Add(booking);
            _context.SaveChangesAsync();
        }

        public void UpdateBooking(Booking booking)
        {
            _context.Update(booking);
            _context.SaveChangesAsync();
        }

        public void RemoveBooking(Booking booking)
        {
            _context.Booking.Remove(booking);
            _context.SaveChangesAsync();
        }

    }
}
