using Construction.Config.JSONModel;

namespace Construction.Factory;

public interface IComponentFactory
{
    public Component Create(string type);
}