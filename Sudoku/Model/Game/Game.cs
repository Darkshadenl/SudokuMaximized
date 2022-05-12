using GenerateLib.Boards;
using GenerateLib.SolveAlgo;
using GenerateLib.Viewable;

namespace Sudoku.Model.Game;

public class Game
{
    private readonly ISolver _solver;
    public string BoardType => Board.Type;

    private AbstractBoard? _board; 
    public AbstractBoard Board
    {
        get => _board!;
        set
        {
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

    public void SetupSolveAlgo(ISolver solver)
    {
        Board.Solver = solver;
    }
    
}