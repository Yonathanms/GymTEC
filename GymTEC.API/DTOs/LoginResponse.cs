namespace GymTEC.API.DTOs
{
    public class LoginResponse
    {
        public string NumCedula { get; set; }
        public string Nombre { get; set; }
        public int Rol { get; set; }
        public string RolNombre { get; set; }
        // Puedes agregar más campos si necesitas
    }
}
