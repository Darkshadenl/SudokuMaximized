using GenerateLib.Helpers;
using GenerateLib.Viewable;

namespace GenerateLib.Visitors;

public interface IPrintBoardVisitor
{

    public void Draw(List<IViewable> board, BoardTypes type);

    // TODO etcetera

}