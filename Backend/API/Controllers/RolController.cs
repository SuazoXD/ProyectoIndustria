using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Infrastructure.Persistence;
using Aplication.DTOs.Rol;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolController : ControllerBase
    {
        private readonly ProjectDBContext _context;

        public RolController(ProjectDBContext context)
        {
            _context = context;
        }

        // Obtener el Rol del usuario desde el token
        [HttpGet("GetRol")]
        [Authorize]  // Requiere autenticación
        public async Task<IActionResult> GetRol()
        {
            // Obtener el token desde el header Authorization
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
                return Unauthorized(new { message = "Token no proporcionado." });

            try
            {
                // Decodificar el token
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

                if (jsonToken == null)
                    return Unauthorized(new { message = "Token inválido." });

                // Extraer el claim del IdRol
                var idRolClaim = jsonToken?.Claims.FirstOrDefault(c => c.Type == "IdRol")?.Value;

                if (idRolClaim == null)
                    return Unauthorized(new { message = "IdRol no encontrado en el token." });

                // Buscar el rol en la base de datos
                var rolId = int.Parse(idRolClaim);
                var rol = await _context.Roles.FirstOrDefaultAsync(r => r.Id == rolId);

                if (rol == null)
                    return NotFound(new { message = "Rol no encontrado." });

                // Si todo está bien, devolver los datos del rol
                var rolResponse = new RolResponseDTO
                {
                    Id = rol.Id,
                    NombreRol = rol.NombreRol,
                    Descripcion = rol.Descripcion
                };

                return Ok(rolResponse);
            }
            catch
            {
                return Unauthorized(new { message = "Error al procesar el token." });
            }
        }

    }
}