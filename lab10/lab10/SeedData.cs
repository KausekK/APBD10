using System;
using System.Linq;
using lab10.Context;
using lab10.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace lab10;

public class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new ApbdContext(
                   serviceProvider.GetRequiredService<DbContextOptions<ApbdContext>>()))
        {
            if (context.Doctors.Any() && context.Patients.Any() && context.Medicaments.Any())
            {
                return; // DB has been seeded
            }

            // Seed Doctors
            var doctors = new[]
            {
                new Doctor { FirstName = "Jan", LastName = "Kowalski", Email = "jan.kowalski@example.com" },
                new Doctor { FirstName = "Anna", LastName = "Nowak", Email = "anna.nowak@example.com" }
            };
            context.Doctors.AddRange(doctors);
            context.SaveChanges();

            // Seed Patients
            var patients = new[]
            {
                new Patient { FirstName = "Piotr", LastName = "Zielinski", Birthdate = DateTime.Parse("1980-01-01") },
                new Patient { FirstName = "Maria", LastName = "Wiśniewska", Birthdate = DateTime.Parse("1990-02-02") }
            };
            context.Patients.AddRange(patients);
            context.SaveChanges();

            // Seed Medicaments
            var medicaments = new[]
            {
                new Medicament { Name = "Paracetamol", Description = "Lek przeciwbólowy i przeciwgorączkowy", Type = "Tabletki" },
                new Medicament { Name = "Ibuprofen", Description = "Lek przeciwbólowy i przeciwzapalny", Type = "Tabletki" }
            };
            context.Medicaments.AddRange(medicaments);
            context.SaveChanges();

            // Seed Prescriptions
            var prescriptions = new[]
            {
                new Prescription
                {
                    Date = DateTime.Now, DueDate = DateTime.Now.AddMonths(1), IdDoctor = doctors[0].IdDoctor,
                    IdPatient = patients[0].IdPatient
                },
                new Prescription
                {
                    Date = DateTime.Now, DueDate = DateTime.Now.AddMonths(1), IdDoctor = doctors[1].IdDoctor,
                    IdPatient = patients[1].IdPatient
                }
            };
            context.Prescriptions.AddRange(prescriptions);
            context.SaveChanges();

            // Seed PrescriptionMedicaments
            var prescriptionMedicaments = new[]
            {
                new PrescriptionMedicament
                {
                    IdMedicament = medicaments[0].IdMedicament, IdPrescription = prescriptions[0].IdPrescription,
                    Dose = 2, Details = "2 razy dziennie"
                },
                new PrescriptionMedicament
                {
                    IdMedicament = medicaments[1].IdMedicament, IdPrescription = prescriptions[1].IdPrescription,
                    Dose = 1, Details = "1 raz dziennie"
                }
            };
            context.PrescriptionMedicaments.AddRange(prescriptionMedicaments);
            context.SaveChanges();
        }
    }
}
