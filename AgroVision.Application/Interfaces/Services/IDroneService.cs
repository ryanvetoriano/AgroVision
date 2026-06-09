using AgroVision.Application.DTOs.Drone;

namespace AgroVision.Application.Interfaces.Services;

public interface IDroneService
{
    Task<IEnumerable<DroneResponseDto>> GetAllAsync();
    Task<DroneResponseDto?> GetByIdAsync(int id);
    Task<IEnumerable<DroneResponseDto>> GetAtivosAsync();
    Task<DroneResponseDto> CreateAsync(DroneCreateDto dto);
    Task<DroneResponseDto?> UpdateAsync(int id, DroneUpdateDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> AtivarAsync(int id);
    Task<bool> DesativarAsync(int id);
    Task<bool?> VerificarAutonomiaAsync(int id, DroneAutonomiaDto dto);
}