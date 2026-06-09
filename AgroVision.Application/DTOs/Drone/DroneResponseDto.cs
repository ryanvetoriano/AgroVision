namespace AgroVision.Application.DTOs.Drone;

public class DroneResponseDto
{
    public int Id { get; set; }
    public string CodigoIdentificacao { get; set; } = string.Empty;
    public string Modelo { get; set; } = string.Empty;
    public string Fabricante { get; set; } = string.Empty;
    public decimal AutonomiaMinutos { get; set; }
    public bool Ativo { get; set; }
    public int TotalMissoes { get; set; }
}