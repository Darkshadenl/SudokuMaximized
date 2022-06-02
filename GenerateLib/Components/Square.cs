namespace GenerateLib.Components;

public class Square : Component
{
    public int Id { get; set; }
    public Square(int id)
    {
        Id = id;
        // X = coordinateX;
        // Y = coordinateY;
    }

    public override Cell? GetCursor()
    {
        return Components.FirstOrDefault(c => c.IsCursor) as Cell;
    }
}