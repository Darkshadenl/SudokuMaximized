namespace Factories.Config.JSONModel;

public class ComponentJSONModel
{
    public Component[] components { get; set; }
}

public class Component
{
    public string match { get; set; }
    public string library { get; set; }
    public string _namespace { get; set; }
}