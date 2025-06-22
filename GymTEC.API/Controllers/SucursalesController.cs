using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GymTEC.API.Data;
using GymTEC.API.Models;
using GymTEC.API.DTOs;

namespace GymTEC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SucursalesController : ControllerBase
    {
        private readonly GymTECDbContext _context;

        public SucursalesController(GymTECDbContext context)
        {
            _context = context;
        }

        // GET: api/Sucursales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sucursal>>> GetBranches()
        {
            return await _context.Sucursal.ToListAsync();
        }

        // GET: api/Sucursales/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sucursal>> GetSucursal(int id)
        {
            var sucursal = await _context.Sucursal.FindAsync(id);

            if (sucursal == null)
            {
                return NotFound();
            }

            return sucursal;
        }

        // PUT: api/Sucursales/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSucursal(int id, Sucursal sucursal)
        {
            if (id != sucursal.IdSucursal)
            {
                return BadRequest();
            }

            _context.Entry(sucursal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SucursalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Sucursales
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostSucursal([FromBody] RegistroSucursalRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var admin = await _context.Administrador
                .FirstOrDefaultAsync(a => a.NumCedula == request.NumCedulaAdministrador);

            if (admin == null)
                return BadRequest("El administrador no existe.");

            if (admin.IdSucursal != null)
                return BadRequest("El administrador ya administra una sucursal.");

            var sucursal = new Sucursal
            {
                NombreSucursal = request.NombreSucursal,
                Provincia = request.Provincia,
                Canton = request.Canton,
                Distrito = request.Distrito,
                OpeningDate = DateOnly.FromDateTime(request.OpeningDate),
                Telefono = request.Telefono,
                MaxCapacity = request.MaxCapacity,
                SpaEnabled = false,
                StoreEnabled = false
            };

            _context.Sucursal.Add(sucursal);
            await _context.SaveChangesAsync();

            admin.IdSucursal = sucursal.IdSucursal;
            await _context.SaveChangesAsync();

            return Ok("Sucursal registrada correctamente y administrador asignado.");
        }


        // DELETE: api/Sucursales/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSucursal(int id)
        {
            var sucursal = await _context.Sucursal.FindAsync(id);
            if (sucursal == null)
            {
                return NotFound();
            }

            _context.Sucursal.Remove(sucursal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/Sucursales/ActivarSpa/5
        [HttpPut("ActivarSpa/{id}")]
        public async Task<IActionResult> ActivarSpa(int id)
        {
            var sucursal = await _context.Sucursal.FindAsync(id);
            if (sucursal == null)
                return NotFound("Sucursal no encontrada.");

            sucursal.SpaEnabled = true;
            await _context.SaveChangesAsync();

            return Ok("Spa activado correctamente.");
        }

        // PUT: api/Sucursales/ActivarTienda/5
        [HttpPut("ActivarTienda/{id}")]
        public async Task<IActionResult> ActivarTienda(int id)
        {
            var sucursal = await _context.Sucursal.FindAsync(id);
            if (sucursal == null)
                return NotFound("Sucursal no encontrada.");

            sucursal.StoreEnabled = true;
            await _context.SaveChangesAsync();

            return Ok("Tienda activada correctamente.");
        }


        private bool SucursalExists(int id)
        {
            return _context.Sucursal.Any(e => e.IdSucursal == id);
        }
    }
}
