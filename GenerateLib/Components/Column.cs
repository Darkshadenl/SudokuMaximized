namespace GenerateLib.Components;

public class Column : Component
{
    public override Cell? GetCursor()
    {
        return Components.First(c => c.IsCursor) as Cell;
    }

    public List<Component> GetData()
    {
        var data = new List<Component>();
        foreach (var cell in Components)
        {
            if (cell is not Cell c) continue;
            data.Add(c);
        }

        return data;    
    }

    public override Cell GetNewCursor(int x, int y)
    {
        return (Components.First(cell => cell is Cell && cell.X == x && cell.Y == y) as Cell)!;
    }
}