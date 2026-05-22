using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DepartmentList.Models;

public class Department
{
    public int DepartmentId { get; set; }

    [Required(ErrorMessage = "部門名を入力してください。")]
    [Display(Name = "部門名")]
    public string DepartmentName { get; set; } = string.Empty;
}