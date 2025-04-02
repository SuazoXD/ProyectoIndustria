using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;



namespace Infrastructure.Repositories
{
    public class ArchivoListaReproduccionRepository : IArchivoListaReproduccionRepository
    {
        private readonly ProjectDBContext _context;

        public ArchivoListaReproduccionRepository(ProjectDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ArchivoListaReproduccion>> GetAllAsync() =>
        await _context.Archivos_ListasDeReproduccion.ToListAsync();

        public async Task<ArchivoListaReproduccion> GetByIdAsync(int id) =>
            await _context.Archivos_ListasDeReproduccion.FindAsync(id);

        public async Task<ArchivoListaReproduccion> CreateAsync(ArchivoListaReproduccion entity)
        {
            _context.Archivos_ListasDeReproduccion.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> UpdateAsync(ArchivoListaReproduccion entity)
        {
            _context.Archivos_ListasDeReproduccion.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Archivos_ListasDeReproduccion.FindAsync(id);
            if (entity == null)
                return false;
            _context.Archivos_ListasDeReproduccion.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
