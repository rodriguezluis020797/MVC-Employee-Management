using EmployeeManagement.Models.CoreModels;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Services
{
    public class CoreDataService : DbContext
    {
        private readonly string coreConnectionString;
        public CoreDataService(IConfiguration configuration)
        {
            coreConnectionString = configuration.GetConnectionString("CoreDb");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(coreConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SupervisorModel>().HasKey(x => x.SupervisorId);

            modelBuilder.Entity<EmployeeModel>().HasKey(x => x.EmployeeId);
            modelBuilder.Entity<SupervisorModel>()
                .HasMany(x => x.Employees)
                .WithOne(x => x.Supervisor)
                .HasForeignKey(x => x.SupervisorId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
