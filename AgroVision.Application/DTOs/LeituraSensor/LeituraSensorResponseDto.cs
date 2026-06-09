using AgroVision.Domain.Enums;

namespace AgroVision.Application.DTOs.LeituraSensor;

public class LeituraSensorResponseDto
{
    public int Id { get; set; }
    public int MissaoDroneId { get; set; }
    public TipoSensor TipoSensor { get; set; }
    public decimal Valor { get; set; }
    public string UnidadeMedida { get; set; } = string.Empty;
    public DateTime DataLeitura { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public bool ForaDoPadrao { get; set; }
}