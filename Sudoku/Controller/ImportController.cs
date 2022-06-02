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
    // wat wil je hiermee?
    // private readonly IBoardInterpreterFactory _boardInterpreterFactory;
    private readonly IInterpreter _interpreter;

    public ImportController(ImportView view, ImportHandler importHandler, IInterpreter interpreter)
    {
        _view = view;
        _importHandler = importHandler;
        // wat wil je hiermee?
        // _boardInterpreterFactory = boardInterpreterFactory;
        _interpreter = interpreter;
        _view.SetController(this);
    }

    public MainController Controller { get; set; }

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
        // wat wil je hiermee?
        // var factory = _boardInterpreterFactory.Create(boardFile.Extension); 
        return _interpreter.Interpret(boardFile);
    }

    private BoardFile StartImport()
    {
        _view.ShowWelcome();

        // import file from user input
        var fileInfo = _view.HandleImportUserInput(_importHandler.GetAvailableImportableFiles());
        
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