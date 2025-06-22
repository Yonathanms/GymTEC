namespace GymTEC.API.DTOs
{
    public class TratamientoResponse
    {
        public int IdTratamiento { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public bool EsPorDefecto { get; set; }
    }
}
