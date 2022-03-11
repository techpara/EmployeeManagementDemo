#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementWebApp.Models;
using EmployeeManagementWebApp.Data;

namespace EmployeeManagementWebApp.Api.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeAPIController : ControllerBase
    {
        private readonly EmployeeManagementContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public EmployeeAPIController(EmployeeManagementContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<IEnumerable<Employees>>> GetEmployees()
        {
            try
            {
                return await _context.Employees.Include(x => x.Department).ToListAsync();

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet]
        [Route("details/{id}")]
        public async Task<ActionResult<Employees>> GetEmployees(int id)
        {
            var employees = await _context.Employees.FindAsync(id);

            if (employees == null)
            {
                return NotFound();
            }

            return employees;
        }

        // PUT: api/EmployeeAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Route("updateEmployee/{id}")]
        //[Consumes("application/json", "application/json-patch+json", "multipart/form-data")]
        public async Task<IActionResult> PutEmployees(int id, [FromForm] EmployeesDTO employees)
        {
            if (id != employees.ID)
            {
                return BadRequest();
            }
            string fileName = "";
            if (employees.EmployeeImage != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "EmployeePhoto");
                fileName = $"{Guid.NewGuid().ToString()} {System.IO.Path.GetExtension(employees.EmployeeImage.FileName)}";
                string filePath = Path.Combine(uploadsFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    employees.EmployeeImage.CopyTo(fileStream);
                }
            }

            //Employees emp = new Employees();
            var emp = await _context.Employees.FindAsync(id);
            emp.FirstName = employees.FirstName;
            emp.LastName = employees.LastName;
            emp.Email = employees.Email;
            emp.PhoneNumber = employees.PhoneNumber;
            emp.Bio = employees.Bio;
            emp.Salary = employees.Salary;
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                emp.Photo = fileName;
            }
            emp.DepartmentID = employees.DepartmentID;
            _context.Entry(emp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/EmployeeAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("createEmployee")]
        public async Task<ActionResult<Employees>> PostEmployees([FromForm] EmployeesDTO employees)
        {
            string fileName = "";
            if (employees.EmployeeImage != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "EmployeePhoto");
                fileName = $"{Guid.NewGuid().ToString()} {System.IO.Path.GetExtension(employees.EmployeeImage.FileName)}";
                string filePath = Path.Combine(uploadsFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    employees.EmployeeImage.CopyTo(fileStream);
                }
            }

            Employees emp = new Employees();
            emp.FirstName = employees.FirstName;
            emp.LastName = employees.LastName;
            emp.Email = employees.Email;
            emp.PhoneNumber = employees.PhoneNumber;
            emp.Bio = employees.Bio;
            emp.Salary = employees.Salary;
            emp.Photo = fileName;
            emp.DepartmentID = employees.DepartmentID;
            _context.Employees.Add(emp);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployees", new { id = employees.ID }, employees);
        }

        // DELETE: api/EmployeeAPI/5
        [HttpDelete]
        [Route("deleteEmployee/{id}")]
        public async Task<IActionResult> DeleteEmployees(int id)
        {
            var employees = await _context.Employees.FindAsync(id);
            if (employees == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employees);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeesExists(int id)
        {
            return _context.Employees.Any(e => e.ID == id);
        }
    }
}
