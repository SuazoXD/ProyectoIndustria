
using Aplication.DTOs.ArchivoListaReproduccion;
using Aplication.Interfaces.ArchivosListaReproduccion;
using Domain.Entities;

namespace Aplication.Services.ArchivosListaReproduccion
{
    public class ArchivoListaReproduccionService : IArchivoListaReproduccionService
    {
        private readonly IArchivoListaReproduccionRepository _repository;

        public ArchivoListaReproduccionService(IArchivoListaReproduccionRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ArchivoListaReproduccionResponseDTO>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return entities.Select(e => new ArchivoListaReproduccionResponseDTO
            {
                Id = e.Id,
                IdLista = e.IdLista,
                IdArchivo = e.IdArchivo
            });
        }

        public async Task<ArchivoListaReproduccionResponseDTO> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;
            return new ArchivoListaReproduccionResponseDTO
            {
                Id = entity.Id,
                IdLista = entity.IdLista,
                IdArchivo = entity.IdArchivo
            };
        }

        public async Task<ArchivoListaReproduccionResponseDTO> CreateAsync(ArchivoListaReproduccionRequestDTO dto)
        {
            var entity = new ArchivoListaReproduccion(dto.IdLista, dto.IdArchivo);
            var created = await _repository.CreateAsync(entity);
            return new ArchivoListaReproduccionResponseDTO
            {
                Id = created.Id,
                IdLista = created.IdLista,
                IdArchivo = created.IdArchivo
            };
        }

        public async Task<bool> UpdateAsync(int id, ArchivoListaReproduccionRequestDTO dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;
            // Actualización básica: se reasignan valores (puedes mejorar según lógica de negocio)
            entity.GetType().GetProperty("IdLista").SetValue(entity, dto.IdLista);
            entity.GetType().GetProperty("IdArchivo").SetValue(entity, dto.IdArchivo);
            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
