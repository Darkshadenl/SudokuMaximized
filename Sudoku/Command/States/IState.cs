namespace Sudoku.Command.States;

public interface IState
{
    public GenerateLib.Helpers.States State { get; set; }
    
}