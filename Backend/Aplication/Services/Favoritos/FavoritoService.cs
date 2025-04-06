using Aplication.DTOs.Archivo;
using Aplication.DTOs.Favorito;
using Aplication.DTOs.Usuarios;
using Aplication.Interfaces.Favoritos;
using Domain.Entities;


namespace Aplication.Services.Favoritos
{
    public class FavoritoService : IFavoritoService
    {
        private readonly IFavoritoRepository _favoritoRepository;

        public FavoritoService(IFavoritoRepository favoritoRepository)
        {
            _favoritoRepository = favoritoRepository;
        }

        public async Task<IEnumerable<FavoritoResponseDTO>> GetAllAsync()
        {
            var favoritos = await _favoritoRepository.GetAllAsync();
            return favoritos.Select(f => new FavoritoResponseDTO
            {
                Id = f.Id,
                IdUsuario = f.IdUsuario,
                IdArchivo = f.IdArchivo,
                Usuario = f.Usuario != null ? new UsuarioResponseDTO
                {
                    Id = f.Usuario.Id,
                    NombreUsuario = f.Usuario.NombreUsuario,
                    Correo = f.Usuario.Correo,
                    FechaRegistro = f.Usuario.FechaRegistro,
                    IdRol = f.Usuario.IdRol
                } : null,
                Archivo = f.Archivo != null ? new ArchivoResponseDTO
                {
                    Id = f.Archivo.Id,
                    NombreArchivo = f.Archivo.NombreArchivo,
                    TipoArchivo = f.Archivo.TipoArchivo,
                    FuenteAlmacenamiento = f.Archivo.FuenteAlmacenamiento,
                    FechaSubida = f.Archivo.FechaSubida,
                    IdUsuario = f.Archivo.IdUsuario
                } : null
            });
        }

        public async Task<FavoritoResponseDTO> GetByIdAsync(int id)
        {
            var favorito = await _favoritoRepository.GetByIdAsync(id);
            if (favorito == null)
                return null;
            return new FavoritoResponseDTO
            {
                Id = favorito.Id,
                IdUsuario = favorito.IdUsuario,
                IdArchivo = favorito.IdArchivo
            };
        }

        public async Task<FavoritoResponseDTO> CreateAsync(FavoritoRequestDTO dto)
        {
            var favorito = new Favorito(dto.IdUsuario, dto.IdArchivo);
            var created = await _favoritoRepository.CreateAsync(favorito);
            return new FavoritoResponseDTO
            {
                Id = created.Id,
                IdUsuario = created.IdUsuario,
                IdArchivo = created.IdArchivo
            };
        }

        public async Task<bool> UpdateAsync(int id, FavoritoRequestDTO dto)
        {
            var favorito = await _favoritoRepository.GetByIdAsync(id);
            if (favorito == null)
                return false;

            // Si las propiedades tienen setters privados, se puede usar reflection o métodos públicos en la entidad.
            // En este ejemplo, usaremos reflection:
            var type = typeof(Favorito);
            type.GetProperty("IdUsuario").SetValue(favorito, dto.IdUsuario);
            type.GetProperty("IdArchivo").SetValue(favorito, dto.IdArchivo);

            return await _favoritoRepository.UpdateAsync(favorito);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _favoritoRepository.DeleteAsync(id);
        }
    }
}