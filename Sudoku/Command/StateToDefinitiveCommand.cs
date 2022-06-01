using Sudoku.Command.States;
using Sudoku.Model.Game;

namespace Sudoku.Command;

public class StateToDefinitiveCommand : ICommand
{
    private readonly Game _game;
    private readonly Dictionary<ConsoleKey, int> _availableKeys;

    public StateToDefinitiveCommand(Game game, Dictionary<ConsoleKey, int> availableKeys)
    {
        _game = game;
        _availableKeys = availableKeys;
    }

    public void Execute()
    {
        _game.State = new DefinitiveState(_game, _availableKeys);
        _game.ForceRedraw();
    }
}