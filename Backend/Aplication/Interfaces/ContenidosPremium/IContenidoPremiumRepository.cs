

using Domain.Entities;

namespace Aplication.Interfaces.ContenidosPremium
{
        public interface IContenidoPremiumRepository
        {
            Task<IEnumerable<ContenidoPremium>> GetAllAsync();
            Task<ContenidoPremium> GetByIdAsync(int id);
            Task<ContenidoPremium> CreateAsync(ContenidoPremium contenido);
            Task<bool> UpdateAsync(ContenidoPremium contenido);
            Task<bool> DeleteAsync(int id);
        }
    }

