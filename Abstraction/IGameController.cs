using Helpers.Viewable;

namespace Abstraction;

public interface IGameController
{
    public void DrawUI(List<ISimpleViewMessage>? pre = null, List<ISimpleViewMessage>? post = null);
    public int CurrentBoardIndex { get; set; }
}