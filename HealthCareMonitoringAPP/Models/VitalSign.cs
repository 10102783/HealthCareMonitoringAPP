namespace HealthCareMonitoringAPP.Models
{
    public class VitalSign
    {
        public int VitalSignId { get; set; }  // Primary Key
        public int CustomerId { get; set; }   // Foreign Key to Customer
        public double Temperature { get; set; }
        public double BloodPressureSystolic { get; set; }
        public double BloodPressureDiastolic { get; set; }
        public double HeartRate { get; set; }
        public DateTime RecordedAt { get; set; } // Date and time of recording

        // Navigation property
        public Customer Customer { get; set; }
    }
}
