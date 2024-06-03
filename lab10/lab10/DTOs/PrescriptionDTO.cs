using lab10.Controllers;
using lab10.Models;

namespace lab10.DTOs;

public class PrescriptionDTO
{
 
        public int IdPrescription { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public DoctorDTO Doctor { get; set; }
        public PatientDTO Patient { get; set; }
        public List<PrescriptionMedicamentDTO> Medicaments { get; set; }
    
}