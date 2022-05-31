using GenerateLib.Config.Factory.JSONModel;

namespace GenerateLib.Factory;

public interface IComponentFactory
{
    public Component Create(string type);
}