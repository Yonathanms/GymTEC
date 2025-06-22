namespace GymTEC.API.Models
{
    public class CrearClaseRequest
    {
        public string Tipo { get; set; } = null!;
        public string NumCedulaInstructor { get; set; } = null!;
        public bool EsGrupal { get; set; }
        public int Capacidad { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFinal { get; set; }
    }
}
