using Microsoft.AspNetCore.Mvc;
using Aplication.DTOs.Factura;
using System.Threading.Tasks;
using Aplication.Interfaces.Facturas;


namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacturaController : ControllerBase
    {
        private readonly IFacturaService _facturaService;

        public FacturaController(IFacturaService facturaService)
        {
            _facturaService = facturaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var facturas = await _facturaService.GetAllAsync();
            return Ok(facturas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var factura = await _facturaService.GetByIdAsync(id);
            if (factura == null)
                return NotFound();
            return Ok(factura);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FacturaRequestDTO dto)
        {
            var created = await _facturaService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FacturaRequestDTO dto)
        {
            var result = await _facturaService.UpdateAsync(id, dto);
            if (!result)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _facturaService.DeleteAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
