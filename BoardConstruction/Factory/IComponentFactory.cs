using BoardConstruction.Config.JSONModel;

namespace BoardConstruction.Factory;

public interface IComponentFactory
{
    public Component Create(string type);
}