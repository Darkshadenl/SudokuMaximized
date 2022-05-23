using Sudoku.Model.Game;

namespace Sudoku.Command;

public class HelpSelectCommand : ICommand
{
    private readonly Game _game;
    private readonly Dictionary<ConsoleKey, int> _availableKeys;

    public HelpSelectCommand(Game game, Dictionary<ConsoleKey, int> availableKeys)
    {
        _game = game;
        _availableKeys = availableKeys;
    }

    public void Execute()
    {
        throw new NotImplementedException();
    }
}