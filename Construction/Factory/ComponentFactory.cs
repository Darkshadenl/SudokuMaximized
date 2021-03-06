using Construction.Config.JSONModel;
using Newtonsoft.Json;

namespace Construction.Factory;

public class ComponentFactory : IComponentFactory
{
    private Dictionary<string, Func<Component>>? _components = new();
 
    public ComponentFactory()
    {
        var json = File.ReadAllText(Environment.GetEnvironmentVariable("COMPONENTCONFIG") ?? 
                                    "./Config/Factory/ComponentFactoryConfiguration.json");
        
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