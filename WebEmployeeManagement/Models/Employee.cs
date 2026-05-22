using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeList.Models;

public class Employee
{
    public int EmployeeId { get; set; }

    [Required(ErrorMessage = "社員番号を入力してください。")]
    [Display(Name = "社員番号")]
    public string EmployeeName { get; set; } = string.Empty;
    [Required(ErrorMessage = "社員名を入力してください。")]
    [Display(Name = "社員名")]

    public int DepartmentId { get; set; }
}