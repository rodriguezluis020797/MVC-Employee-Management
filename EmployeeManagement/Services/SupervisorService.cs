using EmployeeManagement.Models.CoreModels;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Services
{
    public interface ISupervisorService
    {
        public Task<SupervisorModel> GetSupervisorByIdAsync(long id);
        public Task<SupervisorModel> GetSupervisorByEMailAsync(string eMail);
    }

    public class SupervisorService : ISupervisorService
    {
        private readonly CoreDataService _coreDataService;
        public SupervisorService(CoreDataService coreDataService)
        {
            _coreDataService = coreDataService;
        }
        public async Task<SupervisorModel> GetSupervisorByIdAsync(long id)
        {
            return await _coreDataService.Supervisors
              .Where(x => x.SupervisorId == id)
              .FirstOrDefaultAsync();
        }

        public async Task<SupervisorModel> GetSupervisorByEMailAsync(string eMail)
        {
            return await _coreDataService.Supervisors
                .Where(x => x.Email.Equals(eMail))
                .FirstOrDefaultAsync();
        }
    }
}
