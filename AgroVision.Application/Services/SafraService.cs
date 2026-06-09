using AgroVision.Application.DTOs.Safra;
using AgroVision.Application.Interfaces.Repositories;
using AgroVision.Application.Interfaces.Services;
using AgroVision.Domain.Entities;

namespace AgroVision.Application.Services;

public class SafraService : ISafraService
{
    private readonly ISafraRepository _safraRepository;
    private readonly IPlantacaoRepository _plantacaoRepository;

    public SafraService(
        ISafraRepository safraRepository,
        IPlantacaoRepository plantacaoRepository)
    {
        _safraRepository = safraRepository;
        _plantacaoRepository = plantacaoRepository;
    }

    public async Task<IEnumerable<SafraResponseDto>> GetAllAsync()
    {
        var safras = await _safraRepository.GetAllAsync();

        return safras.Select(MapToResponse);
    }

    public async Task<SafraResponseDto?> GetByIdAsync(int id)
    {
        var safra = await _safraRepository.GetByIdAsync(id);

        return safra is null ? null : MapToResponse(safra);
    }

    public async Task<IEnumerable<SafraResponseDto>> GetByPlantacaoIdAsync(int plantacaoId)
    {
        var safras = await _safraRepository.GetByPlantacaoIdAsync(plantacaoId);

        return safras.Select(MapToResponse);
    }

    public async Task<SafraResponseDto> CreateAsync(SafraCreateDto dto)
    {
        var plantacaoExiste = await _plantacaoRepository.ExistsAsync(dto.PlantacaoId);

        if (!plantacaoExiste)
            throw new Exception("Plantação informada não encontrada.");

        var safra = new Safra(
            dto.PlantacaoId,
            dto.DataColheita,
            dto.QuantidadeColhida,
            dto.ReceitaEstimada,
            dto.PerdaEstimada);

        await _safraRepository.AddAsync(safra);
        await _safraRepository.SaveChangesAsync();

        return MapToResponse(safra);
    }

    public async Task<SafraResponseDto?> UpdateAsync(int id, SafraUpdateDto dto)
    {
        var safra = await _safraRepository.GetByIdAsync(id);

        if (safra is null)
            return null;

        safra.Atualizar(
            dto.DataColheita,
            dto.QuantidadeColhida,
            dto.ReceitaEstimada,
            dto.PerdaEstimada);

        _safraRepository.Update(safra);
        await _safraRepository.SaveChangesAsync();

        return MapToResponse(safra);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var safra = await _safraRepository.GetByIdAsync(id);

        if (safra is null)
            return false;

        _safraRepository.Delete(safra);
        await _safraRepository.SaveChangesAsync();

        return true;
    }

    private static SafraResponseDto MapToResponse(Safra safra)
    {
        return new SafraResponseDto
        {
            Id = safra.Id,
            PlantacaoId = safra.PlantacaoId,
            Cultura = safra.Plantacao?.Cultura,
            DataColheita = safra.DataColheita,
            QuantidadeColhida = safra.QuantidadeColhida,
            ReceitaEstimada = safra.ReceitaEstimada,
            PerdaEstimada = safra.PerdaEstimada,
            ReceitaLiquidaEstimada = safra.CalcularReceitaLiquidaEstimada(),
            TevePerdaRelevante = safra.TevePerdaRelevante()
        };
    }
}