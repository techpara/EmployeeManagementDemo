using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementWebApp.Models
{
    public class Departments
    {
        public int ID { get; set; }
        public string Name { get; set; }
        [MaxLength(200)]
        public string Desciption { get; set; }
    }
}
