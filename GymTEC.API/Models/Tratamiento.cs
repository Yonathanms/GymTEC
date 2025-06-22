using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymTEC.API.Models
{
    [Table("tratamiento")]
    public class Tratamiento
    {
        [Key]
        [Column("idtratamiento")]
        public int IdTratamiento { get; set; }

        [Required]
        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [Column("espordefecto")]
        public bool EsPorDefecto { get; set; }

        public ICollection<TratamientoSucursal> TratamientosSucursal { get; set; }
    }
}
