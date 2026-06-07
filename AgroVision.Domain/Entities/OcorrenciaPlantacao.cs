using AgroVision.Domain.Common;
using AgroVision.Domain.Enums;
using AgroVision.Domain.Exceptions;

namespace AgroVision.Domain.Entities;

public class OcorrenciaPlantacao : BaseEntity
{
    public int PlantacaoId { get; private set; }
    public int? AnaliseDroneId { get; private set; }

    public TipoOcorrencia TipoOcorrencia { get; private set; }
    public NivelRisco NivelRisco { get; private set; }
    public string Descricao { get; private set; } = string.Empty;
    public DateTime DataOcorrencia { get; private set; }
    public bool Resolvida { get; private set; }

    public Plantacao? Plantacao { get; private set; }
    public AnaliseDrone? AnaliseDrone { get; private set; }

    protected OcorrenciaPlantacao()
    {
    }

    public OcorrenciaPlantacao(
        int plantacaoId,
        int? analiseDroneId,
        TipoOcorrencia tipoOcorrencia,
        NivelRisco nivelRisco,
        string descricao)
    {
        if (plantacaoId <= 0)
            throw new DomainException("A plantação da ocorrência é obrigatória.");

        if (string.IsNullOrWhiteSpace(descricao))
            throw new DomainException("A descrição da ocorrência é obrigatória.");

        PlantacaoId = plantacaoId;
        AnaliseDroneId = analiseDroneId;
        TipoOcorrencia = tipoOcorrencia;
        NivelRisco = nivelRisco;
        Descricao = descricao;
        DataOcorrencia = DateTime.Now;
        Resolvida = false;
    }

    public void MarcarComoResolvida()
    {
        Resolvida = true;
    }

    public void Reabrir()
    {
        Resolvida = false;
    }

    public bool ExigeAcaoImediata()
    {
        return NivelRisco == NivelRisco.ALTO || NivelRisco == NivelRisco.CRITICO;
    }
}