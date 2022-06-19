using Abstraction;
using Helpers.Helpers;
using Solvers;
using Sudoku.Controller;

namespace Sudoku.Command;

public class SolveCommand : ICommand
{
    private readonly BoardTypes _boardType;
    private readonly List<IComponent> _components;
    private readonly GameController _gameController;
    private readonly AbstractSolver _abstractSolver;

    public SolveCommand(BoardTypes boardType, List<IComponent> components, GameController gameController)
    {
        _boardType = boardType;
        _components = components;
        _gameController = gameController;
    }

    public void Execute()
    {
        if (_boardType == BoardTypes.samurai)
        {
            var solver = new SamuraiSolver();
            solver.Controller = _gameController;
            solver.SolveBoards(_components);
        }
        else
        {
            var solver = new BackTrackingAlgo();
            solver.Controller = _gameController;
            solver.SolveBoard(_components[0]);
        }
    }

}