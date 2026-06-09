using AgroVision.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgroVision.Infrastructure.Persistence;

public class AgroVisionContext : DbContext
{
    public AgroVisionContext(DbContextOptions<AgroVisionContext> options)
        : base(options)
    {
    }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Plantacao> Plantacoes => Set<Plantacao>();
    public DbSet<Safra> Safras => Set<Safra>();
    public DbSet<Insumo> Insumos => Set<Insumo>();
    public DbSet<AnaliseDrone> AnalisesDrone => Set<AnaliseDrone>();
    public DbSet<Drone> Drones => Set<Drone>();
    public DbSet<MissaoDrone> MissoesDrone => Set<MissaoDrone>();
    public DbSet<LeituraSensor> LeiturasSensor => Set<LeituraSensor>();
    public DbSet<OcorrenciaPlantacao> OcorrenciasPlantacao => Set<OcorrenciaPlantacao>();
    public DbSet<RecomendacaoAgronomica> RecomendacoesAgronomicas => Set<RecomendacaoAgronomica>();
    public DbSet<LogErro> LogsErro => Set<LogErro>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AgroVisionContext).Assembly);
    }
}