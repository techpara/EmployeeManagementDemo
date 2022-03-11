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

namespace DepartmentManagementWebApp.Api.Controllers
{
    [Route("api/department")]
    [ApiController]
    public class DepartmentAPIController : ControllerBase
    {
        private readonly EmployeeManagementContext _context;

        public DepartmentAPIController(EmployeeManagementContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<IEnumerable<Departments>>> GetDepartments()
        {
            return await _context.Departments.ToListAsync();
        }

        [HttpGet]
        [Route("details/{id}")]
        public async Task<ActionResult<Departments>> GetDepartments(int id)
        {
            var Departments = await _context.Departments.FindAsync(id);

            if (Departments == null)
            {
                return NotFound();
            }

            return Departments;
        }

        // PUT: api/DepartmentAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Route("updateDepartment/{id}")]
        public async Task<IActionResult> PutDepartments(int id, [FromForm] Departments Departments)
        {
            if (id != Departments.ID)
            {
                return BadRequest();
            }

            _context.Entry(Departments).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentsExists(id))
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

        [HttpPost]
        [Route("createDepartment")]
        public async Task<ActionResult<Departments>> PostDepartments([FromForm] Departments Departments)
        {
            _context.Departments.Add(Departments);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDepartments", new { id = Departments.ID }, Departments);
        }

        [HttpDelete]
        [Route("deleteDepartment/{id}")]
        public async Task<IActionResult> DeleteDepartments(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            var departmentEmployees = _context.Employees.Where(x => x.DepartmentID == id).ToArrayAsync();
            _context.Employees.RemoveRange(departmentEmployees.Result);

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DepartmentsExists(int id)
        {
            return _context.Departments.Any(e => e.ID == id);
        }
    }
}
