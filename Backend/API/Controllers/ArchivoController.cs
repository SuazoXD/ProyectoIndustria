using Aplication.DTOs.Archivo;
using Aplication.Interfaces.Archivos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArchivoController : ControllerBase
    {
        private readonly IArchivoService _archivoService;

        public ArchivoController(IArchivoService archivoService)
        {
            _archivoService = archivoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var archivos = await _archivoService.GetAllAsync();
            return Ok(archivos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var archivo = await _archivoService.GetByIdAsync(id);
            if (archivo == null)
                return NotFound();
            return Ok(archivo);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ArchivoRequestDTO dto)
        {
            var created = await _archivoService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ArchivoRequestDTO dto)
        {
            var result = await _archivoService.UpdateAsync(id, dto);
            if (!result)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _archivoService.DeleteAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
