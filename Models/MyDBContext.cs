using Microsoft.EntityFrameworkCore;

namespace Cw6.Models
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions options) : base(options)
        {

        } 

        public DbSet<Patient> Patients { get; set; } 
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Prescription_Medicament> Prescription_Medicaments { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>(e =>
            {
                e.HasData(new List<Patient>()
                {
                    new Patient
                    {
                        IdPatient = 1,
                        FirstName = "Jan",
                        LastName = "Kowalski",
                        Birthdate = DateTime.Now,
                    },
                    new Patient
                    {
                        IdPatient = 2,
                        FirstName = "Adam",
                        LastName = "Sierawski",
                        Birthdate = DateTime.Now,
                    }
                });
                e.Property(e => e.IdPatient).ValueGeneratedNever();
               
            });

            
            modelBuilder.Entity<Doctor>(e =>
            {
                e.HasData(new List<Doctor>()
                {
                    new Doctor
                    {
                        IdDoctor = 1,
                        FirstName = "Arkadiusz",
                        LastName = "Leczulski",
                        Email = "a.leczulski@nfz.pl",
                    },
                    new Doctor
                    {
                        IdDoctor = 2,
                        FirstName = "Cezary",
                        LastName = "Kolański",
                        Email = "c.kolański@nfz.pl",
                    }
                });
            });
            modelBuilder.Entity<Medicament>(e =>
            {
                e.HasData(new List<Medicament>()
                {
                    new Medicament
                    {
                        IdMedicament = 1,
                        Name = "Paracetamol",
                        Description = "Dobry na wszystko",
                        Type = "Tabletki",
                    },
                                        new Medicament
                    {
                        IdMedicament = 2,
                        Name = "Mucosolvan",
                        Description = "Na kaszelek suchy i mokry",
                        Type = "Syrop",
                    },
                });
            });
            modelBuilder.Entity<Prescription>(e =>
            {
                e.HasData(new List<Prescription>() {
                    new Prescription
                    {
                        IdPrescriptions = 1,
                        Date = DateTime.Now,
                        DueDate = DateTime.Now.AddDays(10),
                        IdPatient = 1,
                        IdDoctor = 2,
                    },
                    new Prescription
                    {
                        IdPrescriptions = 2,
                        Date = DateTime.Now,
                        DueDate = DateTime.Now.AddDays(8),
                        IdPatient = 2,
                        IdDoctor = 1,
                    }
                });
            });
            modelBuilder.Entity<Prescription_Medicament>(e =>
            {
                e.HasData(new List<Prescription_Medicament>()
                {
                    new Prescription_Medicament
                    {
                        IdMedicament = 1,
                        IdPrescription = 2,
                        Dose = 3,
                        Details = "2 razy dziennie",
                    },
                    new Prescription_Medicament
                    {
                        IdMedicament = 2,
                        IdPrescription = 1,
                        Dose = 2,
                        Details = "3 razy dziennie",
                    }
                });

                e.HasKey(e => new { e.IdPrescription, e.IdMedicament }).HasName("Prescription_Medicament_PK");


                e.ToTable("Prescription_Medicament");
            });
        }

    }
}
