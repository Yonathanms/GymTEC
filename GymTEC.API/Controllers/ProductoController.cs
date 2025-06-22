using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GymTEC.API.Models;
using GymTEC.API.Data;
using GymTEC.API.DTOs;
using GymTEC.API.DTOs.Producto;

namespace GymTEC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly GymTECDbContext _context;

        public ProductoController(GymTECDbContext context)
        {
            _context = context;
        }

        // POST: api/Producto
        [HttpPost]
        public async Task<IActionResult> CrearProducto([FromBody] ProductoRequest request)
        {
            var nuevo = new Producto
            {
                CodigoBarras = request.CodigoBarras,
                Nombre = request.Nombre,
                Descripcion = request.Descripcion,
                Costo = request.Costo
            };

            _context.Producto.Add(nuevo);
            await _context.SaveChangesAsync();
            return Ok(new { mensaje = "Producto creado exitosamente" });
        }

        // POST: api/Producto/Asignar
        [HttpPost("Asignar")]
        public async Task<IActionResult> AsignarProducto([FromBody] AsignarProductoSucursalRequest request)
        {
            var sucursal = await _context.Sucursal.FindAsync(request.IdSucursal);
            if (sucursal == null)
                return NotFound("Sucursal no encontrada");

            if (!sucursal.StoreEnabled)
                return BadRequest("La sucursal no tiene tienda activada");

            var yaAsignado = await _context.ProductoSucursal
                .AnyAsync(p => p.CodigoBarras == request.CodigoBarras && p.IdSucursal == request.IdSucursal);

            if (yaAsignado)
                return BadRequest("Producto ya asignado a esta sucursal");

            var asignacion = new ProductoSucursal
            {
                CodigoBarras = request.CodigoBarras,
                IdSucursal = request.IdSucursal
            };

            _context.ProductoSucursal.Add(asignacion);
            await _context.SaveChangesAsync();
            return Ok(new { mensaje = "Producto asignado a la sucursal exitosamente" });
        }

        // DELETE: api/Producto/Desasignar
        [HttpDelete("Desasignar")]
        public async Task<IActionResult> DesasignarProducto([FromBody] AsignarProductoSucursalRequest request)
        {
            var productoSucursal = await _context.ProductoSucursal
                .FirstOrDefaultAsync(p => p.CodigoBarras == request.CodigoBarras && p.IdSucursal == request.IdSucursal);

            if (productoSucursal == null)
                return NotFound("Producto no asignado a esta sucursal");

            _context.ProductoSucursal.Remove(productoSucursal);
            await _context.SaveChangesAsync();
            return Ok(new { mensaje = "Producto desasignado de la sucursal exitosamente" });
        }

        [HttpGet("Sucursal/{idSucursal}")]
        public async Task<IActionResult> ObtenerProductosSucursal(int idSucursal)
        {
            var productos = await _context.ProductoSucursal
                .Where(p => p.IdSucursal == idSucursal)
                .Join(
                    _context.Producto,
                    ps => ps.CodigoBarras,
                    p => p.CodigoBarras,
                    (ps, p) => new ProductoAsignadoDTO
                    {
                        CodigoBarras = p.CodigoBarras,
                        Nombre = p.Nombre,
                        Descripcion = p.Descripcion,
                        Costo = p.Costo
                    }
                )
                .ToListAsync();

            return Ok(productos);
        }

        [HttpGet("NoAsociados/{idSucursal}")]
        public async Task<IActionResult> ObtenerProductosNoAsociados(int idSucursal)
        {
            var sucursal = await _context.Sucursal.FindAsync(idSucursal);

            if (sucursal == null)
                return NotFound(new { mensaje = "Sucursal no encontrada" });

            if (!sucursal.StoreEnabled)
                return BadRequest(new { mensaje = "La tienda de esta sucursal no está activada" });

            // Productos asociados
            var productosAsociados = _context.ProductoSucursal
                .Where(ps => ps.IdSucursal == idSucursal)
                .Select(ps => ps.CodigoBarras);

            // Productos no asociados
            var productosNoAsociados = await _context.Producto
                .Where(p => !productosAsociados.Contains(p.CodigoBarras))
                .ToListAsync();

            return Ok(productosNoAsociados);
        }


    }
}
