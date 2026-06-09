using AgroVision.Domain.Entities;

namespace AgroVision.Application.Interfaces.Repositories;

public interface IMissaoDroneRepository
{
    Task<IEnumerable<MissaoDrone>> GetAllAsync();
    Task<MissaoDrone?> GetByIdAsync(int id);
    Task<IEnumerable<MissaoDrone>> GetByDroneIdAsync(int droneId);
    Task<IEnumerable<MissaoDrone>> GetByPlantacaoIdAsync(int plantacaoId);
    Task<IEnumerable<MissaoDrone>> GetMissoesEmAndamentoAsync();
    Task AddAsync(MissaoDrone missaoDrone);
    void Update(MissaoDrone missaoDrone);
    void Delete(MissaoDrone missaoDrone);
    Task<bool> ExistsAsync(int id);
    Task SaveChangesAsync();
}