using GenerateLib.Components;
using GenerateLib.Helpers;
using Sudoku.Command;
using Sudoku.Model.Game;

namespace Sudoku.State;

public class DefinitiveState : IState
{
    private readonly Game _game;
    private readonly Dictionary<ConsoleKey, int> _availableKeys;
    public States State { get; set; } = States.Definitive;

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