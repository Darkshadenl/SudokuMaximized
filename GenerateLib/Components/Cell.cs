namespace GenerateLib.Components;

public class Cell : Component
{
    // HardNumber = had a value above 0 from the start. Unchangeable. 
    public bool HardNumber { get; }
    public List<Row> Row { get; }
    public List<Square> Square { get; set; }
    public List<Column> Column { get; }

    public Cell(int value, int x, int y, bool hardNumber)
    {
        Row = new List<Row>();
        Square = new List<Square>();
        Column = new List<Column>();
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
        if (Value == 0) return this;
        return null;
    }

    public bool IsCellValueDuplicateInRows(int number)
    {
        return Row.All(c => c.HasDuplicate(this, number));
    }

    public bool IsCellValueDuplicateInColumns(int number)
    {
        return Column.All(c => c.HasDuplicate(this, number));
    }

    public bool IsCellValueDuplicateInSquares(int number)
    {
        return Square.All(c => c.HasDuplicate(this, number));   // TODO possible bug
    }
}