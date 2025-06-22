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
    public class ClientesController : ControllerBase
    {
        private readonly GymTECDbContext _context;

        public ClientesController(GymTECDbContext context)
        {
            _context = context;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetCliente()
        {
            return await _context.Cliente.ToListAsync();
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(string id)
        {
            var cliente = await _context.Cliente.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(string id, Cliente cliente)
        {
            if (id != cliente.NumCedula)
            {
                return BadRequest();
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
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

        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("registro")]
        public async Task<IActionResult> RegistrarCliente([FromBody] RegistroClienteRequest request)
        {
            var persona = new Persona
            {
                NumCedula = request.NumCedula,
                Nombre = request.Nombre,
                Apellido1 = request.Apellido1,
                Apellido2 = request.Apellido2,
                FechaNacimiento = request.FechaNacimiento.ToDateOnly(),
                Provincia = request.Provincia,
                Canton = request.Canton,
                Distrito = request.Distrito,
                CorreoElectronico = request.CorreoElectronico,
                Password = Utilidades.EncriptarMD5(request.Password)
            };

            var cliente = new Cliente
            {
                NumCedula = request.NumCedula
            };

            _context.Personas.Add(persona);
            _context.Cliente.Add(cliente);
            _context.PersonaxRol.Add(new PersonaxRol
            {
                NumCedula = request.NumCedula,
                IdRol = 1 // ID del rol "Cliente"
            });

            await _context.SaveChangesAsync();
            return Ok(new { message = "Cliente registrado correctamente" });
        }


        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(string id)
        {
            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClienteExists(string id)
        {
            return _context.Cliente.Any(e => e.NumCedula == id);
        }
    }
}
