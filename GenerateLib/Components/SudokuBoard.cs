using GenerateLib.Helpers;

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

    public bool CanCursorMove(Directions direction, Component cursor)
    {
        var rowNr = cursor.Y;
        var colNr = cursor.X;

        switch (direction)
        {
            case Directions.Up:
                return rowNr - 1 >= 0;
            case Directions.Down:
                return rowNr + 1 < BoardHeight;
            case Directions.Left:
                return colNr - 1 >= 0;
            case Directions.Right:
                return colNr + 1 < BoardWidth;
            default:
                return false;
        }
    }

    // private void FindCursorRowColSquare()
    // {
    //     if (CursorRow != null || CursorColumn != null) return;
    //     
    //     CursorRow = Components.First(e => e is Row {HasCursor: true});
    //     var row = CursorRow as Row;
    //     CursorColumn = row!.GetCursorCol();
    //     CursorSquare = Components.First(s => s is Square {HasCursor: true});
    // }

    private void UpdateSquareAfterMove()
    {
        // Check if current square still contains cursor, else update square
        var square = CursorSquare as Square;
        if (square!.GetCursor() == null)
        {
            CursorSquare!.HasCursor = false;
            var newCursorSquare = FindCursorSquare();
            newCursorSquare.HasCursor = true;
            CursorSquare = newCursorSquare;
        }
    }

    private void UpdateRowColAfterMove(Cell newCursor)
    {
        // Check if row and col still contain cursor, else update row and/or col
        if (CursorRow!.GetCursor() == null || CursorColumn!.GetCursor() == null)
        {
            // update
            CursorRow!.HasCursor = false;
            var row = Components.First(c => c is Row && c.Y == newCursor.Y) as Row;
            row!.HasCursor = true;
            var col = row.SetColHasCursor(newCursor.X);
            CursorColumn = col;
            CursorRow = row;
        }
    }

    private Square FindCursorSquare()
    {
        foreach (var component in Components)
        {
            if (component is not Square square) continue;
            var cursor = square.GetCursor();
            if (cursor != null) return square;
        }
        
        return null!;
    }
    
    public Cell MoveCursorRight(Cell cursor)
    {
        // Move cursor
        cursor.IsCursor = false;
        var cursorNewX = cursor.X + 1;
        var cursorNewY = cursor.Y;

        var newCursor = GetNewCursor(cursorNewX, cursorNewY);
        newCursor.IsCursor = true;
        
        // update cols, rows and squares. 
        UpdateSquareAfterMove();
        UpdateRowColAfterMove(newCursor);

        return newCursor;
    }

    public Cell MoveCursorLeft(Cell cursor)
    {
        // Move cursor
        cursor.IsCursor = false;
        var cursorNewX = cursor.X - 1;
        var cursorNewY = cursor.Y;

        var newCursor = GetNewCursor(cursorNewX, cursorNewY);
        newCursor.IsCursor = true;
        
        // update cols, rows and squares. 
        UpdateSquareAfterMove();
        UpdateRowColAfterMove(newCursor);

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
        
        // update cols, rows and squares. 
        UpdateSquareAfterMove();
        UpdateRowColAfterMove(newCursor);

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
        
        // update cols, rows and squares. 
        UpdateSquareAfterMove();
        UpdateRowColAfterMove(newCursor);

        return newCursor;
    }
}