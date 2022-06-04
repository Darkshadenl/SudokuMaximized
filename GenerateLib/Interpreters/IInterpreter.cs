using GenerateLib.Boards;
using GenerateLib.Import;

namespace GenerateLib.Interpreters;

public interface IInterpreter
{
    public List<AbstractBoard> Interpret(List<BoardFile> boardFileList);
}