using GenerateLib.Components;

namespace Sudoku.State;

public interface IState
{
    public string State { get; set; }
    public void Configure();
}