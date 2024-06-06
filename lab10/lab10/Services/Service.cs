using lab10.Context;
using lab10.DTOs;
using lab10.Models;
using Microsoft.EntityFrameworkCore;
using lab10.Controllers;

namespace lab10.Services;

public class Service
{
    private readonly ApbdContext _context;

    public Service(ApbdContext context)
    {
        _context = context;
    }

    public async Task<string> AddPrescription(PrescriptionDTO request)
    {
        var patient = await _context.Patients.FindAsync(request.Patient.IdPatient);
        if (patient == null)
        {
            patient = new Patient
            {
                FirstName = request.Patient.FirstName,
                LastName = request.Patient.LastName,
                Birthdate = request.Patient.Birthdate
            };
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }

        var doctor = await _context.Doctors.FindAsync(request.Doctor.IdDoctor);
        if (doctor == null)
        {
            return "Lekarz o podanym Id nie istnieje.";
        }

        var medicamentIds = request.Medicaments.Select(m => m.IdMedicament).ToList();
        var existingMedicaments = await _context.Medicaments
            .Where(m => medicamentIds.Contains(m.IdMedicament))
            .Select(m => m.IdMedicament)
            .ToListAsync();

        foreach (var medicament in request.Medicaments)
        {
            if (!existingMedicaments.Contains(medicament.IdMedicament))
            {
                return $"Lek o IdMedicament {medicament.IdMedicament} nie istnieje.";
            }
        }

        if (request.Medicaments.Count > 10)
        {
            return "Przekroczono limit 10 leków na recepcie.";
        }

        if (request.DueDate < request.Date)
        {
            return "Duedate musi być większe lub równe Date.";
        }

        var prescription = new Prescription
        {
            Patient = patient,
            Doctor = doctor,
            PrescrptionMedicaments = request.Medicaments.Select(m => new PrescriptionMedicament
            {
                IdMedicament = m.IdMedicament,
                Dose = m.Dose,
                Details = m.Description
            }).ToList(),
            Date = request.Date,
            DueDate = request.DueDate
        };

        _context.Prescriptions.Add(prescription);
        await _context.SaveChangesAsync();

        return "Dodano receptę";
    }

    public async Task<PatientPrescriptionDTO> GetPatientPrescriptions(int id)
    {
        
        var patientPrescription = await _context.Patients
            .Where(p => p.IdPatient == id)
            .Select(p => new PatientPrescriptionDTO
            {
                PatientDto = new PatientDTO
                {
                    IdPatient = p.IdPatient,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Birthdate = p.Birthdate
                },
                PrescriptionForPatientDtos = p.Prescriptions
                    .OrderBy(d => d.DueDate)
                    .Select(pr => new PrescriptionForPatientDTO
                    {
                        IdPrescription = pr.IdPrescription,
                        Date = pr.Date,
                        DueDate = pr.DueDate,
                        Medicaments = pr.PrescrptionMedicaments
                            .Select(pm => new PrescriptionMedicamentDTO
                            {
                                IdMedicament = pm.IdMedicament,
                                Name = pm.Medicament.Name,
                                Dose = pm.Dose,
                                Description = pm.Details
                            }).ToList()
                    }).ToList(),
                DoctorDto = p.Prescriptions
                    .Select(d => new DoctorDTO
                    {
                        IdDoctor = d.Doctor.IdDoctor,
                        FirstName = d.Doctor.FirstName,
                        LastName = d.Doctor.LastName,
                        Email = d.Doctor.Email
                    })
                    .FirstOrDefault()
            })
            .FirstOrDefaultAsync();

        if (patientPrescription == null)
        {
            throw new Exception($"Nie znaleziono pacjenta o Id {id}.");
        }
        return patientPrescription;
    }

}