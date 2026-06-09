namespace AgroVision.Application.DTOs.Insumo;

public class InsumoUpdateDto
{
    public string NomeInsumo { get; set; } = string.Empty;
    public string UnidadeMedida { get; set; } = string.Empty;
    public decimal QuantidadeEstoque { get; set; }
    public decimal EstoqueMinimo { get; set; }
}