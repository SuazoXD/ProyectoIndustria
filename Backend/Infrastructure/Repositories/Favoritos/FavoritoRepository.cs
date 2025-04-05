using Aplication.Interfaces.Favoritos;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories.Favoritos
{
    public class FavoritoRepository : IFavoritoRepository
    {
        private readonly ProjectDBContext _context;

        public FavoritoRepository(ProjectDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Favorito>> GetAllAsync() =>
            await _context.Favoritos.ToListAsync();

        public async Task<Favorito> GetByIdAsync(int id) =>
            await _context.Favoritos.FindAsync(id);

        public async Task<Favorito> CreateAsync(Favorito favorito)
        {
            _context.Favoritos.Add(favorito);
            await _context.SaveChangesAsync();
            return favorito;
        }

        public async Task<bool> UpdateAsync(Favorito favorito)
        {
            _context.Favoritos.Update(favorito);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var favorito = await _context.Favoritos.FindAsync(id);
            if (favorito == null)
                return false;
            _context.Favoritos.Remove(favorito);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
