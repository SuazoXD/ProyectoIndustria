using System.IdentityModel.Tokens.Jwt;
using Aplication.DTOs.Creditos;
using Aplication.Interfaces.Creditos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]  // Requiere autenticación
    public class CreditoController : ControllerBase
    {
        private readonly ICreditoService _creditoService;

        public CreditoController(ICreditoService creditoService)
        {
            _creditoService = creditoService;
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

        // Obtener todos los créditos del usuario
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = GetUserIdFromToken();
            if (userId == 0) return Unauthorized();

            var creditos = await _creditoService.GetAllByUserIdAsync(userId);
            return Ok(creditos);
        }

        // Obtener crédito por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userId = GetUserIdFromToken();
            if (userId == 0) return Unauthorized();

            var credito = await _creditoService.GetByIdAsync(id);
            if (credito == null || credito.IdUsuario != userId)
                return Unauthorized(new { message = "No tienes permiso para ver este crédito." });

            return Ok(credito);
        }

        // Crear un nuevo crédito
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreditoRequestDTO dto)
        {
            var userId = GetUserIdFromToken();
            if (userId == 0) return Unauthorized();

            dto.IdUsuario = userId;
            var created = await _creditoService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // Actualizar crédito
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreditoRequestDTO dto)
        {
            var userId = GetUserIdFromToken();
            if (userId == 0) return Unauthorized();

            var existingCredito = await _creditoService.GetByIdAsync(id);
            if (existingCredito == null || existingCredito.IdUsuario != userId)
                return Unauthorized(new { message = "No tienes permiso para modificar este crédito." });

            var result = await _creditoService.UpdateAsync(id, dto);
            if (!result) return NotFound();
            return NoContent();
        }

        // Eliminar crédito
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetUserIdFromToken();
            if (userId == 0) return Unauthorized();

            var existingCredito = await _creditoService.GetByIdAsync(id);
            if (existingCredito == null || existingCredito.IdUsuario != userId)
                return Unauthorized(new { message = "No tienes permiso para eliminar este crédito." });

            var result = await _creditoService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}