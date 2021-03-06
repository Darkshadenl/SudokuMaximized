using Helpers.Viewable;

namespace Helpers.DrawingAlgsConsole;

public interface IDraw
{
    void DrawRegularBoard(int size, List<IViewable> board);
    void DrawJigSawBoard(int size, List<IViewable> board);
    void DrawSamuraiBoard(int size, List<IViewable> board);
}