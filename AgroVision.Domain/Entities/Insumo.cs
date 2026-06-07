using AgroVision.Domain.Common;
using AgroVision.Domain.Exceptions;

namespace AgroVision.Domain.Entities;

public class Insumo : BaseEntity
{
    public int PlantacaoId { get; private set; }

    public string NomeInsumo { get; private set; } = string.Empty;
    public string UnidadeMedida { get; private set; } = string.Empty;

    public decimal QuantidadeEstoque { get; private set; }
    public decimal EstoqueMinimo { get; private set; }

    public DateTime? DataUltimaAplicacao { get; private set; }

    public Plantacao? Plantacao { get; private set; }

    protected Insumo()
    {
    }

    public Insumo(
        int plantacaoId,
        string nomeInsumo,
        string unidadeMedida,
        decimal quantidadeEstoque,
        decimal estoqueMinimo)
    {
        Validar(
            plantacaoId,
            nomeInsumo,
            unidadeMedida,
            quantidadeEstoque,
            estoqueMinimo);

        PlantacaoId = plantacaoId;
        NomeInsumo = nomeInsumo;
        UnidadeMedida = unidadeMedida;
        QuantidadeEstoque = quantidadeEstoque;
        EstoqueMinimo = estoqueMinimo;
    }

    public void Atualizar(
        string nomeInsumo,
        string unidadeMedida,
        decimal quantidadeEstoque,
        decimal estoqueMinimo)
    {
        Validar(
            PlantacaoId,
            nomeInsumo,
            unidadeMedida,
            quantidadeEstoque,
            estoqueMinimo);

        NomeInsumo = nomeInsumo;
        UnidadeMedida = unidadeMedida;
        QuantidadeEstoque = quantidadeEstoque;
        EstoqueMinimo = estoqueMinimo;
    }

    public bool EstoqueBaixo()
    {
        return QuantidadeEstoque <= EstoqueMinimo;
    }

    public void RegistrarAplicacao(decimal quantidadeAplicada)
    {
        if (quantidadeAplicada <= 0)
            throw new DomainException("A quantidade aplicada deve ser maior que zero.");

        if (quantidadeAplicada > QuantidadeEstoque)
            throw new DomainException("A quantidade aplicada não pode ser maior que o estoque disponível.");

        QuantidadeEstoque -= quantidadeAplicada;
        DataUltimaAplicacao = DateTime.Now;
    }

    public void ReporEstoque(decimal quantidade)
    {
        if (quantidade <= 0)
            throw new DomainException("A quantidade de reposição deve ser maior que zero.");

        QuantidadeEstoque += quantidade;
    }

    public decimal CalcularPercentualEstoque()
    {
        if (EstoqueMinimo <= 0)
            return 100;

        return QuantidadeEstoque / EstoqueMinimo * 100;
    }

    private static void Validar(
        int plantacaoId,
        string nomeInsumo,
        string unidadeMedida,
        decimal quantidadeEstoque,
        decimal estoqueMinimo)
    {
        if (plantacaoId <= 0)
            throw new DomainException("A plantação do insumo é obrigatória.");

        if (string.IsNullOrWhiteSpace(nomeInsumo))
            throw new DomainException("O nome do insumo é obrigatório.");

        if (string.IsNullOrWhiteSpace(unidadeMedida))
            throw new DomainException("A unidade de medida do insumo é obrigatória.");

        if (quantidadeEstoque < 0)
            throw new DomainException("A quantidade em estoque não pode ser negativa.");

        if (estoqueMinimo < 0)
            throw new DomainException("O estoque mínimo não pode ser negativo.");
    }
}