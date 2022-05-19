namespace GenerateLib.Components;

public class Square : Component
{
    public Square(int coordinateX, int coordinateY)
    {
        X = coordinateX;
        Y = coordinateY;
    }

    public override Cell? GetCursor()
    {
        return Components.FirstOrDefault(c => c.IsCursor) as Cell;
    }
}