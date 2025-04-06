using Application.DTOs.Pago;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Interfaces.Pagos;
using Aplication.DTOs.Pago;
using Aplication.Interfaces.Pagos;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagoController : ControllerBase
    {
        private readonly IPagoService _pagoService;

        public PagoController(IPagoService pagoService)
        {
            _pagoService = pagoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pagos = await _pagoService.GetAllAsync();
            return Ok(pagos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pago = await _pagoService.GetByIdAsync(id);
            if (pago == null) return NotFound();
            return Ok(pago);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PagoRequestDTO dto)
        {
            var createdPago = await _pagoService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdPago.Id }, createdPago);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PagoRequestDTO dto)
        {
            var result = await _pagoService.UpdateAsync(id, dto);
            if (!result)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _pagoService.DeleteAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
