using AgroVision.Domain.Enums;

namespace AgroVision.Application.DTOs.Plantacao;

public class PlantacaoResponseDto
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public string? NomeUsuario { get; set; }
    public string TipoPlantio { get; set; } = string.Empty;
    public string Cultura { get; set; } = string.Empty;
    public decimal AreaPlantio { get; set; }
    public decimal ProdutividadeEstimada { get; set; }
    public DateTime DataPlantio { get; set; }
    public string LocalPlantio { get; set; } = string.Empty;
    public StatusPlantio StatusPlantio { get; set; }
    public bool Ativa { get; set; }
    public decimal IdadeEmDias { get; set; }
    public bool EmRisco { get; set; }
    public bool PossuiOcorrenciasPendentes { get; set; }
}