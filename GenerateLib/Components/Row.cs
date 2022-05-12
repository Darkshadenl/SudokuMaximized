namespace GenerateLib.Components;

public class Row : Component
{
    public Row(int rowNumber)
    {
        _id = rowNumber;
    }

    public List<Component> GetData()
    {
        var data = new List<Component>();
        foreach (var cell in _components)
        {
            if (cell is not Cell c) continue;
            data.Add(c);
        }

        return data;
    }
}