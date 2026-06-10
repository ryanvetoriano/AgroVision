using AgroVision.Domain.Entities;

namespace AgroVision.Application.Interfaces.Repositories;

public interface IRecomendacaoAgronomicaRepository
{
    Task<IEnumerable<RecomendacaoAgronomica>> GetAllAsync();
    Task<RecomendacaoAgronomica?> GetByIdAsync(int id);
    Task<IEnumerable<RecomendacaoAgronomica>> GetByAnaliseDroneIdAsync(int analiseDroneId);
    Task<IEnumerable<RecomendacaoAgronomica>> GetPendentesAsync();
    Task<IEnumerable<RecomendacaoAgronomica>> GetUrgentesAsync();
    Task AddAsync(RecomendacaoAgronomica recomendacaoAgronomica);
    void Update(RecomendacaoAgronomica recomendacaoAgronomica);
    void Delete(RecomendacaoAgronomica recomendacaoAgronomica);
    Task<bool> ExistsAsync(int id);
    Task SaveChangesAsync();
}