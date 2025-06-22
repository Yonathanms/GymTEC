using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


[Table("producto")]
public class Producto
{
    [Key]
    [Column("codigobarras")]
    public string CodigoBarras { get; set; }

    [Column("nombre")]
    public string Nombre { get; set; }

    [Column("descripcion")]
    public string Descripcion { get; set; }

    [Column("costo")]
    public decimal Costo { get; set; }
}
