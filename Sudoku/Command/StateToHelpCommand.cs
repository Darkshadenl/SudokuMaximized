using Sudoku.Model.Game;
using Sudoku.State;

namespace Sudoku.Command;

public class StateToHelpCommand : ICommand
{
    private readonly Game _game;
    private readonly Dictionary<ConsoleKey, int> _availableKeys;

    public StateToHelpCommand(Game game, Dictionary<ConsoleKey, int> availableKeys)
    {
        _game = game;
        _availableKeys = availableKeys;
    }


    public void Execute()
    {
        _game.State = new HelpState(_game, _availableKeys);
        _game.ForceRedraw();
    }
}