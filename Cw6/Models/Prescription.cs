using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cw6.Models
{
    public class Prescription
    {
        [Key]
        public int IdPrescriptions { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        [Required]
        public int IdPatient { get; set; }
        [Required]
        public int IdDoctor { get; set; }
        public virtual ICollection<Prescription_Medicament> PrescriptionMedicament { get; set; }
        [ForeignKey(nameof(IdPatient))]
        public virtual Patient Patient { get; set; }
        [ForeignKey(nameof(IdDoctor))]
        public virtual Doctor Doctor { get; set; }
    }
}
