using GenerateLib.Helpers;
using GenerateLib.Viewable;

namespace GenerateLib.Components;

public abstract class Component : IViewable, ICloneable
{
    public int Value { get; set; }
    public List<int> PossibleValues { get; set; } = new();
    public bool IsCursor { get; set; }

    public List<Component> Components = new();
    public int X { get; set; } // TODO weghalen
    public int Y { get; set; } // TODO weghalen
    public Coordinates Coordinates { get; set; } = new();

    public virtual object Clone()
    {
        throw new NotImplementedException();
    }

    public virtual void Add(Component c)
    {
        Components.Add(c);
    }

    public virtual bool IsComposite()
    {
        return true;
    }

    public bool HasDuplicate(Cell cell, int number)
    {
        foreach (Component component in Components)
        {
            if (component.IsComposite()) continue;
            var c = (Cell)component;
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
                return maybeEmpty;
            }
        }

        return emptyCell;
    }

    public bool ReplaceCell(Cell oldCell, Cell newCell)
    {
        if (this is not Row && this is not Column) return false;

        if (Components.Contains(oldCell))
        {
            var i = Components.IndexOf(oldCell);
            Components[i] = newCell;
            return true;
        }

        return false;
    }
}