using AgroVision.Domain.Entities;

namespace AgroVision.Application.Interfaces.Repositories;

public interface IPlantacaoRepository
{
    Task<IEnumerable<Plantacao>> GetAllAsync();
    Task<Plantacao?> GetByIdAsync(int id);
    Task<IEnumerable<Plantacao>> GetByUsuarioIdAsync(int usuarioId);
    Task<IEnumerable<Plantacao>> GetAtivasAsync();
    Task AddAsync(Plantacao plantacao);
    void Update(Plantacao plantacao);
    void Delete(Plantacao plantacao);
    Task<bool> ExistsAsync(int id);
    Task SaveChangesAsync();
}