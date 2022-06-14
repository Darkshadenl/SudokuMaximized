using BoardConstruction.Boards;
using Import.Import;

namespace BoardConstruction.Interpreters;

public interface IInterpreter
{
    public AbstractBoard Interpret(BoardFile boardFile);
}