using Elfie.Serialization;
using Klinik.Context;
using Klinik.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Klinik.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DoctorController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webEnvironment;        
        public DoctorController(AppDbContext context, IWebHostEnvironment webEnvironment)
        {
            _context = context;
            _webEnvironment = webEnvironment;
        }
        public IActionResult Index()
        {
            var doctors = _context.Doctors.Include(doctor => doctor.Department).ToList();

            return View(doctors);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Doctor doctor)
        {
            if (!doctor.ImgUpload.ContentType.Contains("image"))
            {
                ModelState.AddModelError("ImgUpload", "File must be Image Format");
                    return View(doctor);
            }
            string filename =Guid.NewGuid() + doctor.ImgUpload.FileName;
            string path =_webEnvironment.WebRootPath+ @"\UploadImage\Doctor\";
            using (FileStream fileStream = new FileStream(path+filename, FileMode.Create))
            {
                doctor.ImgUpload.CopyTo(fileStream);
            }
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
            _context.Doctors.Add(doctor);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            Doctor? doctor = _context.Doctors.Find(id);
            if (doctor == null) { return NotFound("Doctor not found"); }
            _context.Doctors.Remove(doctor);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int Id)
        {
            _context.Doctors.Find(Id);
            Doctor? existingDoctor = _context.Doctors.Find(Id);
            if (existingDoctor == null) { return NotFound("Doctor not found"); }
            return View(nameof(Create), existingDoctor);
        }
        [HttpPost]
        public IActionResult Update(Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Name", "Name mutleq yazilmalidir");
                return View(nameof(Create), doctor);
            }
            Doctor existingDoctor = _context.Doctors.FirstOrDefault(c => c.Id == doctor.Id);
            if (existingDoctor == null) { return NotFound("Category could not be found"); }
            existingDoctor.Name = doctor.Name;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
    
           
