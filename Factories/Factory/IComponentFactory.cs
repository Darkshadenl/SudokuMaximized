using Factories.Config.JSONModel;

namespace Factories.Factory;

public interface IComponentFactory
{
    public Component Create(string type);
}