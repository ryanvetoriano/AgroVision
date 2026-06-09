using AgroVision.Application.DTOs.Drone;
using AgroVision.Application.Interfaces.Repositories;
using AgroVision.Application.Interfaces.Services;
using AgroVision.Domain.Entities;
using AgroVision.Domain.Exceptions;

namespace AgroVision.Application.Services;

public class DroneService : IDroneService
{
    private readonly IDroneRepository _droneRepository;

    public DroneService(IDroneRepository droneRepository)
    {
        _droneRepository = droneRepository;
    }

    public async Task<IEnumerable<DroneResponseDto>> GetAllAsync()
    {
        var drones = await _droneRepository.GetAllAsync();

        return drones.Select(MapToResponse);
    }

    public async Task<DroneResponseDto?> GetByIdAsync(int id)
    {
        var drone = await _droneRepository.GetByIdAsync(id);

        return drone is null ? null : MapToResponse(drone);
    }

    public async Task<IEnumerable<DroneResponseDto>> GetAtivosAsync()
    {
        var drones = await _droneRepository.GetAtivosAsync();

        return drones.Select(MapToResponse);
    }

    public async Task<DroneResponseDto> CreateAsync(DroneCreateDto dto)
    {
        var droneExistente = await _droneRepository.GetByCodigoIdentificacaoAsync(dto.CodigoIdentificacao);

        if (droneExistente is not null)
            throw new DomainException("Já existe um drone cadastrado com este código de identificação.");

        var drone = new Drone(
            dto.CodigoIdentificacao,
            dto.Modelo,
            dto.Fabricante,
            dto.AutonomiaMinutos);

        await _droneRepository.AddAsync(drone);
        await _droneRepository.SaveChangesAsync();

        return MapToResponse(drone);
    }

    public async Task<DroneResponseDto?> UpdateAsync(int id, DroneUpdateDto dto)
    {
        var drone = await _droneRepository.GetByIdAsync(id);

        if (drone is null)
            return null;

        drone.AtualizarDados(
            dto.Modelo,
            dto.Fabricante,
            dto.AutonomiaMinutos);

        _droneRepository.Update(drone);
        await _droneRepository.SaveChangesAsync();

        return MapToResponse(drone);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var drone = await _droneRepository.GetByIdAsync(id);

        if (drone is null)
            return false;

        _droneRepository.Delete(drone);
        await _droneRepository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AtivarAsync(int id)
    {
        var drone = await _droneRepository.GetByIdAsync(id);

        if (drone is null)
            return false;

        drone.Ativar();

        _droneRepository.Update(drone);
        await _droneRepository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DesativarAsync(int id)
    {
        var drone = await _droneRepository.GetByIdAsync(id);

        if (drone is null)
            return false;

        drone.Desativar();

        _droneRepository.Update(drone);
        await _droneRepository.SaveChangesAsync();

        return true;
    }

    public async Task<bool?> VerificarAutonomiaAsync(int id, DroneAutonomiaDto dto)
    {
        var drone = await _droneRepository.GetByIdAsync(id);

        if (drone is null)
            return null;

        return drone.PossuiAutonomiaParaMissao(dto.DuracaoEstimadaMinutos);
    }

    private static DroneResponseDto MapToResponse(Drone drone)
    {
        return new DroneResponseDto
        {
            Id = drone.Id,
            CodigoIdentificacao = drone.CodigoIdentificacao,
            Modelo = drone.Modelo,
            Fabricante = drone.Fabricante,
            AutonomiaMinutos = drone.AutonomiaMinutos,
            Ativo = drone.Ativo,
            TotalMissoes = drone.Missoes?.Count ?? 0
        };
    }
}