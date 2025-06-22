using System.ComponentModel.DataAnnotations.Schema;

namespace GymTEC.API.Models
{
    [Table("tratamientosucursal")]
    public class TratamientoSucursal
    {
        [Column("idsucursal")]
        public int IdSucursal { get; set; }

        [Column("idtratamiento")]
        public int IdTratamiento { get; set; }

        [ForeignKey("IdTratamiento")]
        public Tratamiento? Tratamiento { get; set; }

        [ForeignKey("IdSucursal")]
        public Sucursal? Sucursal { get; set; }
    }
}
