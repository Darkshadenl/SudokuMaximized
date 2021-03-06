using Abstraction;
using Construction.Boards;
using Construction.Factory;
using Helpers.Enums;
using Helpers.Viewable;
using Newtonsoft.Json;
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

        DrawUI();

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
                        if (_game.GameMode.State == States.Definitive)
                            _game.Solve.Execute();
                        break;
                    // case ConsoleKey.H:
                    //     var copy = _game.Board.SudokuBoards.Cast<IComponent>().ToList().Copy();
                    //     _game.Solver.SolveBoards(copy);
                        // break;
                    case ConsoleKey.E:
                        if (_game.BoardType == BoardTypes.samurai && CurrentBoardIndex + 1 < boardCount)
                                CurrentBoardIndex++;
                        break;
                    case ConsoleKey.Q:
                        if (_game.BoardType == BoardTypes.samurai && CurrentBoardIndex - 1 != (-1))
                                CurrentBoardIndex--;
                        break;
                    case ConsoleKey.F:
                        gameOver = true;
                        break;
                }

                DrawUI();

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
        if (_game.BoardType == BoardTypes.samurai)
        {
            _game.Board.CurrentBoardIndex = 2;
        }

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

    public void DrawUI(List<ISimpleViewMessage>? pre = null, List<ISimpleViewMessage>? post = null)
    {
        var viewData = new ViewData(_game.GetViewableData(), _game.GameMode.State,
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
            {
                var key = res.keys[i].key;
                var value = res.keys[i].value;
                _game.AvailableKeys.Add(res.keys[i].key, int.Parse(value));
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Valid file extension list could not be loaded");
            Console.WriteLine(e);
        }
    }
}