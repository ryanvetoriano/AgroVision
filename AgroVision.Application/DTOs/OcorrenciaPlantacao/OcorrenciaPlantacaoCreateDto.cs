using AgroVision.Domain.Enums;

namespace AgroVision.Application.DTOs.OcorrenciaPlantacao;

public class OcorrenciaPlantacaoCreateDto
{
    public int PlantacaoId { get; set; }
    public int? AnaliseDroneId { get; set; }
    public TipoOcorrencia TipoOcorrencia { get; set; }
    public NivelRisco NivelRisco { get; set; }
    public string Descricao { get; set; } = string.Empty;
}