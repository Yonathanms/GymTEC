using System.ComponentModel.DataAnnotations;

namespace GymTEC.API.Models
{
    public class Branch
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Province { get; set; } = string.Empty;

        [Required]
        public string Canton { get; set; } = string.Empty;

        [Required]
        public string District { get; set; } = string.Empty;

        public DateTime OpeningDate { get; set; } 

        public string PhoneNumber { get; set; } = string.Empty;

        public int MaxCapacity { get; set; }

        public bool SpaEnabled { get; set; }

        public bool StoreEnabled { get; set; }
    }
}