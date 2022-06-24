namespace Sudoku.Command.States;

public interface IGameState
{
    public Helpers.Enums.States State { get; set; }
    
}