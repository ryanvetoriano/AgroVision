using AgroVision.Application.DTOs.Insumo;

namespace AgroVision.Application.Interfaces.Services;

public interface IInsumoService
{
    Task<IEnumerable<InsumoResponseDto>> GetAllAsync();
    Task<InsumoResponseDto?> GetByIdAsync(int id);
    Task<IEnumerable<InsumoResponseDto>> GetByPlantacaoIdAsync(int plantacaoId);
    Task<IEnumerable<InsumoResponseDto>> GetComEstoqueBaixoAsync();
    Task<InsumoResponseDto> CreateAsync(InsumoCreateDto dto);
    Task<InsumoResponseDto?> UpdateAsync(int id, InsumoUpdateDto dto);
    Task<bool> DeleteAsync(int id);
    Task<InsumoResponseDto?> RegistrarAplicacaoAsync(int id, InsumoAplicacaoDto dto);
    Task<InsumoResponseDto?> ReporEstoqueAsync(int id, InsumoReposicaoDto dto);
}