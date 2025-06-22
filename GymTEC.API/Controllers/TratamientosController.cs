using GymTEC.API.Data;
using GymTEC.API.DTOs;
using GymTEC.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymTEC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TratamientosController : ControllerBase
    {
        private readonly GymTECDbContext _context;

        public TratamientosController(GymTECDbContext context)
        {
            _context = context;
        }

        // GET: api/Tratamientos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TratamientoResponse>>> GetTratamientos()
        {
            var tratamientos = await _context.Tratamientos
                .Select(t => new TratamientoResponse
                {
                    IdTratamiento = t.IdTratamiento,
                    Nombre = t.Nombre,
                    EsPorDefecto = t.EsPorDefecto
                })
                .ToListAsync();

            return Ok(tratamientos);
        }

        // POST: api/Tratamientos
        [HttpPost]
        public async Task<ActionResult> CrearTratamiento([FromBody] TratamientoRequest request)
        {
            var nuevo = new Tratamiento
            {
                Nombre = request.Nombre,
                EsPorDefecto = false // el admin solo puede crear tratamientos personalizados
            };

            _context.Tratamientos.Add(nuevo);
            await _context.SaveChangesAsync();

            return Ok("Tratamiento creado correctamente.");
        }

        // PUT: api/Tratamientos/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> ModificarTratamiento(int id, [FromBody] TratamientoRequest request)
        {
            var tratamiento = await _context.Tratamientos.FindAsync(id);
            if (tratamiento == null)
                return NotFound("Tratamiento no encontrado.");

            tratamiento.Nombre = request.Nombre;

            try
            {
                await _context.SaveChangesAsync(); // el trigger bloqueará si es por defecto
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex.InnerException?.Message ?? ex.Message);
            }

            return Ok("Tratamiento modificado correctamente.");
        }

        // DELETE: api/Tratamientos/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarTratamiento(int id)
        {
            var tratamiento = await _context.Tratamientos.FindAsync(id);
            if (tratamiento == null)
                return NotFound("Tratamiento no encontrado.");

            _context.Tratamientos.Remove(tratamiento);

            try
            {
                await _context.SaveChangesAsync(); // el trigger bloqueará si no se puede eliminar
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex.InnerException?.Message ?? ex.Message);
            }

            return Ok("Tratamiento eliminado correctamente.");
        }
    }
}
