using System.ComponentModel.DataAnnotations.Schema;

[Table("horaslaboradas")]
public class HorasLaboradas
{
    [Column("numcedula")]
    public string NumCedula { get; set; }

    [Column("cantidadhoras")]
    public int CantidadHoras { get; set; }
}
