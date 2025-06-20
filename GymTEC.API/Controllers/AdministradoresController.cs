using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GymTEC.API.Data;
using GymTEC.API.Models;

namespace GymTEC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministradoresController : ControllerBase
    {
        private readonly GymTECDbContext _context;

        public AdministradoresController(GymTECDbContext context)
        {
            _context = context;
        }

        // GET: api/Administradores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Administrador>>> GetAdministrador()
        {
            return await _context.Administrador.ToListAsync();
        }

        // GET: api/Administradores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Administrador>> GetAdministrador(string id)
        {
            var administrador = await _context.Administrador.FindAsync(id);

            if (administrador == null)
            {
                return NotFound();
            }

            return administrador;
        }

        // PUT: api/Administradores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdministrador(string id, Administrador administrador)
        {
            if (id != administrador.NumCedula)
            {
                return BadRequest();
            }

            _context.Entry(administrador).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdministradorExists(id))
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

        // POST: api/Administradores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Administrador>> PostAdministrador(Administrador administrador)
        {
            _context.Administrador.Add(administrador);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AdministradorExists(administrador.NumCedula))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAdministrador", new { id = administrador.NumCedula }, administrador);
        }

        // DELETE: api/Administradores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdministrador(string id)
        {
            var administrador = await _context.Administrador.FindAsync(id);
            if (administrador == null)
            {
                return NotFound();
            }

            _context.Administrador.Remove(administrador);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdministradorExists(string id)
        {
            return _context.Administrador.Any(e => e.NumCedula == id);
        }
    }
}
