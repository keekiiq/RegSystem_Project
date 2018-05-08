using RegSystem.Domain.Entities;
using System.Data.Entity;

namespace RegSystem.Domain.Concrete {
    public class EFDbContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
    }
}