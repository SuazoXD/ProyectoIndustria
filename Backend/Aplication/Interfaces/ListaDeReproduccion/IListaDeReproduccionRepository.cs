using Domain.AggregateRoots;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.ListasDeReproduccion
{
    public interface IListaDeReproduccionRepository
    {
        Task<IEnumerable<ListaDeReproduccion>> GetAllAsync();
        Task<ListaDeReproduccion> GetByIdAsync(int id);
        Task<ListaDeReproduccion> CreateAsync(ListaDeReproduccion lista);
        Task<bool> UpdateAsync(ListaDeReproduccion lista);
        Task<bool> DeleteAsync(int id);
    }
}
