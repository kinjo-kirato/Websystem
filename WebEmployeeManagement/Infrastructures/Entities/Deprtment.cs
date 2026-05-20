using System.ComponentModel.DataAnnotations;

namespace WebEmployeeManagement.Infrastructures.Entities
{
    public class Department
    {
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "部門名を入力してください。")]
        [Display(Name = "部門名")]
        public string DepartmentName { get; set; } = string.Empty;

    }
}