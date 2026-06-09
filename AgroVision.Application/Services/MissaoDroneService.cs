using AgroVision.Application.DTOs.MissaoDrone;
using AgroVision.Application.Interfaces.Repositories;
using AgroVision.Application.Interfaces.Services;
using AgroVision.Domain.Entities;

namespace AgroVision.Application.Services;

public class MissaoDroneService : IMissaoDroneService
{
    private readonly IMissaoDroneRepository _missaoDroneRepository;
    private readonly IDroneRepository _droneRepository;
    private readonly IPlantacaoRepository _plantacaoRepository;

    public MissaoDroneService(
        IMissaoDroneRepository missaoDroneRepository,
        IDroneRepository droneRepository,
        IPlantacaoRepository plantacaoRepository)
    {
        _missaoDroneRepository = missaoDroneRepository;
        _droneRepository = droneRepository;
        _plantacaoRepository = plantacaoRepository;
    }

    public async Task<IEnumerable<MissaoDroneResponseDto>> GetAllAsync()
    {
        var missoes = await _missaoDroneRepository.GetAllAsync();

        return missoes.Select(MapToResponse);
    }

    public async Task<MissaoDroneResponseDto?> GetByIdAsync(int id)
    {
        var missao = await _missaoDroneRepository.GetByIdAsync(id);

        return missao is null ? null : MapToResponse(missao);
    }

    public async Task<IEnumerable<MissaoDroneResponseDto>> GetByDroneIdAsync(int droneId)
    {
        var missoes = await _missaoDroneRepository.GetByDroneIdAsync(droneId);

        return missoes.Select(MapToResponse);
    }

    public async Task<IEnumerable<MissaoDroneResponseDto>> GetByPlantacaoIdAsync(int plantacaoId)
    {
        var missoes = await _missaoDroneRepository.GetByPlantacaoIdAsync(plantacaoId);

        return missoes.Select(MapToResponse);
    }

    public async Task<IEnumerable<MissaoDroneResponseDto>> GetMissoesEmAndamentoAsync()
    {
        var missoes = await _missaoDroneRepository.GetMissoesEmAndamentoAsync();

        return missoes.Select(MapToResponse);
    }

    public async Task<MissaoDroneResponseDto> CreateAsync(MissaoDroneCreateDto dto)
    {
        var drone = await _droneRepository.GetByIdAsync(dto.DroneId);

        if (drone is null)
            throw new Exception("Drone informado não encontrado.");

        if (!drone.Ativo)
            throw new Exception("Não é possível agendar missão para um drone inativo.");

        var plantacaoExiste = await _plantacaoRepository.ExistsAsync(dto.PlantacaoId);

        if (!plantacaoExiste)
            throw new Exception("Plantação informada não encontrada.");

        var missao = new MissaoDrone(
            dto.DroneId,
            dto.PlantacaoId,
            dto.DataInicio,
            dto.AreaMapeada,
            dto.AltitudeMedia);

        await _missaoDroneRepository.AddAsync(missao);
        await _missaoDroneRepository.SaveChangesAsync();

        return MapToResponse(missao);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var missao = await _missaoDroneRepository.GetByIdAsync(id);

        if (missao is null)
            return false;

        _missaoDroneRepository.Delete(missao);
        await _missaoDroneRepository.SaveChangesAsync();

        return true;
    }

    public async Task<MissaoDroneResponseDto?> IniciarMissaoAsync(int id)
    {
        var missao = await _missaoDroneRepository.GetByIdAsync(id);

        if (missao is null)
            return null;

        missao.IniciarMissao();

        _missaoDroneRepository.Update(missao);
        await _missaoDroneRepository.SaveChangesAsync();

        return MapToResponse(missao);
    }

    public async Task<MissaoDroneResponseDto?> FinalizarMissaoAsync(int id)
    {
        var missao = await _missaoDroneRepository.GetByIdAsync(id);

        if (missao is null)
            return null;

        missao.FinalizarMissao();

        _missaoDroneRepository.Update(missao);
        await _missaoDroneRepository.SaveChangesAsync();

        return MapToResponse(missao);
    }

    public async Task<MissaoDroneResponseDto?> CancelarMissaoAsync(int id)
    {
        var missao = await _missaoDroneRepository.GetByIdAsync(id);

        if (missao is null)
            return null;

        missao.CancelarMissao();

        _missaoDroneRepository.Update(missao);
        await _missaoDroneRepository.SaveChangesAsync();

        return MapToResponse(missao);
    }

    private static MissaoDroneResponseDto MapToResponse(MissaoDrone missao)
    {
        return new MissaoDroneResponseDto
        {
            Id = missao.Id,
            DroneId = missao.DroneId,
            CodigoDrone = missao.Drone?.CodigoIdentificacao,
            PlantacaoId = missao.PlantacaoId,
            Cultura = missao.Plantacao?.Cultura,
            DataInicio = missao.DataInicio,
            DataFim = missao.DataFim,
            AreaMapeada = missao.AreaMapeada,
            AltitudeMedia = missao.AltitudeMedia,
            Status = missao.Status,
            DuracaoMinutos = missao.CalcularDuracaoMinutos(),
            TotalLeiturasSensor = missao.LeiturasSensor?.Count ?? 0
        };
    }
}