using Microsoft.AspNetCore.Mvc;
using HealthCareMonitoringAPP.Models;
using HealthCareMonitoringAPP.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace HealthCareMonitoringAPP.Controllers
{
    public class VitalSignsController : Controller
    {
        private readonly HealthCareDBContext _context;

        public VitalSignsController(HealthCareDBContext context)
        {
            _context = context;
        }

        // GET: VitalSigns
        public IActionResult Index()
        {
            // Ensure customers are included for display and any related data is loaded
            var vitalSigns = _context.VitalSigns.Include(v => v.Customer).ToList(); // Eager loading to avoid lazy loading issues
            return View(vitalSigns);
        }

        // GET: VitalSigns/Details/5
        public IActionResult Details(int id)
        {
            var vitalSign = _context.VitalSigns.Include(v => v.Customer)
                                              .FirstOrDefault(v => v.VitalSignId == id);
            if (vitalSign == null)
            {
                return NotFound();
            }
            return View(vitalSign);
        }

        // GET: VitalSigns/Create
        public IActionResult Create()
        {
            // Populate ViewBag with customers for dropdown list
            ViewBag.Customers = new SelectList(_context.Customers, "CustomerId", "Name");
            return View();
        }

        // POST: VitalSigns/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("VitalSignId,CustomerId,Temperature,BloodPressureSystolic,BloodPressureDiastolic,HeartRate,RecordedAt")] VitalSign vitalSign)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vitalSign);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            // Re-populate ViewBag if validation fails
            ViewBag.Customers = new SelectList(_context.Customers, "CustomerId", "Name", vitalSign.CustomerId);
            return View(vitalSign);
        }

        // GET: VitalSigns/Edit/5
        public IActionResult Edit(int id)
        {
            var vitalSign = _context.VitalSigns.Include(v => v.Customer)
                                              .FirstOrDefault(v => v.VitalSignId == id);
            if (vitalSign == null)
            {
                return NotFound();
            }

            // Populate ViewBag for the dropdown list
            ViewBag.Customers = new SelectList(_context.Customers, "CustomerId", "Name", vitalSign.CustomerId);
            return View(vitalSign);
        }

        // POST: VitalSigns/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("VitalSignId,CustomerId,Temperature,BloodPressureSystolic,BloodPressureDiastolic,HeartRate,RecordedAt")] VitalSign vitalSign)
        {
            if (id != vitalSign.VitalSignId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vitalSign);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.VitalSigns.Any(v => v.VitalSignId == vitalSign.VitalSignId))
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

            // Re-populate ViewBag if validation fails
            ViewBag.Customers = new SelectList(_context.Customers, "CustomerId", "Name", vitalSign.CustomerId);
            return View(vitalSign);
        }

        // GET: VitalSigns/Delete/5
        public IActionResult Delete(int id)
        {
            var vitalSign = _context.VitalSigns.Include(v => v.Customer)
                                              .FirstOrDefault(v => v.VitalSignId == id);
            if (vitalSign == null)
            {
                return NotFound();
            }
            return View(vitalSign);
        }

        // POST: VitalSigns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var vitalSign = _context.VitalSigns.FirstOrDefault(v => v.VitalSignId == id);
            if (vitalSign != null)
            {
                _context.VitalSigns.Remove(vitalSign);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
