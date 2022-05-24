using GenerateLib.Boards;
using GenerateLib.Helpers;
using GenerateLib.SolveAlgo;
using GenerateLib.Viewable;
using Sudoku.Controller;
using Sudoku.State;
using ICommand = Sudoku.Command.ICommand;

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

    public IState State { get; set; }
    public GameController Controller { get; set; }

    public ICommand Select { get; set; }
    public ICommand ShiftState { get; set; }

    private readonly List<ISimpleViewMessage> _preBoardMessages = new();
    private readonly List<ISimpleViewMessage> _postBoardMessages = new();

    private readonly Dictionary<ConsoleKey, int> _availableKeys = new()
    {
        {ConsoleKey.D1, 1},
        {ConsoleKey.D2, 2},
        {ConsoleKey.D3, 3},
        {ConsoleKey.D4, 4},
        {ConsoleKey.D5, 5},
        {ConsoleKey.D6, 6},
        {ConsoleKey.D7, 7},
        {ConsoleKey.D8, 8},
        {ConsoleKey.D9, 9}
    };
    public Game(ISolver solver)
    {
        _solver = solver;
        // TODO make definitive again
        State = new HelpState(this, _availableKeys);
    }

    public void AddMessages(ISimpleViewMessage message)
    {
        switch (message.Timing)
        {   
            case BoardDrawTimings.PostBoard:
                _postBoardMessages.Add(message);
                break;
            case BoardDrawTimings.PreBoard:
                _preBoardMessages.Add(message);
                break;
        }
    }

    public void ForceRedraw()
    {
        Controller.ReDraw(_preBoardMessages, _postBoardMessages);
        _preBoardMessages.Clear();
        _postBoardMessages.Clear();
    }

    public List<IViewable> GetViewableData()
    {
        return Board.GetViewables();
    }

}