using Sudoku.Model.Components;

namespace Sudoku.Model;

public class Solver
{
    public Component SudokuBoard { get; set; }

    public Component SolveBoard()
    {
        var solved = Solve();
        if (solved)
        {
            return SudokuBoard;
        }
        return null;
    }
    
    private bool Solve()
    {
        Cell? cellNoNumber = FindEmpty();
    
        if (cellNoNumber == null)
        {
            return true;
        }
    
        for (int targetNumber = 1; targetNumber < 10; targetNumber++) // 1 to 9
        {
            if (Valid(cellNoNumber, targetNumber))
            {
                cellNoNumber.Value = targetNumber;
    
                if (Solve())
                    return true;
    
                cellNoNumber.Value = 0;
            }
        }
    
        return false;
    }

    private bool Valid(Cell emptyCell, int number)
    {
        var foundDuplicate = emptyCell.IsCellValueDuplicateInRow(number);
        if (foundDuplicate) return false;
        
        foundDuplicate = emptyCell.IsCellValueDuplicateInColumn(number);
        if (foundDuplicate) return false;
        
        foundDuplicate = emptyCell.IsCellValueDuplicateInSquare(number);
        if (foundDuplicate) return false;
    
        return true;
    }

    private Cell? FindEmpty()
    {
        return SudokuBoard.FindEmptyCell();
    }
    
}