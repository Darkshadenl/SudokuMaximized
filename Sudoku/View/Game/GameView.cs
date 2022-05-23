using GenerateLib.Helpers;
using GenerateLib.Viewable;
using GenerateLib.Visitors;
using Sudoku.Controller;

namespace Sudoku.View.Game;

public class GameView : IBoardView
{
    private IPrintBoardVisitor _visitor;
    private GameController _controller;
    public BoardTypes BoardType { get; set; } = BoardTypes.nine;
    
    public void Controller(GameController controller)
    {
        _controller = controller;
    }

    public void DrawBoard(IViewData viewData)
    {
        Console.Clear();
        _visitor.Draw(viewData, BoardType);
    }

    public void Accept(IPrintBoardVisitor visitor)
    {
        _visitor = visitor;
    }

}