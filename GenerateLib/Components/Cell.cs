namespace GenerateLib.Components;

public class Cell : Component
{
    // HardNumber = had a value above 0 from the start. Unchangeable. 
    public bool HardNumber { get; }
    public Row Row { get; set; }
    public Square Square { get; set; }
    public Column Column { get; set; }

    public Cell(int value, int x, int y, bool hardNumber)
    {
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

    public void PrintSelf()
    {
        Console.Write($" {Value} ");
    }

    public override Cell? GetCursor()
    {
        if (IsCursor)
            return this;
        return null;
    }

    public override bool HasEmptyCell()
    {
        return Value == 0;
    }

    public override Cell? FindEmptyCell()
    {
        if (Value == 0) return this;
        return null;
    }

    public bool IsCellValueDuplicateInRow(int number)
    {
        return Row.HasDuplicate(this, number);
    }

    public bool IsCellValueDuplicateInColumn(int number)
    {
        return Column.HasDuplicate(this, number);
    }

    public bool IsCellValueDuplicateInSquare(int number)
    {
        return Square.HasDuplicate(this, number);
    }
}