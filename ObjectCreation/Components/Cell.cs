using Abstraction;
using Helpers.Viewable;

namespace ObjectCreation.Components;

public class Cell : IComponent, IViewable, ICell
{
    // HardNumber = had a value above 0 from the start. Unchangeable. 
    public bool HardNumber { get; }
    public List<Abstraction.IComponent> Rows { get; }
    public List<Abstraction.IComponent> Squares { get; }
    public List<Abstraction.IComponent> Columns { get; }
    public bool IsClone { get; set; }

    public Cell(int value, int x, int y, bool hardNumber)
    {
        Rows = new List<Abstraction.IComponent>();
        Squares = new List<Abstraction.IComponent>();
        Columns = new List<Abstraction.IComponent>();
        HardNumber = hardNumber;
        Value = value;
        X = x;
        Y = y;
    }

    public override void Add(IComponent c)
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
        return Rows.Any(row => row.HasDuplicateCellValue(this, number));
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