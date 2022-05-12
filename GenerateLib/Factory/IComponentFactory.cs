using GenerateLib.Factory.Config.JSONModel;

namespace GenerateLib.Factory;

public interface IComponentFactory
{
    public Component Create(string type);
}