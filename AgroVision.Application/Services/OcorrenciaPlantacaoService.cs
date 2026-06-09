using AgroVision.Application.DTOs.OcorrenciaPlantacao;
using AgroVision.Application.Interfaces.Repositories;
using AgroVision.Application.Interfaces.Services;
using AgroVision.Domain.Entities;

namespace AgroVision.Application.Services;

public class OcorrenciaPlantacaoService : IOcorrenciaPlantacaoService
{
    private readonly IOcorrenciaPlantacaoRepository _ocorrenciaRepository;
    private readonly IPlantacaoRepository _plantacaoRepository;
    private readonly IAnaliseDroneRepository _analiseDroneRepository;

    public OcorrenciaPlantacaoService(
        IOcorrenciaPlantacaoRepository ocorrenciaRepository,
        IPlantacaoRepository plantacaoRepository,
        IAnaliseDroneRepository analiseDroneRepository)
    {
        _ocorrenciaRepository = ocorrenciaRepository;
        _plantacaoRepository = plantacaoRepository;
        _analiseDroneRepository = analiseDroneRepository;
    }

    public async Task<IEnumerable<OcorrenciaPlantacaoResponseDto>> GetAllAsync()
    {
        var ocorrencias = await _ocorrenciaRepository.GetAllAsync();
        return ocorrencias.Select(MapToResponse);
    }

    public async Task<OcorrenciaPlantacaoResponseDto?> GetByIdAsync(int id)
    {
        var ocorrencia = await _ocorrenciaRepository.GetByIdAsync(id);
        return ocorrencia is null ? null : MapToResponse(ocorrencia);
    }

    public async Task<IEnumerable<OcorrenciaPlantacaoResponseDto>> GetByPlantacaoIdAsync(int plantacaoId)
    {
        var ocorrencias = await _ocorrenciaRepository.GetByPlantacaoIdAsync(plantacaoId);
        return ocorrencias.Select(MapToResponse);
    }

    public async Task<IEnumerable<OcorrenciaPlantacaoResponseDto>> GetPendentesAsync()
    {
        var ocorrencias = await _ocorrenciaRepository.GetPendentesAsync();
        return ocorrencias.Select(MapToResponse);
    }

    public async Task<IEnumerable<OcorrenciaPlantacaoResponseDto>> GetCriticasAsync()
    {
        var ocorrencias = await _ocorrenciaRepository.GetCriticasAsync();
        return ocorrencias.Select(MapToResponse);
    }

    public async Task<OcorrenciaPlantacaoResponseDto> CreateAsync(OcorrenciaPlantacaoCreateDto dto)
    {
        var plantacaoExiste = await _plantacaoRepository.ExistsAsync(dto.PlantacaoId);

        if (!plantacaoExiste)
            throw new Exception("Plantação informada não encontrada.");

        if (dto.AnaliseDroneId is not null)
        {
            var analiseExiste = await _analiseDroneRepository.ExistsAsync(dto.AnaliseDroneId.Value);

            if (!analiseExiste)
                throw new Exception("Análise de drone informada não encontrada.");
        }

        var ocorrencia = new OcorrenciaPlantacao(
            dto.PlantacaoId,
            dto.AnaliseDroneId,
            dto.TipoOcorrencia,
            dto.NivelRisco,
            dto.Descricao);

        await _ocorrenciaRepository.AddAsync(ocorrencia);
        await _ocorrenciaRepository.SaveChangesAsync();

        return MapToResponse(ocorrencia);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var ocorrencia = await _ocorrenciaRepository.GetByIdAsync(id);

        if (ocorrencia is null)
            return false;

        _ocorrenciaRepository.Delete(ocorrencia);
        await _ocorrenciaRepository.SaveChangesAsync();

        return true;
    }

    public async Task<OcorrenciaPlantacaoResponseDto?> MarcarComoResolvidaAsync(int id)
    {
        var ocorrencia = await _ocorrenciaRepository.GetByIdAsync(id);

        if (ocorrencia is null)
            return null;

        ocorrencia.MarcarComoResolvida();

        _ocorrenciaRepository.Update(ocorrencia);
        await _ocorrenciaRepository.SaveChangesAsync();

        return MapToResponse(ocorrencia);
    }

    public async Task<OcorrenciaPlantacaoResponseDto?> ReabrirAsync(int id)
    {
        var ocorrencia = await _ocorrenciaRepository.GetByIdAsync(id);

        if (ocorrencia is null)
            return null;

        ocorrencia.Reabrir();

        _ocorrenciaRepository.Update(ocorrencia);
        await _ocorrenciaRepository.SaveChangesAsync();

        return MapToResponse(ocorrencia);
    }

    private static OcorrenciaPlantacaoResponseDto MapToResponse(OcorrenciaPlantacao ocorrencia)
    {
        return new OcorrenciaPlantacaoResponseDto
        {
            Id = ocorrencia.Id,
            PlantacaoId = ocorrencia.PlantacaoId,
            Cultura = ocorrencia.Plantacao?.Cultura,
            AnaliseDroneId = ocorrencia.AnaliseDroneId,
            TipoOcorrencia = ocorrencia.TipoOcorrencia,
            NivelRisco = ocorrencia.NivelRisco,
            Descricao = ocorrencia.Descricao,
            DataOcorrencia = ocorrencia.DataOcorrencia,
            Resolvida = ocorrencia.Resolvida,
            ExigeAcaoImediata = ocorrencia.ExigeAcaoImediata()
        };
    }
}