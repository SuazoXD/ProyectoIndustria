using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Aplication.DTOs.Rol;
using Aplication.Interfaces.Roles;

namespace API.Controllers
{
    [ApiController]
    [Route("api/Rol")]
    public class RolController : ControllerBase
    {
        private readonly IRolService _rolService;

        public RolController(IRolService rolService)
        {
            _rolService = rolService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _rolService.GetAllAsync();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var rol = await _rolService.GetByIdAsync(id);
            if (rol == null)
                return NotFound();
            return Ok(rol);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RolRequestDTO dto)
        {
            var created = await _rolService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RolRequestDTO dto)
        {
            var result = await _rolService.UpdateAsync(id, dto);
            if (!result)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _rolService.DeleteAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
