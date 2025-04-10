﻿using Aplication.Interfaces.Archivos;
using Domain.AggregateRoots;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories.Archivos
{
    public class ArchivoRepository : IArchivoRepository
    {
        private readonly ProjectDBContext _context;
        public ArchivoRepository(ProjectDBContext context) => _context = context;

        public async Task<IEnumerable<Archivo>> GetAllAsync()
        {
            return await _context.Archivos
               .Include(p => p.Usuario)
               .ToListAsync();

        }

        public async Task<Archivo> GetByIdAsync(int id) =>
            await _context.Archivos.FindAsync(id);

        public async Task<Archivo> CreateAsync(Archivo archivo)
        {
            _context.Archivos.Add(archivo);
            await _context.SaveChangesAsync();
            return archivo;
        }

        public async Task<bool> UpdateAsync(Archivo archivo)
        {
            _context.Archivos.Update(archivo);
            return await _context.SaveChangesAsync() > 0;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var archivo = await _context.Archivos.FindAsync(id);
            if (archivo == null) return false;
            _context.Archivos.Remove(archivo);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}