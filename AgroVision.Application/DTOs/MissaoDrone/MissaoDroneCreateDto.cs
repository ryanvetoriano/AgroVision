namespace AgroVision.Application.DTOs.MissaoDrone;

public class MissaoDroneCreateDto
{
    public int DroneId { get; set; }
    public int PlantacaoId { get; set; }
    public DateTime DataInicio { get; set; }
    public decimal AreaMapeada { get; set; }
    public decimal AltitudeMedia { get; set; }
}