using GenerateLib.Components;
using GenerateLib.Viewable;

namespace GenerateLib.SolveAlgo;

public class BackTrackingAlgo : ISolver
{
    public IState state;
    public List<Component> SudokuBoards { get; set; }
    private List<Component> _squares { get; set; } = new();

    private List<int> _orderOfSolving = new() { 0, 1, 3, 4, 2};

    public void Redraw(int i)
    {
        var viewData = new ViewData(SudokuBoards[i].GetAllViewables(), _game.State.State,
            pre, post);

        _boardView.DrawBoard(viewData);
    }

    public List<Component> SolveBoards(List<Component> boards)
    {
        SudokuBoards = boards;

        var solved = Solve(0);

        Console.ReadKey();
        
        return boards;
    }

    public object RefreshView { get; set; }

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

    private bool Valid(Cell emptyCell, int number)
    {
        var foundDuplicate = emptyCell.IsValueDuplicateInRows(number);
        if (foundDuplicate) return false;

        foundDuplicate = emptyCell.IsValueDuplicateInColumns(number);
        if (foundDuplicate) return false;

        foundDuplicate = emptyCell.IsValueDuplicateInSquares(number);
        if (foundDuplicate) return false;

        return true;
    }

    private Cell? FindEmpty(int i)
    {
        return SudokuBoards[i].FindEmptyCell();
    }
}