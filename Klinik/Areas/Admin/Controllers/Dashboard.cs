using Klinik.Context;
using Klinik.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Klinik.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class Dashboard : Controller
    {
        private readonly AppDbContext _context;
        public Dashboard(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var departments= _context.Departments.Include(doctor => doctor.Doctors).ToList();

            return View(departments);
        }
       
            
    }
}
