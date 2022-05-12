using GenerateLib.Boards;
using GenerateLib.Factory;
using GenerateLib.Import;
using Generating.Import;
using Sudoku.Model;
using Sudoku.Model.Import;
using Sudoku.View.Import;

namespace Sudoku.Controller;

public class ImportController
{
    private readonly ImportView _view;
    private readonly ImportHandler _importHandler;
    private readonly IBoardInterpreterFactory _boardInterpreterFactory;

    public ImportController(ImportView view, ImportHandler importHandler, 
        IBoardInterpreterFactory boardInterpreterFactory)
    {
        _view = view;
        _importHandler = importHandler;
        _boardInterpreterFactory = boardInterpreterFactory;
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
        var factory = _boardInterpreterFactory.Create(boardFile.Extension);
        return factory.Interpret(boardFile);
    }

    private BoardFile StartImport()
    {
        var regular = "C:\\Users\\qmb\\Documents\\Repos\\SudokuMaximized\\Sudoku\\Resources\\Sudoku-files\\puzzle.9x9";
        var jigsaw = "C:\\Users\\qmb\\Documents\\Repos\\SudokuMaximized\\Sudoku\\Resources\\Sudoku-files\\puzzle.jigsaw";
        var samurai = "C:\\Users\\qmb\\Documents\\Repos\\SudokuMaximized\\Sudoku\\Resources\\Sudoku-files\\puzzle.samurai";
        
        
        _view.ShowWelcome();
        // var fileInfo = _view.HandleImportUserInput();   // TODO uncomment
        var fileInfo =
            new FileInfo(jigsaw);
        
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