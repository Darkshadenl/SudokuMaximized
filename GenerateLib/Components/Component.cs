using GenerateLib.Viewable;

namespace GenerateLib.Components;

public abstract class Component : IViewable
{
    public int Value { get; set; }
    public List<int> PossibleValues { get; set; }
    public bool Selected { get; set; }
    
    protected List<Component> _components = new ();
    protected int _id = -1;

    public virtual void Add(Component c)
    {
        _components.Add(c);
    }
    
    public virtual bool IsComposite()
    {
        return true;
    }

    public virtual void PrintId()
    {
        Console.WriteLine(_id);
    }
    
    public virtual bool HasDuplicate(Cell cell, int number)
    {
        foreach (Component component in _components)
        {
            if (component.IsComposite()) continue;
            var c = (Cell) component;
            if (c.Equals(cell)) continue;
            if (c.Value == number) return true;
        }

        return false;
    }

    public virtual bool HasEmptyCell()
    {
        foreach (var component in _components)
        {
            var isEmpty = component.HasEmptyCell();
            if (isEmpty)
            {
                return isEmpty;
            }
        }

        return false;
    }

    public virtual Cell? FindEmptyCell()
    {
        Cell emptyCell = null;
        foreach (var component in _components)
        {
            var maybeEmpty = component.FindEmptyCell();
            if (maybeEmpty != null)
            {
                emptyCell = maybeEmpty;
            }
        }

        return emptyCell;
    }

    public List<List<Component>> GetAllData()
    {
        List<List<Component>> rows = new ();
        foreach (var component in _components)
        {
            if (component is not Row r) continue;
            
            var data = r.GetData();
            rows.Add(data);
        }

        return rows;
    }

  
}