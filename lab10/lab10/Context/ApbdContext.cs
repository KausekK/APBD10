using lab10.Models;
using Microsoft.EntityFrameworkCore;

namespace lab10.Context;

public class ApbdContext:  DbContext
{
    public ApbdContext()
    {
    }

    public ApbdContext(DbContextOptions<ApbdContext> options)
        : base(options)
    {
    }

    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    
    public static void Seed(ApbdContext context)
    {
        var patients = new List<Patient>
        {
            new Patient {  FirstName = "Jan", LastName = "Kowalski", Birthdate = new DateTime(1980, 1, 1) },
            new Patient {  FirstName = "Anna", LastName = "Nowak", Birthdate = new DateTime(1990, 2, 2) }
        };

        var doctors = new List<Doctor>
        {
            new Doctor {  FirstName = "Adam", LastName = "Lekarz", Email = "adam.lekarz@example.com" },
            new Doctor {  FirstName = "Ewa", LastName = "Lekarka", Email = "ewa.lekarka@example.com" }
        };

        var medicaments = new List<Medicament>
        {
            new Medicament {  Name = "Paracetamol", Description = "Lek przeciwbólowy", Type = "Tabletka" },
            new Medicament {  Name = "Ibuprofen", Description = "Lek przeciwzapalny", Type = "Tabletka" }
        };

        context.Patients.AddRange(patients);
        context.Doctors.AddRange(doctors);
        context.Medicaments.AddRange(medicaments);

        context.SaveChanges();
    }
}
