namespace GymTEC.API.DTOs
{
    public class RegistroSucursalRequest
    {
        public string NombreSucursal { get; set; } = string.Empty;
        public string Provincia { get; set; } = string.Empty;
        public string Canton { get; set; } = string.Empty;
        public string Distrito { get; set; } = string.Empty;
        public DateTime OpeningDate { get; set; }
        public string Telefono { get; set; } = string.Empty;
        public int MaxCapacity { get; set; }
        public string NumCedulaAdministrador { get; set; } = string.Empty;
    }
}
