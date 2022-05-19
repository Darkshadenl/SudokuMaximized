using GenerateLib.Boards;
using GenerateLib.Helpers;
using GenerateLib.SolveAlgo;
using GenerateLib.Viewable;

namespace Sudoku.Model.Game;

public class Game
{
    private readonly ISolver _solver;
    public BoardTypes BoardType => Board.Type;

    private AbstractBoard? _board; 
    public AbstractBoard Board
    {
        get => _board!;
        set
        {
            if (_board != null) return;
            _board = value;
            _board.Solver = _solver;
        }
    }

    public Game(ISolver solver)
    {
        _solver = solver;
    }

    public List<IViewable> GetViewableData()
    {
        return Board.GetViewables();
    }

}