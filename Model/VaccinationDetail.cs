using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Assignment_API.Models
{
    public class VaccinationDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long VaccinationId { get; set; }

        public long PatientId { get; set; }

        [Required]
        [MaxLength(100)]
        public string VaccinationName { get; set; } = string.Empty;
       
        public DateTime? VacciatedDate { get; set; }

        [ForeignKey("PatientId")]
        public virtual PatientDetail PatientDetail { get; set; }
    }
}
