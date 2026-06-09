using AgroVision.Domain.Entities;

namespace AgroVision.Application.Interfaces.Repositories;

public interface ILeituraSensorRepository
{
    Task<IEnumerable<LeituraSensor>> GetAllAsync();
    Task<LeituraSensor?> GetByIdAsync(int id);
    Task<IEnumerable<LeituraSensor>> GetByMissaoDroneIdAsync(int missaoDroneId);
    Task<IEnumerable<LeituraSensor>> GetLeiturasForaDoPadraoAsync();
    Task AddAsync(LeituraSensor leituraSensor);
    void Update(LeituraSensor leituraSensor);
    void Delete(LeituraSensor leituraSensor);
    Task<bool> ExistsAsync(int id);
    Task SaveChangesAsync();
}