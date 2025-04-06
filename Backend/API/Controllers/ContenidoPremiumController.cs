using Aplication.DTOs.ContenidosPremium;
using Aplication.Interfaces.ContenidosPremium;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContenidoPremiumController : ControllerBase
    {
        private readonly IContenidoPremiumService _contenidoPremiumService;

        public ContenidoPremiumController(IContenidoPremiumService contenidoPremiumService)
        {
            _contenidoPremiumService = contenidoPremiumService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var contenidos = await _contenidoPremiumService.GetAllAsync();
            return Ok(contenidos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var contenido = await _contenidoPremiumService.GetByIdAsync(id);
            if (contenido == null)
                return NotFound();
            return Ok(contenido);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ContenidoPremiumRequestDTO dto)
        {
            var created = await _contenidoPremiumService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ContenidoPremiumRequestDTO dto)
        {
            var result = await _contenidoPremiumService.UpdateAsync(id, dto);
            if (!result)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _contenidoPremiumService.DeleteAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
