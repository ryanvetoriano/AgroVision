namespace AgroVision.Application.DTOs.Drone;

public class DroneUpdateDto
{
    public string Modelo { get; set; } = string.Empty;
    public string Fabricante { get; set; } = string.Empty;
    public decimal AutonomiaMinutos { get; set; }
}