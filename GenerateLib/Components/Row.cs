namespace GenerateLib.Components;

public class Row : Component
{
    public virtual List<Cell> GetAllCells()
    {
        return Components.Where(c => !c.IsComposite()).Select(c => (Cell) c).ToList();
    }
    
}