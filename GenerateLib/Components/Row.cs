using GenerateLib.Viewable;

namespace GenerateLib.Components;

public class Row : Component
{

    public override Cell? FindEmptyCell()
    {
        return Components.FirstOrDefault(c => c is Cell && c.HasEmptyCell()) as Cell;
    }

    public override List<IViewable> GetAllViewables()
    {
        var viewables = new List<IViewable>();
        foreach (var component in Components)
        {
            if (component is Cell c)
            {
                viewables.Add(c);
            }
        }

        return viewables;
    }

    public override List<Cell> GetAllCells()
    {
        var cells = new List<Cell>();
        foreach (var component in Components)
        {
            if (component is Cell c)
            {
                cells.Add(c);
            }
        }

        return cells;
    }
}