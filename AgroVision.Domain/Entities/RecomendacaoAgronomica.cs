using AgroVision.Domain.Common;
using AgroVision.Domain.Enums;
using AgroVision.Domain.Exceptions;

namespace AgroVision.Domain.Entities;

public class RecomendacaoAgronomica : BaseEntity
{
    public int AnaliseDroneId { get; private set; }

    public string Titulo { get; private set; } = string.Empty;
    public string Descricao { get; private set; } = string.Empty;
    public PrioridadeRecomendacao Prioridade { get; private set; }
    public DateTime DataGeracao { get; private set; }
    public bool Concluida { get; private set; }

    public AnaliseDrone? AnaliseDrone { get; private set; }

    protected RecomendacaoAgronomica()
    {
    }

    public RecomendacaoAgronomica(
        int analiseDroneId,
        string titulo,
        string descricao,
        PrioridadeRecomendacao prioridade)
    {
        if (analiseDroneId <= 0)
            throw new DomainException("A análise do drone é obrigatória.");

        if (string.IsNullOrWhiteSpace(titulo))
            throw new DomainException("O título da recomendação é obrigatório.");

        if (string.IsNullOrWhiteSpace(descricao))
            throw new DomainException("A descrição da recomendação é obrigatória.");

        AnaliseDroneId = analiseDroneId;
        Titulo = titulo;
        Descricao = descricao;
        Prioridade = prioridade;
        DataGeracao = DateTime.Now;
        Concluida = false;
    }

    public void MarcarComoConcluida()
    {
        Concluida = true;
    }

    public void Reabrir()
    {
        Concluida = false;
    }

    public bool EhUrgente()
    {
        return Prioridade == PrioridadeRecomendacao.ALTA ||
               Prioridade == PrioridadeRecomendacao.CRITICA;
    }
}