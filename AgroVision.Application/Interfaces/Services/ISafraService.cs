using AgroVision.Application.DTOs.Safra;

namespace AgroVision.Application.Interfaces.Services;

public interface ISafraService
{
    Task<IEnumerable<SafraResponseDto>> GetAllAsync();
    Task<SafraResponseDto?> GetByIdAsync(int id);
    Task<IEnumerable<SafraResponseDto>> GetByPlantacaoIdAsync(int plantacaoId);
    Task<SafraResponseDto> CreateAsync(SafraCreateDto dto);
    Task<SafraResponseDto?> UpdateAsync(int id, SafraUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}