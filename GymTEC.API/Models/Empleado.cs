using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymTEC.API.Models
{
    [Table("empleado")]
    public class Empleado
    {
        [Key]
        [Column("numcedula")]
        public string NumCedula { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("apellido1")]
        public string Apellido1 { get; set; }

        [Column("apellido2")]
        public string Apellido2 { get; set; }

        [Column("provincia")]
        public string Provincia { get; set; }

        [Column("canton")]
        public string Canton { get; set; }

        [Column("distrito")]
        public string Distrito { get; set; }

        [Column("puestoid")]
        public int PuestoId { get; set; }

        [Column("idsucursal")]
        public int? IdSucursal { get; set; }

        [Column("tipoplanillaid")]
        public int TipoPlanillaId { get; set; }

        [Column("salario")]
        public int Salario { get; set; }

        [Column("correoelectronico")]
        public string CorreoElectronico { get; set; }

        [Column("password")]
        public string Password { get; set; }
    }
}
