using Assignment_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment_API.Data
{
    public class PatientDbContext : DbContext
    {
        public PatientDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<PatientDetail> PatientDetails { get; set; }
        public DbSet<VaccinationDetail> VaccinationDetails { get; set; }
    }
}
