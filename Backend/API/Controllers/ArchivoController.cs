using Aplication.DTOs.Archivo;
using Aplication.Interfaces.Archivos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]  // Requiere autenticación
    public class ArchivoController : ControllerBase
    {
        private readonly IArchivoService _archivoService;

        public ArchivoController(IArchivoService archivoService)
        {
            _archivoService = archivoService;
        }

        // Obtener todos los archivos
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var archivos = await _archivoService.GetAllAsync();
            return Ok(archivos);
        }

        // Obtener archivo por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var archivo = await _archivoService.GetByIdAsync(id);
            if (archivo == null)
                return NotFound();
            return Ok(archivo);
        }

        // Crear un nuevo archivo
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ArchivoRequestDTO dto)
        {
            var userId = GetUserIdFromToken();  // Obtener el usuario del token
            if (userId == 0) return Unauthorized();

            dto.IdUsuario = userId;  // Asignar el usuario del token al archivo
            var created = await _archivoService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // Actualizar archivo
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ArchivoRequestDTO dto)
        {
            var userId = GetUserIdFromToken();  // Obtener el usuario del token
            if (userId == 0) return Unauthorized();

            var archivo = await _archivoService.GetByIdAsync(id);
            if (archivo == null || archivo.IdUsuario != userId) // Verificar propiedad del archivo
                return Unauthorized(new { message = "No tienes permisos para modificar este archivo." });

            var result = await _archivoService.UpdateAsync(id, dto);
            if (!result)
                return NotFound();
            return NoContent();
        }

        // Eliminar archivo
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetUserIdFromToken();  // Obtener el usuario del token
            if (userId == 0) return Unauthorized();

            var archivo = await _archivoService.GetByIdAsync(id);
            if (archivo == null || archivo.IdUsuario != userId) // Verificar propiedad del archivo
                return Unauthorized(new { message = "No tienes permisos para eliminar este archivo." });

            var result = await _archivoService.DeleteAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }

        // Obtener archivos por usuario (según el token)
        [HttpGet("GetByUser")]
        public async Task<IActionResult> GetByUser()
        {
            var userId = GetUserIdFromToken();
            if (userId == 0)
                return Unauthorized(new { message = "Token no proporcionado o inválido." });

            var archivos = await _archivoService.GetByUserAsync(userId);

            if (archivos == null || !archivos.Any())
                return NotFound(new { message = "No se encontraron archivos para este usuario." });

            return Ok(archivos);
        }

        // Método para obtener el userId del token
        private int GetUserIdFromToken()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token)) return 0;

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jsonToken == null) return 0;

            var idUsuarioClaim = jsonToken?.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;

            return idUsuarioClaim != null ? int.Parse(idUsuarioClaim) : 0;
        }
    }
}
