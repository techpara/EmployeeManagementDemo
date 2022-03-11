#nullable disable
using EmployeeManagementWebApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementWebApp.Controllers
{
    public class EmployeeController : Controller
    {
        public EmployeeController(EmployeeManagementContext context)
        {
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            return View();
        }
    }
}
