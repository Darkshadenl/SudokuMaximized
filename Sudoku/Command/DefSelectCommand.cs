using GenerateLib.Viewable;
using Sudoku.Model.Game;

namespace Sudoku.Command;

public class DefSelectCommand : ICommand
{
    private readonly Game _game;
    private readonly Dictionary<ConsoleKey, int> _availableKeys;

    public DefSelectCommand(Game game, Dictionary<ConsoleKey, int> availableKeys)
    {
        _game = game;
        _availableKeys = availableKeys;
    }


    public void Execute()
    {
        if (!_game.Board.Cursor.HardNumber)
        {
            var msg = new SimpleViewMessage("Enter number to place: ",
                ConsoleColor.Green, BoardDrawTimings.PostBoard);
            _game.AddMessages(msg);

            ConsoleKey key = Console.ReadKey(false).Key;
            
            if (_availableKeys.ContainsKey(key))
            {
                _availableKeys.TryGetValue(key, out int val);
                _game.Board.Cursor.Value = val;
            }
        }
        else
        {
            var msg = new SimpleViewMessage("This is a default number. Can't be changed",
                ConsoleColor.Red, BoardDrawTimings.PreBoard);
            _game.AddMessages(msg);
            Thread.Sleep(200);
        }
    }
}