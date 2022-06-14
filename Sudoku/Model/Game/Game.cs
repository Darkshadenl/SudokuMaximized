using BoardConstruction.Boards;
using Helpers.Helpers;
using Helpers.Viewable;
using Solvers;
using Sudoku.Command.States;
using Sudoku.Controller;
using ICommand = Sudoku.Command.ICommand;

namespace Sudoku.Model.Game;

public class Game
{
    public ISolver Solver { get; }
    public BoardTypes BoardType => Board.Type;
    public int CurrentBoardIndex { get; }

    private AbstractBoard _board; 
    public AbstractBoard Board
    {
        get => _board!;
        set
        {
            _board = value;
            _board.Solver = Solver;
            _board.CurrentBoardIndex = CurrentBoardIndex;
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
        Solver = solver;
        State = new DefinitiveState(this, _availableKeys);
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

    public void Reset()
    {
        _board = null;
    }
}