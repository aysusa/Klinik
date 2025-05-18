using System.ComponentModel.DataAnnotations.Schema;

namespace Klinik.Models
{
    public class Doctor:BaseModels
    {
        public string? ImgUrl { get; set; }
        [NotMapped]
        public IFormFile? ImgUpload { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }

    }
}
