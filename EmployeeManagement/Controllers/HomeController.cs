using EmployeeManagement.Models;
using EmployeeManagement.Models.CookieModels;
using EmployeeManagement.Models.CoreModels;
using EmployeeManagement.Models.DTOModels;
using EmployeeManagement.Models.ViewModels;
using EmployeeManagement.Services;
using EmployeeManagement.Tools;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace EmployeeManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IWebAppLogger _logger;
        private readonly ISupervisorService _supervisorService;

        public HomeController(IWebAppLogger logger, ISupervisorService supervisorService, IEmployeeService employeeService)
        {
            _logger = logger;
            _supervisorService = supervisorService;
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            _logger.LogInfo("+");
            var results = new object[0];

            var supervisorTasks = new List<Task<SupervisorModel>>();
            var supervisor = new SupervisorModel();
            var supervisorDto = new SupervisorModelDTO();

            var employeeTasks = new List<Task<List<EmployeeModel>>>();
            var employees = new List<EmployeeModel>();
            var employeeDto = new EmployeeModelDTO();

            var viewModel = new HomeViewModel();
            var supervisorCookieModel = new SupervisorCookieModel();
            var supervisorCookiejSon = string.Empty;

            try
            {
                supervisorCookiejSon = Request.Cookies[StaticStrings.SupervisorDataCookieKey];
                if (string.IsNullOrEmpty(supervisorCookiejSon))
                {
                    _logger.LogInfo("-");
                    return RedirectToAction("SupervisorLoginGet", "Identity");
                }

                try
                {
                    supervisorCookieModel = JsonConvert.DeserializeObject<SupervisorCookieModel>(supervisorCookiejSon);
                }
                catch
                {
                    _logger.LogInfo("-");
                    return RedirectToAction("SupervisorLoginGet", "Identity");
                }

                supervisorTasks = new List<Task<SupervisorModel>>
                {
                    _supervisorService.GetSupervisorByIdAsync(long.Parse(supervisorCookieModel.SupervisorId))
                };


                results = await Task.WhenAll(supervisorTasks);

                foreach (var task in results)
                {
                    supervisor = (SupervisorModel)task;
                }

                employeeTasks = new List<Task<List<EmployeeModel>>>
                {
                    _employeeService.GetAllEmployeesAsync(supervisor.SupervisorId)
                };

                results = new object[0];
                results = await Task.WhenAll(employeeTasks);

                foreach (var task in results)
                {
                    employees = (List<EmployeeModel>)task;
                }

                foreach (var employee in employees)
                {
                    employeeDto = new EmployeeModelDTO();
                    employeeDto.AssignObject(employee);
                    viewModel.Employees.Add(employeeDto);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                _logger.LogInfo("-");
                return RedirectToAction("Error", "Home");
            }

            _logger.LogInfo("-");
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
