using EmployeeManagement.Models.CoreModels;
using EmployeeManagement.Tools;

namespace EmployeeManagement.Models.DTOModels
{
    public class EmployeeModelDTO
    {
        public string EmployeeId { get; set; }
        public string SupervisorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public decimal HourlyRate { get; set; }
        public string ErrorMessage { get; set; }

        public void Validate()
        {
            ErrorMessage = string.Empty;
            if (!string.IsNullOrWhiteSpace(FirstName))
            {
                FirstName = FirstName.Trim();
                if (string.IsNullOrWhiteSpace(FirstName)) ErrorMessage = "First name is required";
            }

            if (!string.IsNullOrWhiteSpace(LastName))
            {
                LastName = LastName.Trim();
                if (string.IsNullOrWhiteSpace(LastName)) ErrorMessage = "Last name is required";
            }

            if (string.IsNullOrWhiteSpace(PhoneNumber))
            {
                ErrorMessage = "Phone number is required";
            }
            else
            {
                PhoneNumber = PhoneNumber.Trim();
                if (!Validation.IsValidPhoneNumber(PhoneNumber)) ErrorMessage = "Phone number is invalid";
            }

            if (string.IsNullOrWhiteSpace(Email))
            {
                ErrorMessage = "Email is required";
            }
            else
            {
                Email = Email.Trim();
                if (!Validation.IsValidEmail(Email)) ErrorMessage = "Email is invalid";
            }

            if (HourlyRate <= 0) ErrorMessage = "Hourly rate must be greater than zero";
        }

        public void AssignObject(EmployeeModel employee)
        {
            EmployeeId = employee.EmployeeId.ToString(); //In production, this would be encrypted
            FirstName = employee.FirstName;
            LastName = employee.LastName;
            PhoneNumber = employee.PhoneNumber;
            Email = employee.EMail;
            HourlyRate = employee.HourlyRate;
        }

        public override string ToString()
        {
            return $"Employee ID: {EmployeeId}" +
                   $"{Environment.NewLine}First Name: {FirstName}" +
                   $"{Environment.NewLine}Last Name: {LastName}" +
                   $"{Environment.NewLine}PhoneNumber Number: {PhoneNumber}" +
                   $"{Environment.NewLine}Email: {Email}" +
                   $"{Environment.NewLine}Hourly Rate: {HourlyRate}";
        }
    }
}
