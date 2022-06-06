using GenerateLib.Components;
using GenerateLib.Helpers;
using GenerateLib.Import;
using GenerateLib.SolveAlgo;
using GenerateLib.Viewable;

namespace GenerateLib.Boards;

public abstract class AbstractBoard
{
    public List<Component> SudokuBoards { get; set; }
    public BoardTypes Type { get; set; }
    public ISolver Solver { get; set; }
    public int Columns { get; set; }
    public int Rows { get; set; }
    public int Squares { get; set; }
    public int SquareLength { get; set; }
    public int StartCursorX { get; set; }
    public int StartCursorY { get; set; }
    public Cell Cursor { get; set; }
    
    public List<IViewable> GetViewables()
    {
        var s = SudokuBoards[0] as SudokuBoard;
        var v =s!.GetAllDataAsViewable();
        return v;
    }

    public virtual AbstractBoard CreateBoardBuild(BoardFile boardFile)
    {
        throw new NotImplementedException();
    }

    public Component GetBoardByIndex(int index)
    {
        return SudokuBoards.ToArray()[index];
    }

    public void MoveCursor(Directions direction)
    {
        // check if chosen direction has a cell
        var sudokuBoard = SudokuBoards[0] as SudokuBoard;
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