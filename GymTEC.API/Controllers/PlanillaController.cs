using GymTEC.API.Data;
using GymTEC.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymTEC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlanillaController : ControllerBase
    {
        private readonly GymTECDbContext _context;

        public PlanillaController(GymTECDbContext context)
        {
            _context = context;
        }

        [HttpPost("HorasLaboradas")]
        public async Task<IActionResult> AsignarHoras([FromBody] AsignarHorasRequest request)
        {
            var empleadoExiste = await _context.Empleados.AnyAsync(e => e.NumCedula == request.NumCedula);
            if (!empleadoExiste) return NotFound("Empleado no encontrado.");

            var registro = new HorasLaboradas
            {
                NumCedula = request.NumCedula,
                CantidadHoras = request.CantidadHoras
            };

            _context.HorasLaboradas.Add(registro);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Horas asignadas exitosamente." });
        }

        [HttpPost("asignar-clases")]
        public async Task<IActionResult> AsignarClases([FromBody] AsignarClasesRequest request)
        {
            var empleadoExiste = await _context.Empleados.AnyAsync(e => e.NumCedula == request.NumCedula);
            if (!empleadoExiste)
            {
                return NotFound("Empleado no encontrado");
            }

            await _context.Database.ExecuteSqlRawAsync(
                "INSERT INTO clasesimpartidas (numcedula, cantidadclases) VALUES ({0}, {1}) " +
                "ON CONFLICT (numcedula) DO UPDATE SET cantidadclases = EXCLUDED.cantidadclases",
                request.NumCedula, request.CantidadClases);

            return Ok("Clases asignadas correctamente.");
        }


        [HttpGet("Generar")]
        public async Task<IActionResult> GenerarPlanilla()
        {
            var resultados = await _context
                .PlanillaResultado
                .FromSqlRaw("SELECT * FROM GenerarPlanilla()")
                .ToListAsync();

            return Ok(resultados);
        }
    }
}
