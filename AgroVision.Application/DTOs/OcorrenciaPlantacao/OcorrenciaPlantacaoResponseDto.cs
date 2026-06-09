using AgroVision.Domain.Enums;

namespace AgroVision.Application.DTOs.OcorrenciaPlantacao;

public class OcorrenciaPlantacaoResponseDto
{
    public int Id { get; set; }
    public int PlantacaoId { get; set; }
    public string? Cultura { get; set; }
    public int? AnaliseDroneId { get; set; }
    public TipoOcorrencia TipoOcorrencia { get; set; }
    public NivelRisco NivelRisco { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public DateTime DataOcorrencia { get; set; }
    public bool Resolvida { get; set; }
    public bool ExigeAcaoImediata { get; set; }
}