using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebEmployeeManagement.Infrastructures.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "社員名を入力してください。")]
        [Display(Name = "社員名")]
        public string EmployeeName { get; set; } = string.Empty;

        [Required(ErrorMessage = "部門を選択してください。")]
        [Display(Name = "部門")]
        public int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }
    }
}
