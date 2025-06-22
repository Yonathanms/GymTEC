using Microsoft.AspNetCore.Mvc;
using GymTEC.API.Models;
using GymTEC.API.DTOs;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using GymTEC.API.Data;

namespace GymTEC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpleadoController : ControllerBase
    {
        private readonly GymTECDbContext _context;

        public EmpleadoController(GymTECDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmpleadoResponse>>> GetEmpleados()
        {
            var empleados = await _context.Empleados
                .Select(e => new EmpleadoResponse
                {
                    NumCedula = e.NumCedula,
                    Nombre = e.Nombre,
                    Apellido1 = e.Apellido1,
                    Apellido2 = e.Apellido2,
                    Provincia = e.Provincia,
                    Canton = e.Canton,
                    Distrito = e.Distrito,
                    PuestoId = e.PuestoId,
                    IdSucursal = e.IdSucursal,
                    TipoPlanillaId = e.TipoPlanillaId,
                    Salario = e.Salario,
                    CorreoElectronico = e.CorreoElectronico
                })
                .ToListAsync();

            return Ok(empleados);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmpleado(EmpleadoRequest request)
        {
            var hashedPassword = Convert.ToHexString(MD5.HashData(Encoding.UTF8.GetBytes(request.Password)));

            var empleado = new Empleado
            {
                NumCedula = request.NumCedula,
                Nombre = request.Nombre,
                Apellido1 = request.Apellido1,
                Apellido2 = request.Apellido2,
                Provincia = request.Provincia,
                Canton = request.Canton,
                Distrito = request.Distrito,
                PuestoId = request.PuestoId,
                IdSucursal = request.IdSucursal,
                TipoPlanillaId = request.TipoPlanillaId,
                Salario = request.Salario,
                CorreoElectronico = request.CorreoElectronico,
                Password = hashedPassword
            };

            _context.Empleados.Add(empleado);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmpleados), new { id = empleado.NumCedula }, empleado);
        }

        [HttpDelete("{cedula}")]
        public async Task<IActionResult> DeleteEmpleado(string cedula)
        {
            var empleado = await _context.Empleados.FindAsync(cedula);
            if (empleado == null)
                return NotFound();

            _context.Empleados.Remove(empleado);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
