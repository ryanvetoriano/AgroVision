namespace AgroVision.Application.DTOs.Safra;

public class SafraResponseDto
{
    public int Id { get; set; }
    public int PlantacaoId { get; set; }
    public string? Cultura { get; set; }
    public DateTime DataColheita { get; set; }
    public decimal QuantidadeColhida { get; set; }
    public decimal ReceitaEstimada { get; set; }
    public decimal PerdaEstimada { get; set; }
    public decimal ReceitaLiquidaEstimada { get; set; }
    public bool TevePerdaRelevante { get; set; }
}