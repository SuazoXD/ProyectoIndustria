using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.AggregateRoots;

namespace Aplication.Interfaces.Archivos
{
    public interface IArchivoRepository
    {
        Task<IEnumerable<Archivo>> GetAllAsync();
        Task<Archivo> GetByIdAsync(int id);
        Task<Archivo> CreateAsync(Archivo archivo);
        Task<bool> UpdateAsync(Archivo archivo);
        Task<bool> DeleteAsync(int id);
    }
}

