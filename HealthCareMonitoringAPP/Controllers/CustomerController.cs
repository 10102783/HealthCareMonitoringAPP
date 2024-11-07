using HealthCareMonitoringAPP.Data;  // Updated namespace for new app name
using HealthCareMonitoringAPP.Models;  // Updated namespace for new app name
using Microsoft.AspNetCore.Mvc;

namespace HealthCareMonitoringAPP.Controllers  // Updated namespace for new app name
{
    public class CustomerController : Controller
    {
        private readonly HealthCareDBContext _context;  // Use HealthCareDBContext

        public CustomerController(HealthCareDBContext context)
        {
            _context = context;  // Updated DbContext type
        }

        // GET: Customer/Details/5
        public IActionResult Details(int id)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // GET: Customer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();
                return RedirectToAction(nameof(Details), new { id = customer.CustomerId });
            }
            return View(customer);
        }

        // GET: Customer/Edit/5
        public IActionResult Edit(int id)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(customer);
                _context.SaveChanges();
                return RedirectToAction(nameof(Details), new { id = customer.CustomerId });
            }
            return View(customer);
        }

        // GET: Customer/Delete/5
        public IActionResult Delete(int id)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index)); // You can redirect to the list of customers or home page
        }

        // Optionally, you can create an Index method to list all customers
        public IActionResult Index()
        {
            return View(_context.Customers.ToList());
        }
    }
}
