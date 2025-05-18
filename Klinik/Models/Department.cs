using System.ComponentModel.DataAnnotations;

namespace Klinik.Models
{
    public class Department:BaseModels
    {
        public string Title { get; set; }
        [Required,MinLength(3),MaxLength]
        public ICollection<Doctor>? Doctors { get; set; }

            
    }
}
