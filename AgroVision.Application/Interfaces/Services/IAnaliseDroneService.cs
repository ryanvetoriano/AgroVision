using AgroVision.Application.DTOs.AnaliseDrone;

namespace AgroVision.Application.Interfaces.Services;

public interface IAnaliseDroneService
{
    Task<IEnumerable<AnaliseDroneResponseDto>> GetAllAsync();
    Task<AnaliseDroneResponseDto?> GetByIdAsync(int id);
    Task<IEnumerable<AnaliseDroneResponseDto>> GetByPlantacaoIdAsync(int plantacaoId);
    Task<IEnumerable<AnaliseDroneResponseDto>> GetAnalisesCriticasAsync();
    Task<AnaliseDroneResponseDto?> GetUltimaAnaliseByPlantacaoIdAsync(int plantacaoId);
    Task<AnaliseDroneResponseDto> CreateAsync(AnaliseDroneCreateDto dto);
    Task<AnaliseDroneResponseDto?> UpdateAsync(int id, AnaliseDroneUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}