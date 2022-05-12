using GenerateLib.Components;
using GenerateLib.SolveAlgo;
using GenerateLib.Viewable;

namespace GenerateLib.Boards;

public abstract class AbstractBoard
{
    protected Component SudokuBoard;
    public string Type { get; set; }
    public ISolver Solver { get; set; }

    public List<IViewable> GetPrintData()
    {
        var printData = new List<IViewable>();
        var data = SudokuBoard.GetAllData();
        foreach (var componentList in data)
        {
            foreach (var component in componentList)
            {
                printData.Add(new Viewable.Viewable(component));
            }
        }
        return printData;
    }

    public List<IViewable> GetViewables()
    {
        var data = SudokuBoard.GetAllData();
        List<IViewable> printData = new List<IViewable>();
        
        foreach (List<Component> list in data)
        {
            printData.AddRange(list.Select(component => component as IViewable));
        }

        return printData;
    }
}