using System.ComponentModel.DataAnnotations;

namespace Cw6.Models
{
    public class Patient
    {
        [Key]
        public int IdPatient { get; set; }
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        [Required]
        public DateTime Birthdate { get; set; }
        public virtual ICollection<Prescription> PatientPrescriptions { get; set; }
    }
}
