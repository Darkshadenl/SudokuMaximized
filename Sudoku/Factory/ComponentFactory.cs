using Newtonsoft.Json;
using Sudoku.Config.JSONModel;
using Sudoku.Interpreters;
using Component = Sudoku.Model.Components.Component;

namespace Sudoku.Factory;

public class ComponentFactory : IComponentFactory
{
    private Dictionary<string, Func<Component>>? _components = new();
 
    public ComponentFactory()
    {
        var json = File.ReadAllText(Environment.GetEnvironmentVariable("COMPONENTCONFIG") ?? 
                                    "./Config/ComponentFactoryConfiguration.json");
        
        var deserializeObject = JsonConvert.DeserializeObject<ComponentJSONModel>(json);
        foreach (var component in deserializeObject.components)
        {
            var type = Type.GetType($"{component._namespace}");
            
            _components!.Add(component.match, () =>
            {
                return Activator.CreateInstance(type) as Component;
            });
        }
    }

    public Component Create(string componentType)
    {
        string lookupValue = componentType.ToLowerInvariant();

        if (_components.TryGetValue(lookupValue, out var componentCreator))
        {
            return componentCreator.Invoke();
        }

        throw new ArgumentException($"Component {componentType} not found");
    }
    
}