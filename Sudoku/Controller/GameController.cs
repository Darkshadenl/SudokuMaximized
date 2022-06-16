using Abstraction;
using BoardConstruction.Boards;
using BoardConstruction.Factory;
using Helpers.Helpers;
using Helpers.Viewable;
using Newtonsoft.Json;
using Solvers;
using Sudoku.Model.Game;
using Sudoku.View.Game;
using Sudoku.Resources.Config;

namespace Sudoku.Controller;

public class GameController : IGameController
{
    private readonly Game _game;
    private readonly IBoardView _boardView;
    private readonly IVisitorFactory _visitorFactory;

    private int _currentBoardIndex;
    public int CurrentBoardIndex
    {
        get => _currentBoardIndex;
        set
        {
            _currentBoardIndex = value;
            _game.Board.CurrentBoardIndex = _currentBoardIndex;
        }
    }

    public MainController Controller { get; set; }

    public GameController(Game game, IBoardView view, IVisitorFactory visitorFactory)
    {
        _game = game;
        _game.Controller = this;
        _boardView = view;
        _visitorFactory = visitorFactory;
        FillKeyList();
    }
    
    public bool RunGame(AbstractBoard abstractBoard)
    {
        InitializingGameData(abstractBoard);

        ReDraw();

        var gameOver = false;
        CurrentBoardIndex = _game.Board.CurrentBoardIndex;
        var boardCount = _game.Board.SudokuBoards.Count;

        do
        {
            while (!Console.KeyAvailable)
            {
                var cki = Console.ReadKey(true);

                // switches states with shift + s
                if ((cki.Modifiers & ConsoleModifiers.Shift) != 0 && cki.Key == ConsoleKey.S)
                    _game.ShiftState.Execute();

                // moves within sudoku with arrow keys
                // or enter sudoku number with "enter" key
                switch (cki.Key)
                {
                    case ConsoleKey.UpArrow:
                        _game.Board.MoveCursor(Directions.Up);
                        break;
                    case ConsoleKey.DownArrow:
                        _game.Board.MoveCursor(Directions.Down);
                        break;
                    case ConsoleKey.RightArrow:
                        _game.Board.MoveCursor(Directions.Right);
                        break;
                    case ConsoleKey.LeftArrow:
                        _game.Board.MoveCursor(Directions.Left);
                        break;
                    case ConsoleKey.Enter:
                        _game.Select.Execute();
                        break;
                    case ConsoleKey.Spacebar:
                        _game.Solver.SolveBoards(_game.Board.SudokuBoards.Cast<IComponent>().ToList());
                        break;
                    case ConsoleKey.E:
                        if (_game.BoardType == BoardTypes.samurai)
                        {
                            if (CurrentBoardIndex + 1 < boardCount)
                            {
                                CurrentBoardIndex++;
                            }
                        }
                        break;
                    case ConsoleKey.Q:
                        if (_game.BoardType == BoardTypes.samurai)
                        {
                            if (CurrentBoardIndex - 1 != (-1))
                            {
                                CurrentBoardIndex--;
                            }
                        }
                        break;
                    case ConsoleKey.End:
                        gameOver = true;
                        break;
                }

                ReDraw();

                if (gameOver) break;
            }
            if (gameOver) break;
        } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

        return RequestNewGame();
    }
    
    private void InitializingGameData(AbstractBoard abstractBoard)
    {
        // set game data
        _game.Board = abstractBoard;
        _game.Solver = new BackTrackingAlgo();
        if (_game.BoardType == BoardTypes.samurai)
        {
            _game.Board.CurrentBoardIndex = 2;
            _game.Solver = new SamuraiSolver();
        }
        _game.Solver.Controller = this;

        _boardView.Accept(_visitorFactory.Create(DotNetEnv.Env.GetString("UI")));
        _boardView.WelcomeMessage();
        _boardView.BoardType = _game.BoardType;
    }

    private bool RequestNewGame()
    {
        ConsoleKeyInfo keyPressed;
        do
        {
            _boardView.StartNewGameMessage();
            keyPressed = Console.ReadKey(true);

            if (keyPressed.Key == ConsoleKey.Y)
                return true;

            if (keyPressed.Key == ConsoleKey.N)
            {
                _boardView.EndGameMessage();
                return false;
            }
        } while (true);
    }

    public void ReDraw(List<ISimpleViewMessage>? pre = null, List<ISimpleViewMessage>? post = null)
    {
        var viewData = new ViewData(_game.GetViewableData(), _game.State.State,
            pre, post);

        _boardView.DrawBoard(viewData);
    }
    
    private void FillKeyList()
    {
        try
        {
            var json = File.ReadAllText(Environment.GetEnvironmentVariable("GAMEKEYCONFIG") ??
                                        "../Resources/Config/GameKeyConfig.json");

            var res = JsonConvert.DeserializeObject<GameKeyJSONModel>(json);
            
            _game.AvailableKeys.Clear();

            for (int i = 0; i < res.keys.Length; i++)
                _game.AvailableKeys.Add(res.keys[i].key, Int32.Parse(res.keys[0].value));
        }
        catch (Exception e)
        {
            Console.WriteLine("Valid file extension list could not be loaded");
            Console.WriteLine(e);
        }
    }
}