using System.ComponentModel.DataAnnotations;

namespace GymTEC.API.Models
{
    public class Sucursal
    {
        [Key]
        public int IdSucursal { get; set; }

        [Required]
        public string NombreSucursal { get; set; } = string.Empty;

        [Required]
        public string Provincia { get; set; } = string.Empty;

        [Required]
        public string Canton { get; set; } = string.Empty;

        [Required]
        public string Distrito { get; set; } = string.Empty;

        [Required]
        public DateOnly OpeningDate { get; set; }

        public string Telefono { get; set; } = string.Empty;

        public int MaxCapacity { get; set; }

        public bool SpaEnabled { get; set; }

        public bool StoreEnabled { get; set; }

        // No poner FK ni [ForeignKey] aquí, solo la navegación opcional:
        public Administrador Administrador { get; set; }
    }
}