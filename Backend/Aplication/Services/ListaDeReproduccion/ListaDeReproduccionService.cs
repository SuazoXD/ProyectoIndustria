
using Aplication.DTOs.ListaDeReproduccion;
using Aplication.DTOs.Usuarios;
using Aplication.Interfaces.ListasDeReproduccion;
using Domain.AggregateRoots;


namespace Aplication.Services.ListasDeReproduccion
{
    public class ListaDeReproduccionService : IListaDeReproduccionService
    {
        private readonly IListaDeReproduccionRepository _listaRepository;

        public ListaDeReproduccionService(IListaDeReproduccionRepository listaRepository)
        {
            _listaRepository = listaRepository;
        }

        public async Task<IEnumerable<ListaDeReproduccionResponseDTO>> GetAllAsync()
        {
            var listas = await _listaRepository.GetAllAsync();
            return listas.Select(l => new ListaDeReproduccionResponseDTO
            {
                Id = l.Id,
                IdUsuario = l.IdUsuario,
                NombreLista = l.NombreLista,
                Descripcion = l.Descripcion,
                Usuario = l.Usuario != null ? new UsuarioResponseDTO
                {
                    Id = l.Usuario.Id,
                    NombreUsuario = l.Usuario.NombreUsuario,
                    Correo = l.Usuario.Correo,
                    FechaRegistro = l.Usuario.FechaRegistro,
                    IdRol = l.Usuario.IdRol
                } : null,
            });
        }

        public async Task<ListaDeReproduccionResponseDTO> GetByIdAsync(int id)
        {
            var lista = await _listaRepository.GetByIdAsync(id);
            if (lista == null)
                return null;
            return new ListaDeReproduccionResponseDTO
            {
                Id = lista.Id,
                IdUsuario = lista.IdUsuario,
                NombreLista = lista.NombreLista,
                Descripcion = lista.Descripcion
            };
        }

        public async Task<ListaDeReproduccionResponseDTO> CreateAsync(ListaDeReproduccionRequestDTO dto)
        {
            var lista = new ListaDeReproduccion(dto.IdUsuario, dto.NombreLista, dto.Descripcion);
            var created = await _listaRepository.CreateAsync(lista);
            return new ListaDeReproduccionResponseDTO
            {
                Id = created.Id,
                IdUsuario = created.IdUsuario,
                NombreLista = created.NombreLista,
                Descripcion = created.Descripcion
            };
        }

        public async Task<bool> UpdateAsync(int id, ListaDeReproduccionRequestDTO dto)
        {
            var lista = await _listaRepository.GetByIdAsync(id);
            if (lista == null)
                return false;

            // Actualiza las propiedades (usa reflection si los setters son privados)
            lista.GetType().GetProperty("NombreLista").SetValue(lista, dto.NombreLista);
            lista.GetType().GetProperty("Descripcion").SetValue(lista, dto.Descripcion);

            return await _listaRepository.UpdateAsync(lista);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _listaRepository.DeleteAsync(id);
        }
        public async Task<IEnumerable<ListaDeReproduccionResponseDTO>> GetAllByUserIdAsync(int userId)
        {
            var listas = await _listaRepository.GetAllAsync();
            return listas
                .Where(l => l.IdUsuario == userId)
                .Select(l => new ListaDeReproduccionResponseDTO
                {
                    Id = l.Id,
                    IdUsuario = l.IdUsuario,
                    NombreLista = l.NombreLista,
                    Descripcion = l.Descripcion,


                });
        }

    }
}