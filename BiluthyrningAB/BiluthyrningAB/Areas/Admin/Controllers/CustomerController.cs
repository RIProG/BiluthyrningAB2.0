using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiluthyrningAB.Data;
using BiluthyrningAB.Models;
using BiluthyrningAB.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BiluthyrningAB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IEntityFrameworkRepository _entityFrameworkRepository;

        public CustomerController(ICustomerRepository customerRepository, IEntityFrameworkRepository entityFrameworkRepository)
        {
            _customerRepository = customerRepository;
            _entityFrameworkRepository = entityFrameworkRepository;
        }

        public async Task<IActionResult> Index()
        {
            var myTask = Task.Run(() => _customerRepository.GetAllCustomers());
            return View(await myTask);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SocialSecurityNumber,FirstName,LastName")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.Id = Guid.NewGuid();
                _customerRepository.AddCustomer(customer);

                _entityFrameworkRepository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        //GET - EDIT
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
                return NotFound();

            var customer = _customerRepository.GetCustomerById(id);

            if (customer == null)
                return NotFound();

            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,SocialSecurityNumber,FirstName,LastName")] Customer customer)
        {
            if (id != customer.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _customerRepository.UpdateCustomer(customer);

                    _entityFrameworkRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
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
            return View(customer);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
                return NotFound();

            var customer = _customerRepository.GetCustomerById(id);

            if (customer == null)
                return NotFound();

            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var customer = _customerRepository.GetCustomerById(id);
            _customerRepository.RemoveCustomer(customer);

            _entityFrameworkRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
                return NotFound();

            var customer = _customerRepository.GetCustomerById(id);

            if (customer == null)
                return NotFound();

            return View(customer);
        }

        private bool CustomerExists(Guid id)
        {
            return _customerRepository.CustomerExists(id);
        }
    }
}