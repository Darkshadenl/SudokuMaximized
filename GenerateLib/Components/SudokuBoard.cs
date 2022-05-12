namespace GenerateLib.Components;

public class SudokuBoard : Component
{
    public override Cell? FindEmptyCell()
    {
        foreach (var component in _components)
        {
            if (component.HasEmptyCell())
            {
                return component.FindEmptyCell();
            }
        }
        return null;
    }
}