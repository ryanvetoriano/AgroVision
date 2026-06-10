using AgroVision.Application.DTOs.LeituraSensor;
using AgroVision.Application.Interfaces.Repositories;
using AgroVision.Application.Interfaces.Services;
using AgroVision.Domain.Entities;
using AgroVision.Domain.Exceptions;

namespace AgroVision.Application.Services;

public class LeituraSensorService : ILeituraSensorService
{
    private readonly ILeituraSensorRepository _leituraSensorRepository;
    private readonly IMissaoDroneRepository _missaoDroneRepository;

    public LeituraSensorService(
        ILeituraSensorRepository leituraSensorRepository,
        IMissaoDroneRepository missaoDroneRepository)
    {
        _leituraSensorRepository = leituraSensorRepository;
        _missaoDroneRepository = missaoDroneRepository;
    }

    public async Task<IEnumerable<LeituraSensorResponseDto>> GetAllAsync()
    {
        var leituras = await _leituraSensorRepository.GetAllAsync();

        return leituras.Select(MapToResponse);
    }

    public async Task<LeituraSensorResponseDto?> GetByIdAsync(int id)
    {
        var leitura = await _leituraSensorRepository.GetByIdAsync(id);

        return leitura is null ? null : MapToResponse(leitura);
    }

    public async Task<IEnumerable<LeituraSensorResponseDto>> GetByMissaoDroneIdAsync(int missaoDroneId)
    {
        var leituras = await _leituraSensorRepository.GetByMissaoDroneIdAsync(missaoDroneId);

        return leituras.Select(MapToResponse);
    }

    public async Task<IEnumerable<LeituraSensorResponseDto>> GetLeiturasForaDoPadraoAsync()
    {
        var leituras = await _leituraSensorRepository.GetLeiturasForaDoPadraoAsync();

        return leituras.Select(MapToResponse);
    }

    public async Task<LeituraSensorResponseDto> CreateAsync(LeituraSensorCreateDto dto)
    {
        var missaoExiste = await _missaoDroneRepository.ExistsAsync(dto.MissaoDroneId);

        if (!missaoExiste)
            throw new DomainException("Missão de drone informada não encontrada.");

        var leitura = new LeituraSensor(
            dto.MissaoDroneId,
            dto.TipoSensor,
            dto.Valor,
            dto.UnidadeMedida,
            dto.DataLeitura,
            dto.Latitude,
            dto.Longitude);

        await _leituraSensorRepository.AddAsync(leitura);
        await _leituraSensorRepository.SaveChangesAsync();

        return MapToResponse(leitura);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var leitura = await _leituraSensorRepository.GetByIdAsync(id);

        if (leitura is null)
            return false;

        _leituraSensorRepository.Delete(leitura);
        await _leituraSensorRepository.SaveChangesAsync();

        return true;
    }

    private static LeituraSensorResponseDto MapToResponse(LeituraSensor leitura)
    {
        return new LeituraSensorResponseDto
        {
            Id = leitura.Id,
            MissaoDroneId = leitura.MissaoDroneId,
            TipoSensor = leitura.TipoSensor,
            Valor = leitura.Valor,
            UnidadeMedida = leitura.UnidadeMedida,
            DataLeitura = leitura.DataLeitura,
            Latitude = leitura.Latitude,
            Longitude = leitura.Longitude,
            ForaDoPadrao = leitura.EstaForaDoPadrao()
        };
    }
}