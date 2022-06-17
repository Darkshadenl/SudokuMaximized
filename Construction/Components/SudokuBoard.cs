using Abstraction;
using Helpers.Helpers;
using Helpers.Viewable;

namespace Construction.Components;

public class SudokuBoard : Component
{
    
    private List<ICell> _cells;
    public int BoardHeight { get; set; }
    public int BoardWidth { get; set; }

    public override Cell? FindEmptyCell()
    {
        Cell? empty = null;
        foreach (var component in Components)
        {
            if (component is not Row r) continue;
            empty = r.FindEmptyCell();
            if (empty is not null)
                return empty;
        }

        return empty;
    }
    
    public override List<IViewable> GetAllViewables()
    {
        var cells = GetAllCells();
        return cells.Cast<IViewable>().ToList();
    }

    public override List<ICell> GetAllCells()
    {
        if (_cells is not null) return _cells;
        _cells = new List<ICell>();

        foreach (var component in Components)
            if (component is Row r)
                _cells.AddRange(r.GetAllCells());
        
        return _cells;
    }

    public bool CanCursorMove(Directions direction, ICell cursor)
    {
        var cells = GetAllCells();
        
        var rowNr = cursor.Y;
        var colNr = cursor.X;
        ICell newPos;

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

    public ICell MoveCursorRight(ICell cursor)
    {
        // Move cursor
        cursor.IsCursor = false;
        var cursorNewX = cursor.X + 1;
        var cursorNewY = cursor.Y;

        var newCursor = GetNewCursor(cursorNewX, cursorNewY);
        newCursor.IsCursor = true;
        Cursor = newCursor;
        return newCursor;
    }

    private ICell GetNewCursor(int cursorNewX, int cursorNewY)
    {
        return GetAllCells().FirstOrDefault(c => c.X == cursorNewX && c.Y == cursorNewY)!;
    }

    public ICell MoveCursorLeft(ICell cursor)
    {
        // Move cursor
        cursor.IsCursor = false;
        var cursorNewX = cursor.X - 1;
        var cursorNewY = cursor.Y;

        var newCursor = GetNewCursor(cursorNewX, cursorNewY);
        newCursor.IsCursor = true;
        Cursor = newCursor;
        return newCursor;
    }

    public ICell MoveCursorDown(ICell cursor)
    {
        // Move cursor
        cursor.IsCursor = false;
        var cursorNewX = cursor.X;
        var cursorNewY = cursor.Y + 1;

        var newCursor = GetNewCursor(cursorNewX, cursorNewY);
        newCursor.IsCursor = true;
        Cursor = newCursor;
        return newCursor;
    }
    
    public ICell MoveCursorUp(ICell cursor)
    {
        // Move cursor
        cursor.IsCursor = false;
        var cursorNewX = cursor.X;
        var cursorNewY = cursor.Y - 1;

        var newCursor = GetNewCursor(cursorNewX, cursorNewY);
        newCursor.IsCursor = true;
        Cursor = newCursor;
        return newCursor;
    }
}