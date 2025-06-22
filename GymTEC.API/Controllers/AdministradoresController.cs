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
using GymTEC.API.Utils;


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

        [HttpPost("registro")]
        public async Task<IActionResult> RegistrarAdministrador([FromBody] RegistroAdministradorRequest request)
        {
            var persona = new Persona
            {
                NumCedula = request.NumCedula,
                Nombre = request.Nombre,
                Apellido1 = request.Apellido1,
                Apellido2 = request.Apellido2,
                FechaNacimiento = DateOnly.FromDateTime(request.FechaNacimiento),
                Provincia = request.Provincia,
                Canton = request.Canton,
                Distrito = request.Distrito,
                CorreoElectronico = request.CorreoElectronico,
                Password = Utilidades.EncriptarMD5(request.Password)
            };

            var administrador = new Administrador
            {
                NumCedula = request.NumCedula,
                TipoPlanilla = request.TipoPlanilla,
                Salario = request.Salario
            };

            _context.Personas.Add(persona);
            _context.Administrador.Add(administrador);
            _context.PersonaxRol.Add(new PersonaxRol
            {
                NumCedula = request.NumCedula,
                IdRol = 3 // ID del rol "Administrador"
            });

            await _context.SaveChangesAsync();
            return Ok(new { message = "Administrador registrado correctamente" });
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
