using GenerateLib.Helpers;

namespace GenerateLib.Viewable;

public class ViewData : IViewData
{
    public List<IViewable> Viewables { get; }
    public States State { get; }
    public List<ISimpleViewMessage>? PostBoardMessages { get; }
    public List<ISimpleViewMessage>? PreBoardMessages { get; }

    public ViewData(List<IViewable> viewables, States state, List<ISimpleViewMessage>? preBoardMessages, 
        List<ISimpleViewMessage>? postBoardMessages)
    {
        Viewables = viewables;
        State = state;
        PostBoardMessages = postBoardMessages;
        PreBoardMessages = preBoardMessages;
    }
}