using GenerateLib.Interpreters;

namespace GenerateLib.Factory;

public interface IBoardInterpreterFactory
{
    public IBoardInterpreter Create(string interpreter);
}