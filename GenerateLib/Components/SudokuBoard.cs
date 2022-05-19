using GenerateLib.Helpers;

namespace GenerateLib.Components;

public class SudokuBoard : Component
{
    public int BoardHeight { get; set; }
    public int BoardWidth { get; set; }

    public Component? CursorRow { get; set; }
    public Component? CursorColumn { get; set; }
    public Component? CursorSquare { get; set; }

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

    public bool CanCursorMove(Directions direction, Component cursor)
    {
        var rowNr = cursor.Y;
        var colNr = cursor.X;

        switch (direction)
        {
            case Directions.UP:
                return rowNr - 1 >= 0;
            case Directions.DOWN:
                return rowNr + 1 <= BoardHeight;
            case Directions.LEFT:
                return colNr - 1 >= 0;
            case Directions.RIGHT:
                return colNr + 1 <= BoardWidth;
            default:
                return false;
        }
    }

    private void FindCursorRowColSquare()
    {
        if (CursorRow != null || CursorColumn != null) return;
        
        CursorRow = Components.First(e => e is Row {HasCursor: true});
        var row = CursorRow as Row;
        CursorColumn = row!.GetCursorCol();
        CursorSquare = Components.First(s => s is Square {HasCursor: true});
    }

    public bool MoveCursorRight(Cell cursor)
    {
        FindCursorRowColSquare();
        
        // Move cursor
        cursor.IsCursor = false;
        var cursorNewX = cursor.X + 1;
        var cursorNewY = cursor.Y;

        var newCursor = GetNewCursor(cursorNewX, cursorNewY);
        newCursor.IsCursor = true;
        CursorRow!.IsCursor = false;
        CursorColumn!.IsCursor = false;
        
        // Check if current square still contains cursor, else update
        var square = CursorSquare as Square;
        var squareCursor = square!.GetCursor();
        if (squareCursor == null)
        {
            CursorSquare.HasCursor = false;
            
        }
        
        
        
        // Setup new CursorRow and CursorColumn
        return false;
    }

    public bool MoveCursorLeft(Cell cursor)
    {
        FindCursorRowColSquare();
        return false;
    }

    public bool MoveCursorDown(Cell cursor)
    {
        FindCursorRowColSquare();
        return false;
    }

    public bool MoveCursorUp(Cell cursor)
    {
        FindCursorRowColSquare();
        return false;
    }
}