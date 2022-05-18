namespace GenerateLib.Components;

public class Column : Component
{
    public Column(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override Cell? GetCursor()
    {
        return Components.First(c => c.IsCursor) as Cell;
    }
}