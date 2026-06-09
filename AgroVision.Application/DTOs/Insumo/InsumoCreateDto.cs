namespace AgroVision.Application.DTOs.Insumo;

public class InsumoCreateDto
{
    public int PlantacaoId { get; set; }
    public string NomeInsumo { get; set; } = string.Empty;
    public string UnidadeMedida { get; set; } = string.Empty;
    public decimal QuantidadeEstoque { get; set; }
    public decimal EstoqueMinimo { get; set; }
}