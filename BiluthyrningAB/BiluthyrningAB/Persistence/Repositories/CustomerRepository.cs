using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiluthyrningAB.Data;
using BiluthyrningAB.Models;
using Microsoft.EntityFrameworkCore;

namespace BiluthyrningAB.Persistence.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddCustomer(Customer customer)
        {
            _context.Add(customer);
        }

        public bool CustomerExists(Guid id)
        {
            return _context.Customer.Any(e => e.Id == id);
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _context.Customer.ToList();
        }

        public Customer GetCustomerById(Guid? id)
        {
            return _context.Customer.FirstOrDefault(m => m.Id == id);
        }

        public void RemoveCustomer(Customer customer)
        {
            _context.Customer.Remove(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            _context.Update(customer);
        }
    }
}