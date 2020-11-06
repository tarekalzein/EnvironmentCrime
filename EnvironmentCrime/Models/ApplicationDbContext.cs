using Microsoft.EntityFrameworkCore;

namespace EnvironmentCrime.Models
{
 /// <summary>
 /// Class that holds the main Database connection of the application
 /// It contains DBsets of all tables in the database.
 /// </summary>
    public class ApplicationDbContext : DbContext
    {
        //Constructor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Errand> Errands { get; set; }
        public DbSet<ErrandStatus> ErrandStatuses { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Sample> Samples { get; set; }
        public DbSet<Sequence> Sequences { get; set; }
    }
}
