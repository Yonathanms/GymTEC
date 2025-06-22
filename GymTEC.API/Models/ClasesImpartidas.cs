using System.ComponentModel.DataAnnotations.Schema;

[Table("clasesimpartidas")]
public class ClasesImpartidas
{
    public int Id { get; set; }

    [Column("numcedula")]
    public string NumCedula { get; set; }

    [Column("cantidadclases")]
    public int CantidadClases { get; set; }
}
