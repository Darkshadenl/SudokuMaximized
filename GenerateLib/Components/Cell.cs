namespace GenerateLib.Components;

public class Cell : Component
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public Row Row { get; set; }
    public Square Square { get; set; }
    public Column Column { get; set; }

    public Cell(int value, int x, int y)
    {
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
    
    public override void PrintId()
    {
        Console.WriteLine("Cell component has no id(ea)");
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