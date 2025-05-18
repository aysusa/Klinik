using Klinik.Context;
using Klinik.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Klinik.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Doctor> Doctors = _context.Doctors.Include(doctor => doctor.Department).ToList();
                  
            return View(Doctors);
        }
    }
}
