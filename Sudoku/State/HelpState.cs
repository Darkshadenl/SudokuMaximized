using GenerateLib.Components;
using Sudoku.Command;
using Sudoku.Model.Game;

namespace Sudoku.State;

public class HelpState : IState
{
    private readonly Game _game;
    private readonly Dictionary<ConsoleKey, int> _availableKeys;
    public string State { get; set; } = "Help";

    public HelpState(Game game, Dictionary<ConsoleKey, int> availableKeys)
    {
        _game = game;
        _availableKeys = availableKeys;
    }


    public void Configure()
    {
        _game.Select = new HelpSelectCommand(_game, _availableKeys);
        _game.ShiftState = new StateToDefinitiveCommand(_game, _availableKeys);

    }
}