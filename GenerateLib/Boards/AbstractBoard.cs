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

    public List<IViewable> GetPrintData()
    {
        var printData = new List<IViewable>();
        var data = SudokuBoard.GetAllData();
        foreach (var componentList in data)
        {
            foreach (var component in componentList)
            {
                printData.Add(new Viewable.Viewable(component));
            }
        }

        return printData;
    }

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
        var canMove = sudokuBoard!.CanCursorMove(direction);
        
        // move cursor
        if (canMove)
        {
            switch (direction)
            {
                case Directions.UP:
                    sudokuBoard.MoveCursorUp();
                    break;
                case Directions.DOWN:
                    sudokuBoard.MoveCursorDown();
                    break;
                case Directions.LEFT:
                    sudokuBoard.MoveCursorLeft();
                    break;
                case Directions.RIGHT:
                    sudokuBoard.MoveCursorRight();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, "Not a direction");
            }
        }
        
        return false;
    }
}