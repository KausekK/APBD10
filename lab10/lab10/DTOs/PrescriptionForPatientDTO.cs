using lab10.Models;

namespace lab10.DTOs;

public class PrescriptionForPatientDTO
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public List<PrescriptionMedicamentDTO> Medicaments { get; set; }
}