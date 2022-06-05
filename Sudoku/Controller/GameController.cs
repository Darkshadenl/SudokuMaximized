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
    }


    public bool RunGame(List<AbstractBoard> boardList)
    {
        var gameOver = false;

        // set game data
        _game.Board = boardList.First();
        _game.BoardList = boardList;

        _boardView.Accept(_visitorFactory.Create(DotNetEnv.Env.GetString("UI")));
        _boardView.WelcomeMessage();
        _boardView.BoardType = _game.BoardType;

        ReDraw();

        var currentBoardIndex = 0;
        var boardCount = _game.BoardList.Count();

        do
        {
            while (!Console.KeyAvailable)
            {
                // reading user input
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
                        _game.Solver.SolveBoard((_game.Board.SudokuBoard as SudokuBoard)!);
                        gameOver = true;
                        break;
                    //next samurai board
                    case ConsoleKey.E:
                        // checks if boardtype is samurai
                        if (_game.BoardType == BoardTypes.samurai)
                        {
                            // not index out of bounds (+1)
                            if (currentBoardIndex + 1 < boardCount)
                            {
                                // save curr progress back to list
                                _game.BoardList[currentBoardIndex] = _game.Board;
                                // sets new board
                                _game.Board = _game.BoardList[currentBoardIndex + 1];
                                currentBoardIndex++;
                            }
                        }
                        break;
                    //prev samurai board
                    case ConsoleKey.W:
                        // checks if boardtype is samurai
                        if (_game.BoardType == BoardTypes.samurai)
                        {
                            // not index out of bounds (-1)
                            if (currentBoardIndex - 1 != (-1))
                            {
                                // save curr progress back to list
                                _game.BoardList[currentBoardIndex] = _game.Board;
                                // sets new board
                                _game.Board = _game.BoardList[currentBoardIndex - 1];
                                currentBoardIndex--;
                            }
                        }
                        break;
                }

                ReDraw();

                if (gameOver) break;
            }
            if (gameOver) break;
        } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

        return RequestNewGame();
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
}