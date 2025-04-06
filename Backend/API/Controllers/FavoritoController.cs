using Microsoft.AspNetCore.Mvc;
using Aplication.DTOs.Favorito;
using System.Threading.Tasks;
using Aplication.Interfaces.Favoritos;


namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoritoController : ControllerBase
    {
        private readonly IFavoritoService _favoritoService;

        public FavoritoController(IFavoritoService favoritoService)
        {
            _favoritoService = favoritoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var favoritos = await _favoritoService.GetAllAsync();
            return Ok(favoritos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var favorito = await _favoritoService.GetByIdAsync(id);
            if (favorito == null)
                return NotFound();
            return Ok(favorito);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FavoritoRequestDTO dto)
        {
            var created = await _favoritoService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FavoritoRequestDTO dto)
        {
            var result = await _favoritoService.UpdateAsync(id, dto);
            if (!result)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _favoritoService.DeleteAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
