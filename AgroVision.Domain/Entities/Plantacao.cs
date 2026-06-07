using AgroVision.Domain.Common;
using AgroVision.Domain.Enums;
using AgroVision.Domain.Exceptions;

namespace AgroVision.Domain.Entities;

public class Plantacao : BaseEntity
{
    public int UsuarioId { get; private set; }

    public string TipoPlantio { get; private set; } = string.Empty;
    public string Cultura { get; private set; } = string.Empty;

    public decimal AreaPlantio { get; private set; }
    public decimal ProdutividadeEstimada { get; private set; }

    public DateTime DataPlantio { get; private set; }

    public string LocalPlantio { get; private set; } = string.Empty;

    public StatusPlantio StatusPlantio { get; private set; }

    public bool Ativa { get; private set; }

    public Usuario? Usuario { get; private set; }

    public ICollection<Safra> Safras { get; private set; } = new List<Safra>();
    public ICollection<Insumo> Insumos { get; private set; } = new List<Insumo>();
    public ICollection<AnaliseDrone> AnalisesDrone { get; private set; } = new List<AnaliseDrone>();
    public ICollection<MissaoDrone> MissoesDrone { get; private set; } = new List<MissaoDrone>();
    public ICollection<OcorrenciaPlantacao> Ocorrencias { get; private set; } = new List<OcorrenciaPlantacao>();

    protected Plantacao()
    {
    }

    public Plantacao(
        int usuarioId,
        string tipoPlantio,
        string cultura,
        decimal areaPlantio,
        decimal produtividadeEstimada,
        DateTime dataPlantio,
        string localPlantio,
        StatusPlantio statusPlantio)
    {
        Validar(usuarioId, tipoPlantio, cultura, areaPlantio, produtividadeEstimada, localPlantio);

        UsuarioId = usuarioId;
        TipoPlantio = tipoPlantio;
        Cultura = cultura;
        AreaPlantio = areaPlantio;
        ProdutividadeEstimada = produtividadeEstimada;
        DataPlantio = dataPlantio;
        LocalPlantio = localPlantio;
        StatusPlantio = statusPlantio;
        Ativa = true;
    }

    public void Atualizar(
        string tipoPlantio,
        string cultura,
        decimal areaPlantio,
        decimal produtividadeEstimada,
        DateTime dataPlantio,
        string localPlantio,
        StatusPlantio statusPlantio)
    {
        Validar(UsuarioId, tipoPlantio, cultura, areaPlantio, produtividadeEstimada, localPlantio);

        TipoPlantio = tipoPlantio;
        Cultura = cultura;
        AreaPlantio = areaPlantio;
        ProdutividadeEstimada = produtividadeEstimada;
        DataPlantio = dataPlantio;
        LocalPlantio = localPlantio;
        StatusPlantio = statusPlantio;
    }

    public void EncerrarPlantacao()
    {
        Ativa = false;
        StatusPlantio = StatusPlantio.DESCANSO;
    }

    public void ReativarPlantacao()
    {
        Ativa = true;
        StatusPlantio = StatusPlantio.PREPARACAO;
    }

    public decimal CalcularIdadeEmDias()
    {
        return Convert.ToDecimal((DateTime.Now - DataPlantio).TotalDays);
    }

    public bool EstaEmRisco()
    {
        return AnalisesDrone.Any(a =>
            a.NivelRisco == NivelRisco.ALTO ||
            a.NivelRisco == NivelRisco.CRITICO);
    }

    public bool PossuiOcorrenciasPendentes()
    {
        return Ocorrencias.Any(o => !o.Resolvida);
    }

    public int TotalOcorrenciasPendentes()
    {
        return Ocorrencias.Count(o => !o.Resolvida);
    }

    public decimal CalcularProdutividadeMedia()
    {
        if (!Safras.Any())
            return 0;

        return Safras.Average(s => s.CalcularProdutividade(AreaPlantio));
    }

    public bool ProdutividadeAbaixoDoEsperado()
    {
        if (!Safras.Any())
            return false;

        var produtividadeMedia = CalcularProdutividadeMedia();

        return produtividadeMedia < ProdutividadeEstimada;
    }

    private static void Validar(
        int usuarioId,
        string tipoPlantio,
        string cultura,
        decimal areaPlantio,
        decimal produtividadeEstimada,
        string localPlantio)
    {
        if (usuarioId <= 0)
            throw new DomainException("O usuário da plantação é obrigatório.");

        if (string.IsNullOrWhiteSpace(tipoPlantio))
            throw new DomainException("O tipo de plantio é obrigatório.");

        if (string.IsNullOrWhiteSpace(cultura))
            throw new DomainException("A cultura da plantação é obrigatória.");

        if (areaPlantio <= 0)
            throw new DomainException("A área de plantio deve ser maior que zero.");

        if (produtividadeEstimada < 0)
            throw new DomainException("A produtividade estimada não pode ser negativa.");

        if (string.IsNullOrWhiteSpace(localPlantio))
            throw new DomainException("O local do plantio é obrigatório.");
    }
}