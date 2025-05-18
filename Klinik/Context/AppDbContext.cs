using Klinik.Models;
using Microsoft.EntityFrameworkCore;

namespace Klinik.Context
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options) { }
        public DbSet<Doctor>Doctors { get; set; }
        public DbSet<Department> Departments { get; set; }

    }
}
