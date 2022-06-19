namespace Abstraction;

public interface ISolver
{
    public IGameController Controller { get; set; }
    public List<IComponent> SolveBoards(List<IComponent> boards);

    public IComponent SolveBoard(IComponent board);
}