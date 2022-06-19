using Sudoku.Model.Game;

namespace Sudoku.Command.States;

public class HelpState : IState
{
    private readonly Game _game;
    private readonly Dictionary<ConsoleKey, int> _availableKeys;
    public Helpers.Helpers.States State { get; set; } = Helpers.Helpers.States.Help;

    public HelpState(Game game, Dictionary<ConsoleKey, int> availableKeys)
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