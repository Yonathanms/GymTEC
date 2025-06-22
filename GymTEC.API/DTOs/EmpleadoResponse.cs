namespace GymTEC.API.DTOs
{
    public class EmpleadoResponse
    {
        public string NumCedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Provincia { get; set; }
        public string Canton { get; set; }
        public string Distrito { get; set; }
        public int PuestoId { get; set; }
        public int? IdSucursal { get; set; }
        public int TipoPlanillaId { get; set; }
        public int Salario { get; set; }
        public string CorreoElectronico { get; set; }
    }
}
