namespace GymTEC.API.DTOs
{
    public class RegistroAdministradorRequest
    {
        public string NumCedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Provincia { get; set; }
        public string Canton { get; set; }
        public string Distrito { get; set; }
        public string CorreoElectronico { get; set; }
        public string Password { get; set; }
        public string TipoPlanilla { get; set; }
        public int Salario { get; set; }
    }
}
