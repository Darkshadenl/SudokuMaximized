namespace Helpers.Viewable;

public interface IViewable
{
    public int Value { get; set; }
    public List<int> PossibleValues { get; set; }
    public bool IsCursor {get; set;}
}