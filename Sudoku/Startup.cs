using Microsoft.Extensions.DependencyInjection;

namespace Sudoku;

public static class Startup
{
    public static IServiceProvider ServiceProvider { get; set; }

    public static IServiceProvider Init()
    {
        var serviceProvider = new ServiceCollection()
            .ConfigureSingleton()
            .ConfigureTransient()
            .ConfigureScoped()
            .BuildServiceProvider();

        ServiceProvider = serviceProvider;

        return serviceProvider;
    }
}