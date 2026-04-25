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


    }
}
