using Sudoku.Interpreters;

namespace Sudoku.Factory;

public interface IBoardInterpreterFactory
{
    public IBoardInterpreter Create(string interpreter);
}