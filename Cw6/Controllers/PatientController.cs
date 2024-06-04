using Cw6.Models;
using Cw6.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Cw6.Controllers
{
    [ApiController]
    [Route("api/DataPatient")]
    public class PatientController : ControllerBase
    {
        private readonly MyDBContext _dbContext;

        public PatientController(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult>DataPatient(int IdPatient)
        {
            DataPatientDTO resault = new DataPatientDTO();

            Patient patient = await _dbContext.Patients.FirstOrDefaultAsync(e => e.IdPatient == IdPatient); 
            if (patient == null)
            {
                return Conflict("Brak pacjenta w bazie danych");
            }

            resault.IdPatient = IdPatient;
            resault.FirstName = patient.FirstName;
            resault.Lastname = patient.LastName;
            resault.prescriptions = _dbContext.Prescriptions
                .Where(e => e.Patient.IdPatient == IdPatient)
                .Select(e => new ExtendedPrescriptionDTO
                {
                    Date = e.Date,
                    DueDate = e.DueDate,
                    doctor = new DoctorDTO
                    {
                        IdDoctor = e.Doctor.IdDoctor,
                        FirstName = e.Doctor.FirstName
                    },
                    IdPrescription = e.IdPrescriptions,
                    medicaments = _dbContext.Prescription_Medicaments
                                   .Where(d => d.IdPrescription == e.IdPrescriptions)
                                   .Select(f =>
                                   new ExtendedMedicamentDTO
                                   {
                                       idMedicament = f.Medicament.IdMedicament,
                                       Name = f.Medicament.Name,
                                       Dose = f.Dose,
                                       Description = f.Medicament.Description,
                                   }).ToList()
                }).OrderBy(o => o.DueDate).ToList();

            return Ok(resault);
        }
    }
}
