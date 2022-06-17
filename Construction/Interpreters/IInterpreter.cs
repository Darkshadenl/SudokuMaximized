using Construction.Boards;
using Import.Import;

namespace Construction.Interpreters;

public interface IInterpreter
{
    public AbstractBoard Interpret(BoardFile boardFile);
}