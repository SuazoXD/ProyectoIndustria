using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplication.Interfaces.Privacidades
{
    public interface IPrivacidadRepository
    {
        Task<IEnumerable<Privacidad>> GetAllAsync();
        Task<Privacidad> GetByIdAsync(int id);
        Task<Privacidad> CreateAsync(Privacidad privacidad);
        Task<bool> UpdateAsync(Privacidad privacidad);
        Task<bool> DeleteAsync(int id);
    }
}
