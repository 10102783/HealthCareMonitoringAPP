namespace HealthCareMonitoringAPP.Models
{
    public class MedicalHistory
    {
        public int MedicalHistoryId { get; set; }
        public int CustomerId { get; set; } // Foreign key to the customer (patient)
        public string Diagnosis { get; set; } // Diagnosis description
        public string Treatment { get; set; } // Treatment prescribed
        public DateTime DateOfDiagnosis { get; set; } // Date of diagnosis
        public string Notes { get; set; } // Additional notes about the condition

        // Navigation property
        public virtual Customer Customer { get; set; } // Link to the Customer model
    }
}