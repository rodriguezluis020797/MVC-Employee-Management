using EmployeeManagement.Models.CoreModels;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace EmployeeManagement.Services
{
    public class CoreDataService : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=tcp:portfoliocore.database.windows.net,1433;Initial Catalog=employeemanagementcore;Persist Security Info=False;User ID=portfolioadmin;Password=53SzyU4XWF1GsMM;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
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
