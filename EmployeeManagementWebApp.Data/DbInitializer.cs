using EmployeeManagementWebApp.Models;
using System.Linq;

namespace EmployeeManagementWebApp.Data
{
    public static class DbInitializer
    {
        public static void Initialize(EmployeeManagementContext context)
        {
            context.Database.EnsureCreated();

            if (context.Employees.Any())
            {
                return;   // DB has been seeded
            }

            var departments = new Departments[]
            {
                new Departments{ Name = "Department 1" , Desciption = "Dept. 1 Description."},
                new Departments{ Name = "Department 2" , Desciption = "Dept. 2 Description."},
                new Departments{ Name = "Department 3" , Desciption = "Dept. 3 Description."}
            };

            context.Departments.AddRange(departments);
            context.SaveChanges();

            var employees = new Employees[]
            {
                new Employees{FirstName = "Emp 1 First Name", LastName = "Emp 1 Last Name", Bio="Emp 1 Bio" , DepartmentID=1, Email="emp1@test.com", PhoneNumber="111-222-3333", Photo="1.png", Salary=100},
                new Employees{FirstName = "Emp 2 First Name", LastName = "Emp 2 Last Name", Bio="Emp 2 Bio" , DepartmentID=2, Email="emp2@test.com", PhoneNumber="444-222-3333", Photo="2.png", Salary=200}

            };

            context.Employees.AddRange(employees);
            context.SaveChanges();
        }
    }
}

