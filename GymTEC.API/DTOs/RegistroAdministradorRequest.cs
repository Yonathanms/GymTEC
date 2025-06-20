using System.ComponentModel.DataAnnotations;

namespace GymTEC.API.DTOs
{
    // DTO para recibir los datos del registro de administrador (campos planos, igual que cliente)
    public class RegistroAdministradorRequest
    {
        // Datos de Persona
        [Required]
        public string NumCedula { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido1 { get; set; }

        [Required]
        public string Apellido2 { get; set; }

        [Required]
        public DateOnly FechaNacimiento { get; set; }

        [Required]
        public string Provincia { get; set; }

        [Required]
        public string Canton { get; set; }

        [Required]
        public string Distrito { get; set; }

        [Required]
        [EmailAddress]
        public string CorreoElectronico { get; set; }

        [Required]
        public string Password { get; set; }

        // Datos de Administrador
        public int? IdSucursal { get; set; } // Opcional

        [Required]
        [RegularExpression("^(Mensual|Xhora|Xclase)$", ErrorMessage = "TipoPlanilla debe ser 'Mensual', 'Xhora' o 'Xclase'.")]
        public string TipoPlanilla { get; set; }

        [Required]
        public int Salario { get; set; }
    }
}