//Modelo de la tabla Personas x Roles

using System.ComponentModel.DataAnnotations.Schema;

namespace GymTEC.API.Models
{
    public class PersonaxRol
    {
        // Clave primaria compuesta y claves foráneas
        public string NumCedula { get; set; } // FK a Persona
        public int IdRol { get; set; }        // FK a Rol

        // Propiedades de navegación
        [ForeignKey("NumCedula")]
        public Persona Persona { get; set; }

        [ForeignKey("IdRol")]
        public Rol Rol { get; set; }
    }
}
