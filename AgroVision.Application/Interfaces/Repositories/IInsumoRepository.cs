using AgroVision.Domain.Entities;

namespace AgroVision.Application.Interfaces.Repositories;

public interface IInsumoRepository
{
    Task<IEnumerable<Insumo>> GetAllAsync();
    Task<Insumo?> GetByIdAsync(int id);
    Task<IEnumerable<Insumo>> GetByPlantacaoIdAsync(int plantacaoId);
    Task<IEnumerable<Insumo>> GetComEstoqueBaixoAsync();
    Task AddAsync(Insumo insumo);
    void Update(Insumo insumo);
    void Delete(Insumo insumo);
    Task<bool> ExistsAsync(int id);
    Task SaveChangesAsync();
}