using AgroVision.Application.DTOs.Plantacao;
using AgroVision.Application.Interfaces.Repositories;
using AgroVision.Application.Interfaces.Services;
using AgroVision.Domain.Entities;

namespace AgroVision.Application.Services;

public class PlantacaoService : IPlantacaoService
{
    private readonly IPlantacaoRepository _plantacaoRepository;
    private readonly IUsuarioRepository _usuarioRepository;

    public PlantacaoService(
        IPlantacaoRepository plantacaoRepository,
        IUsuarioRepository usuarioRepository)
    {
        _plantacaoRepository = plantacaoRepository;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<IEnumerable<PlantacaoResponseDto>> GetAllAsync()
    {
        var plantacoes = await _plantacaoRepository.GetAllAsync();

        return plantacoes.Select(MapToResponse);
    }

    public async Task<PlantacaoResponseDto?> GetByIdAsync(int id)
    {
        var plantacao = await _plantacaoRepository.GetByIdAsync(id);

        return plantacao is null ? null : MapToResponse(plantacao);
    }

    public async Task<IEnumerable<PlantacaoResponseDto>> GetByUsuarioIdAsync(int usuarioId)
    {
        var plantacoes = await _plantacaoRepository.GetByUsuarioIdAsync(usuarioId);

        return plantacoes.Select(MapToResponse);
    }

    public async Task<IEnumerable<PlantacaoResponseDto>> GetAtivasAsync()
    {
        var plantacoes = await _plantacaoRepository.GetAtivasAsync();

        return plantacoes.Select(MapToResponse);
    }

    public async Task<PlantacaoResponseDto> CreateAsync(PlantacaoCreateDto dto)
    {
        var usuarioExiste = await _usuarioRepository.ExistsAsync(dto.UsuarioId);

        if (!usuarioExiste)
            throw new Exception("Usuário informado não encontrado.");

        var plantacao = new Plantacao(
            dto.UsuarioId,
            dto.TipoPlantio,
            dto.Cultura,
            dto.AreaPlantio,
            dto.ProdutividadeEstimada,
            dto.DataPlantio,
            dto.LocalPlantio,
            dto.StatusPlantio);

        await _plantacaoRepository.AddAsync(plantacao);
        await _plantacaoRepository.SaveChangesAsync();

        return MapToResponse(plantacao);
    }

    public async Task<PlantacaoResponseDto?> UpdateAsync(int id, PlantacaoUpdateDto dto)
    {
        var plantacao = await _plantacaoRepository.GetByIdAsync(id);

        if (plantacao is null)
            return null;

        plantacao.Atualizar(
            dto.TipoPlantio,
            dto.Cultura,
            dto.AreaPlantio,
            dto.ProdutividadeEstimada,
            dto.DataPlantio,
            dto.LocalPlantio,
            dto.StatusPlantio);

        _plantacaoRepository.Update(plantacao);
        await _plantacaoRepository.SaveChangesAsync();

        return MapToResponse(plantacao);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var plantacao = await _plantacaoRepository.GetByIdAsync(id);

        if (plantacao is null)
            return false;

        _plantacaoRepository.Delete(plantacao);
        await _plantacaoRepository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> EncerrarAsync(int id)
    {
        var plantacao = await _plantacaoRepository.GetByIdAsync(id);

        if (plantacao is null)
            return false;

        plantacao.EncerrarPlantacao();

        _plantacaoRepository.Update(plantacao);
        await _plantacaoRepository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> ReativarAsync(int id)
    {
        var plantacao = await _plantacaoRepository.GetByIdAsync(id);

        if (plantacao is null)
            return false;

        plantacao.ReativarPlantacao();

        _plantacaoRepository.Update(plantacao);
        await _plantacaoRepository.SaveChangesAsync();

        return true;
    }

    private static PlantacaoResponseDto MapToResponse(Plantacao plantacao)
    {
        return new PlantacaoResponseDto
        {
            Id = plantacao.Id,
            UsuarioId = plantacao.UsuarioId,
            NomeUsuario = plantacao.Usuario?.Nome,
            TipoPlantio = plantacao.TipoPlantio,
            Cultura = plantacao.Cultura,
            AreaPlantio = plantacao.AreaPlantio,
            ProdutividadeEstimada = plantacao.ProdutividadeEstimada,
            DataPlantio = plantacao.DataPlantio,
            LocalPlantio = plantacao.LocalPlantio,
            StatusPlantio = plantacao.StatusPlantio,
            Ativa = plantacao.Ativa,
            IdadeEmDias = plantacao.CalcularIdadeEmDias(),
            EmRisco = plantacao.EstaEmRisco(),
            PossuiOcorrenciasPendentes = plantacao.PossuiOcorrenciasPendentes()
        };
    }
}