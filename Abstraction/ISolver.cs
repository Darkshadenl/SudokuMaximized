namespace Abstraction;

public interface ISolver
{
    public IGameController Controller { get; set; }
    public abstract List<IComponent> SolveBoards(List<IComponent> boards);
}