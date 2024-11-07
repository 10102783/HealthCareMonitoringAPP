using HealthCareMonitoringAPP.Data;
using HealthCareMonitoringAPP.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace HealthCareMonitoringAPP.Controllers
{
    public class MedicalHistoryController : Controller
    {
        private readonly HealthCareDBContext _context;

        public MedicalHistoryController(HealthCareDBContext context)
        {
            _context = context;
        }

        // GET: MedicalHistory
        public IActionResult Index()
        {
            // Fetching all medical histories including the associated customer data
            var medicalHistories = _context.MedicalHistories.Include(m => m.Customer).ToList();
            return View(medicalHistories);
        }

        // GET: MedicalHistory/Details/5
        public IActionResult Details(int id)
        {
            var medicalHistory = _context.MedicalHistories
                .Include(m => m.Customer)
                .FirstOrDefault(m => m.MedicalHistoryId == id);

            if (medicalHistory == null)
            {
                return NotFound(); // Return 404 if medical history is not found
            }

            return View(medicalHistory);
        }

        // GET: MedicalHistory/Create
        public IActionResult Create()
        {
            // Use ViewBag to pass the customers list for dropdown
            ViewBag.Customers = new SelectList(_context.Customers, "CustomerId", "Name");
            return View();
        }

        // POST: MedicalHistory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("MedicalHistoryId,CustomerId,Diagnosis,Treatment,DateOfDiagnosis,Notes")] MedicalHistory medicalHistory)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(medicalHistory); // Add new record to context
                _context.SaveChanges(); // Save changes to the database
                return RedirectToAction(nameof(Index)); // Redirect to the list view after creation
            }

            // Pass the customers list again in case of validation failure
            ViewBag.Customers = new SelectList(_context.Customers, "CustomerId", "Name", medicalHistory.CustomerId);
            return View(medicalHistory);
        }

        // GET: MedicalHistory/Edit/5
        public IActionResult Edit(int id)
        {
            var medicalHistory = _context.MedicalHistories.FirstOrDefault(m => m.MedicalHistoryId == id);

            if (medicalHistory == null)
            {
                return NotFound(); // Return 404 if medical history not found
            }

            // Use ViewBag to pass the customers list
            ViewBag.Customers = new SelectList(_context.Customers, "CustomerId", "Name", medicalHistory.CustomerId);
            return View(medicalHistory);
        }

        // POST: MedicalHistory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("MedicalHistoryId,CustomerId,Diagnosis,Treatment,DateOfDiagnosis,Notes")] MedicalHistory medicalHistory)
        {
            if (id != medicalHistory.MedicalHistoryId)
            {
                return NotFound(); // Ensure correct medical history is being updated
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicalHistory); // Update existing record
                    _context.SaveChanges(); // Save changes to the database
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.MedicalHistories.Any(m => m.MedicalHistoryId == medicalHistory.MedicalHistoryId))
                    {
                        return NotFound(); // Return 404 if the medical history record no longer exists
                    }
                    else
                    {
                        throw; // Rethrow exception if there's a concurrency error
                    }
                }
                return RedirectToAction(nameof(Index)); // Redirect to list view after update
            }

            // Pass the customers list again if validation fails
            ViewBag.Customers = new SelectList(_context.Customers, "CustomerId", "Name", medicalHistory.CustomerId);
            return View(medicalHistory);
        }

        // GET: MedicalHistory/Delete/5
        public IActionResult Delete(int id)
        {
            var medicalHistory = _context.MedicalHistories
                .Include(m => m.Customer)
                .FirstOrDefault(m => m.MedicalHistoryId == id);

            if (medicalHistory == null)
            {
                return NotFound(); // Return 404 if medical history not found
            }

            return View(medicalHistory);
        }

        // POST: MedicalHistory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var medicalHistory = _context.MedicalHistories.FirstOrDefault(m => m.MedicalHistoryId == id);
            if (medicalHistory == null)
            {
                return NotFound(); // Return 404 if the record is not found
            }

            _context.MedicalHistories.Remove(medicalHistory); // Remove the record
            _context.SaveChanges(); // Save changes to the database
            return RedirectToAction(nameof(Index)); // Redirect to list view after deletion
        }
    }
}

