using Construction.Visitors;

namespace Construction.Factory;

public interface IVisitorFactory
{
    public IPrintBoardVisitor Create(string uiType);
}