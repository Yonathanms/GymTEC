// Modelo de la tabla Clientes

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymTEC.API.Models
{
    public class Cliente
    {
        [Key]
        public string NumCedula { get; set; } // Clave primaria y foránea

        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public decimal Peso { get; set; }  // Peso de la persona

        [Required]
        [Column(TypeName = "decimal(4,2)")]
        public decimal IMC { get; set; }  // Índice de Masa Corporal (IMC) de la persona

        // Propiedad de navegación
        [ForeignKey("NumCedula")]
        public Persona Persona { get; set; } // Relación con la entidad Persona, clave foránea a NumCedula
    }
}
