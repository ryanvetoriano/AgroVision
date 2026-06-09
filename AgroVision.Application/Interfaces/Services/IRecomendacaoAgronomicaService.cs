using AgroVision.Application.DTOs.RecomendacaoAgronomica;

namespace AgroVision.Application.Interfaces.Services;

public interface IRecomendacaoAgronomicaService
{
    Task<IEnumerable<RecomendacaoAgronomicaResponseDto>> GetAllAsync();
    Task<RecomendacaoAgronomicaResponseDto?> GetByIdAsync(int id);
    Task<IEnumerable<RecomendacaoAgronomicaResponseDto>> GetByAnaliseDroneIdAsync(int analiseDroneId);
    Task<IEnumerable<RecomendacaoAgronomicaResponseDto>> GetPendentesAsync();
    Task<IEnumerable<RecomendacaoAgronomicaResponseDto>> GetUrgentesAsync();
    Task<RecomendacaoAgronomicaResponseDto> CreateAsync(RecomendacaoAgronomicaCreateDto dto);
    Task<bool> DeleteAsync(int id);
    Task<RecomendacaoAgronomicaResponseDto?> MarcarComoConcluidaAsync(int id);
    Task<RecomendacaoAgronomicaResponseDto?> ReabrirAsync(int id);
}