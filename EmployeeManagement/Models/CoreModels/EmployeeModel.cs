using EmployeeManagement.Models.DTOModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models.CoreModels
{
    [Table("Employee")]
    public class EmployeeModel
    {
        [Key] public long EmployeeId { get; set; }
        [ForeignKey("Supervisor")] public long SupervisorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public decimal HourlyRate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime DeleteDate { get; set; }

        [NotMapped] public virtual SupervisorModel Supervisor { get; set; }

        public void AssignObject(EmployeeModelDTO employee)
        {
            EmployeeId = long.Parse(employee.EmployeeId); //In production, this would be encrypted
            FirstName = employee.FirstName;
            LastName = employee.LastName;
            Phone = employee.PhoneNumber;
            Email = employee.Email;
            HourlyRate = employee.HourlyRate;
        }
    }
}
