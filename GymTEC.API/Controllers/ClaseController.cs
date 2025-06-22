using GymTEC.API.Data;
using GymTEC.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymTEC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClaseController : ControllerBase
    {
        private readonly GymTECDbContext _context;

        public ClaseController(GymTECDbContext context)
        {
            _context = context;
        }

        [HttpPost("crear")]
        public async Task<IActionResult> CrearClase([FromBody] CrearClaseRequest request)
        {
            // Validar que exista el empleado y que sea instructor
            var instructor = await _context.Empleados.FirstOrDefaultAsync(e => e.NumCedula == request.NumCedulaInstructor);

            if (instructor == null)
            {
                return NotFound(new { mensaje = "No se encontró el instructor con la cédula proporcionada." });
            }

            if (instructor.PuestoId != 3) // Asumimos que 3 es el ID de "Instructor"
            {
                return BadRequest(new { mensaje = "El empleado con esa cédula no existe." });
            }

            var clase = new Clase
            {
                Tipo = request.Tipo,
                NumCedulaInstructor = request.NumCedulaInstructor,
                EsGrupal = request.EsGrupal,
                Capacidad = request.Capacidad,
                Fecha = DateOnly.FromDateTime(request.Fecha),
                HoraInicio = TimeOnly.FromTimeSpan(request.HoraInicio),
                HoraFinal = TimeOnly.FromTimeSpan(request.HoraFinal),

            };

            _context.Clases.Add(clase);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Clase creada exitosamente." });
        }
    }
}
