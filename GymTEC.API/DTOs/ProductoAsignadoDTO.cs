namespace GymTEC.API.DTOs.Producto
{
    public class ProductoAsignadoDTO
    {
        public string CodigoBarras { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Costo { get; set; }
    }
}
