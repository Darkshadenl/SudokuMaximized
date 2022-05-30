using Sudoku.Model.Game;

namespace Sudoku.Command.States;

public class DefinitiveState : IState
{
    private readonly Game _game;
    private readonly Dictionary<ConsoleKey, int> _availableKeys;
    public GenerateLib.Helpers.States State { get; set; } = GenerateLib.Helpers.States.Definitive;

    public DefinitiveState(Game game, Dictionary<ConsoleKey, int> availableKeys)
    {
        _game = game;
        _availableKeys = availableKeys;
        Configure();
    }

    private void Configure()
    {
        _game.Select = new DefSelectCommand(_game, _availableKeys);
        _game.ShiftState = new StateToHelpCommand(_game, _availableKeys);
    }
}