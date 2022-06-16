using Abstraction;
using Helpers.Viewable;

namespace BoardConstruction.Components;

public abstract class Component : ICloneable, IComponent
{
    private CellValueRecord _valueRecord = new();

    private record CellValueRecord
    {
        public int Value { get; set; }
    }

    public int Value
    {
        get => _valueRecord.Value;
        set => _valueRecord.Value = value;
    }

    public List<int> PossibleValues { get; set; } = new();
    public bool IsCursor { get; set; }

    public List<IComponent> Components { get; set; } = new();
    
    public int X { get; set; }
    public int Y { get; set; }

    public IComponent Cursor { get; set; }

    public virtual object Clone()
    {
        return MemberwiseClone();
    }

    public virtual IComponent FindCellViaCoordinates(int x, int y)
    {
        foreach (var component in Components)
        {
            if (component is not Square s) continue;

            var newCursor = s.FindCellViaCoordinates(x, y);
            if (newCursor != null)
            {
                return newCursor;
            }
        }

        return Cursor;
    }

    public virtual void Add(Component c)
    {
        Components.Add(c);
    }

    public virtual bool IsComposite()
    {
        return true;
    }

    public bool HasDuplicateCellValue(ICell cell, int number)
    {
        if (!IsComposite()) return false;
        
        foreach (var component in Components)
        {
            if (component is not Cell c) continue;

            if (c.Value == number)
                return true;
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

    public virtual ICell? FindEmptyCell()
    {
        Cell emptyCell = null;

        foreach (var component in Components)
        {
            var maybeEmpty = component.FindEmptyCell();

            if (maybeEmpty != null)
                return maybeEmpty;
        }

        return emptyCell;
    }

    public bool ReplaceCell(ICell oldCell, ICell newCell)
    {
        if (this is not Row && this is not Column && this is not Square) return false;

        if (Components.Contains(oldCell))
        {
            var i = Components.IndexOf(oldCell);
            Components[i] = newCell;
            return true;
        }

        return false;
    }

    public virtual List<IComponent> FindOldCursors()
    {
        var oldCursors = new List<IComponent>();

        foreach (var component in Components)
        {
            if (component is Square s)
            {
                oldCursors.AddRange(s.FindOldCursors());
            }
        }

        return oldCursors;
    }

    public virtual List<IViewable> GetAllViewables()
    {
        List<IViewable> viewables = new();
        foreach (var component in Components)
        {
            if (component.IsComposite())
            {
                viewables.AddRange(component.GetAllViewables());
            }
            else
            {
                if (component is Cell c)
                    viewables.Add(c);
            }
        }

        return viewables;
    }

    public virtual List<ICell> GetAllCells()
    {
        List<ICell> cells = new();
        foreach (var component in Components)
        {
            if (component.IsComposite())
            {
                cells.AddRange(component.GetAllCells());
            }
            else
            {
                if (component is Cell c)
                    cells.Add(c);
            }
        }

        return cells;
    }
}