using GenerateLib.Helpers;
using GenerateLib.Viewable;
using GenerateLib.Visitors;
using Sudoku.Controller;

namespace Sudoku.View.Game;

public interface IBoardView
{
    void Controller(GameController controller);

    void DrawBoard(IViewData viewData);

    void Accept(IPrintBoardVisitor visitor);
    BoardTypes BoardType { get; set; }
}