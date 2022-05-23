namespace GenerateLib.Components;

public class Column : Component
{
    public override Cell? GetCursor()
    {
        return Components.FirstOrDefault(c => c.IsCursor) as Cell;
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

    public Cell GetXthElement(int x)
    {
        var c = Components.ToArray();
        return (c[x] as Cell)!;
    }

    public override Cell GetNewCursor(int x, int y)
    {
        return (Components.First(cell => cell is Cell && cell.X == x && cell.Y == y) as Cell)!;
    }
}