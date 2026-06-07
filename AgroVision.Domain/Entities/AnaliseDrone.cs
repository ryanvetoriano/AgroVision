using AgroVision.Domain.Common;
using AgroVision.Domain.Enums;
using AgroVision.Domain.Exceptions;

namespace AgroVision.Domain.Entities;

public class AnaliseDrone : BaseEntity
{
    public int PlantacaoId { get; private set; }
    public int? MissaoDroneId { get; private set; }

    public DateTime DataAnalise { get; private set; }

    public decimal IndiceSaude { get; private set; }
    public decimal UmidadeSolo { get; private set; }
    public decimal TemperaturaMedia { get; private set; }
    public decimal IndiceVegetacaoNdvi { get; private set; }
    public decimal AreaAfetadaPercentual { get; private set; }

    public bool PragasDetectadas { get; private set; }

    public NivelRisco NivelRisco { get; private set; }
    public StatusAnalise StatusAnalise { get; private set; }

    public string Recomendacao { get; private set; } = string.Empty;
    public string ObservacaoImagem { get; private set; } = string.Empty;

    public Plantacao? Plantacao { get; private set; }
    public MissaoDrone? MissaoDrone { get; private set; }

    public ICollection<OcorrenciaPlantacao> Ocorrencias { get; private set; } = new List<OcorrenciaPlantacao>();
    public ICollection<RecomendacaoAgronomica> Recomendacoes { get; private set; } = new List<RecomendacaoAgronomica>();

    protected AnaliseDrone()
    {
    }

    public AnaliseDrone(
        int plantacaoId,
        int? missaoDroneId,
        DateTime dataAnalise,
        decimal indiceSaude,
        decimal umidadeSolo,
        decimal temperaturaMedia,
        decimal indiceVegetacaoNdvi,
        decimal areaAfetadaPercentual,
        bool pragasDetectadas,
        string observacaoImagem)
    {
        Validar(
            plantacaoId,
            indiceSaude,
            umidadeSolo,
            indiceVegetacaoNdvi,
            areaAfetadaPercentual);

        PlantacaoId = plantacaoId;
        MissaoDroneId = missaoDroneId;
        DataAnalise = dataAnalise;
        IndiceSaude = indiceSaude;
        UmidadeSolo = umidadeSolo;
        TemperaturaMedia = temperaturaMedia;
        IndiceVegetacaoNdvi = indiceVegetacaoNdvi;
        AreaAfetadaPercentual = areaAfetadaPercentual;
        PragasDetectadas = pragasDetectadas;
        ObservacaoImagem = observacaoImagem;

        CalcularDiagnostico();
    }

    public void Atualizar(
        decimal indiceSaude,
        decimal umidadeSolo,
        decimal temperaturaMedia,
        decimal indiceVegetacaoNdvi,
        decimal areaAfetadaPercentual,
        bool pragasDetectadas,
        string observacaoImagem)
    {
        Validar(
            PlantacaoId,
            indiceSaude,
            umidadeSolo,
            indiceVegetacaoNdvi,
            areaAfetadaPercentual);

        IndiceSaude = indiceSaude;
        UmidadeSolo = umidadeSolo;
        TemperaturaMedia = temperaturaMedia;
        IndiceVegetacaoNdvi = indiceVegetacaoNdvi;
        AreaAfetadaPercentual = areaAfetadaPercentual;
        PragasDetectadas = pragasDetectadas;
        ObservacaoImagem = observacaoImagem;

        CalcularDiagnostico();
    }

    public bool ExigeIntervencao()
    {
        return NivelRisco == NivelRisco.ALTO ||
               NivelRisco == NivelRisco.CRITICO ||
               PragasDetectadas ||
               AreaAfetadaPercentual >= 30;
    }

    public bool EstaSaudavel()
    {
        return StatusAnalise == StatusAnalise.SAUDAVEL &&
               NivelRisco == NivelRisco.BAIXO &&
               !PragasDetectadas;
    }

    public string GerarResumoDiagnostico()
    {
        return $"Índice de saúde: {IndiceSaude}%, " +
               $"umidade do solo: {UmidadeSolo}%, " +
               $"temperatura média: {TemperaturaMedia}°C, " +
               $"NDVI: {IndiceVegetacaoNdvi}, " +
               $"área afetada: {AreaAfetadaPercentual}%, " +
               $"risco: {NivelRisco}. " +
               $"Recomendação: {Recomendacao}";
    }

    private void CalcularDiagnostico()
    {
        if (IndiceSaude >= 80 &&
            IndiceVegetacaoNdvi >= 0.6m &&
            !PragasDetectadas &&
            UmidadeSolo >= 40 &&
            AreaAfetadaPercentual < 10)
        {
            NivelRisco = NivelRisco.BAIXO;
            StatusAnalise = StatusAnalise.SAUDAVEL;
            Recomendacao = "Plantação saudável. Manter monitoramento periódico.";
            return;
        }

        if (IndiceSaude >= 60 &&
            IndiceVegetacaoNdvi >= 0.45m &&
            UmidadeSolo >= 30 &&
            AreaAfetadaPercentual < 25)
        {
            NivelRisco = NivelRisco.MEDIO;
            StatusAnalise = StatusAnalise.ATENCAO;
            Recomendacao = "Atenção: acompanhar irrigação, evolução da vegetação e possíveis sinais de estresse.";
            return;
        }

        if (IndiceSaude >= 40 ||
            IndiceVegetacaoNdvi < 0.45m ||
            PragasDetectadas ||
            AreaAfetadaPercentual >= 25)
        {
            NivelRisco = NivelRisco.ALTO;
            StatusAnalise = StatusAnalise.RISCO;
            Recomendacao = "Risco identificado. Recomenda-se vistoria técnica, análise agronômica e possível aplicação de insumos.";
            return;
        }

        NivelRisco = NivelRisco.CRITICO;
        StatusAnalise = StatusAnalise.CRITICO;
        Recomendacao = "Situação crítica. Ação imediata necessária para evitar perda da plantação.";
    }

    private static void Validar(
        int plantacaoId,
        decimal indiceSaude,
        decimal umidadeSolo,
        decimal indiceVegetacaoNdvi,
        decimal areaAfetadaPercentual)
    {
        if (plantacaoId <= 0)
            throw new DomainException("A plantação da análise é obrigatória.");

        if (indiceSaude < 0 || indiceSaude > 100)
            throw new DomainException("O índice de saúde deve estar entre 0 e 100.");

        if (umidadeSolo < 0 || umidadeSolo > 100)
            throw new DomainException("A umidade do solo deve estar entre 0 e 100.");

        if (indiceVegetacaoNdvi < -1 || indiceVegetacaoNdvi > 1)
            throw new DomainException("O índice NDVI deve estar entre -1 e 1.");

        if (areaAfetadaPercentual < 0 || areaAfetadaPercentual > 100)
            throw new DomainException("A área afetada deve estar entre 0 e 100%.");
    }
}