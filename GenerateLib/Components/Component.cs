using GenerateLib.Viewable;

namespace GenerateLib.Components;

public abstract class Component : ICloneable
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

    public List<Component> Components = new();
    public int X { get; set; }
    public int Y { get; set; }

    public Component Cursor { get; set; }

    public virtual object Clone()
    {
        return MemberwiseClone();
    }

    public virtual Component FindCellViaCoordinates(int x, int y)
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

    public bool HasDuplicateCellValue(Cell cell, int number)
    {
        foreach (Component component in Components)
        {
            if (component is not Cell c) continue;

            c = (Cell) component;
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
                return maybeEmpty;
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

    public virtual List<Component> FindOldCursors()
    {
        var oldCursors = new List<Component>();

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
        throw new NotImplementedException();
    }

    public virtual List<Cell> GetAllCells()
    {
        throw new NotImplementedException();
    }
}