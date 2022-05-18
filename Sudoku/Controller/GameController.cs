using GenerateLib.Boards;
using GenerateLib.Factory;
using GenerateLib.Helpers;
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
        _boardView.BoardType = _game.BoardType;
        _boardView.DrawBoard(_game.GetViewableData());
        while (true)
        {
            _boardView.DrawBoard(_game.GetViewableData());

            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        // TODO move cursor
                        _game.MoveCursor(Directions.UP);
                        continue;
                    case ConsoleKey.DownArrow:
                        _game.MoveCursor(Directions.DOWN);
                        continue;
                    case ConsoleKey.RightArrow:
                        _game.MoveCursor(Directions.RIGHT);
                        continue;
                    case ConsoleKey.LeftArrow:
                        _game.MoveCursor(Directions.LEFT);
                        continue;
                }
            }

            Thread.Sleep(300);
        }
    }
}