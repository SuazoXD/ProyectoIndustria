using Aplication.Interfaces.Usuarios;
using Domain.AggregateRoots;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories.Usuarios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ProjectDBContext _context;

        // Constructor que inyecta el DbContext
        public UsuarioRepository(ProjectDBContext context) => _context = context;

        // Método para obtener todos los usuarios
        public async Task<IEnumerable<Usuario>> GetAllAsync() =>
            await _context.Usuarios.ToListAsync();

        // Método para obtener un usuario por su ID
        public async Task<Usuario> GetByIdAsync(int id) =>
            await _context.Usuarios.FindAsync(id);

        // Método para crear un nuevo usuario
        public async Task<Usuario> CreateAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos
            return usuario; // Retornar el usuario recién creado
        }

        public async Task<bool> UpdateAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            return await _context.SaveChangesAsync() > 0; // Si se actualizó correctamente
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return false; // Si el usuario no se encuentra, retornar false

            _context.Usuarios.Remove(usuario);
            return await _context.SaveChangesAsync() > 0; // Si se eliminó correctamente
        }
    }
}
