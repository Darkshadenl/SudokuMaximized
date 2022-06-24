using Abstraction;
using Construction.Boards;
using Helpers.Enums;
using Helpers.Viewable;
using Solvers;
using Sudoku.Command;
using Sudoku.Command.States;
using Sudoku.Controller;
using ICommand = Sudoku.Command.ICommand;

namespace Sudoku.Model.Game;

public class Game
{
    public BoardTypes BoardType => Board.Type;
    public int CurrentBoardIndex { get; }

    private AbstractBoard _board; 
    public AbstractBoard Board
    {
        get => _board!;
        set
        {
            _board = value;
            Solve = new SolveCommand(_board.Type, 
                _board.SudokuBoards.Cast<IComponent>().ToList(), Controller);
            _board.CurrentBoardIndex = CurrentBoardIndex;
        }
    }

    public IGameState GameMode { get; set; }
    public GameController Controller { get; set; }

    public ICommand Select { get; set; }
    public ICommand ShiftState { get; set; }
    public ICommand Solve { get; set; }

    private readonly List<ISimpleViewMessage> _preBoardMessages = new();
    private readonly List<ISimpleViewMessage> _postBoardMessages = new();

    public Dictionary<ConsoleKey, int> AvailableKeys { get; } = new();
    public Game()
    {
        GameMode = new DefinitiveGameState(this, AvailableKeys);
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
        Controller.DrawUI(_preBoardMessages, _postBoardMessages);
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