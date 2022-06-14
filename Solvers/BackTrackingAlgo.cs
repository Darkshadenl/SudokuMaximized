using System.ComponentModel;
using Abstraction;
using IComponent = Abstraction.IComponent;

namespace Solvers;

public class BackTrackingAlgo : ISolver
{
    public List<IComponent> SudokuBoards { get; set; }
    private List<IComponent> _squares { get; set; } = new();

    private List<int> _orderOfSolving = new() { 0, 1, 3, 4, 2};
    public List<IComponent> SolveBoards(List<IComponent> boards)
    {
        SudokuBoards = boards;

        var solved = Solve(0);

        Console.ReadKey();
        
        return boards;
    }

    private bool Solve(int i)
    {
        var cellNoNumber = FindEmpty(i);

        if (cellNoNumber == null) 
            return true;        // solved
        

        for (int targetNumber = 1; targetNumber < 10; targetNumber++) // 1 to 9
        {
            if (Valid(cellNoNumber, targetNumber))
            {
                cellNoNumber.Value = targetNumber;

                if (Solve(i))
                    return true;

                cellNoNumber.Value = 0;
            }
        }

        return false;
    }

    private bool Valid(ICell emptyCell, int number)
    {
        var foundDuplicate = emptyCell.IsValueDuplicateInRows(number);
        if (foundDuplicate) return false;

        foundDuplicate = emptyCell.IsValueDuplicateInColumns(number);
        if (foundDuplicate) return false;

        foundDuplicate = emptyCell.IsValueDuplicateInSquares(number);
        if (foundDuplicate) return false;

        return true;
    }

    private ICell? FindEmpty(int i)
    {
        return SudokuBoards[i].FindEmptyCell();
    }

    
}