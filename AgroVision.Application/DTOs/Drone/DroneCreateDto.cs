namespace AgroVision.Application.DTOs.Drone;

public class DroneCreateDto
{
    public string CodigoIdentificacao { get; set; } = string.Empty;
    public string Modelo { get; set; } = string.Empty;
    public string Fabricante { get; set; } = string.Empty;
    public decimal AutonomiaMinutos { get; set; }
}