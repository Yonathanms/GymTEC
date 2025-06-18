//Modelo de la tabla Rol

using System.ComponentModel.DataAnnotations;

namespace GymTEC.API.Models
{
    public class Rol
    {
        [Key]
        public int IdRol { get; set; } // Clave primaria para el rol
        [Required]
        public string NombreRol { get; set; } = string.Empty; // Nombre del rol, requerido
    }
}
