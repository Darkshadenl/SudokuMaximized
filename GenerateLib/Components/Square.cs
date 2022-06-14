namespace GenerateLib.Components;

public class Square : Component
{
    public int Id { get; }
    public Square(int id)
    {
        Id = id;
    }

    public override Component FindCellViaCoordinates(int x, int y)
    {
        return Components.FirstOrDefault(c => c is Cell && c.X == x && c.Y == y);
    }

    public override Cell? FindEmptyCell()
    {
        return Components.FirstOrDefault(c => c is Cell && c.HasEmptyCell()) as Cell;
    }

    public override List<Component> FindOldCursors()
    {
        var cursors = new List<Component>();
        foreach (var component in Components)
        {
            if (component.IsCursor)
            {
                cursors.Add(component);
            }
        }

        return cursors;
    }
}