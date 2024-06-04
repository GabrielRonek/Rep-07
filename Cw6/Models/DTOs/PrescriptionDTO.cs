using System.ComponentModel.DataAnnotations;

namespace Cw6.Models.DTOs
{
    public class PrescriptionDTO
    {
        [Required]
        public PatientDTO patient { get; set; }
        public DoctorDTO doctor { get; set; }
        public List<ExtendedMedicamentDTO> medicaments { get; set;
        }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
    }

    public class ExtendedPrescriptionDTO : PrescriptionDTO
    {
        public int IdPrescription { get; set; }
    }

    public class PatientDTO
    {
        public int idPatient { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }

    }

    public class MedicamentDTO
    {
        public int idMedicament { get; set; }
        public int? Dose { get; set; }
        public string Description { get; set; }
    }

    public class ExtendedMedicamentDTO: MedicamentDTO
    {
        public string Name { get; set; }
    }
}
