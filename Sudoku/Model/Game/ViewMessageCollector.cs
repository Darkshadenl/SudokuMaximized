using GenerateLib.Viewable;

namespace Sudoku.Model.Game;

public static class ViewMessageCollector
{
    private static List<ISimpleViewMessage> _messages = new();

    public static void AddMessage(string message, ConsoleColor consoleColor, BoardDrawTimings position)
    {
        _messages.Add(new SimpleViewMessage(message, consoleColor, position));
    }

    public static List<ISimpleViewMessage> RetrieveMessages(bool purge)
    {
        var messages = _messages;
        if (purge)
            _messages = new List<ISimpleViewMessage>();

        return messages;
    }
}