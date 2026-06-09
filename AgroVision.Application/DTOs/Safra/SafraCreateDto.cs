namespace AgroVision.Application.DTOs.Safra;

public class SafraCreateDto
{
    public int PlantacaoId { get; set; }
    public DateTime DataColheita { get; set; }
    public decimal QuantidadeColhida { get; set; }
    public decimal ReceitaEstimada { get; set; }
    public decimal PerdaEstimada { get; set; }
}