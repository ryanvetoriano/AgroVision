using AgroVision.Application.DTOs.LeituraSensor;

namespace AgroVision.Application.Interfaces.Services;

public interface ILeituraSensorService
{
    Task<IEnumerable<LeituraSensorResponseDto>> GetAllAsync();
    Task<LeituraSensorResponseDto?> GetByIdAsync(int id);
    Task<IEnumerable<LeituraSensorResponseDto>> GetByMissaoDroneIdAsync(int missaoDroneId);
    Task<IEnumerable<LeituraSensorResponseDto>> GetLeiturasForaDoPadraoAsync();
    Task<LeituraSensorResponseDto> CreateAsync(LeituraSensorCreateDto dto);
    Task<bool> DeleteAsync(int id);
}