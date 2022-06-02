using GenerateLib.Boards;
using GenerateLib.Components;
using GenerateLib.Factory;
using GenerateLib.Helpers;
using GenerateLib.Viewable;
using Sudoku.Model.Game;
using Sudoku.View.Game;

namespace Sudoku.Controller;

public class GameController
{
    private readonly Game _game;
    private readonly IBoardView _boardView;
    private readonly IVisitorFactory _visitorFactory;
    public MainController Controller { get; set; }

    public GameController(Game game, IBoardView view, IVisitorFactory visitorFactory)
    {
        _game = game;
        _game.Controller = this;
        _boardView = view;
        _visitorFactory = visitorFactory;
        _boardView.Controller(this);
    }


    public void RunGame(AbstractBoard board)
    {
        _game.Board = board;

        _boardView.Accept(_visitorFactory.Create(DotNetEnv.Env.GetString("UI")));

        Console.WriteLine("Starting your Sudoku game. Press ESC to quit the game.");
        _boardView.BoardType = _game.BoardType;

        ReDraw();

        do
        {
            while (!Console.KeyAvailable)
            {
                var cki = Console.ReadKey(true);

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
                        _game.Solver.SolveBoard((_game.Board.SudokuBoard as SudokuBoard)!);
                        ReDraw();
                        RequestNewGame();
                        break;
                }
                
                ReDraw();
            }
        } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
    }

    private void RequestNewGame()
    {
        ConsoleKey yesOrNo = 0;
        ConsoleKeyInfo keyPressed;
        do
        {
            Console.WriteLine("Would you like to start a new game? (y/n)");
            keyPressed = Console.ReadKey(true);

            if (keyPressed.Key == ConsoleKey.Y)
            {
                break;
            }

            if (keyPressed.Key == ConsoleKey.N)
            {
                Console.WriteLine("Thank you for playing!");
                yesOrNo = ConsoleKey.N;
                Environment.Exit(0);
            }
        } while (yesOrNo != ConsoleKey.Y || yesOrNo != ConsoleKey.N);
        _game.Reset();
        Controller.ImportController.RunImport();
    }

    public void ReDraw(List<ISimpleViewMessage>? pre = null, List<ISimpleViewMessage>? post = null)
    {
        var viewData = new ViewData(_game.GetViewableData(), _game.State.State,
            pre, post);
        
        _boardView.DrawBoard(viewData);
    }
}