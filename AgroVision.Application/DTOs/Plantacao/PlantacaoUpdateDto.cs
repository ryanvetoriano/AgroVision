using AgroVision.Domain.Enums;

namespace AgroVision.Application.DTOs.Plantacao;

public class PlantacaoUpdateDto
{
    public string TipoPlantio { get; set; } = string.Empty;
    public string Cultura { get; set; } = string.Empty;
    public decimal AreaPlantio { get; set; }
    public decimal ProdutividadeEstimada { get; set; }
    public DateTime DataPlantio { get; set; }
    public string LocalPlantio { get; set; } = string.Empty;
    public StatusPlantio StatusPlantio { get; set; }
}