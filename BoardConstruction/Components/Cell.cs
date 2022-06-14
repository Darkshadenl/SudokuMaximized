using Abstraction;
using Helpers.Viewable;

namespace BoardConstruction.Components;

public class Cell : Component, IViewable, ICell
{
    // HardNumber = had a value above 0 from the start. Unchangeable. 
    public bool HardNumber { get; }
    public List<IComponent> Rows { get; }
    public List<IComponent> Squares { get; }
    public List<IComponent> Columns { get; }
    public bool IsClone { get; set; }

    public Cell(int value, int x, int y, bool hardNumber)
    {
        Rows = new List<IComponent>();
        Squares = new List<IComponent>();
        Columns = new List<IComponent>();
        HardNumber = hardNumber;
        Value = value;
        X = x;
        Y = y;
    }

    public override void Add(Component c)
    {
        throw new NotImplementedException();
    }

    public override bool IsComposite()
    {
        return false;
    }

    public override bool HasEmptyCell()
    {
        return Value == 0;
    }

    public override Cell? FindEmptyCell()
    {
        return Value == 0 ? this : null;
    }
    
    public bool IsValueDuplicateInRows(int number)
    {
        return Rows.Any(row =>
        {
            if (row is Row r)
            {
                return row.HasDuplicateCellValue(this, number);
            }
            return false;
        });
    }

    public bool IsValueDuplicateInColumns(int number)
    {
        return Columns.Any(col => col.HasDuplicateCellValue(this, number));
    }

    public bool IsValueDuplicateInSquares(int number)
    {
        return Squares.Any(square => square.HasDuplicateCellValue(this, number));
    }
}