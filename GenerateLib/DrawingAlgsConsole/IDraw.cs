using GenerateLib.Viewable;

namespace GenerateLib.DrawingAlgsConsole;

public interface IDraw
{
    void DrawRegularBoard(int size, List<IViewable> board);
    void DrawJigSawBoard(int size, List<IViewable> board);
}