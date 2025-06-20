using System.ComponentModel.DataAnnotations;

namespace GymTEC.API.DTOs
{
    // DTO para registrar una sucursal
    public class RegistroSucursalRequest
    {
        [Required]
        public string NombreSucursal { get; set; }

        [Required]
        public string Provincia { get; set; }

        [Required]
        public string Canton { get; set; }

        [Required]
        public string Distrito { get; set; }

        [Required]
        public DateOnly OpeningDate { get; set; }

        [Required]
        public string Telefono { get; set; }

        [Required]
        public int MaxCapacity { get; set; }

        [Required]
        public bool SpaEnabled { get; set; }

        [Required]
        public bool StoreEnabled { get; set; }

        // Cédula del administrador que se asignará a la sucursal (debe estar libre)
        [Required(ErrorMessage = "Debe ingresar la cédula de un administrador libre.")]
        public string NumCedulaAdministrador { get; set; }
    }
}