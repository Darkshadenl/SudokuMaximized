using GenerateLib.Helpers;
using GenerateLib.Viewable;

namespace GenerateLib.Visitors;

public interface IPrintBoardVisitor
{
    public void Draw(IViewData viewData, BoardTypes type);
}