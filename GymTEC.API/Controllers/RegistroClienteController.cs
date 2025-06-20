//Controlador para el registro de clientes en la API de GymTEC

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
    public class RegistroClienteController : ControllerBase
    {
        private readonly GymTECDbContext _context;

        public RegistroClienteController(GymTECDbContext context)
        {
            _context = context;
        }

        // POST: api/RegistroCliente
        [HttpPost]
        public async Task<IActionResult> RegistrarCliente([FromBody] RegistroClienteRequest request)
        {
            // 1. Validar si la cédula ya existe
            if (await _context.Personas.AnyAsync(p => p.NumCedula == request.NumCedula))
            {
                return BadRequest("La cédula ya está registrada.");
            }

            // 2. Crear y guardar la Persona (encriptando la contraseña)
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

            // 3. Crear y guardar el Cliente (misma cédula)
            var cliente = new Cliente
            {
                NumCedula = request.NumCedula,
                Peso = request.Peso,
                IMC = request.IMC
            };
            _context.Cliente.Add(cliente);

            // 4. Asignar el rol de cliente (IdRol = 1) en PersonaxRol
            var personaRol = new PersonaxRol
            {
                NumCedula = request.NumCedula,
                IdRol = 1 // 1 = Cliente
            };
            _context.PersonaxRol.Add(personaRol);

            // 5. Guardar todos los cambios en la base de datos en una transacción
            await _context.SaveChangesAsync();

            // 6. Responder éxito
            return Ok("Cliente registrado correctamente.");
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
