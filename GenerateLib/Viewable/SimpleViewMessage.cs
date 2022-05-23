namespace GenerateLib.Viewable;

public class SimpleViewMessage : ISimpleViewMessage
{
    public string Message { get; }
    public ConsoleColor MessageColor { get; }
    public BoardDrawTimings Position { get; }

    public SimpleViewMessage(string message, ConsoleColor messageColor, BoardDrawTimings placement)
    {
        Message = message;
        MessageColor = messageColor;
        Position = placement;
    }
}