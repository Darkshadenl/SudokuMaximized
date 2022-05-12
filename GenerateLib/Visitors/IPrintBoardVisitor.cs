using GenerateLib.Viewable;

namespace GenerateLib.Visitors;

public interface IPrintBoardVisitor
{
    public void DrawRegular(List<IViewable> board);
    public void DrawJigSaw(List<IViewable> board);
    
    // TODO etcetera

}