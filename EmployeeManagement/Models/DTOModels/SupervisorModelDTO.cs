using EmployeeManagement.Models.CoreModels;

namespace EmployeeManagement.Models.DTOModels
{
    public class SupervisorModelDTO
    {
        public string SupervisorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Phone { get; set; }
        public string Email { get; set; }

        public void AssignObject(SupervisorModel supervisor)
        {
            SupervisorId = supervisor.SupervisorId.ToString(); //In production, this would be encrypted
            FirstName = supervisor.FirstName;
            LastName = supervisor.LastName;
            Phone = supervisor.Phone;
            Email = supervisor.Email;
        }
    }
}
