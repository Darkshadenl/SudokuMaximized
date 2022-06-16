using Helpers.Helpers;
using Helpers.Viewable;

namespace BoardConstruction.Visitors;

public interface IPrintBoardVisitor
{
    public void Draw(IViewData viewData, BoardTypes type);
}