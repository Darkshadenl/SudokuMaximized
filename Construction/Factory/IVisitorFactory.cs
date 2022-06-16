using BoardConstruction.Visitors;

namespace BoardConstruction.Factory;

public interface IVisitorFactory
{
    public IPrintBoardVisitor Create(string uiType);
}