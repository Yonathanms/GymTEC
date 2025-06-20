using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymTEC.API.Models
{
    public class Administrador
    {
        [Key]
        [Required]
        public string NumCedula { get; set; } // PK y FK a Persona

        public int? IdSucursal { get; set; } // FK a Sucursal

        [Required]
        public string TipoPlanilla { get; set; } // Tipo de planilla del administrador

        [Required]
        public int Salario { get; set; }

        [ForeignKey("NumCedula")]
        public Persona Persona { get; set; }

        [ForeignKey("IdSucursal")]
        public Sucursal Sucursal { get; set; }
    }
}