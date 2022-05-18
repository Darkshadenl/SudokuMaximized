﻿using GenerateLib.Helpers;
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

    public List<List<Component>> GetAllData()
    {
        List<List<Component>> rows = new ();
        foreach (var component in Components)
        {
            if (component is not Row r) continue;
            
            var data = r.GetData();
            rows.Add(data);
        }

        return rows;
    }

    public virtual Cell? GetCursor()
    {
        return Components.First(c => c is Column && c.HasCursor).GetCursor();
    }

    public virtual Cell? GetCell(int x, int y)
    {
        return Components.First(e => e is SudokuBoard).GetCell(x, y);
    }
    
}