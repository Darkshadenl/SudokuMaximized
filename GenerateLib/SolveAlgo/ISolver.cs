using GenerateLib.Components;

namespace GenerateLib.SolveAlgo;

public interface ISolver
{
    public Component SudokuBoard { get; set; }
    public Component SolveBoard();
}