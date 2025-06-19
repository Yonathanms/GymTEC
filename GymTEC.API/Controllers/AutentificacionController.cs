using GymTEC.API.Data;
using GymTEC.API.DTOs;
using GymTEC.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GymTEC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutentificacionController : ControllerBase
    {
        private readonly GymTECDbContext _context;

        public AutentificacionController(GymTECDbContext context)
        {
            _context = context;
        }

        // POST: api/Autentificacion/Login
        // Acción para autenticar al usuario usando cédula y contraseña
        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            // Encriptar la contraseña ingresada usando MD5
            string hashedPassword = HashPasswordMD5(request.Password);

            //  Buscar persona por cédula y contraseña encriptada
            var persona = await _context.Personas
                .FirstOrDefaultAsync(p => p.NumCedula == request.NumCedula && p.Password == hashedPassword);

            // Si no existe la persona o la contraseña es incorrecta
            if (persona == null)
            {
                return Unauthorized("Cédula o contraseña incorrecta.");
            }

            // Buscar el rol asignado a esta persona usando la tabla puente PersonaxRol
            // Incluye la navegación a la entidad Rol para obtener el nombre del rol
            var personaRol = await _context.PersonaxRol
                .Include(pr => pr.Rol)
                .FirstOrDefaultAsync(pr => pr.NumCedula == request.NumCedula);

            // Si no tiene rol asignado, no puede acceder
            if (personaRol == null)
            {
                return Unauthorized("Rol no asignado.");
            }

            //  Preparar la respuesta con los datos necesarios para el frontend
            var response = new LoginResponse
            {
                NumCedula = persona.NumCedula,
                Nombre = persona.Nombre,
                Rol = personaRol.IdRol, // Id del rol (1=Cliente, 2=Entrenador, 3=Admin)
                RolNombre = personaRol.Rol != null ? personaRol.Rol.NombreRol : "" // Nombre del rol (ej: "Cliente")
            };

            return Ok(response);
        }

        // Función privada para encriptar la contraseña usando MD5
        private string HashPasswordMD5(string password)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}