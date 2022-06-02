using GenerateLib.Helpers;
using GenerateLib.Viewable;

namespace GenerateLib.Components;

public class SudokuBoard : Component
{
    public int BoardHeight { get; set; }
    public int BoardWidth { get; set; }

    public override Cell? FindEmptyCell()
    {
        foreach (var component in Components)
        {
            if (component.HasEmptyCell())
            {
                return component.FindEmptyCell();
            }
        }

        return null;
    }
    
    public List<IViewable> GetAllDataAsViewable()
    {
        var cells = GetAllCells();
        return cells.Cast<IViewable>().ToList();
    }

    protected virtual List<Cell> GetAllCells()
    {
        var cells = new List<Cell>();

        foreach (var component in Components)
            if (component is Row r)
                cells.AddRange(r.GetAllCells());
        
        return cells;
    }

    public bool CanCursorMove(Directions direction, Cell cursor)
    {
        var cells = GetAllCells();
        
        var rowNr = cursor.Y;
        var colNr = cursor.X;
        Cell newPos;

        switch (direction)
        {
            case Directions.Up:
                newPos = cells.FirstOrDefault(c => c.Y == rowNr - 1)!;
                return newPos != null;

            case Directions.Down:
                newPos = cells.FirstOrDefault(c => c.Y == rowNr + 1)!;
                return newPos != null;
            
            case Directions.Left:
                newPos = cells.FirstOrDefault(c => c.X == colNr - 1)!;
                return newPos != null;
            
            case Directions.Right:
                newPos = cells.FirstOrDefault(c => c.X == colNr + 1)!;
                return newPos != null;
            
            default:
                return false;
        }
    }

    public Cell MoveCursorRight(Cell cursor)
    {
        // Move cursor
        cursor.IsCursor = false;
        var cursorNewX = cursor.X + 1;
        var cursorNewY = cursor.Y;

        var newCursor = GetNewCursor(cursorNewX, cursorNewY);
        newCursor.IsCursor = true;

        return newCursor;
    }

    private Cell GetNewCursor(int cursorNewX, int cursorNewY)
    {
        return GetAllCells().FirstOrDefault(c => c.X == cursorNewX && c.Y == cursorNewY)!;
    }

    public Cell MoveCursorLeft(Cell cursor)
    {
        // Move cursor
        cursor.IsCursor = false;
        var cursorNewX = cursor.X - 1;
        var cursorNewY = cursor.Y;

        var newCursor = GetNewCursor(cursorNewX, cursorNewY);
        newCursor.IsCursor = true;

        return newCursor;
    }

    public Cell MoveCursorDown(Cell cursor)
    {
        // Move cursor
        cursor.IsCursor = false;
        var cursorNewX = cursor.X;
        var cursorNewY = cursor.Y + 1;

        var newCursor = GetNewCursor(cursorNewX, cursorNewY);
        newCursor.IsCursor = true;

        return newCursor;
    }
    
    public Cell MoveCursorUp(Cell cursor)
    {
        // Move cursor
        cursor.IsCursor = false;
        var cursorNewX = cursor.X;
        var cursorNewY = cursor.Y - 1;

        var newCursor = GetNewCursor(cursorNewX, cursorNewY);
        newCursor.IsCursor = true;

        return newCursor;
    }
}