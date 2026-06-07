using AgroVision.Domain.Common;
using AgroVision.Domain.Enums;
using AgroVision.Domain.Exceptions;

namespace AgroVision.Domain.Entities;

public class LeituraSensor : BaseEntity
{
    public int MissaoDroneId { get; private set; }

    public TipoSensor TipoSensor { get; private set; }
    public decimal Valor { get; private set; }
    public string UnidadeMedida { get; private set; } = string.Empty;
    public DateTime DataLeitura { get; private set; }

    public decimal? Latitude { get; private set; }
    public decimal? Longitude { get; private set; }

    public MissaoDrone? MissaoDrone { get; private set; }

    protected LeituraSensor()
    {
    }

    public LeituraSensor(
        int missaoDroneId,
        TipoSensor tipoSensor,
        decimal valor,
        string unidadeMedida,
        DateTime dataLeitura,
        decimal? latitude,
        decimal? longitude)
    {
        if (missaoDroneId <= 0)
            throw new DomainException("A missão do drone é obrigatória.");

        if (string.IsNullOrWhiteSpace(unidadeMedida))
            throw new DomainException("A unidade de medida é obrigatória.");

        MissaoDroneId = missaoDroneId;
        TipoSensor = tipoSensor;
        Valor = valor;
        UnidadeMedida = unidadeMedida;
        DataLeitura = dataLeitura;
        Latitude = latitude;
        Longitude = longitude;
    }

    public bool EstaForaDoPadrao()
    {
        return TipoSensor switch
        {
            TipoSensor.UMIDADE_SOLO => Valor < 30 || Valor > 90,
            TipoSensor.TEMPERATURA => Valor < 10 || Valor > 40,
            TipoSensor.NDVI => Valor < 0.4m,
            TipoSensor.PH_SOLO => Valor < 5.5m || Valor > 7.5m,
            _ => false
        };
    }
}