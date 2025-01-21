using EmployeeManagement.Models.CoreModels;
using EmployeeManagement.Models.DTOModels;
using EmployeeManagement.Services;
using EmployeeManagement.Tools;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IWebAppLogger _logger;

        public EmployeeController(IWebAppLogger logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> AddEmployeeGet()
        {
            var employeeDto = new EmployeeModelDTO();
            return View(employeeDto);
        }

        [HttpGet]
        public async Task<IActionResult> EditEmployeeGet(long id)
        {
            _logger.LogInfo("+");
            var employeeDto = new EmployeeModelDTO();
            try
            {
                employeeDto.AssignObject(_employeeService.GetEmployeeById(id));
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                _logger.LogInfo("-");
                return RedirectToAction("Error", "Home");
            }

            _logger.LogInfo("-");
            return View(employeeDto);
        }

        [HttpPost]
        public async Task<IActionResult> EditEmployeePost(EmployeeModelDTO employeeDto)
        {
            _logger.LogInfo("+");
            var employee = new EmployeeModel();
            try
            {
                employeeDto.Validate();
                if (!string.IsNullOrWhiteSpace(employeeDto.ErrorMessage))
                {
                    return View("EditEmployeeGet", employeeDto);
                }
                _employeeService.UpdateEmployee(employeeDto);
                _logger.LogAudit(employeeDto.ToString());
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                _logger.LogInfo("-");
                return RedirectToAction("Error", "Home");
            }

            _logger.LogInfo("-");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteEmployee(long id)
        {
            _logger.LogInfo("+");
            try
            {
                _employeeService.DeleteEmployee(id, out var FullName);
                _logger.LogAudit($"Deleted Employee: {FullName}");
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                _logger.LogInfo("-");
                return RedirectToAction("Error", "Home");
            }

            _logger.LogInfo("-");
            return RedirectToAction("Index", "Home");
        }
    }
}
