using API.Custom;
using Aplication.DTOs.Usuarios;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccesoController : ControllerBase
    {
        private readonly ProjectDBContext _context;
        private readonly Utilidades _utilidades;
        public AccesoController(ProjectDBContext context, Utilidades utilidades)
        {
            _context = context;
            _utilidades = utilidades;
        }

        [HttpPost]
        [Route("Registrarse")]
        public async Task<IActionResult> Registrarse(UsuarioRequestDTO objeto)
        {
            var modeloUsuario = new Usuario(
                objeto.NombreUsuario,
                _utilidades.encriptarSHA256(objeto.Contrasenia),
                objeto.Correo,
                objeto.IdRol
            );

            await _context.Usuarios.AddAsync(modeloUsuario);
            await _context.SaveChangesAsync();

            if (modeloUsuario.IdRol != 0)
            {
                return Ok(new { mensaje = "Usuario registrado correctamente" });
            }
            else
            {
                return BadRequest(new { mensaje = "Error al registrar el usuario" });
            }
        }

        [HttpPost]
        [Route("IniciarSesion")]
        public async Task<IActionResult> IniciarSesion(UsuarioRequestDTO objeto)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.NombreUsuario == objeto.NombreUsuario && u.Contrasenia == _utilidades.encriptarSHA256(objeto.Contrasenia));
            if (usuario != null)
            {
                var token = _utilidades.GenerarJWT(usuario);
                return Ok(new { token });
            }
            else
            {
                return Unauthorized(new { mensaje = "Credenciales incorrectas" });
            }
        }
    }
}



