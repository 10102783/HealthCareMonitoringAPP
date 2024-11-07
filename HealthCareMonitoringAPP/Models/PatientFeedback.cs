namespace HealthCareMonitoringAPP.Models
{
    public class PatientFeedback
    {
        public int PatientFeedbackId { get; set; } // Primary key

        public int CustomerId { get; set; } // Foreign key to Customer (Patient)
        public string Feedback { get; set; } // Patient's feedback
        public int Rating { get; set; } // Rating (1-5 scale)
        public DateTime DateSubmitted { get; set; } // Date of feedback submission
        public string Notes { get; set; } // Optional additional notes

        // Navigation property to Customer (Patient)
        public Customer Customer { get; set; }
    }
}
