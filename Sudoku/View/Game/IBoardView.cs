using GenerateLib.Viewable;
using GenerateLib.Visitors;
using Sudoku.Controller;

namespace Sudoku.View.Game;

public interface IBoardView
{
    void Controller(GameController controller);
    
    void DrawBoard(List<IViewable> viewables);

    void Accept(IPrintBoardVisitor visitor);
}