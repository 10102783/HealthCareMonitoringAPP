using Microsoft.EntityFrameworkCore;
using HealthCareMonitoringAPP.Models;

namespace HealthCareMonitoringAPP.Data
{
    public class HealthCareDBContext : DbContext
    {
        public HealthCareDBContext(DbContextOptions<HealthCareDBContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        // Add the VitalSigns DbSet here
        public DbSet<VitalSign> VitalSigns { get; set; }

        // Add the MedicalHistory DbSet here
        public DbSet<MedicalHistory> MedicalHistories { get; set; }

        // Add the PatientFeedback DbSet here for storing feedback surveys
        public DbSet<PatientFeedback> PatientFeedbacks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Customer entity configurations
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(c => c.PhoneNumber)
                      .HasMaxLength(15);

                entity.Property(c => c.DateOfBirth)
                      .HasColumnType("date");

                entity.Property(c => c.Gender)
                      .HasMaxLength(10);

                entity.Property(c => c.EmergencyContactName)
                      .HasMaxLength(100);

                entity.Property(c => c.EmergencyContactPhone)
                      .HasMaxLength(15);

                entity.Property(c => c.EmergencyContactRelation)
                      .HasMaxLength(50);

                entity.Property(c => c.InsuranceProvider)
                      .HasMaxLength(100);

                entity.Property(c => c.InsurancePolicyNumber)
                      .HasMaxLength(50);
            });

            // Product entity configurations
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(p => p.Price)
                      .HasColumnType("decimal(18,2)");

                entity.Property(p => p.Category)
                      .HasMaxLength(50);

                entity.Property(p => p.Dosage)
                      .HasMaxLength(50);

                entity.Property(p => p.Manufacturer)
                      .HasMaxLength(100);
            });

            // Order entity configurations
            modelBuilder.Entity<Order>(entity =>
            {
                // Ignore the computed property TotalPrice, as it's not stored in the database
                entity.Ignore(o => o.TotalPrice);

                // Other configurations for Order
                entity.Property(o => o.Discount)
                      .HasColumnType("decimal(18,2)");  // Specify precision and scale for Discount
            });

            // OrderItem entity configurations
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.Property(oi => oi.Price)
                      .HasColumnType("decimal(18,2)");

                // Specify precision and scale for Discount
                entity.Property(oi => oi.Discount)
                      .HasColumnType("decimal(18,2)");  // Specify precision and scale for Discount
            });

            // CartItem entity configurations
            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.Property(ci => ci.Price)
                      .HasColumnType("decimal(18,2)");

                // Specify precision and scale for Discount and Tax
                entity.Property(ci => ci.Discount)
                      .HasColumnType("decimal(18,2)");  // Specify precision and scale for Discount

                entity.Property(ci => ci.Tax)
                      .HasColumnType("decimal(18,2)");  // Specify precision and scale for Tax
            });

            // VitalSign entity configuration (optional but recommended)
            modelBuilder.Entity<VitalSign>(entity =>
            {
                entity.Property(v => v.RecordedAt)
                      .HasColumnType("datetime");
            });

            // MedicalHistory entity configuration
            modelBuilder.Entity<MedicalHistory>(entity =>
            {
                entity.Property(mh => mh.Diagnosis)
                      .HasMaxLength(500);  // Optional: Adjust max length for Diagnosis field

                entity.Property(mh => mh.Treatment)
                      .HasMaxLength(500);  // Optional: Adjust max length for Treatment field

                entity.Property(mh => mh.Notes)
                      .HasMaxLength(1000);  // Optional: Adjust max length for Notes field

                entity.Property(mh => mh.DateOfDiagnosis)
                      .HasColumnType("date");  // Ensure it's stored as a date
            });

            // PatientFeedback entity configuration
            modelBuilder.Entity<PatientFeedback>(entity =>
            {
                entity.Property(pf => pf.Feedback)
                      .HasMaxLength(1000);  // Optional: Adjust max length for Feedback

                entity.Property(pf => pf.Rating)
                      .HasColumnType("decimal(3,2)"); // Optional: Adjust precision for rating (e.g., 1.00-5.00)

                entity.Property(pf => pf.DateSubmitted)
                      .HasColumnType("datetime"); // Store submission date/time
            });
        }
    }
}
