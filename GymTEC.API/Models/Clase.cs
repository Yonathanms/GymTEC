using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymTEC.API.Models
{
    [Table("Clase")]
    public class Clase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("tipo")]
        public string Tipo { get; set; }

        [Column("numcedulainstructor")]
        public string NumCedulaInstructor { get; set; }

        [Column("esgrupal")]
        public bool EsGrupal { get; set; }

        [Column("capacidad")]
        public int Capacidad { get; set; }

        [Column("fecha")]
        public DateOnly Fecha { get; set; }

        [Column("horainicio")]
        public TimeOnly HoraInicio { get; set; }

        [Column("horafinal")]
        public TimeOnly HoraFinal { get; set; }
    }
}
