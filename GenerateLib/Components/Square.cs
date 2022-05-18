namespace GenerateLib.Components;

public class Square : Component
{
    public int CoordinateX { get; }
    public int CoordinateY { get; }

    public Square(int coordinateX, int coordinateY)
    {
        CoordinateX = coordinateX;
        CoordinateY = coordinateY;
    }

    public override Cell? GetCursor()
    {
        return Components.First(c => c.IsCursor) as Cell;
    }
}