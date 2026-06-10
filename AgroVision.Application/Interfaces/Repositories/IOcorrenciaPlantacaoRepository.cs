using AgroVision.Domain.Entities;

namespace AgroVision.Application.Interfaces.Repositories;

public interface IOcorrenciaPlantacaoRepository
{
    Task<IEnumerable<OcorrenciaPlantacao>> GetAllAsync();
    Task<OcorrenciaPlantacao?> GetByIdAsync(int id);
    Task<IEnumerable<OcorrenciaPlantacao>> GetByPlantacaoIdAsync(int plantacaoId);
    Task<IEnumerable<OcorrenciaPlantacao>> GetPendentesAsync();
    Task<IEnumerable<OcorrenciaPlantacao>> GetCriticasAsync();
    Task AddAsync(OcorrenciaPlantacao ocorrenciaPlantacao);
    void Update(OcorrenciaPlantacao ocorrenciaPlantacao);
    void Delete(OcorrenciaPlantacao ocorrenciaPlantacao);
    Task<bool> ExistsAsync(int id);
    Task SaveChangesAsync();
}