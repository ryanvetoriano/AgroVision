using AgroVision.Application.Interfaces.Services;
using AgroVision.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AgroVision.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IPlantacaoService, PlantacaoService>();
        services.AddScoped<ISafraService, SafraService>();
        services.AddScoped<IInsumoService, InsumoService>();
        services.AddScoped<IDroneService, DroneService>();
        services.AddScoped<IMissaoDroneService, MissaoDroneService>();
        services.AddScoped<ILeituraSensorService, LeituraSensorService>();
        services.AddScoped<IAnaliseDroneService, AnaliseDroneService>();
        services.AddScoped<IOcorrenciaPlantacaoService, OcorrenciaPlantacaoService>();
        services.AddScoped<IRecomendacaoAgronomicaService, RecomendacaoAgronomicaService>();

        return services;
    }
}