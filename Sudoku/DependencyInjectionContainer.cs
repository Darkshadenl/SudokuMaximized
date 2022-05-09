using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using Sudoku.Controller;
using Sudoku.Interpreters;
using Sudoku.Model.Components;
using Sudoku.Visitor;

namespace Sudoku;

public static class DependencyInjectionContainer
{

    public static IServiceCollection ConfigureSingleton(this IServiceCollection services)
    {
        
        return services;
    }

    public static IServiceCollection ConfigureTransient(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromCallingAssembly()
            
            .AddClasses(c => c.InNamespaceOf<MainController>())
                .AsSelf()
                .WithSingletonLifetime()
            
            .AddClasses(c => c.NotInNamespaceOf<IBoardInterpreter>())
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsImplementedInterfaces()
        );
        Console.WriteLine();

        return services;
    }
    
    public static IServiceCollection ConfigureScoped(this IServiceCollection services)
    {
        return services;
    }
}