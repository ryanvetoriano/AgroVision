namespace AgroVision.Application.DTOs.Safra;

public class SafraUpdateDto
{
    public DateTime DataColheita { get; set; }
    public decimal QuantidadeColhida { get; set; }
    public decimal ReceitaEstimada { get; set; }
    public decimal PerdaEstimada { get; set; }
}