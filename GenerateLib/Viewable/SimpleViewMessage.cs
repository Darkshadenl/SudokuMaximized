namespace GenerateLib.Viewable;

public class SimpleViewMessage : ISimpleViewMessage
{
    public string Message { get; }
    public ConsoleColor MessageColor { get; }
    public BoardDrawTimings Timing { get; }

    public SimpleViewMessage(string message, ConsoleColor messageColor, BoardDrawTimings timing)
    {
        Message = message;
        MessageColor = messageColor;
        Timing = timing;
    }
}