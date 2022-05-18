using GenerateLib.Helpers;

namespace GenerateLib.Components;

public class SudokuBoard : Component
{
    public int BoardHeight { get; set; }
    public int BoardWidth { get; set; }

    public Component? CursorRow { get; set; }
    public Component? CursorColumn { get; set; }

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

    public bool CanCursorMove(Directions direction)
    {
        FindCursorRowCol();
        var rowNr = CursorRow!.Id;
        var colNr = CursorColumn!.Id;

        switch (direction)
        {
            case Directions.UP:
                return rowNr - 1 >= 0;
            case Directions.DOWN:
                return rowNr + 1 <= BoardHeight;
            case Directions.LEFT:
                return colNr - 1 >= 0;
            case Directions.RIGHT:
                return colNr + 1 >= BoardWidth;
            default:
                return false;
        }
    }

    private void FindCursorRowCol()
    {
        if (CursorRow != null || CursorColumn != null) return;
        CursorRow = Components.First(e => e is Row {HasCursor: true});
        CursorColumn = Components.First(e => e is Column {HasCursor: true});
    }

    public void MoveCursorRight()
    {
        FindCursorRowCol();
        // Move cursor
        var cursor = CursorColumn!.GetCursor();
        cursor!.IsCursor = false;
        var cursorNewX = cursor.X + 1;

        // Setup new CursorRow and CursorColumn
    }

    public void MoveCursorLeft()
    {
        FindCursorRowCol();
    }

    public void MoveCursorDown()
    {
        FindCursorRowCol();
        
    }

    public void MoveCursorUp()
    {
        FindCursorRowCol();
    }

    public override Cell? GetCell(int x, int y)
    {
        
    }
}