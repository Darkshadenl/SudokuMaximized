using GenerateLib.Viewable;
using GenerateLib.Visitors;
using Sudoku.Controller;

namespace Sudoku.View.Game;

public class GameView : IBoardView
{
    private IPrintBoardVisitor _visitor;
    private GameController _controller;
    
    public void Controller(GameController controller)
    {
        _controller = controller;
    }

    public void DrawBoard(List<IViewable> viewables)
    {
        Console.Clear();
        _visitor.DrawRegular(viewables);
    }

    public void Accept(IPrintBoardVisitor visitor)
    {
        _visitor = visitor;
    }
}