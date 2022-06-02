using GenerateLib.Boards;
using GenerateLib.Factory;
using GenerateLib.Import;
using GenerateLib.Interpreters;
using Generating.Import;
using Sudoku.Model;
using Sudoku.Model.Import;
using Sudoku.View.Import;

namespace Sudoku.Controller;

public class ImportController
{
    private readonly ImportView _view;
    private readonly ImportHandler _importHandler;
    private readonly IInterpreter _interpreter;

    public ImportController(ImportView view, ImportHandler importHandler, IInterpreter interpreter)
    {
        _view = view;
        _importHandler = importHandler;
        _interpreter = interpreter;
        _view.SetController(this);
    }

    public AbstractBoard RunImport()
    {
        BoardFile import = StartImport();

        while (import == null)
        {
            import = StartImport();
        }
        return Interpret(import);
    }

    private AbstractBoard Interpret(BoardFile boardFile)
    {
        return _interpreter.Interpret(boardFile);
    }

    private BoardFile StartImport()
    {
        _view.ShowWelcome();

        // import file from user input
        var fileInfo = _view.HandleImportUserInput(_importHandler.GetAvailableImportableFiles(), _importHandler.GetValidExtensions());
        
        BoardFile boardFile;
        try
        {
            boardFile = _importHandler.ImportFromPath(fileInfo);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }

        return boardFile;
    }
}