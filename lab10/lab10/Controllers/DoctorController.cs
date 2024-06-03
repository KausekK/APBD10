using lab10.Context;
using lab10.DTOs;
using lab10.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lab10.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DoctorController : ControllerBase
{
    private readonly ApbdContext _apbdContext;
    public DoctorController(ApbdContext apbdContext)
    {
        _apbdContext = apbdContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetDoctors()
    {
        var doctors = await _apbdContext.Doctors.ToListAsync();
        var doctorsList = doctors.Select(d => new DoctorDTO
        {
            IdDoctor = d.IdDoctor,
            FirstName = d.FirstName,
            LastName = d.LastName,
            Email = d.Email
        }).ToList();
        return Ok(doctorsList);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDoctor(int id)
    {
        var doctor = await _apbdContext.Doctors.FindAsync(id);

        if (doctor == null)
        {
            return NotFound();
        }

        var doctorDto = new DoctorDTO()
        {
            IdDoctor = doctor.IdDoctor,
            FirstName = doctor.FirstName,
            LastName = doctor.LastName,
            Email = doctor.Email
        };

        return Ok(doctorDto);
    }

    [HttpPost]
    public async Task<IActionResult> AddDoctor(DoctorDTO doctorDto)
    {
        
        var newDoctor = new Doctor()
        {
            FirstName = doctorDto.FirstName,
            LastName = doctorDto.LastName,
            Email = doctorDto.Email
        };
         _apbdContext.Add(newDoctor);
        await _apbdContext.SaveChangesAsync();
        return Ok("Doctor added");
    }

    [HttpDelete("{idDoctor}")]
    public async Task<IActionResult> DeleteDoctor(int idDoctor)
    {
        var doctorToRemove = await _apbdContext.Doctors.FindAsync(idDoctor);

        if (doctorToRemove ==null)
        {
            return NotFound($"Doctor with ID {idDoctor} not found.");
        }

        _apbdContext.Doctors.Remove(doctorToRemove);
        await _apbdContext.SaveChangesAsync();
        return Ok($"Doctor with ID {idDoctor} removed");
    }

    [HttpPut("{idDoctor}")]   
    public async Task<IActionResult> UpdateDoctor(int idDoctor, DoctorDTO doctorDto)
    {
        var doctorToUpdate = await _apbdContext.Doctors.FindAsync(idDoctor);
    
        if (doctorToUpdate ==null)
        {
            return NotFound($"Doctor with ID {idDoctor} not found.");
        }
        
        doctorToUpdate.FirstName = doctorDto.FirstName;
        doctorToUpdate.LastName = doctorDto.LastName;
        doctorToUpdate.Email = doctorDto.Email;
        await _apbdContext.SaveChangesAsync();
    
        return Ok("Doctor updated");
    }

    [HttpGet("prescription/{id}")]
    public async Task<IActionResult> GetPrescription(int id)
    {
        var prescription = await _apbdContext.Prescriptions
            .Where(p => p.IdPrescription == id)
            .Select(p => new PrescriptionDTO
            {
                IdPrescription = p.IdPrescription,
                Date = p.Date,
                DueDate = p.DueDate,
                Doctor = new DoctorDTO
                {
                    IdDoctor = p.Doctor.IdDoctor,
                    FirstName = p.Doctor.FirstName,
                    LastName = p.Doctor.LastName,
                    Email = p.Doctor.Email
                },
                Patient = new PatientDTO
                {
                    IdPatient = p.Patient.IdPatient,
                    FirstName = p.Patient.FirstName,
                    LastName = p.Patient.LastName,
                    Birthdate = p.Patient.Birthdate
                },
                Medicaments = p.PrescrptionMedicaments.Select(pm => new PrescriptionMedicamentDTO
                {
                    IdMedicament = pm.Medicament.IdMedicament,
                    MedicamentName = pm.Medicament.Name,
                    Dose = pm.Dose,
                    Details = pm.Details
                }).ToList()
            }).FirstOrDefaultAsync();
        if (prescription == null)
        {
            return NotFound();
        }

        return Ok(prescription);
    }

}