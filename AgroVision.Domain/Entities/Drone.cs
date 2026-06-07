using AgroVision.Domain.Common;
using AgroVision.Domain.Exceptions;

namespace AgroVision.Domain.Entities;

public class Drone : BaseEntity
{
    public string CodigoIdentificacao { get; private set; } = string.Empty;
    public string Modelo { get; private set; } = string.Empty;
    public string Fabricante { get; private set; } = string.Empty;
    public decimal AutonomiaMinutos { get; private set; }
    public bool Ativo { get; private set; }

    public ICollection<MissaoDrone> Missoes { get; private set; } = new List<MissaoDrone>();

    protected Drone()
    {
    }

    public Drone(string codigoIdentificacao, string modelo, string fabricante, decimal autonomiaMinutos)
    {
        if (string.IsNullOrWhiteSpace(codigoIdentificacao))
            throw new DomainException("O código de identificação do drone é obrigatório.");

        if (string.IsNullOrWhiteSpace(modelo))
            throw new DomainException("O modelo do drone é obrigatório.");

        if (string.IsNullOrWhiteSpace(fabricante))
            throw new DomainException("O fabricante do drone é obrigatório.");

        if (autonomiaMinutos <= 0)
            throw new DomainException("A autonomia do drone deve ser maior que zero.");

        CodigoIdentificacao = codigoIdentificacao;
        Modelo = modelo;
        Fabricante = fabricante;
        AutonomiaMinutos = autonomiaMinutos;
        Ativo = true;
    }

    public void AtualizarDados(string modelo, string fabricante, decimal autonomiaMinutos)
    {
        if (string.IsNullOrWhiteSpace(modelo))
            throw new DomainException("O modelo do drone é obrigatório.");

        if (string.IsNullOrWhiteSpace(fabricante))
            throw new DomainException("O fabricante do drone é obrigatório.");

        if (autonomiaMinutos <= 0)
            throw new DomainException("A autonomia do drone deve ser maior que zero.");

        Modelo = modelo;
        Fabricante = fabricante;
        AutonomiaMinutos = autonomiaMinutos;
    }

    public void Ativar()
    {
        Ativo = true;
    }

    public void Desativar()
    {
        Ativo = false;
    }

    public bool PossuiAutonomiaParaMissao(decimal duracaoEstimadaMinutos)
    {
        return Ativo && AutonomiaMinutos >= duracaoEstimadaMinutos;
    }
}