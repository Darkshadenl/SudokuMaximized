using GenerateLib.Boards;
using GenerateLib.Factory;
using Sudoku.Model.Game;
using Sudoku.View.Game;

namespace Sudoku.Controller;

public class GameController
{
    private readonly Game _game;
    private readonly IBoardView _boardView;
    private readonly IVisitorFactory _visitorFactory;

    public GameController(Game game, IBoardView view, IVisitorFactory visitorFactory)
    {
        _game = game;
        _boardView = view;
        _visitorFactory = visitorFactory;
        _boardView.Controller(this);
    }

    public void RunGame(AbstractBoard board)
    {
        _game.Board = board;
        _boardView.Accept(_visitorFactory.Create(DotNetEnv.Env.GetString("UI")));

        Console.WriteLine("Starting your Sudoku game. Press ESC to quit the game.");
        _boardView.DrawBoard(_game.GetViewableData());

        do
        {
            while (!Console.KeyAvailable)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        _boardView.DrawBoard(_game.GetViewableData());
                        break;
                    case ConsoleKey.DownArrow:
                        _boardView.DrawBoard(_game.GetViewableData());
                        break;
                    case ConsoleKey.RightArrow:
                        _boardView.DrawBoard(_game.GetViewableData());
                        break;
                    case ConsoleKey.LeftArrow:
                        _boardView.DrawBoard(_game.GetViewableData());
                        break;
                }
            }
        } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
    }
}