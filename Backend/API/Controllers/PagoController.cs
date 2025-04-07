using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Aplication.DTOs.Pago;
using Aplication.Interfaces.Pagos;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]  // Requiere autenticación
    public class PagoController : ControllerBase
    {
        private readonly IPagoService _pagoService;

        public PagoController(IPagoService pagoService)
        {
            _pagoService = pagoService;
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

        // Obtener todos los pagos del usuario
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = GetUserIdFromToken();
            if (userId == 0) return Unauthorized();

            var pagos = await _pagoService.GetAllByUserIdAsync(userId);
            return Ok(pagos);
        }

        // Obtener pago por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userId = GetUserIdFromToken();
            if (userId == 0) return Unauthorized();

            var pago = await _pagoService.GetByIdAsync(id);
            if (pago == null || pago.IdUsuario != userId)
                return Unauthorized(new { message = "No tienes permiso para ver este pago." });

            return Ok(pago);
        }

        // Crear un nuevo pago
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PagoRequestDTO dto)
        {
            var userId = GetUserIdFromToken();
            if (userId == 0) return Unauthorized();

            dto.IdUsuario = userId;
            var created = await _pagoService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // Actualizar pago
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PagoRequestDTO dto)
        {
            var userId = GetUserIdFromToken();
            if (userId == 0) return Unauthorized();

            var existingPago = await _pagoService.GetByIdAsync(id);
            if (existingPago == null || existingPago.IdUsuario != userId)
                return Unauthorized(new { message = "No tienes permiso para modificar este pago." });

            var result = await _pagoService.UpdateAsync(id, dto);
            if (!result) return NotFound();
            return NoContent();
        }

        // Eliminar pago
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetUserIdFromToken();
            if (userId == 0) return Unauthorized();

            var existingPago = await _pagoService.GetByIdAsync(id);
            if (existingPago == null || existingPago.IdUsuario != userId)
                return Unauthorized(new { message = "No tienes permiso para eliminar este pago." });

            var result = await _pagoService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
