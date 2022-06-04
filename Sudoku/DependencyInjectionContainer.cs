using GenerateLib.Boards;
using GenerateLib.Builder;
using GenerateLib.Interpreters;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using Sudoku.Controller;

namespace Sudoku;

public static class DependencyInjectionContainer
{

    public static IServiceCollection ConfigureSingleton(this IServiceCollection services)
    {
        // services.Scan(scan => scan
        //     .FromCallingAssembly()
        //     
        //     .AddClasses(c =>  c.InNamespaceOf<MainController>())
        //     .AsSelf()
        //     .WithSingletonLifetime()
        // );
        return services;
    }

    // kan je ff checken of deze func nog klopt? 
    public static IServiceCollection ConfigureTransient(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblyOf<AbstractBoard>()

                .AddClasses(c =>
                {
                    c.NotInNamespaces(new[]
                    {
                        "GenerateLib.Components",
                        "GenerateLib.Factory.Config",
                        "GenerateLib.Boards",
                        "GenerateLib.Visitors"
                    });
                })
                .AsImplementedInterfaces()
                
                .AddClasses(c => c.InNamespaceOf<BoardBuildDirector>())
                .AsSelf()
        );
        
        services.Scan(scan => scan
            .FromCallingAssembly()

            .AddClasses()
                    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                    .AsImplementedInterfaces()
                
            .AddClasses(c =>
                {
                    c.NotInNamespaces("Sudoku.Controller", "Sudoku.Resources",
                        "Sudoku.View.Game", "Sudoku.Visitor");
                })
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsSelf()
        
            .AddClasses(c =>  c.InNamespaceOf<MainController>())
                .AsSelf()
        
        );
        
        return services;
    }
    
    public static IServiceCollection ConfigureScoped(this IServiceCollection services)
    {
        return services;
    }
}