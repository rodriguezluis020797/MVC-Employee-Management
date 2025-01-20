using EmployeeManagement.Models.CookieModels;
using EmployeeManagement.Models.CoreModels;
using EmployeeManagement.Models.Identity;
using EmployeeManagement.Services;
using EmployeeManagement.Tools;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EmployeeManagement.Controllers
{
    public class IdentityController : Controller
    {
        private readonly IWebAppLogger _logger;
        private readonly ISupervisorService _supervisorService;

        public IdentityController(IWebAppLogger logger, ISupervisorService supervisorService)
        {
            _logger = logger;
            _supervisorService = supervisorService;
        }

        [HttpGet]
        public async Task<IActionResult> SupervisorLoginGet(IdentityModelDTO loginObject)
        {
            _logger.LogInfo("+");
            if (string.IsNullOrWhiteSpace(loginObject.EMail))
            {
                loginObject = new IdentityModelDTO();
                loginObject.EMail = "john.doe@gmail.com"; //added for simplicity
            }

            _logger.LogInfo("-");
            return View(loginObject);
        }

        [HttpPost]
        public async Task<IActionResult> SupervisorLoginPost(IdentityModelDTO dto)
        {
            _logger.LogInfo("+");
            var supervisorCookieModel = new SupervisorCookieModel();
            var taskList = new List<Task<SupervisorModel>>();
            var result = new SupervisorModel[0];
            var cookieOptions = new CookieOptions();
            var supervisor = new SupervisorModel();

            try
            {
                taskList = new List<Task<SupervisorModel>>
                {
                    _supervisorService.GetSupervisorByEMailAsync(dto.EMail)
                };

                result = await Task.WhenAll(taskList);

                foreach (var task in result)
                {
                    supervisor = task;
                }

                if (supervisor == null)
                {
                    dto.Password = string.Empty;
                    dto.ErrorMessage = "Invalid Credentials";
                    return RedirectToAction("SupervisorLoginGet", "Identity", dto);
                }

                supervisorCookieModel.SupervisorId = supervisor.SupervisorId.ToString();

                cookieOptions = new CookieOptions
                {
                    HttpOnly = true, // Prevents client-side access via JavaScript
                    Secure = true, // Ensures cookie is sent over HTTPS only
                    SameSite = SameSiteMode.Strict // Prevents CSRF attacks
                                                   // No Expires or MaxAge set, making it a session cookie
                };
                HttpContext.Response.Cookies.Append(StaticStrings.SupervisorDataCookieKey,
                    JsonSerializer.Serialize(supervisorCookieModel),
                    cookieOptions);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                _logger.LogInfo("-");
                return RedirectToAction("Error", "Home");
            }

            _logger.LogAudit($"Supervisor Id: {supervisorCookieModel.SupervisorId}");
            _logger.LogInfo("-");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> SupervisorLogoutGet()
        {
            _logger.LogInfo("+");
            try
            {
                var cookies = HttpContext.Request.Cookies;
                foreach (var cookie in cookies) HttpContext.Response.Cookies.Delete(cookie.Key);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                _logger.LogInfo("-");
                return RedirectToAction("Error", "Home");
            }

            _logger.LogInfo("-");
            return RedirectToAction("SupervisorLoginGet", "Identity");
        }
    }
}
