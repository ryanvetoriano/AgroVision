using AgroVision.Application.DTOs.MissaoDrone;

namespace AgroVision.Application.Interfaces.Services;

public interface IMissaoDroneService
{
    Task<IEnumerable<MissaoDroneResponseDto>> GetAllAsync();
    Task<MissaoDroneResponseDto?> GetByIdAsync(int id);
    Task<IEnumerable<MissaoDroneResponseDto>> GetByDroneIdAsync(int droneId);
    Task<IEnumerable<MissaoDroneResponseDto>> GetByPlantacaoIdAsync(int plantacaoId);
    Task<IEnumerable<MissaoDroneResponseDto>> GetMissoesEmAndamentoAsync();
    Task<MissaoDroneResponseDto> CreateAsync(MissaoDroneCreateDto dto);
    Task<bool> DeleteAsync(int id);
    Task<MissaoDroneResponseDto?> IniciarMissaoAsync(int id);
    Task<MissaoDroneResponseDto?> FinalizarMissaoAsync(int id);
    Task<MissaoDroneResponseDto?> CancelarMissaoAsync(int id);
}