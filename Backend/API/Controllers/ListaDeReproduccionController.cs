using Microsoft.AspNetCore.Mvc;
using Application.DTOs.ListaDeReproduccion;
using System.Threading.Tasks;
using Application.Interfaces.ListasDeReproduccion;
using Aplication.DTOs.ListaDeReproduccion;
using Aplication.Interfaces.ListasDeReproduccion;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ListaDeReproduccionController : ControllerBase
    {
        private readonly IListaDeReproduccionService _listaService;

        public ListaDeReproduccionController(IListaDeReproduccionService listaService)
        {
            _listaService = listaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var listas = await _listaService.GetAllAsync();
            return Ok(listas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var lista = await _listaService.GetByIdAsync(id);
            if (lista == null)
                return NotFound();
            return Ok(lista);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ListaDeReproduccionRequestDTO dto)
        {
            var created = await _listaService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ListaDeReproduccionRequestDTO dto)
        {
            var result = await _listaService.UpdateAsync(id, dto);
            if (!result)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _listaService.DeleteAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
