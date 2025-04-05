using Microsoft.AspNetCore.Mvc;
using Aplication.DTOs.Permisos;
using Aplication.Interfaces.Permisos;

namespace API.Controllers
{
    [ApiController]
    [Route("api/Permiso")]
    public class PermisoController : ControllerBase
    {
        private readonly IPermisoService _permisoService;

        public PermisoController(IPermisoService permisoService)
        {
            _permisoService = permisoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var permisos = await _permisoService.GetAllAsync();
            return Ok(permisos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var permiso = await _permisoService.GetByIdAsync(id);
            if (permiso == null)
                return NotFound();
            return Ok(permiso);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PermisoRequestDTO dto)
        {
            var created = await _permisoService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PermisoRequestDTO dto)
        {
            var result = await _permisoService.UpdateAsync(id, dto);
            if (!result)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _permisoService.DeleteAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
