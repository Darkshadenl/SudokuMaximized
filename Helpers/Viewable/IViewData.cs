using Helpers.Helpers;

namespace Helpers.Viewable;

public interface IViewData
{
    List<IViewable> Viewables { get; }
    States State { get; }
    List<ISimpleViewMessage>? PostBoardMessages { get; }
    List<ISimpleViewMessage>? PreBoardMessages { get; }

}