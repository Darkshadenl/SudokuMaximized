using Sudoku.Model.Components;
using Sudoku.Model.Viewable;

namespace Sudoku.Model;

public abstract class AbstractBoard
{
    protected Component _sudokuBoard;

    public List<IViewable> GetPrintData()
    {
        var printData = new List<IViewable>();
        var data = _sudokuBoard.GetAllData();
        foreach (var componentList in data)
        {
            foreach (var component in componentList)
            {
                printData.Add(new Viewable.Viewable(component));
            }
        }
        return printData;
    }
}