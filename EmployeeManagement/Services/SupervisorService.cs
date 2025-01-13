using EmployeeManagement.Models.CoreModels;

namespace EmployeeManagement.Services
{
    public interface ISupervisorService
    {
        public SupervisorModel GetSupervisorById(long id);
        public SupervisorModel GetSupervisorByEMail(string eMail);
    }

    public class SupervisorService : ISupervisorService
    {
        public SupervisorModel GetSupervisorById(long id)
        {
            return GetSupervisors().Where(x => x.SupervisorId == id).FirstOrDefault();
        }

        public SupervisorModel GetSupervisorByEMail(string eMail)
        {
            return GetSupervisors().Where(x => x.Email.Equals(eMail)).FirstOrDefault();
        }

        public List<SupervisorModel> GetSupervisors()
        {
            return new List<SupervisorModel>
        {
            new()
            {
                SupervisorId = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@gmail.com",
                Phone = "1234567890"
            }
        };
        }
    }
}
