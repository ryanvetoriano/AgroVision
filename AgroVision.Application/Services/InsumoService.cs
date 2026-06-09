using AgroVision.Application.DTOs.Insumo;
using AgroVision.Application.Interfaces.Repositories;
using AgroVision.Application.Interfaces.Services;
using AgroVision.Domain.Entities;

namespace AgroVision.Application.Services;

public class InsumoService : IInsumoService
{
    private readonly IInsumoRepository _insumoRepository;
    private readonly IPlantacaoRepository _plantacaoRepository;

    public InsumoService(
        IInsumoRepository insumoRepository,
        IPlantacaoRepository plantacaoRepository)
    {
        _insumoRepository = insumoRepository;
        _plantacaoRepository = plantacaoRepository;
    }

    public async Task<IEnumerable<InsumoResponseDto>> GetAllAsync()
    {
        var insumos = await _insumoRepository.GetAllAsync();

        return insumos.Select(MapToResponse);
    }

    public async Task<InsumoResponseDto?> GetByIdAsync(int id)
    {
        var insumo = await _insumoRepository.GetByIdAsync(id);

        return insumo is null ? null : MapToResponse(insumo);
    }

    public async Task<IEnumerable<InsumoResponseDto>> GetByPlantacaoIdAsync(int plantacaoId)
    {
        var insumos = await _insumoRepository.GetByPlantacaoIdAsync(plantacaoId);

        return insumos.Select(MapToResponse);
    }

    public async Task<IEnumerable<InsumoResponseDto>> GetComEstoqueBaixoAsync()
    {
        var insumos = await _insumoRepository.GetComEstoqueBaixoAsync();

        return insumos.Select(MapToResponse);
    }

    public async Task<InsumoResponseDto> CreateAsync(InsumoCreateDto dto)
    {
        var plantacaoExiste = await _plantacaoRepository.ExistsAsync(dto.PlantacaoId);

        if (!plantacaoExiste)
            throw new Exception("Plantação informada não encontrada.");

        var insumo = new Insumo(
            dto.PlantacaoId,
            dto.NomeInsumo,
            dto.UnidadeMedida,
            dto.QuantidadeEstoque,
            dto.EstoqueMinimo);

        await _insumoRepository.AddAsync(insumo);
        await _insumoRepository.SaveChangesAsync();

        return MapToResponse(insumo);
    }

    public async Task<InsumoResponseDto?> UpdateAsync(int id, InsumoUpdateDto dto)
    {
        var insumo = await _insumoRepository.GetByIdAsync(id);

        if (insumo is null)
            return null;

        insumo.Atualizar(
            dto.NomeInsumo,
            dto.UnidadeMedida,
            dto.QuantidadeEstoque,
            dto.EstoqueMinimo);

        _insumoRepository.Update(insumo);
        await _insumoRepository.SaveChangesAsync();

        return MapToResponse(insumo);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var insumo = await _insumoRepository.GetByIdAsync(id);

        if (insumo is null)
            return false;

        _insumoRepository.Delete(insumo);
        await _insumoRepository.SaveChangesAsync();

        return true;
    }

    public async Task<InsumoResponseDto?> RegistrarAplicacaoAsync(int id, InsumoAplicacaoDto dto)
    {
        var insumo = await _insumoRepository.GetByIdAsync(id);

        if (insumo is null)
            return null;

        insumo.RegistrarAplicacao(dto.QuantidadeAplicada);

        _insumoRepository.Update(insumo);
        await _insumoRepository.SaveChangesAsync();

        return MapToResponse(insumo);
    }

    public async Task<InsumoResponseDto?> ReporEstoqueAsync(int id, InsumoReposicaoDto dto)
    {
        var insumo = await _insumoRepository.GetByIdAsync(id);

        if (insumo is null)
            return null;

        insumo.ReporEstoque(dto.Quantidade);

        _insumoRepository.Update(insumo);
        await _insumoRepository.SaveChangesAsync();

        return MapToResponse(insumo);
    }

    private static InsumoResponseDto MapToResponse(Insumo insumo)
    {
        return new InsumoResponseDto
        {
            Id = insumo.Id,
            PlantacaoId = insumo.PlantacaoId,
            Cultura = insumo.Plantacao?.Cultura,
            NomeInsumo = insumo.NomeInsumo,
            UnidadeMedida = insumo.UnidadeMedida,
            QuantidadeEstoque = insumo.QuantidadeEstoque,
            EstoqueMinimo = insumo.EstoqueMinimo,
            DataUltimaAplicacao = insumo.DataUltimaAplicacao,
            EstoqueBaixo = insumo.EstoqueBaixo(),
            PercentualEstoque = insumo.CalcularPercentualEstoque()
        };
    }
}