using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment_API.Models
{
    public class PatientDetail
    {
        public PatientDetail()
        {
            VaccinationDetail vaccinationDetail = new VaccinationDetail();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PatientId { get; set; }

        [Required]
        [MaxLength(100)]
        public string PatientName { get; set; } = string.Empty;
        public int Age { get; set; }


        [Required]
        [MaxLength(15)]
        public string MobileNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Address { get; set; } = string.Empty;

        [Required]
        public GenderEnum Gender { get; set; }
        public Boolean IsVaccinated { get; set; } = false;
        public virtual List<VaccinationDetail> VaccinationDetails { get; set; }

    }
}
