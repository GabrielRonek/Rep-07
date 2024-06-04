using Cw6.Models;
using Cw6.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cw6.Controllers
{
    [ApiController]
    [Route("api/prescription")]
    public class PrescriptionController : ControllerBase
    {
        private readonly MyDBContext _dbContext;

        public PrescriptionController(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddPrescription(PrescriptionDTO prescriptionDTO)
        {
            var patient = await _dbContext.Patients.FindAsync(prescriptionDTO.patient.idPatient);
            
            if (patient == null)
            {
                await _dbContext.Patients.AddAsync(new Patient
                {
                    IdPatient = prescriptionDTO.patient.idPatient,
                    FirstName = prescriptionDTO.patient.FirstName,
                    LastName = prescriptionDTO.patient.LastName,
                    Birthdate = prescriptionDTO.patient.Birthdate
                });

            }

            _dbContext.SaveChanges();

            bool ifExists = true;

            prescriptionDTO.medicaments.ForEach(async e =>
            {
                var med = await _dbContext.Medicaments.FindAsync(e.idMedicament);
                if (med == null)
                    ifExists = false;
            }); 

            if (!ifExists)
            {
                return NotFound("Taki lek nie istnieje");
            }

            if (prescriptionDTO.medicaments.Count() > 10)
            {
                return Conflict("Nie można przypisać więcej niż 10 leków do jednej recepty");
            }
            if (prescriptionDTO.DueDate < prescriptionDTO.Date)
            {
                return Conflict("Data przydatności nie może być mniejsza niż aktualna data");
            }

            var newPrescription = new Prescription
            {
                Date = prescriptionDTO.Date,
                DueDate = prescriptionDTO.DueDate,
                IdPatient = prescriptionDTO.patient.idPatient,
                IdDoctor = prescriptionDTO.doctor.IdDoctor,
            };

            _dbContext.Prescriptions.Add(newPrescription);

            _dbContext.SaveChanges();

            int id = await _dbContext.Prescriptions.MaxAsync(e=> e.IdPrescriptions);

            prescriptionDTO.medicaments.ForEach(async e =>
            {
                _dbContext.Prescription_Medicaments.Add(new Prescription_Medicament
                {
                    Medicament = await _dbContext.Medicaments.FindAsync(e.idMedicament),
                    Prescription = newPrescription,
                    Dose = e.Dose,
                    Details = e.Description
                });
                
            });

            _dbContext.SaveChanges();

            return Created();
        }

    }
}
