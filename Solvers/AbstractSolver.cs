using Abstraction;

namespace Solvers;

public abstract class AbstractSolver
{
    public IGameController Controller { get; set; }
    public abstract List<IComponent> SolveBoards(List<IComponent> boards);
}