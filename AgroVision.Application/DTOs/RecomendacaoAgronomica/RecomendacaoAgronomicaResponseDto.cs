using AgroVision.Domain.Enums;

namespace AgroVision.Application.DTOs.RecomendacaoAgronomica;

public class RecomendacaoAgronomicaResponseDto
{
    public int Id { get; set; }
    public int AnaliseDroneId { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public PrioridadeRecomendacao Prioridade { get; set; }
    public DateTime DataGeracao { get; set; }
    public bool Concluida { get; set; }
    public bool Urgente { get; set; }
}