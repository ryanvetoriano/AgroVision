using AgroVision.Domain.Entities;

namespace AgroVision.Application.Interfaces.Repositories;

public interface IDroneRepository
{
    Task<IEnumerable<Drone>> GetAllAsync();
    Task<Drone?> GetByIdAsync(int id);
    Task<Drone?> GetByCodigoIdentificacaoAsync(string codigoIdentificacao);
    Task<IEnumerable<Drone>> GetAtivosAsync();
    Task AddAsync(Drone drone);
    void Update(Drone drone);
    void Delete(Drone drone);
    Task<bool> ExistsAsync(int id);
    Task SaveChangesAsync();
}