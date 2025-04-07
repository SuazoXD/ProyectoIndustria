using Domain.Entities;


namespace Aplication.Interfaces.Favoritos
{
    public interface IFavoritoRepository
    {
        Task<IEnumerable<Favorito>> GetAllAsync();
        Task<Favorito> GetByIdAsync(int id);
        Task<Favorito> CreateAsync(Favorito favorito);
        Task<bool> UpdateAsync(Favorito favorito);
        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<Credito>> GetAllByUserIdAsync(int userId);

    }
}
