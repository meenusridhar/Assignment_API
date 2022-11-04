using Assignment_API.Models;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace Assignment_API.Dto
{
    public class PatientDetailDto
    {
        public long PatientId { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public int Age { get; set; }
        public string MobileNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int Gender { get; set; }
        public Boolean IsVaccinated { get; set; } = false;
        public List<VaccinationDto> Vaccinations { get; set; }

    }

    [AutoMap(typeof(VaccinationDetail))]
    public class VaccinationDto
    {
        public long VaccinationId { get; set; }
        public string VaccinationName { get; set; } = string.Empty;
        public DateTime? VacciatedDate { get; set; }

    }
}
