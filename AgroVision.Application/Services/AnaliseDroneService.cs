using AgroVision.Application.DTOs.AnaliseDrone;
using AgroVision.Application.Interfaces.Repositories;
using AgroVision.Application.Interfaces.Services;
using AgroVision.Domain.Entities;

namespace AgroVision.Application.Services;

public class AnaliseDroneService : IAnaliseDroneService
{
    private readonly IAnaliseDroneRepository _analiseDroneRepository;
    private readonly IPlantacaoRepository _plantacaoRepository;
    private readonly IMissaoDroneRepository _missaoDroneRepository;

    public AnaliseDroneService(
        IAnaliseDroneRepository analiseDroneRepository,
        IPlantacaoRepository plantacaoRepository,
        IMissaoDroneRepository missaoDroneRepository)
    {
        _analiseDroneRepository = analiseDroneRepository;
        _plantacaoRepository = plantacaoRepository;
        _missaoDroneRepository = missaoDroneRepository;
    }

    public async Task<IEnumerable<AnaliseDroneResponseDto>> GetAllAsync()
    {
        var analises = await _analiseDroneRepository.GetAllAsync();

        return analises.Select(MapToResponse);
    }

    public async Task<AnaliseDroneResponseDto?> GetByIdAsync(int id)
    {
        var analise = await _analiseDroneRepository.GetByIdAsync(id);

        return analise is null ? null : MapToResponse(analise);
    }

    public async Task<IEnumerable<AnaliseDroneResponseDto>> GetByPlantacaoIdAsync(int plantacaoId)
    {
        var analises = await _analiseDroneRepository.GetByPlantacaoIdAsync(plantacaoId);

        return analises.Select(MapToResponse);
    }

    public async Task<IEnumerable<AnaliseDroneResponseDto>> GetAnalisesCriticasAsync()
    {
        var analises = await _analiseDroneRepository.GetAnalisesCriticasAsync();

        return analises.Select(MapToResponse);
    }

    public async Task<AnaliseDroneResponseDto?> GetUltimaAnaliseByPlantacaoIdAsync(int plantacaoId)
    {
        var analise = await _analiseDroneRepository.GetUltimaAnaliseByPlantacaoIdAsync(plantacaoId);

        return analise is null ? null : MapToResponse(analise);
    }

    public async Task<AnaliseDroneResponseDto> CreateAsync(AnaliseDroneCreateDto dto)
    {
        var plantacaoExiste = await _plantacaoRepository.ExistsAsync(dto.PlantacaoId);

        if (!plantacaoExiste)
            throw new Exception("Plantação informada não encontrada.");

        if (dto.MissaoDroneId is not null)
        {
            var missaoExiste = await _missaoDroneRepository.ExistsAsync(dto.MissaoDroneId.Value);

            if (!missaoExiste)
                throw new Exception("Missão de drone informada não encontrada.");
        }

        var analise = new AnaliseDrone(
            dto.PlantacaoId,
            dto.MissaoDroneId,
            dto.DataAnalise,
            dto.IndiceSaude,
            dto.UmidadeSolo,
            dto.TemperaturaMedia,
            dto.IndiceVegetacaoNdvi,
            dto.AreaAfetadaPercentual,
            dto.PragasDetectadas,
            dto.ObservacaoImagem);

        await _analiseDroneRepository.AddAsync(analise);
        await _analiseDroneRepository.SaveChangesAsync();

        return MapToResponse(analise);
    }

    public async Task<AnaliseDroneResponseDto?> UpdateAsync(int id, AnaliseDroneUpdateDto dto)
    {
        var analise = await _analiseDroneRepository.GetByIdAsync(id);

        if (analise is null)
            return null;

        analise.Atualizar(
            dto.IndiceSaude,
            dto.UmidadeSolo,
            dto.TemperaturaMedia,
            dto.IndiceVegetacaoNdvi,
            dto.AreaAfetadaPercentual,
            dto.PragasDetectadas,
            dto.ObservacaoImagem);

        _analiseDroneRepository.Update(analise);
        await _analiseDroneRepository.SaveChangesAsync();

        return MapToResponse(analise);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var analise = await _analiseDroneRepository.GetByIdAsync(id);

        if (analise is null)
            return false;

        _analiseDroneRepository.Delete(analise);
        await _analiseDroneRepository.SaveChangesAsync();

        return true;
    }

    private static AnaliseDroneResponseDto MapToResponse(AnaliseDrone analise)
    {
        return new AnaliseDroneResponseDto
        {
            Id = analise.Id,
            PlantacaoId = analise.PlantacaoId,
            Cultura = analise.Plantacao?.Cultura,
            MissaoDroneId = analise.MissaoDroneId,
            DataAnalise = analise.DataAnalise,
            IndiceSaude = analise.IndiceSaude,
            UmidadeSolo = analise.UmidadeSolo,
            TemperaturaMedia = analise.TemperaturaMedia,
            IndiceVegetacaoNdvi = analise.IndiceVegetacaoNdvi,
            AreaAfetadaPercentual = analise.AreaAfetadaPercentual,
            PragasDetectadas = analise.PragasDetectadas,
            NivelRisco = analise.NivelRisco,
            StatusAnalise = analise.StatusAnalise,
            Recomendacao = analise.Recomendacao,
            ObservacaoImagem = analise.ObservacaoImagem,
            ExigeIntervencao = analise.ExigeIntervencao(),
            ResumoDiagnostico = analise.GerarResumoDiagnostico()
        };
    }
}