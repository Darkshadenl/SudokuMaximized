namespace Helpers.Viewable;

public interface ISimpleViewMessage
{
    string Message { get; }
    ConsoleColor MessageColor { get; }
    
    BoardDrawTimings Timing { get; }
}