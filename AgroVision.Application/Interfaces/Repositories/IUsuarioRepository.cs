using AgroVision.Domain.Entities;

namespace AgroVision.Application.Interfaces.Repositories;

public interface IUsuarioRepository
{
    Task<IEnumerable<Usuario>> GetAllAsync();
    Task<Usuario?> GetByIdAsync(int id);
    Task<Usuario?> GetByCpfAsync(long cpf);
    Task AddAsync(Usuario usuario);
    void Update(Usuario usuario);
    void Delete(Usuario usuario);
    Task<bool> ExistsAsync(int id);
    Task SaveChangesAsync();
}