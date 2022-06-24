using Sudoku.Model.Game;

namespace Sudoku.Command.States;

public class HelpGameState : IGameState
{
    private readonly Game _game;
    private readonly Dictionary<ConsoleKey, int> _availableKeys;
    public Helpers.Enums.States State { get; set; } = Helpers.Enums.States.Help;

    public HelpGameState(Game game, Dictionary<ConsoleKey, int> availableKeys)
    {
        _game = game;
        _availableKeys = availableKeys;
        Configure();
    }

    private void Configure()
    {
        _game.Select = new HelpSelectCommand(_game, _availableKeys);
        _game.ShiftState = new StateToDefinitiveCommand(_game, _availableKeys);

    }
}