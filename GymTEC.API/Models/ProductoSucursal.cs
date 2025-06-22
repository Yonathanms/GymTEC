using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GymTEC.API.Models;

[Table("productosucursal")]
public class ProductoSucursal
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("codigobarras")]
    public string CodigoBarras { get; set; }

    [ForeignKey("CodigoBarras")]
    public Producto Producto { get; set; }

    [Required]
    [Column("idsucursal")]
    public int IdSucursal { get; set; }

    [ForeignKey("IdSucursal")]
    public Sucursal Sucursal { get; set; }
}

