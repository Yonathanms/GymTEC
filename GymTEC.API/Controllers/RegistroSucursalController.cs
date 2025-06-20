using GymTEC.API.Data;
using GymTEC.API.DTOs;
using GymTEC.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GymTEC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroSucursalController : ControllerBase
    {
        private readonly GymTECDbContext _context;

        public RegistroSucursalController(GymTECDbContext context)
        {
            _context = context;
        }

        // POST: api/RegistroSucursal
        [HttpPost]
        public async Task<IActionResult> RegistrarSucursal([FromBody] RegistroSucursalRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Buscar el admin y validar que no administre otra sucursal
            var admin = await _context.Administrador
                .FirstOrDefaultAsync(a => a.NumCedula == request.NumCedulaAdministrador);

            if (admin == null)
                return BadRequest("El administrador no existe.");

            if (admin.IdSucursal != null)
                return BadRequest("El administrador ya administra una sucursal.");

            // Crear la sucursal (sin administrador todavía)
            var sucursal = new Sucursal
            {
                NombreSucursal = request.NombreSucursal,
                Provincia = request.Provincia,
                Canton = request.Canton,
                Distrito = request.Distrito,
                OpeningDate = request.OpeningDate,
                Telefono = request.Telefono,
                MaxCapacity = request.MaxCapacity,
                SpaEnabled = request.SpaEnabled,
                StoreEnabled = request.StoreEnabled
            };

            // Agregar y guardar para obtener el IdSucursal
            _context.Sucursal.Add(sucursal);
            await _context.SaveChangesAsync();

            // Asignar el IdSucursal al administrador
            admin.IdSucursal = sucursal.IdSucursal;
            await _context.SaveChangesAsync();

            return Ok("Sucursal registrada correctamente y administrador asignado.");
        }
    }
}