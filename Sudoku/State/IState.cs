using GenerateLib.Components;
using GenerateLib.Helpers;

namespace Sudoku.State;

public interface IState
{
    public States State { get; set; }
    
}