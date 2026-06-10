using AgroVision.Application.Interfaces.Repositories;
using AgroVision.Infrastructure.Persistence;
using AgroVision.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AgroVision.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("OracleConnection");

        services.AddDbContext<AgroVisionContext>(options =>
            options.UseOracle(
                connectionString,
                oracleOptions => oracleOptions.UseOracleSQLCompatibility(
                    OracleSQLCompatibility.DatabaseVersion19)));

        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IPlantacaoRepository, PlantacaoRepository>();
        services.AddScoped<ISafraRepository, SafraRepository>();
        services.AddScoped<IInsumoRepository, InsumoRepository>();
        services.AddScoped<IAnaliseDroneRepository, AnaliseDroneRepository>();
        services.AddScoped<IDroneRepository, DroneRepository>();
        services.AddScoped<IMissaoDroneRepository, MissaoDroneRepository>();
        services.AddScoped<ILeituraSensorRepository, LeituraSensorRepository>();
        services.AddScoped<IOcorrenciaPlantacaoRepository, OcorrenciaPlantacaoRepository>();
        services.AddScoped<IRecomendacaoAgronomicaRepository, RecomendacaoAgronomicaRepository>();
        services.AddScoped<ILogErroRepository, LogErroRepository>();

        return services;
    }
}