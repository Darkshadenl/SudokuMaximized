using GenerateLib.Boards;
using GenerateLib.Import;
using Generating.Import;

namespace GenerateLib.Interpreters;

public interface IBoardInterpreter
{
    AbstractBoard Interpret(BoardFile boardFile);
}