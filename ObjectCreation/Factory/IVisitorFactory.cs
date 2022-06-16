using ObjectCreation.Visitors;

namespace Factories.Factory;

public interface IVisitorFactory
{
    public IPrintBoardVisitor Create(string uiType);
}