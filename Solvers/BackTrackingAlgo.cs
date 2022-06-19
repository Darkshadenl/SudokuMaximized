using Abstraction;
using IComponent = Abstraction.IComponent;

namespace Solvers;

public class BackTrackingAlgo : AbstractSolver
{
    private IComponent SudokuBoard { get; set; }
    private List<IComponent> _squares { get; set; } = new();
    private List<List<String>> _errorList = new();

    private List<int> _orderOfSolving = new() { 0, 1, 3, 4, 2};

    public override IComponent SolveBoard(IComponent board)
    {
        SudokuBoard = board;  // sudokuboards
        var solved = Solve();
        
        return board;
    }

    private bool Solve()
    {
        var cellNoNumber = FindEmpty();

        if (cellNoNumber == null) 
            return true;        // solved
        

        for (int targetNumber = 1; targetNumber < 10; targetNumber++) // 1 to 9
        {
            if (Valid(cellNoNumber, targetNumber))
            {
                cellNoNumber.Value = targetNumber;
                // Controller.ReDraw();
                // Thread.Sleep(20);

                if (Solve())
                {
                    // Controller.ReDraw();
                    return true;
                }

                cellNoNumber.Value = 0;
            }
        }

        return false;
    }

    private bool Valid(ICell emptyCell, int number)
    {
        var foundDuplicate = emptyCell.IsValueDuplicateInSquares(number);
        if (foundDuplicate) return false;
        
        foundDuplicate = emptyCell.IsValueDuplicateInColumns(number);
        if (foundDuplicate) return false;
        
        foundDuplicate = emptyCell.IsValueDuplicateInRows(number);
        if (foundDuplicate) return false;

        return true;
    }

    private ICell? FindEmpty()
    {
        return SudokuBoard.FindEmptyCell();
    }
    
}
