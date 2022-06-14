using BoardConstruction.Boards;
using BoardConstruction.Builder;
using Factories.Factory;
using Helpers.Helpers;
using Helpers.Visitors;
using Import.Import;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using Solvers;

namespace Sudoku;

public class Registering
{
    public Registering(IServiceCollection services)
    {
        Services = services;
    }

    public IServiceCollection Services { get; set; }
    
    
    public Registering RegisterSudokuAssembly()
    {
        Services.Scan(scan => scan
            .FromCallingAssembly()
            .AddClasses()
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .AddClasses(c =>
            {
                c.NotInNamespaces("Sudoku.Controller", "Sudoku.Resources",
                    "Sudoku.View.Game", "Sudoku.Registering");
            })
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsSelf()
            // .AddClasses(c => c.InNamespaceOf<MainController>())
            // .AsSelf()
        );
        return this;
    }

    public Registering RegisterBoardConstructionAssesmbly()
    {
        Services.Scan(scan => scan
            .FromAssemblyOf<AbstractBoard>()

            .AddClasses()
            .AsImplementedInterfaces()

            .AddClasses(c => c.InNamespaceOf<BoardBuildDirector>())
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsSelf()
        );
        return this;
    }

    public IServiceCollection Build()
    {
        return Services;
    }

    public Registering RegisterFactories()
    {
        Services.Scan(scan => scan
            .FromAssemblyOf<IComponentFactory>()
            .AddClasses(c => c.NotInNamespaces("Factories.Config"))
            .AsImplementedInterfaces()
        );
        return this;
    }

    public Registering RegisterHelpers()
    {
        Services.Scan(scan => scan
            .FromAssemblyOf<ConsoleVisitor>()
            
            .AddClasses(c => c.InNamespaces("Helpers.Viewable", "Helpers.Visitors"))
            .AsImplementedInterfaces()
            
            .AddClasses(c => c.InNamespaceOf<BoardTypes>())
            .AsSelf()
        );
        return this;
    }

    public Registering RegisterImport()
    {
        Services.Scan(scan => scan
            .FromAssemblyOf<IBoardFile>()
            
            .AddClasses(c => c.NotInNamespaces("Import.Config"))
            .AsImplementedInterfaces()
        );
        return this;
    }

    public Registering RegisterSolvers()
    {
        Services.Scan(scan => scan
            .FromAssemblyOf<BackTrackingAlgo>()
            .AddClasses()
            .AsImplementedInterfaces()
        );
        return this;
    }
}