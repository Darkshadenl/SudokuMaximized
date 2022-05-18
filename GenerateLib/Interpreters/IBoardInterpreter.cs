using GenerateLib.Boards;
using GenerateLib.Builder;
using GenerateLib.Import;
using Generating.Import;

namespace GenerateLib.Interpreters;

public interface IBoardInterpreter
{
    public AbstractBoard Interpret(BoardFile boardFile);
    public void Setup(BoardBuildDirector director, BoardBuilder boardBuilder);
}