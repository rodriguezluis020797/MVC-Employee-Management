using EmployeeManagement.Models.CoreModels;
using EmployeeManagement.Models.DTOModels;
using EmployeeManagement.Services;
using Microsoft.Extensions.Configuration;

namespace EmploymentManagementTests
{
    public class MainTests
    {
        private ISupervisorService _supervisorService;
        private CoreDataService _coreDataService;
        private IConfiguration _configuration;
        [SetUp]
        public void Setup()
        {

            SetUpConfigs();
            _coreDataService = new CoreDataService(_configuration);
            _supervisorService = new SupervisorService(_coreDataService);
        }

        public void SetUpConfigs()
        {
            // Build configuration using the appsettings.json file
            _configuration = new ConfigurationBuilder()
                .SetBasePath(TestContext.CurrentContext.TestDirectory) // Base path for the config file
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        [TearDown]
        public void TearDown()
        {
            _coreDataService.Dispose();
        }

        [Test]
        public void ResetData()
        {
            var supervisors = _coreDataService.Supervisors
                .ToList();

            using (var trans = _coreDataService.Database.BeginTransaction())
            {
                foreach (var supervisor in supervisors)
                {
                    _coreDataService.Supervisors.Remove(supervisor);
                    _coreDataService.SaveChanges();
                }
                trans.Commit();
            }

            var supervisorDto = new SupervisorModelDTO()
            {
                Email = "john.doe@gmail.com",
                FirstName = "John",
                LastName = "Doe",
                Phone = "1234567890",
            };

            var supervisorModel = new SupervisorModel();
            supervisorModel.CreateNewRecord(supervisorDto);

            var employeeOneDto = new EmployeeModelDTO()
            {
                EMail = "employee.one@gmail.com",
                FirstName = "Employee",
                PhoneNumber = "1234567890",
                LastName = "One",
                HourlyRate = 10.00m
            };

            var employeeTwoDto = new EmployeeModelDTO()
            {
                EMail = "employee.two@gmail.com",
                FirstName = "Employee",
                PhoneNumber = "1234567890",
                LastName = "Two",
                HourlyRate = 15.00m
            };

            var employeeOne = new EmployeeModel();
            employeeOne.CreateNewRecord(employeeOneDto);
            var employeeTwo = new EmployeeModel();
            employeeTwo.CreateNewRecord(employeeTwoDto);

            using (var trans = _coreDataService.Database.BeginTransaction())
            {
                _coreDataService.Supervisors.Add(supervisorModel);
                _coreDataService.SaveChanges();

                employeeOne.SupervisorId = supervisorModel.SupervisorId;
                employeeTwo.SupervisorId = supervisorModel.SupervisorId;

                _coreDataService.Employees.Add(employeeOne);
                _coreDataService.SaveChanges();
                _coreDataService.Employees.Add(employeeTwo);
                _coreDataService.SaveChanges();

                trans.Commit();
            }

            Assert.Pass();
        }
    }
}