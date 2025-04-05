using Microsoft.AspNetCore.Mvc;
using Aplication.DTOs.Privacidades;
using Aplication.Interfaces.Privacidades;

namespace API.Controllers
{
    [ApiController]
    [Route("api/Privacidad")]
    public class PrivacidadController : ControllerBase
    {
        private readonly IPrivacidadService _privacidadService;

        public PrivacidadController(IPrivacidadService privacidadService)
        {
            _privacidadService = privacidadService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var privacidades = await _privacidadService.GetAllAsync();
            return Ok(privacidades);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var privacidad = await _privacidadService.GetByIdAsync(id);
            if (privacidad == null)
                return NotFound();
            return Ok(privacidad);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PrivacidadRequestDTO dto)
        {
            var created = await _privacidadService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PrivacidadRequestDTO dto)
        {
            var result = await _privacidadService.UpdateAsync(id, dto);
            if (!result)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _privacidadService.DeleteAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
