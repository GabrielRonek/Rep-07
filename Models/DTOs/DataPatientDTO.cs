namespace Cw6.Models.DTOs
{
    public class DataPatientDTO
    {
        public int IdPatient { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public List<ExtendedPrescriptionDTO> prescriptions { get; set; }
    }

    public class PrescriptionsDTO
    {
        public int IdPrescription { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public List <ExtendedMedicamentDTO> medicaments { get; set; }
        public DoctorDTO Doctor { get; set; }
    }

    public class DoctorDTO
    {
        public int IdDoctor { get; set; }
        public string FirstName { get; set; }
    }
}
