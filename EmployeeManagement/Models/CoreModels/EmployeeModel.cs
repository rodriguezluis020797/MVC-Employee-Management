﻿using EmployeeManagement.Models.DTOModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models.CoreModels
{
    [Table("Employee")]
    public class EmployeeModel
    {
        [Key] public long EmployeeId { get; set; }
        [ForeignKey("Supervisor")] public long SupervisorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EMail { get; set; }
        public decimal HourlyRate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        [NotMapped] public virtual SupervisorModel Supervisor { get; set; }
        [NotMapped] public virtual List<EmployeeTimeEntryModel> TimeEntries { get; set; }

        public void CreateNewRecord(EmployeeModelDTO employee)
        {
            FirstName = employee.FirstName;
            LastName = employee.LastName;
            PhoneNumber = employee.PhoneNumber;
            EMail = employee.EMail;
            HourlyRate = employee.HourlyRate;
            CreateDate = DateTime.UtcNow;
        }

        public void UpdateRecord(EmployeeModelDTO employee)
        {
            SupervisorId = long.Parse(employee.SupervisorId);
            FirstName = employee.FirstName;
            LastName = employee.LastName;
            PhoneNumber = employee.PhoneNumber;
            EMail = employee.EMail;
            HourlyRate = employee.HourlyRate;

        }

        public void DeleteRecord()
        {
            DeleteDate = DateTime.UtcNow;
        }
    }
}
