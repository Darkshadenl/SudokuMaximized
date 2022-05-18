namespace GenerateLib.Components;

public class Row : Component
{
    public Row(int x, int y)
    {
        X = x;
        Y = y;
    }
    
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
}