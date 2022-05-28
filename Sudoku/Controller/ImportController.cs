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
    // private readonly IBoardInterpreterFactory _boardInterpreterFactory;
    private readonly IInterpreter _interpreter;

    public ImportController(ImportView view, ImportHandler importHandler, IInterpreter interpreter)
    {
        _view = view;
        _importHandler = importHandler;
        // _boardInterpreterFactory = boardInterpreterFactory;
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
        // var factory = _boardInterpreterFactory.Create(boardFile.Extension);
        return _interpreter.Interpret(boardFile);
    }

    private BoardFile StartImport()
    {
        // THIS IS ONLY FOR TEMP SOLUTION ....
        var regular = @"..\\..\\..\\Resources\\Sudoku-files\\puzzle.9x9";
        var jigsaw = @"..\\..\\..\\Resources\\Sudoku-files\\puzzle.jigsaw";
        var samurai = @"..\\..\\..\\Resources\\Sudoku-files\\puzzle.samurai";
        var four = @"..\\..\\..\\Resources\\Sudoku-files\\puzzle.4x4";
        var six = @"..\\..\\..\\Resources\\Sudoku-files\\puzzle2.6x6";

        _view.ShowWelcome();
        // var fileInfo = _view.HandleImportUserInput();   // TODO uncomment
        var fileInfo =
            new FileInfo(regular);
        
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