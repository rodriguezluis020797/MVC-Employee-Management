using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models.CoreModels
{
    [Table("EmployeeTimeEntry")]
    public class EmployeeTimeEntryModel
    {
        [Key] public long EmployeeTimeEntryId { get; set; }
        [ForeignKey("Employee")] public long EmployeeId { get; set; }
        public DateTime InDate { get; set; }
        public DateTime OutDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        [NotMapped] public virtual EmployeeModel Employee { get; set; }
    }
}
