namespace BoardConstruction.Components;

public class Square : IComponent
{
    public int Id { get; }
    public Square(int id)
    {
        Id = id;
    }

    public override Abstraction.IComponent FindCellViaCoordinates(int x, int y)
    {
        return Components.FirstOrDefault(c => c is Cell && c.X == x && c.Y == y);
    }

    public override Cell? FindEmptyCell()
    {
        return Components.FirstOrDefault(c => c is Cell && c.HasEmptyCell()) as Cell;
    }

    public override List<Abstraction.IComponent> FindOldCursors()
    {
        var cursors = new List<Abstraction.IComponent>();
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