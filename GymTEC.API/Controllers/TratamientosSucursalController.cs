using GymTEC.API.Data;
using GymTEC.API.DTOs;
using GymTEC.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymTEC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TratamientosSucursalController : ControllerBase
    {
        private readonly GymTECDbContext _context;

        public TratamientosSucursalController(GymTECDbContext context)
        {
            _context = context;
        }

        // GET: api/TratamientosSucursal/Asociados/5
        [HttpGet("Asociados/{idSucursal}")]
        public async Task<ActionResult<IEnumerable<TratamientoSimpleResponse>>> GetTratamientosAsociados(int idSucursal)
        {
            var tratamientos = await _context.TratamientosSucursal
                .Where(ts => ts.IdSucursal == idSucursal)
                .Include(ts => ts.Tratamiento)
                .Select(ts => new TratamientoSimpleResponse
                {
                    IdTratamiento = ts.IdTratamiento,
                    Nombre = ts.Tratamiento!.Nombre
                })
                .ToListAsync();

            return Ok(tratamientos);
        }

        // GET: api/TratamientosSucursal/NoAsociados/5
        [HttpGet("NoAsociados/{idSucursal}")]
        public async Task<ActionResult<IEnumerable<TratamientoSimpleResponse>>> GetTratamientosNoAsociados(int idSucursal)
        {
            var asociados = await _context.TratamientosSucursal
                .Where(ts => ts.IdSucursal == idSucursal)
                .Select(ts => ts.IdTratamiento)
                .ToListAsync();

            var noAsociados = await _context.Tratamientos
                .Where(t => !asociados.Contains(t.IdTratamiento))
                .Select(t => new TratamientoSimpleResponse
                {
                    IdTratamiento = t.IdTratamiento,
                    Nombre = t.Nombre
                })
                .ToListAsync();

            return Ok(noAsociados);
        }

        // POST: api/TratamientosSucursal/Asociar
        [HttpPost("Asociar")]
        public async Task<ActionResult> AsociarTratamiento([FromBody] AsociarTratamientoRequest request)
        {
            var yaExiste = await _context.TratamientosSucursal
                .AnyAsync(ts => ts.IdSucursal == request.IdSucursal && ts.IdTratamiento == request.IdTratamiento);

            if (yaExiste)
                return BadRequest("Este tratamiento ya está asociado a esta sucursal.");

            _context.TratamientosSucursal.Add(new TratamientoSucursal
            {
                IdSucursal = request.IdSucursal,
                IdTratamiento = request.IdTratamiento
            });

            await _context.SaveChangesAsync();
            return Ok("Tratamiento asociado correctamente.");
        }

        // DELETE: api/TratamientosSucursal/Desasociar
        [HttpDelete("Desasociar")]
        public async Task<ActionResult> DesasociarTratamiento([FromBody] AsociarTratamientoRequest request)
        {
            var asociacion = await _context.TratamientosSucursal
                .FirstOrDefaultAsync(ts => ts.IdSucursal == request.IdSucursal && ts.IdTratamiento == request.IdTratamiento);

            if (asociacion == null)
                return NotFound("La asociación no existe.");

            _context.TratamientosSucursal.Remove(asociacion);
            await _context.SaveChangesAsync();
            return Ok("Tratamiento desasociado correctamente.");
        }
    }
}
