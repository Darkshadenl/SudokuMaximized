namespace Abstraction;

public interface ICell : IComponent
{
    public bool IsValueDuplicateInRows(int number);
    public bool IsValueDuplicateInColumns(int number);
    public bool IsValueDuplicateInSquares(int number);
    
    public bool HardNumber { get; }
    public List<IComponent> Rows { get; }
    public List<IComponent> Squares { get; }
    public List<IComponent> Columns { get; }
    public bool IsClone { get; set; }
}