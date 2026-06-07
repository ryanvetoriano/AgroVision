using AgroVision.Domain.Common;
using AgroVision.Domain.Enums;
using AgroVision.Domain.Exceptions;

namespace AgroVision.Domain.Entities;

public class MissaoDrone : BaseEntity
{
    public int DroneId { get; private set; }
    public int PlantacaoId { get; private set; }

    public DateTime DataInicio { get; private set; }
    public DateTime? DataFim { get; private set; }

    public decimal AreaMapeada { get; private set; }
    public decimal AltitudeMedia { get; private set; }
    public StatusMissaoDrone Status { get; private set; }

    public Drone? Drone { get; private set; }
    public Plantacao? Plantacao { get; private set; }

    public ICollection<LeituraSensor> LeiturasSensor { get; private set; } = new List<LeituraSensor>();

    protected MissaoDrone()
    {
    }

    public MissaoDrone(
        int droneId,
        int plantacaoId,
        DateTime dataInicio,
        decimal areaMapeada,
        decimal altitudeMedia)
    {
        if (droneId <= 0)
            throw new DomainException("O drone da missão é obrigatório.");

        if (plantacaoId <= 0)
            throw new DomainException("A plantação da missão é obrigatória.");

        if (areaMapeada <= 0)
            throw new DomainException("A área mapeada deve ser maior que zero.");

        if (altitudeMedia <= 0)
            throw new DomainException("A altitude média deve ser maior que zero.");

        DroneId = droneId;
        PlantacaoId = plantacaoId;
        DataInicio = dataInicio;
        AreaMapeada = areaMapeada;
        AltitudeMedia = altitudeMedia;
        Status = StatusMissaoDrone.AGENDADA;
    }

    public void IniciarMissao()
    {
        if (Status != StatusMissaoDrone.AGENDADA)
            throw new DomainException("A missão só pode ser iniciada se estiver agendada.");

        Status = StatusMissaoDrone.EM_ANDAMENTO;
        DataInicio = DateTime.Now;
    }

    public void FinalizarMissao()
    {
        if (Status != StatusMissaoDrone.EM_ANDAMENTO)
            throw new DomainException("A missão só pode ser finalizada se estiver em andamento.");

        Status = StatusMissaoDrone.CONCLUIDA;
        DataFim = DateTime.Now;
    }

    public void CancelarMissao()
    {
        if (Status == StatusMissaoDrone.CONCLUIDA)
            throw new DomainException("Não é possível cancelar uma missão já concluída.");

        Status = StatusMissaoDrone.CANCELADA;
    }

    public decimal CalcularDuracaoMinutos()
    {
        if (DataFim is null)
            return 0;

        return Convert.ToDecimal((DataFim.Value - DataInicio).TotalMinutes);
    }
}