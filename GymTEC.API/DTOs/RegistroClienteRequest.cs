namespace GymTEC.API.DTOs
{
    // DTO para recibir los datos del registro de cliente
    public class RegistroClienteRequest
    {
        // Datos de Persona
        public string NumCedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public DateOnly FechaNacimiento { get; set; }
        public string Provincia { get; set; }
        public string Canton { get; set; }
        public string Distrito { get; set; }
        public string CorreoElectronico { get; set; }
        public string Password { get; set; }

        // Datos de Cliente
        public decimal Peso { get; set; }
        public decimal IMC { get; set; }
    }
}
