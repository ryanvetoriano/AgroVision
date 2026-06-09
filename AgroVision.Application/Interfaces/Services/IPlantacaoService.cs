using AgroVision.Application.DTOs.Plantacao;

namespace AgroVision.Application.Interfaces.Services;

public interface IPlantacaoService
{
    Task<IEnumerable<PlantacaoResponseDto>> GetAllAsync();
    Task<PlantacaoResponseDto?> GetByIdAsync(int id);
    Task<IEnumerable<PlantacaoResponseDto>> GetByUsuarioIdAsync(int usuarioId);
    Task<IEnumerable<PlantacaoResponseDto>> GetAtivasAsync();
    Task<PlantacaoResponseDto> CreateAsync(PlantacaoCreateDto dto);
    Task<PlantacaoResponseDto?> UpdateAsync(int id, PlantacaoUpdateDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> EncerrarAsync(int id);
    Task<bool> ReativarAsync(int id);
}