using EmployeeManagement.Models.CoreModels;

namespace EmployeeManagement.Services
{
    public interface IEmployeeService
    {
        public List<EmployeeModel> GetAllEmployees();
        public EmployeeModel GetEmployeeById(long id);
        public void EditEmployee(EmployeeModel employee);
        public void DeleteEmployee(long id, out string FullName);
    }

    public class EmployeeService : IEmployeeService
    {
        public List<EmployeeModel> GetAllEmployees()
        {
            return GetEmployeeList();
        }

        public EmployeeModel GetEmployeeById(long id)
        {
            return GetEmployeeList().Where(x => x.EmployeeId == id).FirstOrDefault();
        }

        public void EditEmployee(EmployeeModel employee)
        {
        }

        public void DeleteEmployee(long id, out string FullName)
        {
            FullName = GetEmployeeList().Where(x => x.EmployeeId == id).Select(x => x.FirstName).FirstOrDefault()
                       + " "
                       + GetEmployeeList().Where(x => x.EmployeeId == id).Select(x => x.LastName).FirstOrDefault();
        }

        private List<EmployeeModel> GetEmployeeList()
        {
            return new List<EmployeeModel>
        {
            new()
            {
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane.doe@gmail.com",
                Phone = "1234567890",
                EmployeeId = 1,
                HourlyRate = 20.00m
            },
            new()
            {
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane.doe@gmail.com",
                Phone = "1234567890",
                EmployeeId = 2,
                HourlyRate = 23.00m
            }
        };
        }
    }
}
