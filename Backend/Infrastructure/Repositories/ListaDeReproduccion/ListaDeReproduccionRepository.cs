using Aplication.Interfaces.ArchivosListaReproduccion;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories.ListaDeReproduccion
{
    public class ListaDeReproduccionRepository : IListaDeReproduccionRepository
    {
        private readonly ProjectDBContext _context;
        public ListaDeReproduccionRepository(ProjectDBContext context) => _context = context;

        public async Task<IEnumerable<ListaDeReproduccion>> GetAllAsync() =>
            await _context.ListasDeReproduccion.ToListAsync();

        public async Task<ListaDeReproduccion> GetByIdAsync(int id) =>
            await _context.ListasDeReproduccion.FindAsync(id);

        public async Task<ListaDeReproduccion> CreateAsync(ListaDeReproduccion lista)
        {
            _context.ListasDeReproduccion.Add(lista);
            await _context.SaveChangesAsync();
            return lista;
        }

        public async Task<bool> UpdateAsync(ListaDeReproduccion lista)
        {
            _context.ListasDeReproduccion.Update(lista);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var lista = await _context.ListasDeReproduccion.FindAsync(id);
            if (lista == null) return false;
            _context.ListasDeReproduccion.Remove(lista);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
