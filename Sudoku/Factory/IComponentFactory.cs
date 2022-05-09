using Sudoku.Interpreters;
using Sudoku.Model.Components;

namespace Sudoku.Factory;

public interface IComponentFactory
{
    public Component Create(string type);
}