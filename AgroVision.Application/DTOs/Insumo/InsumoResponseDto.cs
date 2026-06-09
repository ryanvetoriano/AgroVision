namespace AgroVision.Application.DTOs.Insumo;

public class InsumoResponseDto
{
    public int Id { get; set; }
    public int PlantacaoId { get; set; }
    public string? Cultura { get; set; }
    public string NomeInsumo { get; set; } = string.Empty;
    public string UnidadeMedida { get; set; } = string.Empty;
    public decimal QuantidadeEstoque { get; set; }
    public decimal EstoqueMinimo { get; set; }
    public DateTime? DataUltimaAplicacao { get; set; }
    public bool EstoqueBaixo { get; set; }
    public decimal PercentualEstoque { get; set; }
}