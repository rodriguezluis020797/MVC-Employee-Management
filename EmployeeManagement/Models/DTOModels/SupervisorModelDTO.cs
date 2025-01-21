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

        public List<EmployeeModelDTO> Employees { get; set; } = new List<EmployeeModelDTO>();

        public void AssignObject(SupervisorModel supervisor)
        {
            SupervisorId = supervisor.SupervisorId.ToString(); //In production, this would be encrypted
            FirstName = supervisor.FirstName;
            LastName = supervisor.LastName;
            Phone = supervisor.PhoneNumber;
            Email = supervisor.EMail;
            var employeeDto = new EmployeeModelDTO();
            foreach (var employee in supervisor.Employees)
            {
                employeeDto = new EmployeeModelDTO();
                employeeDto.AssignObject(employee);
                Employees.Add(employeeDto);
            }
        }
    }
}
