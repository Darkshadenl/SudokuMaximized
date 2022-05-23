namespace GenerateLib.Viewable;

public class ViewData : IViewData
{
    public List<IViewable> Viewables { get; }
    public string State { get; }
    public List<ISimpleViewMessage>? PostBoardMessages { get; }
    public List<ISimpleViewMessage>? PreBoardMessages { get; }

    public ViewData(List<IViewable> viewables, string state, List<ISimpleViewMessage>? postBoardMessages,
        List<ISimpleViewMessage>? preBoardMessages)
    {
        Viewables = viewables;
        State = state;
        PostBoardMessages = postBoardMessages;
        PreBoardMessages = preBoardMessages;
    }
}