using GenerateLib.Components;
using GenerateLib.Helpers;
using GenerateLib.Import;
using GenerateLib.SolveAlgo;
using GenerateLib.Viewable;

namespace GenerateLib.Boards;

public abstract class AbstractBoard
{
    protected Component SudokuBoard;
    public BoardTypes Type { get; set; }
    public ISolver Solver { get; set; }
    public int? Columns { get; set; }
    public int? Rows { get; set; }
    public int? Squares { get; set; }
    public int? SquareLength { get; set; }
    public int? StartCursorX { get; set; }
    public int? StartCursorY { get; set; }
    public Cell Cursor { get; set; }
    
    public List<IViewable> GetViewables()
    {
        var data = SudokuBoard.GetAllData();
        List<IViewable> printData = new List<IViewable>();

        for (var index = 0; index < data.Count; index++)
        {
            List<Component> list = data[index];
            printData.AddRange(list.Select(component => component as IViewable));
        }

        return printData;
    }

    public virtual AbstractBoard CreateBoardBuild(BoardFile boardFile)
    {
        throw new NotImplementedException();
    }

    public bool MoveCursor(Directions direction)
    {
        // check if chosen direction has a cell
        var sudokuBoard = SudokuBoard as SudokuBoard;
        var canMove = sudokuBoard!.CanCursorMove(direction, Cursor);
        
        switch (canMove)
        {
            case false:
                Console.WriteLine("Cannot move cursor in that direction");
                return false;
            
            case true:
                switch (direction)
                {
                    case Directions.UP:
                        return sudokuBoard.MoveCursorUp(Cursor);
                    case Directions.DOWN:
                        return sudokuBoard.MoveCursorDown(Cursor);
                    case Directions.LEFT:
                        return sudokuBoard.MoveCursorLeft(Cursor);
                    case Directions.RIGHT:
                        return sudokuBoard.MoveCursorRight(Cursor);
                    default:
                        throw new ArgumentOutOfRangeException(nameof(direction), direction, "Not a direction");
                }
        }
    }
}