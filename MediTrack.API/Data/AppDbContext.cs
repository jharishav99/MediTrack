using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using MediTrack.API.Models; 

namespace MediTrack.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Patient> Patients { get; set; } 
        public DbSet<Doctor> Doctors { get; set; } 
        public DbSet<Appointment> Appointments { get; set; } 
        public DbSet<User> Users { get; set; } 


        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Appointment>()
            .HasOne(a=>a.patient) // Appointment sanga Patient ko relationship
            .WithMany(p =>p.Appointments) // Patient sanga dherai Appointments ko relationship
            .HasForeignKey(a => a.PatientId) // Foreign key ko definition
            .OnDelete(DeleteBehavior.Cascade); // Cascade delete, jaba patient delete huncha, tyo sanga sambandhit appointments pani delete huncha


            modelBuilder.Entity<Appointment>()
            .HasOne(a=>a.doctor) // Appointment sanga Doctor ko relationship
            .WithMany(d=>d.Appointments) // Doctor sanga dherai Appointments ko relationship
            .HasForeignKey(a=>a.DoctorId) // Foreign key ko definition
            .OnDelete(DeleteBehavior.Restrict); // Restrict delete, jaba doctor delete huncha, tyo sanga sambandhit appointments delete hudaina, doctor delete garna milena jab samma tyo sanga sambandhit appointments haru delete hudaina


            // Seed data for testing
            // Doctor haru ko seed data
            modelBuilder.Entity<Doctor>().HasData(
           new Doctor { Id = 1, Name = "Dr. Priya Sharma", Specialization = "Cardiology" },
           new Doctor { Id = 2, Name = "Dr. Arjun Mehta", Specialization = "Neurology" }
       );
            // Patient haru ko seed data
            modelBuilder.Entity<Patient>().HasData(
                new Patient { Id = 1, Name = "Rahul Verma", Phone = "9876543210", Age = 35 }
            );











        }

    }
}
