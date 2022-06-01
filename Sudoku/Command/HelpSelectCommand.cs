using GenerateLib.Viewable;
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
        if (!_game.Board.Cursor.HardNumber)
        {
            var msg = new SimpleViewMessage("Enter number to place or enter same number to clear: ",
                ConsoleColor.Green, BoardDrawTimings.PostBoard);
            _game.AddMessages(msg);
            _game.ForceRedraw();
            
            ConsoleKey key = Console.ReadKey(false).Key;
            
            if (_availableKeys.ContainsKey(key))
            {
                _availableKeys.TryGetValue(key, out int val); 
                
                // check if contains
                var contains = _game.Board.Cursor.PossibleValues.Contains(val);
                if (!contains)
                {
                    // if not, add.
                    _game.Board.Cursor.PossibleValues.Add(val);
                } else if (contains)
                {
                    // If same, remove.
                    _game.Board.Cursor.PossibleValues.Remove(val);
                }
            }
        }
        else
        {
            var msg = new SimpleViewMessage("This is a default number. Can't be changed",
                ConsoleColor.Red, BoardDrawTimings.PreBoard);
            _game.AddMessages(msg);
            _game.ForceRedraw();
            Thread.Sleep(800);
        }
    }
}