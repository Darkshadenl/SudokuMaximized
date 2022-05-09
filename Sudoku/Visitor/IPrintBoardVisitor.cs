using Sudoku.Model.Viewable;

namespace Sudoku.Visitor;

public interface IPrintBoardVisitor
{
    public void Draw(List<IViewable> board);

}