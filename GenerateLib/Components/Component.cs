using GenerateLib.Helpers;
using GenerateLib.Viewable;

namespace GenerateLib.Components;

public abstract class Component : IViewable
{
    public int Value { get; set; }
    public List<int> PossibleValues { get; set; }
    public bool IsCursor { get; set; }
    
    protected List<Component> Components = new ();
    public int X { get; set; }
    public int Y { get; set; }
    public bool HasCursor { get; set; }

    public virtual void Add(Component c)
    {
        Components.Add(c);
    }
    
    public virtual bool IsComposite()
    {
        return true;
    }
    
    public virtual bool HasDuplicate(Cell cell, int number)
    {
        foreach (Component component in Components)
        {
            if (component.IsComposite()) continue;
            var c = (Cell) component;
            if (c.Equals(cell)) continue;
            if (c.Value == number) return true;
        }

        return false;
    }

    public virtual bool HasEmptyCell()
    {
        foreach (var component in Components)
        {
            var isEmpty = component.HasEmptyCell();
            if (isEmpty)
            {
                return isEmpty;
            }
        }

        return false;
    }

    public virtual Cell? FindEmptyCell()
    {
        Cell emptyCell = null;
        foreach (var component in Components)
        {
            var maybeEmpty = component.FindEmptyCell();
            if (maybeEmpty != null)
            {
                emptyCell = maybeEmpty;
            }
        }

        return emptyCell;
    }

    /*
     * Every row has every column. Every column has every cell.
     */
    public List<List<Component>> GetAllData()
    {
        var columns = Components.First(e => e is Row).Components.Where(c => c is Column);
        return columns.Select(c => c.Components).ToList();
    }
    
    public List<IViewable> GetAllDataAsViewable(int rows, int squares, int cols)
    {
        var columns = Components.First(e => e is Row).Components.Where(c => c is Column).ToArray();
        List<IViewable> data = new List<IViewable>();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                var c = columns[j] as Column;
                data.Add(c!.GetXthElement(i));
            }
        }

        return data;
    }

    public virtual Cell? GetCursor()
    {
        return Components.First(c => c.HasCursor).GetCursor();
    }

    public virtual Cell GetNewCursor(int x, int y)
    {
        return Components.First(row => row is Row && row.Y == y).GetNewCursor(x, y);
    }

}