using Klinik.Context;
using Klinik.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Klinik.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DepartmentController : Controller
    {
        private readonly AppDbContext _context;
        public DepartmentController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var departments = _context.Departments.Include(doctor => doctor.Doctors).ToList();

            return View(departments);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _context.Departments.Add(department);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            Department? department = _context.Departments.Find(id);
            if (department == null) { return NotFound("Department not found"); }
            _context.Departments.Remove(department);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int Id)
        {
            _context.Departments.Find(Id);
            Department? existingDepartment = _context.Departments.Find(Id);
            if (existingDepartment == null) { return NotFound("Department not found"); }
            return View(nameof(Create), existingDepartment);
        }
        [HttpPost]
        public IActionResult Update(Department department)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Title", "Title mutleq yazilmalidir");
                return View(nameof(Create),department);
            }
            Department existingDepartment = _context.Departments.FirstOrDefault(c => c.Id == department.Id);
            if (existingDepartment == null) { return NotFound("Category could not be found"); }
            existingDepartment.Title = department.Title;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
