namespace AgroVision.Application.DTOs.AnaliseDrone;

public class AnaliseDroneUpdateDto
{
    public decimal IndiceSaude { get; set; }
    public decimal UmidadeSolo { get; set; }
    public decimal TemperaturaMedia { get; set; }
    public decimal IndiceVegetacaoNdvi { get; set; }
    public decimal AreaAfetadaPercentual { get; set; }
    public bool PragasDetectadas { get; set; }
    public string ObservacaoImagem { get; set; } = string.Empty;
}