namespace Sudoku.Resources.Config;

public class GameKeyJSONModel
{
    public Key[] keys { get; set; }
}

public class Key
{
    public ConsoleKey key { get; set; }
    public string value { get; set; }
}