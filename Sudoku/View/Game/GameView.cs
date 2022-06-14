using Helpers.Helpers;
using Helpers.Viewable;
using Helpers.Visitors;
using Sudoku.Controller;

namespace Sudoku.View.Game;

public class GameView : IBoardView
{
    private IPrintBoardVisitor _visitor;
    public BoardTypes BoardType { get; set; } = BoardTypes.nine;

    public void DrawBoard(IViewData viewData)
    {
        Console.Clear();
        _visitor.Draw(viewData, BoardType);
    }

    public void WelcomeMessage()
    {
        Console.WriteLine("Starting your Sudoku game. Press ESC to quit the game.");
    }

    public void StartNewGameMessage()
    {
        Console.WriteLine("Would you like to start a new game? (y/n)");
    }

    public void EndGameMessage()
    {
        Console.WriteLine("Thank you for playing!");
    }

    public void Accept(IPrintBoardVisitor visitor)
    {
        _visitor = visitor;
    }

}