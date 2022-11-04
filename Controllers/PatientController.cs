using Assignment_API.Data;
using Assignment_API.Dto;
using Assignment_API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Assignment_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : Controller
    {
        private readonly PatientDbContext patientDbContext;
        private readonly IMapper mapper;

        public PatientController(PatientDbContext patientDbContext, IMapper mapper)
        {
            this.mapper = mapper;
            this.patientDbContext = patientDbContext;   
        }

        /// <summary>
        /// This method to get all the patient details with vaccination taken
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Route("GetPatientDetails")]
        public async Task<List<PatientDetailDto>> GetPatients()
        {
            List<PatientDetailDto> patientDetails = new List<PatientDetailDto>();
            List<PatientDetail> patients = await patientDbContext.PatientDetails.Include(x => x.VaccinationDetails).ToListAsync();
            if(patients != null)
            {
                foreach (var item in patients)
                {
                    PatientDetailDto patientDetail = new PatientDetailDto
                    {
                        PatientId = item.PatientId,
                        PatientName = item.PatientName,
                        MobileNumber = item.MobileNumber,
                        Address = item.Address,
                        Age = item.Age,
                        IsVaccinated = item.IsVaccinated,
                        Gender = (int)item.Gender,
                        Vaccinations = mapper.Map<List<VaccinationDto>>(item.VaccinationDetails)
                      
                    };
                    patientDetails.Add(patientDetail);

                }
            }
            return patientDetails;
        }

        [HttpGet]
        [Route("GetPatientDetails/{id}")]
        public async Task<PatientDetailDto> GetPatient(int id)
        {
            PatientDetailDto patientDetail = null;
            PatientDetail patient = await patientDbContext.PatientDetails.Include(x => x.VaccinationDetails).Where(x => x.PatientId == id).FirstOrDefaultAsync();
                                                                
            if (patient != null)
            {
                     patientDetail = new PatientDetailDto
                    {
                        PatientId = patient.PatientId,
                        PatientName = patient.PatientName,
                        MobileNumber = patient.MobileNumber,
                        Address = patient.Address,
                        Age = patient.Age,
                        IsVaccinated = patient.IsVaccinated,
                        Gender = (int)patient.Gender,
                        Vaccinations = mapper.Map<List<VaccinationDto>>(patient.VaccinationDetails)
                    };
            }
            return patientDetail;
        }

        /// <summary>
        /// To add the Patient Details
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddPatientDetails")]
        public async Task<bool> AddPatients(PatientDetailDto input)
        {
            if (input == null) return false;
            else
            {
                PatientDetail patientDetail = new PatientDetail {
                    PatientName = input.PatientName,
                    MobileNumber=input.MobileNumber,
                    Age=input.Age,
                    Address = input.Address,
                    IsVaccinated = input.IsVaccinated,
                    Gender=(GenderEnum)input.Gender,
                    VaccinationDetails = new()
                };

                if (input.Vaccinations != null && input.Vaccinations.Any())
                {
                    foreach (var item in input.Vaccinations)
                    {
                        VaccinationDetail vaccinationDetail = new VaccinationDetail
                        {
                            VaccinationName = item.VaccinationName,
                            VacciatedDate = item.VacciatedDate
                        };
                        patientDetail.VaccinationDetails.Add(vaccinationDetail);
                    }
                }
                await patientDbContext.PatientDetails.AddAsync(patientDetail);
                await patientDbContext.SaveChangesAsync();
                return true;
            }
        }

        /// <summary>
        /// To Update the Patient Details
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdatePatientDetails")]
        public async Task<bool> EditPatients(PatientDetailDto input)
        {
            try
            {
                var existingPatient = await patientDbContext.PatientDetails.FirstOrDefaultAsync(x => x.PatientId == input.PatientId);
                if (existingPatient != null)
                {
                    existingPatient.PatientName = input.PatientName;
                    existingPatient.MobileNumber = input.MobileNumber;
                    existingPatient.Address = input.Address;
                    existingPatient.Gender = (GenderEnum)input.Gender;
                    existingPatient.Age = input.Age;
                    await patientDbContext.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return false;

        }

        /// <summary>
        /// To delete the Patient Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeletePatientDetails/{id}")]
        public async Task<bool> DeletePatients(long id)
        {
            try
            {
                var existingPatient = await patientDbContext.PatientDetails.FindAsync(id);
                if (existingPatient != null)
                {
                    patientDbContext.PatientDetails.Remove(existingPatient);
                    await patientDbContext.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return false;

        }
    }
}
