using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementWebApp.Models
{
    public class Employees
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Salary { get; set; }
        public string Bio { get; set; }
        public string Photo { get; set; }
        public int DepartmentID { get; set; }
        public Departments Department { get; set; }
    }

    public class EmployeesDTO
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Salary { get; set; }
        public string Bio { get; set; }
        public string Photo { get; set; }
        public int DepartmentID { get; set; }

        public IFormFile? EmployeeImage { get; set; }
    }
}
