using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using Sudoku.Controller;

namespace Sudoku;

public static class DependencyInjectionContainer
{

    public static IServiceCollection ConfigureSingleton(this IServiceCollection services)
    {
        services.Scan(scan => scan
                .FromCallingAssembly()
                
                .AddClasses(c => c.InNamespaceOf<MainController>())
                .AsImplementedInterfaces()
                .WithSingletonLifetime()
                
                .AddClasses(c => c.InNamespaceOf<MainController>())
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsSelf()
                .WithSingletonLifetime()
        );
        return services;
    }
    
    public static IServiceCollection ConfigureTransient(this IServiceCollection services)
    {
        var EasyRegister = new Registering(services);
        
        services = EasyRegister
            .RegisterBoardConstructionAssesmbly()
            .RegisterSudokuAssembly()
            .RegisterFactories()
            .RegisterHelpers()
            .RegisterImport()
            // .RegisterSolvers()
            .Build();
        
        return services;
    }
    
    public static IServiceCollection ConfigureScoped(this IServiceCollection services)
    {
        return services;
    }
}

    