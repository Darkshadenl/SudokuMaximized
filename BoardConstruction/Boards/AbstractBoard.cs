using Abstraction;
using BoardConstruction.Components;
using Helpers.Helpers;
using Helpers.Viewable;
using Import.Import;
using Solvers;

namespace BoardConstruction.Boards;

public abstract class AbstractBoard
{
    public List<Component> SudokuBoards { get; set; }

    private int _oldBoardIndex;
    private int _currentBoardIndex;

    public int CurrentBoardIndex
    {
        get => _currentBoardIndex;
        set
        {
            _oldBoardIndex = _currentBoardIndex;
            _currentBoardIndex = value;
            UpdateCursorPositions();
        }
    }

    public BoardTypes Type { get; set; }
    public ISolver Solver { get; set; }
    public int Columns { get; set; }
    public int Rows { get; set; }
    public int Squares { get; set; }
    public int SquareLength { get; set; }
    public int StartCursorX { get; set; }
    public int StartCursorY { get; set; }
    public ICell Cursor { get; set; }

    public List<IViewable> GetViewables()
    {
        // CleanOldCursors();
        var s = getBoard();
        var v = s!.GetAllViewables();

        var c = v.Where(c => c.IsCursor).ToList();
        return v;
    }

    private void UpdateCursorPositions()
    {
        // we got current X and Y
        var oldBoard = SudokuBoards[_oldBoardIndex];
        var currentBoard = SudokuBoards[_currentBoardIndex];

        if (oldBoard.Cursor.X == currentBoard.Cursor.X && 
            oldBoard.Cursor.Y == currentBoard.Cursor.Y)
            return;
        
        // find cell in next board with those X and Y. 
        var newCursorCell = currentBoard.FindCellViaCoordinates(oldBoard.Cursor.X, oldBoard.Cursor.Y);
        newCursorCell.IsCursor = true;

        // Remove isCursor from old Cursor.
        currentBoard.Cursor.IsCursor = false;

        // Set Sudokuboard.Cursor to new Cursor.
        currentBoard.Cursor = newCursorCell;
        Cursor = (Cell) currentBoard.Cursor;
    }

    private void CleanOldCursors()
    {
        // clean old leftover cursors if there are any
        var oldCursors = SudokuBoards[_currentBoardIndex].FindOldCursors();
        if (oldCursors.Count == 1) 
            return;
        foreach (var c in oldCursors)
            c.IsCursor = false;
    }

    private SudokuBoard getBoard()
    {
        return (SudokuBoards[CurrentBoardIndex] as SudokuBoard)!;
    }

    public virtual AbstractBoard CreateBoardBuild(BoardFile boardFile)
    {
        throw new NotImplementedException();
    }

    public void MoveCursor(Directions direction)
    {
        // check if chosen direction has a cell
        var sudokuBoard = SudokuBoards[CurrentBoardIndex] as SudokuBoard;
        var canMove = sudokuBoard!.CanCursorMove(direction, Cursor);

        switch (canMove)
        {
            case false:
                return;

            case true:
                switch (direction)
                {
                    case Directions.Up:
                        Cursor = sudokuBoard.MoveCursorUp(Cursor);
                        return;
                    case Directions.Down:
                        Cursor = sudokuBoard.MoveCursorDown(Cursor);
                        return;
                    case Directions.Left:
                        Cursor = sudokuBoard.MoveCursorLeft(Cursor);
                        return;
                    case Directions.Right:
                        Cursor = sudokuBoard.MoveCursorRight(Cursor);
                        return;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(direction), direction, "Not a direction");
                }
        }
    }
}