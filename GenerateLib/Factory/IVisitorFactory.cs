using GenerateLib.Config.Factory.JSONModel;
using GenerateLib.Visitors;

namespace GenerateLib.Factory;

public interface IVisitorFactory
{
    public IPrintBoardVisitor Create(string uiType);
}