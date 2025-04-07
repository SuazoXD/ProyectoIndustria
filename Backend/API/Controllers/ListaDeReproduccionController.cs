using Microsoft.AspNetCore.Mvc;
using Aplication.DTOs.ListaDeReproduccion;
using System.Threading.Tasks;
using Aplication.Interfaces.ListasDeReproduccion;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]  // Requiere autenticación
    public class ListaDeReproduccionController : ControllerBase
    {
        private readonly IListaDeReproduccionService _listaService;

        public ListaDeReproduccionController(IListaDeReproduccionService listaService)
        {
            _listaService = listaService;
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

        // GET api/ListaDeReproduccion
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = GetUserIdFromToken();
            if (userId == 0) return Unauthorized();

            var listas = await _listaService.GetAllByUserIdAsync(userId);
            return Ok(listas);
        }

        // GET api/ListaDeReproduccion/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userId = GetUserIdFromToken();
            if (userId == 0) return Unauthorized();

            var lista = await _listaService.GetByIdAsync(id);
            if (lista == null || lista.IdUsuario != userId)
                return Unauthorized(new { message = "No tienes permiso para ver esta lista." });

            return Ok(lista);
        }

        // POST api/ListaDeReproduccion
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ListaDeReproduccionRequestDTO dto)
        {
            var userId = GetUserIdFromToken();
            if (userId == 0) return Unauthorized();

            dto.IdUsuario = userId;
            var created = await _listaService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT api/ListaDeReproduccion/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ListaDeReproduccionRequestDTO dto)
        {
            var userId = GetUserIdFromToken();
            if (userId == 0) return Unauthorized();

            var existing = await _listaService.GetByIdAsync(id);
            if (existing == null || existing.IdUsuario != userId)
                return Unauthorized(new { message = "No tienes permiso para modificar esta lista." });

            var result = await _listaService.UpdateAsync(id, dto);
            if (!result) return NotFound();
            return NoContent();
        }

        // DELETE api/ListaDeReproduccion/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetUserIdFromToken();
            if (userId == 0) return Unauthorized();

            var existing = await _listaService.GetByIdAsync(id);
            if (existing == null || existing.IdUsuario != userId)
                return Unauthorized(new { message = "No tienes permiso para eliminar esta lista." });

            var result = await _listaService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}

