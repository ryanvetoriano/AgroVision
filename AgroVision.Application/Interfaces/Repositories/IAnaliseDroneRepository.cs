using AgroVision.Domain.Entities;

namespace AgroVision.Application.Interfaces.Repositories;

public interface IAnaliseDroneRepository
{
    Task<IEnumerable<AnaliseDrone>> GetAllAsync();
    Task<AnaliseDrone?> GetByIdAsync(int id);
    Task<IEnumerable<AnaliseDrone>> GetByPlantacaoIdAsync(int plantacaoId);
    Task<IEnumerable<AnaliseDrone>> GetAnalisesCriticasAsync();
    Task<AnaliseDrone?> GetUltimaAnaliseByPlantacaoIdAsync(int plantacaoId);
    Task AddAsync(AnaliseDrone analiseDrone);
    void Update(AnaliseDrone analiseDrone);
    void Delete(AnaliseDrone analiseDrone);
    Task<bool> ExistsAsync(int id);
    Task SaveChangesAsync();
}