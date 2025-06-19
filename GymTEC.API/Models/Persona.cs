//modelo de la tabla personas

using System.ComponentModel.DataAnnotations;

namespace GymTEC.API.Models
{
    public class Persona
    {
        [Key]
        public string NumCedula { get; set; } // Número de cédula, clave primaria

        [Required]
        public string Nombre { get; set; } // Nombre de la persona

        [Required]
        public string Apellido1 { get; set; } // Primer apellido de la persona

        [Required]
        public string Apellido2 { get; set; } // Segundo apellido de la persona

        [Required]
        public DateOnly FechaNacimiento { get; set; } // Fecha de nacimiento de la persona

        [Required]
        public string Provincia { get; set; } // Provincia de residencia

        [Required]
        public string Canton { get; set; } // Cantón de residencia

        [Required]
        public string Distrito { get; set; } // Distrito de residencia

        [Required]
        public string CorreoElectronico { get; set; } // Correo electrónico de la persona

        [Required]
        public string Password { get; set; } // Contraseña de la persona
    }
}
