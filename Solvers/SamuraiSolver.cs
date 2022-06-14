using Abstraction;
using IComponent = Abstraction.IComponent;

namespace Solvers;

public class SamuraiSolver : AbstractSolver
{
    public List<IComponent> SudokuBoards { get; set; }


    private List<int> _topList = new List<int>
    {
        3, 4
    };

    private List<int> _bottomList = new List<int>
    {
        0, 1
    };

    private List<List<IComponent>> _topRowLists = new();
    private List<List<IComponent>> _bottomRowLists = new();


    public override List<IComponent> SolveBoards(List<IComponent> sudokuBoards)
    {
        SudokuBoards = sudokuBoards;
        
        /* Normal solver's not working so...
        For the upper boards (upperleft and right) we're gonna grab the last row
        and solve the cells first.
        For the lower ones, we're gonna grab the first row cells first and solve
        those.
        Then the second / second last. And so on.
        
        */

        // Only rows from boards
        ExtractRowsFromBoard(_topList, sudokuBoards, true);
        ExtractRowsFromBoard(_bottomList, sudokuBoards, false);
        ExtractRowsFromBoard(2, sudokuBoards, true);
        ExtractRowsFromBoard(2, sudokuBoards, false);

        var allSolved = false;
        var offsetFromBottom = 0;
        var offsetFromTop = 0;
        var amountRowsSolved = 0;
        var bothList = new List<List<int>> {_topList, _bottomList};
        var order = bothList.SelectMany(i => i).ToList();

        while (!allSolved)
        {
            if (amountRowsSolved == 9)
            {
                allSolved = true;
            }
            
            var toBeSolvedRows = new List<IComponent>();
            List<bool> solved = new List<bool>();

            // grab rows

            for (var i = 0; i < _topRowLists.Count; i++)
            {
                toBeSolvedRows.Add(GrabOffsetTheTop(offsetFromTop));
                toBeSolvedRows.Add(GrabOffsetTheBottom(offsetFromBottom));
            }

            for (var index = 0; index < toBeSolvedRows.Count; index++)
            {
                Controller.CurrentBoardIndex = order[index];
                Controller.ReDraw();
                var toBeSolvedRow = toBeSolvedRows[index];

                var solve = Solve(toBeSolvedRow);
                if (solve == false)
                {
                    Console.WriteLine("Something went wrong");
                    break;
                }
            }

            amountRowsSolved++;
            offsetFromBottom++;
            offsetFromTop++;
        }

        return sudokuBoards;
    }

    private bool Solve(IComponent toBeSolvedRow)
    {
        var cellNoNumber = FindEmpty(toBeSolvedRow);

        if (cellNoNumber == null) 
            return true;        // solved
        

        for (int targetNumber = 1; targetNumber < 10; targetNumber++) // 1 to 9
        {
            if (Valid(cellNoNumber, targetNumber))
            {
                cellNoNumber.Value = targetNumber;
                Controller.ReDraw();
                Thread.Sleep(20);

                if (Solve(toBeSolvedRow))
                {
                    Controller.ReDraw();
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

    private ICell? FindEmpty(IComponent row)
    {
        return row.FindEmptyCell();
    }

    private void ExtractRowsFromBoard(List<int> list, List<IComponent> sudokuBoards, bool top)
    {
        foreach (var i in list)
        {
            var board = sudokuBoards[i];
            var rows = board.Components.Where(c => c is IRow).ToList();
            if (top)
                _topRowLists.Add(rows.ToList());
            else
                _bottomRowLists.Add(rows.ToList());
        }
    }
    
    private void ExtractRowsFromBoard(int index, List<IComponent> sudokuBoards, bool top)
    {
        var board = sudokuBoards[index];
        var rows = board.Components.Where(c => c is IRow).ToList();
        if (top)
            _topRowLists.Add(rows.ToList());
        else
            _bottomRowLists.Add(rows.ToList());
    }

    private IComponent GrabOffsetTheBottom(int offsetFromBottom)
    {
        foreach (var row in _bottomRowLists)
        {
            var amount = row.Count;
            return row[amount- 1 - offsetFromBottom];
        }

        return null!;
    }

    private IComponent GrabOffsetTheTop(int fromTop)
    {
        foreach (var row in _topRowLists)
        {
            return row[fromTop];
        }

        return null!;
    }
}