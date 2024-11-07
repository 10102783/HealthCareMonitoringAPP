using Microsoft.AspNetCore.Mvc;
using HealthCareMonitoringAPP.Data;
using HealthCareMonitoringAPP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;

namespace HealthCareMonitoringAPP.Controllers
{
    public class PatientFeedbackController : Controller
    {
        private readonly HealthCareDBContext _context;

        public PatientFeedbackController(HealthCareDBContext context)
        {
            _context = context;
        }

        // GET: PatientFeedback
        public IActionResult Index()
        {
            var feedbacks = _context.PatientFeedbacks.Include(f => f.Customer).ToList();
            return View(feedbacks);
        }

        // GET: PatientFeedback/Details/5
        public IActionResult Details(int id)
        {
            var feedback = _context.PatientFeedbacks
                .Include(f => f.Customer)
                .FirstOrDefault(f => f.PatientFeedbackId == id);

            if (feedback == null)
            {
                return NotFound();
            }

            return View(feedback);
        }

        // GET: PatientFeedback/Create
        public IActionResult Create()
        {
            ViewBag.Customers = new SelectList(_context.Customers, "CustomerId", "Name");
            return View();
        }

        // POST: PatientFeedback/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("PatientFeedbackId,CustomerId,Feedback,Rating,DateSubmitted,Notes")] PatientFeedback feedback)
        {
            if (ModelState.IsValid)
            {
                feedback.DateSubmitted = DateTime.Now; // Automatically set the current date/time
                _context.Add(feedback);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            // If validation fails, pass customers list again and return to the Create view
            ViewBag.Customers = new SelectList(_context.Customers, "CustomerId", "Name", feedback.CustomerId);
            return View(feedback);
        }

        // GET: PatientFeedback/Delete/5
        public IActionResult Delete(int id)
        {
            var feedback = _context.PatientFeedbacks
                .Include(f => f.Customer)
                .FirstOrDefault(f => f.PatientFeedbackId == id);

            if (feedback == null)
            {
                return NotFound();
            }

            return View(feedback);
        }

        // POST: PatientFeedback/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var feedback = _context.PatientFeedbacks.FirstOrDefault(f => f.PatientFeedbackId == id);
            if (feedback == null)
            {
                return NotFound();
            }

            _context.PatientFeedbacks.Remove(feedback);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
