using AgroVision.Domain.Entities;

namespace AgroVision.Application.Interfaces.Repositories;

public interface ISafraRepository
{
    Task<IEnumerable<Safra>> GetAllAsync();
    Task<Safra?> GetByIdAsync(int id);
    Task<IEnumerable<Safra>> GetByPlantacaoIdAsync(int plantacaoId);
    Task AddAsync(Safra safra);
    void Update(Safra safra);
    void Delete(Safra safra);
    Task<bool> ExistsAsync(int id);
    Task SaveChangesAsync();
}