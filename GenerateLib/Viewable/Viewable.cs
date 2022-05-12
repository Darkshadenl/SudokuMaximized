using GenerateLib.Components;

namespace GenerateLib.Viewable;

public class Viewable : IViewable
{
    public int Value { get; set; }
    public List<int> PossibleValues { get; set; } = new();
    public bool Selected { get; set; }
    
    public Viewable(Component component)
    {
        Selected = false;
       
        if (component is Cell cell)
        {
            Value = cell.Value;
            if (cell.PossibleValues.Count > 0)
            {
                PossibleValues = cell.PossibleValues;
            }
        }
        else
        {
            Console.WriteLine("Viewable: component is not a cell");
        }
    }
    
}