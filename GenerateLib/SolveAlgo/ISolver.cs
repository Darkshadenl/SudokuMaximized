using GenerateLib.Components;

namespace GenerateLib.SolveAlgo;

public interface ISolver
{
    public Component? SolveBoard(SudokuBoard sudokuBoard);
}