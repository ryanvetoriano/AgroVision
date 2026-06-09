using AgroVision.Application.DTOs.OcorrenciaPlantacao;

namespace AgroVision.Application.Interfaces.Services;

public interface IOcorrenciaPlantacaoService
{
    Task<IEnumerable<OcorrenciaPlantacaoResponseDto>> GetAllAsync();
    Task<OcorrenciaPlantacaoResponseDto?> GetByIdAsync(int id);
    Task<IEnumerable<OcorrenciaPlantacaoResponseDto>> GetByPlantacaoIdAsync(int plantacaoId);
    Task<IEnumerable<OcorrenciaPlantacaoResponseDto>> GetPendentesAsync();
    Task<IEnumerable<OcorrenciaPlantacaoResponseDto>> GetCriticasAsync();
    Task<OcorrenciaPlantacaoResponseDto> CreateAsync(OcorrenciaPlantacaoCreateDto dto);
    Task<bool> DeleteAsync(int id);
    Task<OcorrenciaPlantacaoResponseDto?> MarcarComoResolvidaAsync(int id);
    Task<OcorrenciaPlantacaoResponseDto?> ReabrirAsync(int id);
}