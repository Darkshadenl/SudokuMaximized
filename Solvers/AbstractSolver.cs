using Abstraction;

namespace Solvers;

public abstract class AbstractSolver : ISolver
{
    public IGameController Controller { get; set; }

    public virtual IComponent SolveBoard(IComponent board)
    {
        Console.WriteLine("Solving boards...");
        return null;
    }

    public virtual List<IComponent> SolveBoards(List<IComponent> boards)
    {
        Console.WriteLine("Solving boards...");
        return null;
    }
}