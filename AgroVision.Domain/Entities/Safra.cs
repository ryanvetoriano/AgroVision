using AgroVision.Domain.Common;
using AgroVision.Domain.Exceptions;

namespace AgroVision.Domain.Entities;

public class Safra : BaseEntity
{
    public int PlantacaoId { get; private set; }

    public DateTime DataColheita { get; private set; }

    public decimal QuantidadeColhida { get; private set; }
    public decimal ReceitaEstimada { get; private set; }
    public decimal PerdaEstimada { get; private set; }

    public Plantacao? Plantacao { get; private set; }

    protected Safra()
    {
    }

    public Safra(
        int plantacaoId,
        DateTime dataColheita,
        decimal quantidadeColhida,
        decimal receitaEstimada,
        decimal perdaEstimada)
    {
        Validar(
            plantacaoId,
            quantidadeColhida,
            receitaEstimada,
            perdaEstimada);

        PlantacaoId = plantacaoId;
        DataColheita = dataColheita;
        QuantidadeColhida = quantidadeColhida;
        ReceitaEstimada = receitaEstimada;
        PerdaEstimada = perdaEstimada;
    }

    public void Atualizar(
        DateTime dataColheita,
        decimal quantidadeColhida,
        decimal receitaEstimada,
        decimal perdaEstimada)
    {
        Validar(
            PlantacaoId,
            quantidadeColhida,
            receitaEstimada,
            perdaEstimada);

        DataColheita = dataColheita;
        QuantidadeColhida = quantidadeColhida;
        ReceitaEstimada = receitaEstimada;
        PerdaEstimada = perdaEstimada;
    }

    public decimal CalcularProdutividade(decimal areaPlantio)
    {
        if (areaPlantio <= 0)
            throw new DomainException("A área de plantio deve ser maior que zero.");

        return QuantidadeColhida / areaPlantio;
    }

    public bool TeveBaixaProdutividade(decimal areaPlantio, decimal produtividadeEsperada)
    {
        if (produtividadeEsperada <= 0)
            throw new DomainException("A produtividade esperada deve ser maior que zero.");

        var produtividadeReal = CalcularProdutividade(areaPlantio);

        return produtividadeReal < produtividadeEsperada;
    }

    public decimal CalcularReceitaLiquidaEstimada()
    {
        return ReceitaEstimada - PerdaEstimada;
    }

    public bool TevePerdaRelevante()
    {
        if (ReceitaEstimada <= 0)
            return false;

        var percentualPerda = PerdaEstimada / ReceitaEstimada * 100;

        return percentualPerda >= 20;
    }

    private static void Validar(
        int plantacaoId,
        decimal quantidadeColhida,
        decimal receitaEstimada,
        decimal perdaEstimada)
    {
        if (plantacaoId <= 0)
            throw new DomainException("A plantação da safra é obrigatória.");

        if (quantidadeColhida <= 0)
            throw new DomainException("A quantidade colhida deve ser maior que zero.");

        if (receitaEstimada < 0)
            throw new DomainException("A receita estimada não pode ser negativa.");

        if (perdaEstimada < 0)
            throw new DomainException("A perda estimada não pode ser negativa.");

        if (perdaEstimada > receitaEstimada)
            throw new DomainException("A perda estimada não pode ser maior que a receita estimada.");
    }
}