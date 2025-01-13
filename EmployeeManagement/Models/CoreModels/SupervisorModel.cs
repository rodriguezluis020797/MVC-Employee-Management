using EmployeeManagement.Models.DTOModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models.CoreModels
{
    [Table("Supervisor")]
    public class SupervisorModel
    {
        [Key] public long SupervisorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime DeleteDate { get; set; }

        [NotMapped] public virtual List<EmployeeModel> Employees { get; set; }

        public void AssignObject(SupervisorModelDTO supervisor)
        {
            SupervisorId = long.Parse(supervisor.SupervisorId); //In production, this would be decrypted
            FirstName = supervisor.FirstName;
            LastName = supervisor.LastName;
            Phone = supervisor.Phone;
            Email = supervisor.Email;
        }
    }
}
