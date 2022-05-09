using Sudoku.Import;
using Sudoku.Model;

namespace Sudoku.Interpreters;

public interface IBoardInterpreter
{
    AbstractBoard Interpret(BoardFile boardFile);
}