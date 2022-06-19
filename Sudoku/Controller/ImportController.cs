using Newtonsoft.Json;
using Sudoku.Model.Import;
using Sudoku.View.Import;
using System.Diagnostics;
using System.Text;
using Construction.Boards;
using Construction.Interpreters;
using Import.Config.JSONModel;
using Import.Import;

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

        // run funcs to initialize/fill lists
        FillValidExtensionsList();
        FillAvailableFilesList();
    }

    public MainController Controller { get; set; }

    public BoardFile RunImport()
    {
        BoardFile import = StartImport();

        while (import == null)
            import = StartImport();

        return import;
    }

    public AbstractBoard Interpret(BoardFile boardFile)
    {
        return _interpreter.Interpret(boardFile);
    }

    private BoardFile StartImport()
    {
        _view.ShowWelcome();

        // import file from user input
        var fileInfo = _view.HandleImportUserInput(_importHandler.AvailableImportableFiles,
            _importHandler.ValidExtensions);

        BoardFile boardFile;
        try
        {
            boardFile = ImportFromPath(fileInfo);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }

        return boardFile;
    }

    private void FillValidExtensionsList()
    {
        try
        {
            var json = File.ReadAllText(Environment.GetEnvironmentVariable("IMPORTVALIDEXTENSIONCONFIG") ??
            "./Config/ImportHandler/ImportValidFileExtensionConfiguration.json");

            var res = JsonConvert.DeserializeObject<ImportJSONModel>(json);

            // clear list before inserting new extensions
            _importHandler.ValidExtensions.Clear();

            for (int i = 0; i < res.validExtensions.Count(); i++)
            {
                _importHandler.ValidExtensions.Add(res.validExtensions[i].extension);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Valid file extension list could not be loaded");
            Console.WriteLine(e);
            return;
        }
    }

    private void FillAvailableFilesList()
    {
        try
        {
            // checks if path exists + dir has any files
            if (Directory.Exists(_importHandler.FilePath) && Directory.GetFiles(_importHandler.FilePath).Any())
            {
                // puts allowed files (with valid ext) in arr
                var allowedFiles = Directory.GetFiles(_importHandler.FilePath).
                    Where(file => IsValidExtension(Path.GetExtension(file))).ToArray();

                // clear list before inserting new data
                _importHandler.AvailableImportableFiles.Clear();

                for (int i = 0; i < allowedFiles.Count(); i++)
                {
                    // fills list with FILE NAME (not whole path to file)
                    _importHandler.AvailableImportableFiles.Add(Path.GetFileName(allowedFiles[i]));
                }
            }
            else
            {
                // no found folder and files
                _importHandler.AvailableImportableFiles.Add("No available sudoku files found!");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Sudoku file folder can't be found.");
            Console.WriteLine(e);
        }
    }

    private BoardFile ImportFromPath(FileInfo fileInfo)
    {
        Debug.Assert(fileInfo.DirectoryName != null, "fileInfo.DirectoryName != null");

        string[] data = File.ReadAllText(fileInfo.FullName)
            .Split(new [] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        string extension = fileInfo.Extension;

        if (IsValidExtension(extension))
            return new BoardFile(data, extension);
        
        // if it all goes wrong 
        var sb = new StringBuilder("Wrong file. Accepted extensions are");

        foreach (var ext in _importHandler.ValidExtensions)
        {
            if (ext == _importHandler.ValidExtensions.Last())
            {
                sb.Append($" {ext}.");
                continue;
            }
            sb.Append($" {ext},");
        }

        throw new InvalidDataException(sb.ToString());
    }

    public bool IsValidExtension(string extension)
    {
        return _importHandler.ValidExtensions.Contains(extension);
    }
}