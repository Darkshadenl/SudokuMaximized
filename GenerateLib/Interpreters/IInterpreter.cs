using GenerateLib.Boards;
using GenerateLib.Import;

namespace GenerateLib.Interpreters;

public interface IInterpreter
{
    public AbstractBoard Interpret(BoardFile boardFile);

}