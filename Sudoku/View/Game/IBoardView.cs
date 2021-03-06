using Construction.Visitors;
using Helpers.Enums;
using Helpers.Viewable;
using Sudoku.Controller;

namespace Sudoku.View.Game;

public interface IBoardView
{
    void DrawBoard(IViewData viewData);

    void Accept(IPrintBoardVisitor visitor);
    BoardTypes BoardType { get; set; }
    
    void StartNewGameMessage();
    void EndGameMessage();
    void WelcomeMessage();
}