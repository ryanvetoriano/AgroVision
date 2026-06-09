using AgroVision.Domain.Enums;

namespace AgroVision.Application.DTOs.MissaoDrone;

public class MissaoDroneResponseDto
{
    public int Id { get; set; }
    public int DroneId { get; set; }
    public string? CodigoDrone { get; set; }
    public int PlantacaoId { get; set; }
    public string? Cultura { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
    public decimal AreaMapeada { get; set; }
    public decimal AltitudeMedia { get; set; }
    public StatusMissaoDrone Status { get; set; }
    public decimal DuracaoMinutos { get; set; }
    public int TotalLeiturasSensor { get; set; }
}