namespace Sudoku.Model.Import;

public class ImportHandler
{
    private List<string> _validExtensions;
    private List<string> _availableImportableFiles;
    private List<string> _fileDataList; // for multi record sudoku files
    private string _filePath;

    public List<string> ValidExtensions { get => _validExtensions; set => _validExtensions = value; }
    public List<string> AvailableImportableFiles { get => _availableImportableFiles; set => _availableImportableFiles = value; }
    public List<string> FilesDataList { get => _fileDataList; set => _fileDataList = value; }
    public string FilePath { get => _filePath; set => _filePath = value; }

    public ImportHandler()
    {
        // initialize lists
        _validExtensions = new List<string>();
        _availableImportableFiles = new List<string>();
        _fileDataList = new List<string>();

        // set default sudoku file path
        _filePath = "Resources\\Sudoku-files\\";
    }
}