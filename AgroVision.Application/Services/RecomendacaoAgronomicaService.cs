using AgroVision.Application.DTOs.RecomendacaoAgronomica;
using AgroVision.Application.Interfaces.Repositories;
using AgroVision.Application.Interfaces.Services;
using AgroVision.Domain.Entities;
using AgroVision.Domain.Exceptions;

namespace AgroVision.Application.Services;

public class RecomendacaoAgronomicaService : IRecomendacaoAgronomicaService
{
    private readonly IRecomendacaoAgronomicaRepository _recomendacaoRepository;
    private readonly IAnaliseDroneRepository _analiseDroneRepository;

    public RecomendacaoAgronomicaService(
        IRecomendacaoAgronomicaRepository recomendacaoRepository,
        IAnaliseDroneRepository analiseDroneRepository)
    {
        _recomendacaoRepository = recomendacaoRepository;
        _analiseDroneRepository = analiseDroneRepository;
    }

    public async Task<IEnumerable<RecomendacaoAgronomicaResponseDto>> GetAllAsync()
    {
        var recomendacoes = await _recomendacaoRepository.GetAllAsync();
        return recomendacoes.Select(MapToResponse);
    }

    public async Task<RecomendacaoAgronomicaResponseDto?> GetByIdAsync(int id)
    {
        var recomendacao = await _recomendacaoRepository.GetByIdAsync(id);
        return recomendacao is null ? null : MapToResponse(recomendacao);
    }

    public async Task<IEnumerable<RecomendacaoAgronomicaResponseDto>> GetByAnaliseDroneIdAsync(int analiseDroneId)
    {
        var recomendacoes = await _recomendacaoRepository.GetByAnaliseDroneIdAsync(analiseDroneId);
        return recomendacoes.Select(MapToResponse);
    }

    public async Task<IEnumerable<RecomendacaoAgronomicaResponseDto>> GetPendentesAsync()
    {
        var recomendacoes = await _recomendacaoRepository.GetPendentesAsync();
        return recomendacoes.Select(MapToResponse);
    }

    public async Task<IEnumerable<RecomendacaoAgronomicaResponseDto>> GetUrgentesAsync()
    {
        var recomendacoes = await _recomendacaoRepository.GetUrgentesAsync();
        return recomendacoes.Select(MapToResponse);
    }

    public async Task<RecomendacaoAgronomicaResponseDto> CreateAsync(RecomendacaoAgronomicaCreateDto dto)
    {
        var analiseExiste = await _analiseDroneRepository.ExistsAsync(dto.AnaliseDroneId);

        if (!analiseExiste)
            throw new DomainException("Análise de drone informada não encontrada.");

        var recomendacao = new RecomendacaoAgronomica(
            dto.AnaliseDroneId,
            dto.Titulo,
            dto.Descricao,
            dto.Prioridade);

        await _recomendacaoRepository.AddAsync(recomendacao);
        await _recomendacaoRepository.SaveChangesAsync();

        return MapToResponse(recomendacao);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var recomendacao = await _recomendacaoRepository.GetByIdAsync(id);

        if (recomendacao is null)
            return false;

        _recomendacaoRepository.Delete(recomendacao);
        await _recomendacaoRepository.SaveChangesAsync();

        return true;
    }

    public async Task<RecomendacaoAgronomicaResponseDto?> MarcarComoConcluidaAsync(int id)
    {
        var recomendacao = await _recomendacaoRepository.GetByIdAsync(id);

        if (recomendacao is null)
            return null;

        recomendacao.MarcarComoConcluida();

        _recomendacaoRepository.Update(recomendacao);
        await _recomendacaoRepository.SaveChangesAsync();

        return MapToResponse(recomendacao);
    }

    public async Task<RecomendacaoAgronomicaResponseDto?> ReabrirAsync(int id)
    {
        var recomendacao = await _recomendacaoRepository.GetByIdAsync(id);

        if (recomendacao is null)
            return null;

        recomendacao.Reabrir();

        _recomendacaoRepository.Update(recomendacao);
        await _recomendacaoRepository.SaveChangesAsync();

        return MapToResponse(recomendacao);
    }

    private static RecomendacaoAgronomicaResponseDto MapToResponse(RecomendacaoAgronomica recomendacao)
    {
        return new RecomendacaoAgronomicaResponseDto
        {
            Id = recomendacao.Id,
            AnaliseDroneId = recomendacao.AnaliseDroneId,
            Titulo = recomendacao.Titulo,
            Descricao = recomendacao.Descricao,
            Prioridade = recomendacao.Prioridade,
            DataGeracao = recomendacao.DataGeracao,
            Concluida = recomendacao.Concluida,
            Urgente = recomendacao.EhUrgente()
        };
    }
}