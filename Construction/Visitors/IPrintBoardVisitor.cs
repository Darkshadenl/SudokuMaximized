using Helpers.Helpers;
using Helpers.Viewable;

namespace Construction.Visitors;

public interface IPrintBoardVisitor
{
    public void Draw(IViewData viewData, BoardTypes type);
}