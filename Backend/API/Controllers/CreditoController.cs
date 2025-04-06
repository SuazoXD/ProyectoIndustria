using Aplication.DTOs.Creditos;
using Aplication.Interfaces.Creditos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreditoController : ControllerBase
    {
        private readonly ICreditoService _creditoService;

        public CreditoController(ICreditoService creditoService)
        {
            _creditoService = creditoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var creditos = await _creditoService.GetAllAsync();
            return Ok(creditos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var credito = await _creditoService.GetByIdAsync(id);
            if (credito == null)
                return NotFound();
            return Ok(credito);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreditoRequestDTO dto)
        {
            var created = await _creditoService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreditoRequestDTO dto)
        {
            var result = await _creditoService.UpdateAsync(id, dto);
            if (!result)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _creditoService.DeleteAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
