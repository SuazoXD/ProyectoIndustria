using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Aplication.DTOs.Privacidades;
using Aplication.Interfaces.Privacidades;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]  // Requiere autenticación
    public class PrivacidadController : ControllerBase
    {
        private readonly IPrivacidadService _privacidadService;

        public PrivacidadController(IPrivacidadService privacidadService)
        {
            _privacidadService = privacidadService;
        }

        // Helper para extraer userId del token
        private int GetUserIdFromToken()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token)) return 0;

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
            var idClaim = jsonToken?.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            return idClaim != null ? int.Parse(idClaim) : 0;
        }

        // Obtener todas las configuraciones de privacidad del usuario
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = GetUserIdFromToken();
            if (userId == 0) return Unauthorized();

            var privacidad = await _privacidadService.GetByUserIdAsync(userId);
            return Ok(privacidad);
        }

        // Obtener configuración de privacidad por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userId = GetUserIdFromToken();
            if (userId == 0) return Unauthorized();

            var privacidad = await _privacidadService.GetByIdAsync(id);
            if (privacidad == null || privacidad.IdUsuario != userId)
                return Unauthorized(new { message = "No tienes permiso para ver esta configuración de privacidad." });

            return Ok(privacidad);
        }

        // Crear configuración de privacidad
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PrivacidadRequestDTO dto)
        {
            var userId = GetUserIdFromToken();
            if (userId == 0) return Unauthorized();

            dto.IdUsuario = userId; // Asignar el usuario autenticado
            var created = await _privacidadService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // Actualizar configuración de privacidad
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PrivacidadRequestDTO dto)
        {
            var userId = GetUserIdFromToken();
            if (userId == 0) return Unauthorized();

            var existingPrivacidad = await _privacidadService.GetByIdAsync(id);
            if (existingPrivacidad == null || existingPrivacidad.IdUsuario != userId)
                return Unauthorized(new { message = "No tienes permiso para modificar esta configuración de privacidad." });

            var result = await _privacidadService.UpdateAsync(id, dto);
            if (!result) return NotFound();
            return NoContent();
        }

        // Eliminar configuración de privacidad
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetUserIdFromToken();
            if (userId == 0) return Unauthorized();

            var existingPrivacidad = await _privacidadService.GetByIdAsync(id);
            if (existingPrivacidad == null || existingPrivacidad.IdUsuario != userId)
                return Unauthorized(new { message = "No tienes permiso para eliminar esta configuración de privacidad." });

            var result = await _privacidadService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
