using Helpers.Viewable;

namespace Abstraction;

public interface IComponent
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Value { get; set; }
    public ICell? FindEmptyCell();
    public List<int> PossibleValues { get; set; }
    public bool IsCursor { get; set; }
    public List<IComponent> Components { get; set; }
    public virtual bool HasEmptyCell()
    {
        throw new NotImplementedException();
    }

    public bool HasDuplicateCellValue(ICell cell, int number);

    public bool ReplaceCell(ICell oldCell, ICell newCell);
}