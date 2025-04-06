using Microsoft.AspNetCore.Mvc;
using Aplication.DTOs.MetodoPago;
using System.Threading.Tasks;
using Aplication.Interfaces.MetodosPago;



namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MetodoPagoController : ControllerBase
    {
        private readonly IMetodoPagoService _metodoPagoService;

        public MetodoPagoController(IMetodoPagoService metodoPagoService)
        {
            _metodoPagoService = metodoPagoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var metodos = await _metodoPagoService.GetAllAsync();
            return Ok(metodos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var metodo = await _metodoPagoService.GetByIdAsync(id);
            if (metodo == null)
                return NotFound();
            return Ok(metodo);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MetodoPagoRequestDTO dto)
        {
            var created = await _metodoPagoService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MetodoPagoRequestDTO dto)
        {
            var result = await _metodoPagoService.UpdateAsync(id, dto);
            if (!result)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _metodoPagoService.DeleteAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
