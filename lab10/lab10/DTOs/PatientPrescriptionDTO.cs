using lab10.Controllers;

namespace lab10.DTOs;

public class PatientPrescriptionDTO
{
    public PatientDTO PatientDto { get; set; }
    public List<PrescriptionForPatientDTO> PrescriptionForPatientDtos { get; set; }
    public DoctorDTO? DoctorDto { get; set; }

}