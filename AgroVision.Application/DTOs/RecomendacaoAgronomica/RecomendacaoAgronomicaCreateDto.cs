using AgroVision.Domain.Enums;

namespace AgroVision.Application.DTOs.RecomendacaoAgronomica;

public class RecomendacaoAgronomicaCreateDto
{
    public int AnaliseDroneId { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public PrioridadeRecomendacao Prioridade { get; set; }
}