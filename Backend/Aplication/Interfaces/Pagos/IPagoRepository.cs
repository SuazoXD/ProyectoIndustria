﻿using Domain.AggregateRoots;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Pagos
{
    public interface IPagoRepository
    {
        Task<IEnumerable<Pago>> GetAllAsync();
        Task<Pago> GetByIdAsync(int id);
        Task<Pago> CreateAsync(Pago pago);
        Task<bool> UpdateAsync(Pago pago);
        Task<bool> DeleteAsync(int id);
    }
}
