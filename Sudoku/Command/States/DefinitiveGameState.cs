using Sudoku.Model.Game;

namespace Sudoku.Command.States;

public class DefinitiveGameState : IGameState
{
    private readonly Game _game;
    private readonly Dictionary<ConsoleKey, int> _availableKeys;
    public Helpers.Enums.States State { get; set; } = Helpers.Enums.States.Definitive;

    public DefinitiveGameState(Game game, Dictionary<ConsoleKey, int> availableKeys)
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