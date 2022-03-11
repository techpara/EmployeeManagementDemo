#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementWebApp.Models;
using EmployeeManagementWebApp.Data;

namespace EmployeeManagementWebApp.Controllers
{
    public class DepartmentController : Controller
    {
        public DepartmentController(EmployeeManagementContext context)
        {
        }

        // GET: Employee
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: Employee/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            return View();
        }

    }
}
