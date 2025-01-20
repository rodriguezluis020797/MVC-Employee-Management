using EmployeeManagement.Models.CoreModels;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Services
{
    public interface IEmployeeService
    {
        public Task<List<EmployeeModel>> GetAllEmployeesAsync(long supervisorId);
        public EmployeeModel GetEmployeeById(long employeeId);
        public void UpdateEmployee(EmployeeModel employee);
        public void DeleteEmployee(long id, out string employeeName);
    }

    public class EmployeeService : IEmployeeService
    {
        private readonly CoreDataService _coreDataService;
        public EmployeeService(CoreDataService coreDataService)
        {
            _coreDataService = coreDataService;
        }
        public async Task<List<EmployeeModel>> GetAllEmployeesAsync(long supervisorId)
        {
            return await _coreDataService.Employees
                .Where(x => x.SupervisorId == supervisorId)
                .ToListAsync();
        }

        public EmployeeModel GetEmployeeById(long EmployeeId)
        {
            return _coreDataService.Employees
                .Where(x => x.EmployeeId == EmployeeId)
                .FirstOrDefault();
        }

        public void UpdateEmployee(EmployeeModel employee)
        {
            var employeeToEdit = _coreDataService.Employees
                .Where(x => x.EmployeeId == employee.EmployeeId)
                .FirstOrDefault();

            //employeeToEdit.UpdateRecord(employee);

            _coreDataService.Employees.Update(employeeToEdit);
            _coreDataService.SaveChanges();
        }

        public void DeleteEmployee(long id, out string employeeName)
        {
            var employee = _coreDataService.Employees
                .Where(x => x.EmployeeId == id)
                .FirstOrDefault();

            if (employee != null)
            {
                employeeName = $"{employee.FirstName} {employee.LastName}";
                _coreDataService.Employees.Remove(employee);
                _coreDataService.SaveChanges();
            }
            else
            {
                employeeName = null;
            }
        }


    }
}
