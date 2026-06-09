using AgroVision.Domain.Enums;

namespace AgroVision.Application.DTOs.AnaliseDrone;

public class AnaliseDroneResponseDto
{
    public int Id { get; set; }
    public int PlantacaoId { get; set; }
    public string? Cultura { get; set; }
    public int? MissaoDroneId { get; set; }
    public DateTime DataAnalise { get; set; }
    public decimal IndiceSaude { get; set; }
    public decimal UmidadeSolo { get; set; }
    public decimal TemperaturaMedia { get; set; }
    public decimal IndiceVegetacaoNdvi { get; set; }
    public decimal AreaAfetadaPercentual { get; set; }
    public bool PragasDetectadas { get; set; }
    public NivelRisco NivelRisco { get; set; }
    public StatusAnalise StatusAnalise { get; set; }
    public string Recomendacao { get; set; } = string.Empty;
    public string ObservacaoImagem { get; set; } = string.Empty;
    public bool ExigeIntervencao { get; set; }
    public string ResumoDiagnostico { get; set; } = string.Empty;
}