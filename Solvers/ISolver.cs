using Abstraction;

namespace Solvers;

public interface ISolver
{
    public List<IComponent> SolveBoards(List<IComponent> sudokuBoards);
}