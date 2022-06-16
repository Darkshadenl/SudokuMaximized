﻿using Abstraction;
using Helpers.Viewable;

namespace BoardConstruction.Components;

public abstract class IComponent : ICloneable, Abstraction.IComponent
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

    public List<Abstraction.IComponent> Components { get; set; } = new();
    
    public int X { get; set; }
    public int Y { get; set; }

    public Abstraction.IComponent Cursor { get; set; }

    public virtual object Clone()
    {
        return MemberwiseClone();
    }

    public virtual Abstraction.IComponent FindCellViaCoordinates(int x, int y)
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

    public virtual void Add(IComponent c)
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
        
        foreach (Abstraction.IComponent component in Components)
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
        if (this is not Row && this is not Column) return false;

        if (Components.Contains(oldCell))
        {
            var i = Components.IndexOf(oldCell);
            Components[i] = newCell;
            return true;
        }

        return false;
    }

    public virtual List<Abstraction.IComponent> FindOldCursors()
    {
        var oldCursors = new List<Abstraction.IComponent>();

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

    public virtual List<ICell> GetAllCells()
    {
        throw new NotImplementedException();
    }
}