using GenerateLib.Components;

namespace GenerateLib.SolveAlgo;

public interface ISolver
{
    public List<Component> SolveBoards(List<Component> sudokuBoards);
}