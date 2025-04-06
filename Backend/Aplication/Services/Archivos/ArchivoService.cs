
using Aplication.DTOs.Archivo;
using Aplication.DTOs.Usuarios;
using Aplication.Interfaces.Archivos;
using Aplication.Interfaces.Usuarios;
using Domain.AggregateRoots;

namespace Aplication.Services.Archivos
{
    public class ArchivoService : IArchivoService
    {
        private readonly IArchivoRepository _archivoRepository;
        private readonly IUsuarioRepository _usuarioRepository; 

        public ArchivoService(IArchivoRepository archivoRepository, IUsuarioRepository usuarioRepository) // Modificar el constructor
        {
            _archivoRepository = archivoRepository;
            _usuarioRepository = usuarioRepository; 
        }

        public async Task<IEnumerable<ArchivoResponseDTO>> GetAllAsync()
        {
            var archivos = await _archivoRepository.GetAllAsync();
            return archivos.Select(a => new ArchivoResponseDTO
            {
                Id = a.Id,
                NombreArchivo = a.NombreArchivo,
                TipoArchivo = a.TipoArchivo,
                FechaSubida = a.FechaSubida,
                FuenteAlmacenamiento = a.FuenteAlmacenamiento,
                Metadatos = a.Metadatos,
                IdUsuario = a.IdUsuario,
                Usuario = a.Usuario != null ? new UsuarioResponseDTO
                {
                    Id = a.Usuario.Id,
                    NombreUsuario = a.Usuario.NombreUsuario,
                    Correo = a.Usuario.Correo,
                    FechaRegistro = a.Usuario.FechaRegistro,
                    IdRol = a.Usuario.IdRol
                } : null

            });
        }

        public async Task<ArchivoResponseDTO> GetByIdAsync(int id)
        {
       
            var archivo = await _archivoRepository.GetByIdAsync(id);

            
            if (archivo == null)
                return null; 

            
            return new ArchivoResponseDTO
            {
                Id = archivo.Id,
                NombreArchivo = archivo.NombreArchivo,
                TipoArchivo = archivo.TipoArchivo,
                FechaSubida = archivo.FechaSubida,
                FuenteAlmacenamiento = archivo.FuenteAlmacenamiento,
                Metadatos = archivo.Metadatos,
                IdUsuario = archivo.IdUsuario
            };
        }


        public async Task<ArchivoResponseDTO> CreateAsync(ArchivoRequestDTO dto)
        {
           
            var usuario = await _usuarioRepository.GetByIdAsync(dto.IdUsuario);
            if (usuario == null)
            {
                throw new ArgumentException("El usuario no existe.");
            }

            
            var archivo = new Archivo(
                nombreArchivo: dto.NombreArchivo,
                tipoArchivo: dto.TipoArchivo,
                fuenteAlmacenamiento: dto.FuenteAlmacenamiento,
                metadatos: dto.Metadatos,
                idUsuario: dto.IdUsuario
            );

            
            var createdArchivo = await _archivoRepository.CreateAsync(archivo);

           
            return new ArchivoResponseDTO
            {
                Id = createdArchivo.Id,
                NombreArchivo = createdArchivo.NombreArchivo,
                TipoArchivo = createdArchivo.TipoArchivo,
                FechaSubida = createdArchivo.FechaSubida,
                FuenteAlmacenamiento = createdArchivo.FuenteAlmacenamiento,
                Metadatos = createdArchivo.Metadatos,
                IdUsuario = createdArchivo.IdUsuario
            };
        }


        public async Task<bool> UpdateAsync(int id, ArchivoRequestDTO dto)
        {
           
            var archivo = await _archivoRepository.GetByIdAsync(id);

           
            if (archivo == null)
                return false; 

           
            archivo.Update(dto.NombreArchivo, dto.TipoArchivo, dto.FuenteAlmacenamiento, dto.Metadatos);

            
            return await _archivoRepository.UpdateAsync(archivo);
        }



        public async Task<bool> DeleteAsync(int id)
        {
            return await _archivoRepository.DeleteAsync(id);
        }
    }
}

