using GenerateLib.Components;

namespace GenerateLib.SolveAlgo;

public class BackTrackingAlgo : ISolver
{
    public List<Component> SudokuBoards { get; set; }

    public List<Component> SolveBoards(List<Component> boards)
    {
        SudokuBoards = boards;
        for(int i = 0; i < SudokuBoards.Count; i++)
        {
            SudokuBoards[i] = boards[i] as SudokuBoard;
        }

        for(int i = 0; i< SudokuBoards.Count; i++)
        {
            if (i == 2)
                continue;

            var solved = Solve(i);
            if (solved && SudokuBoards.Count == i)
            {
                return SudokuBoards;
            }
        }

        return null;
    }

    private bool Solve(int i)
    {
        Cell cellNoNumber = FindEmpty(i);

        if (cellNoNumber == null)
            return true;

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

    private bool Valid(Cell emptyCell, int number)
    {
        var foundDuplicate = emptyCell.IsCellValueDuplicateInRows(number);
        if (foundDuplicate) return false;

        foundDuplicate = emptyCell.IsCellValueDuplicateInColumns(number);
        if (foundDuplicate) return false;

        foundDuplicate = emptyCell.IsCellValueDuplicateInSquares(number);
        if (foundDuplicate) return false;

        return true;
    }

    private Cell FindEmpty(int i)
    {
        return SudokuBoards[i].FindEmptyCell();
    }
}