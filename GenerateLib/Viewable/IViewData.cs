namespace GenerateLib.Viewable;

public interface IViewData
{
    List<IViewable> Viewables { get; }
    string State { get; }
    List<ISimpleViewMessage>? PostBoardMessages { get; }
    List<ISimpleViewMessage>? PreBoardMessages { get; }

}