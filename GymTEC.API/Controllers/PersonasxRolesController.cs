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
    public class PersonasxRolesController : ControllerBase
    {
        private readonly GymTECDbContext _context;

        public PersonasxRolesController(GymTECDbContext context)
        {
            _context = context;
        }

        // GET: api/PersonasxRoles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonaxRol>>> GetPersonaxRol()
        {
            return await _context.PersonaxRol.ToListAsync();
        }

        // GET: api/PersonasxRoles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonaxRol>> GetPersonaxRol(string id)
        {
            var personaxRol = await _context.PersonaxRol.FindAsync(id);

            if (personaxRol == null)
            {
                return NotFound();
            }

            return personaxRol;
        }

        // PUT: api/PersonasxRoles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonaxRol(string id, PersonaxRol personaxRol)
        {
            if (id != personaxRol.NumCedula)
            {
                return BadRequest();
            }

            _context.Entry(personaxRol).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonaxRolExists(id))
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

        // POST: api/PersonasxRoles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PersonaxRol>> PostPersonaxRol(PersonaxRol personaxRol)
        {
            _context.PersonaxRol.Add(personaxRol);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PersonaxRolExists(personaxRol.NumCedula))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPersonaxRol", new { id = personaxRol.NumCedula }, personaxRol);
        }

        // DELETE: api/PersonasxRoles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonaxRol(string id)
        {
            var personaxRol = await _context.PersonaxRol.FindAsync(id);
            if (personaxRol == null)
            {
                return NotFound();
            }

            _context.PersonaxRol.Remove(personaxRol);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonaxRolExists(string id)
        {
            return _context.PersonaxRol.Any(e => e.NumCedula == id);
        }
    }
}
