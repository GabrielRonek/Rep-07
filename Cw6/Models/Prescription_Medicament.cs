using Microsoft.EntityFrameworkCore;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cw6.Models
{

    public class Prescription_Medicament
    {
        [Required]
        public int IdMedicament {  get; set; }
        [Required]
        public int IdPrescription { get; set; }
        public int? Dose { get; set; }
        [Required]
        [MaxLength(100)]
        public string Details { get; set; }
        [ForeignKey(nameof(IdMedicament))]
        public virtual Medicament Medicament { get; set; }
        [ForeignKey(nameof(IdPrescription))]
        public virtual Prescription Prescription {  get; set; } 
    }
}
