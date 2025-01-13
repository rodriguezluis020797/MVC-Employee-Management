using EmployeeManagement.Models.DTOModels;

namespace EmployeeManagement.Models.ViewModels
{
    public class HomeViewModel
    {
        public SupervisorModelDTO Supervisor { get; set; } = new SupervisorModelDTO();
        public List<EmployeeModelDTO> Employees { get; set; } = new List<EmployeeModelDTO>();
    }
}
