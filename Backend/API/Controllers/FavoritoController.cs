using Microsoft.AspNetCore.Mvc;
using Aplication.DTOs.Favorito;
using Aplication.Interfaces.Favoritos;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]  // Requiere autenticación
    public class FavoritoController : ControllerBase
    {
        private readonly IFavoritoService _favoritoService;

        public FavoritoController(IFavoritoService favoritoService)
        {
            _favoritoService = favoritoService;
        }

        // Obtener todos los favoritos del usuario autenticado
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = GetUserIdFromToken();  // Obtener el usuario del token
            if (userId == 0) return Unauthorized();

            var favoritos = await _favoritoService.GetAllByUserIdAsync(userId);
            return Ok(favoritos);
        }

        // Obtener favorito por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userId = GetUserIdFromToken();  // Obtener el usuario del token
            if (userId == 0) return Unauthorized();

            var favorito = await _favoritoService.GetByIdAsync(id);
            if (favorito == null || favorito.IdUsuario != userId)
                return Unauthorized(new { message = "No tienes permiso para acceder a este favorito." });

            return Ok(favorito);
        }

        // Crear un nuevo favorito
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FavoritoRequestDTO dto)
        {
            var userId = GetUserIdFromToken();  // Obtener el usuario del token
            if (userId == 0) return Unauthorized();

            dto.IdUsuario = userId;  // Asignar el usuario al favorito
            var created = await _favoritoService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // Actualizar favorito
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FavoritoRequestDTO dto)
        {
            var userId = GetUserIdFromToken();  // Obtener el usuario del token
            if (userId == 0) return Unauthorized();

            var favorito = await _favoritoService.GetByIdAsync(id);
            if (favorito == null || favorito.IdUsuario != userId) // Verificar propiedad del favorito
                return Unauthorized(new { message = "No tienes permiso para modificar este favorito." });

            var result = await _favoritoService.UpdateAsync(id, dto);
            if (!result)
                return NotFound();
            return NoContent();
        }

        // Eliminar favorito
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetUserIdFromToken();  // Obtener el usuario del token
            if (userId == 0) return Unauthorized();

            var favorito = await _favoritoService.GetByIdAsync(id);
            if (favorito == null || favorito.IdUsuario != userId) // Verificar propiedad del favorito
                return Unauthorized(new { message = "No tienes permiso para eliminar este favorito." });

            var result = await _favoritoService.DeleteAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
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
