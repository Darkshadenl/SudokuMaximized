namespace GenerateLib.Components;

public class Row : Component
{
    public void AddCellToCol(Cell c)
    {
        var cols = Components.ToArray();

        for (int i = 0; i < cols.Length; i++)
        {
            if (i == c.X && cols[i] is Column col && col.X == c.X)
                col.Add(c);
        }
    }
    
    public override Cell? GetCursor()
    {
        return Components.First(c => c.HasCursor).GetCursor();
    }

    public Component? GetCursorCol()
    {
        if (HasCursor)
        {
            return Components.First(c => c.HasCursor);
        }
        return null;
    }

    public List<Component> GetData()
    {
        var data = new List<Component>();
        foreach (var column in Components)
        {
            if (column is not Column c) continue;
            data = c.GetData();
        }

        return data;
    }

    public void SetColHasCursor(int x)
    {
        Components.First(c => c.X == x).HasCursor = true;
    }

    public override Cell GetNewCursor(int x, int y)
    {
        return Components.First(column => column.X == x).GetNewCursor(x, y);
    }
}