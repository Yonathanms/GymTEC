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
    public class RegistroAdministradorController : ControllerBase
    {
        private readonly GymTECDbContext _context;

        public RegistroAdministradorController(GymTECDbContext context)
        {
            _context = context;
        }

        // POST: api/RegistroAdministrador
        [HttpPost]
        public async Task<IActionResult> RegistrarAdministrador([FromBody] RegistroAdministradorRequest request)
        {
            // 1. Validar el modelo de datos
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // 2. Validar si la cédula ya existe como persona
            if (await _context.Personas.AnyAsync(p => p.NumCedula == request.NumCedula))
            {
                return BadRequest("La cédula ya está registrada como persona.");
            }

            // 3. Crear la Persona (encripta la contraseña)
            var persona = new Persona
            {
                NumCedula = request.NumCedula,
                Nombre = request.Nombre,
                Apellido1 = request.Apellido1,
                Apellido2 = request.Apellido2,
                FechaNacimiento = request.FechaNacimiento,
                Provincia = request.Provincia,
                Canton = request.Canton,
                Distrito = request.Distrito,
                CorreoElectronico = request.CorreoElectronico,
                Password = HashPasswordMD5(request.Password)
            };
            _context.Personas.Add(persona);

            // 4. Crear y guardar el Administrador (puede tener IdSucursal en null)
            var admin = new Administrador
            {
                NumCedula = request.NumCedula,
                IdSucursal = request.IdSucursal,
                TipoPlanilla = request.TipoPlanilla,
                Salario = request.Salario,
                Persona = persona
            };
            _context.Administrador.Add(admin);

            // 5. Asignar el rol de administrador (IdRol = 3) en PersonaxRol
            var personaRol = new PersonaxRol
            {
                NumCedula = request.NumCedula,
                IdRol = 3 // 3 = Administrador
            };
            _context.PersonaxRol.Add(personaRol);

            // 6. Guardar todos los cambios en la base de datos en una transacción
            await _context.SaveChangesAsync();

            // 7. Responder éxito
            return Ok("Administrador registrado correctamente.");
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