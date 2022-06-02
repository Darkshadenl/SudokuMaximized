using System.Diagnostics;
using System.Text;
using GenerateLib.Config.ImportHandler.JSONModel;
using GenerateLib.Import;
using Newtonsoft.Json;

namespace Sudoku.Model.Import;

public class ImportHandler
{
    private string[] _validExtensions;
    private string[] _availableImportableFiles;
    private string filePath;

    public ImportHandler()
    {
        filePath = "Resources\\Sudoku-files\\"; // its hardcoded filepath is that ok?
        FillValidExtensionsList();
        FillAvailableFilesList();
    }

    private void FillValidExtensionsList()
    {
        try
        {
            var json = File.ReadAllText(Environment.GetEnvironmentVariable("IMPORTVALIDEXTENSIONCONFIG") ??
            "./Config/ImportHandler/ImportValidFileExtensionConfiguration.json");

            var res = JsonConvert.DeserializeObject<ImportJSONModel>(json);

            _validExtensions = new string[res.validExtensions.Count()];

            for(int i = 0; i < res.validExtensions.Count(); i++)
            {
                _validExtensions[i] = res.validExtensions[i].extension;
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
            if (Directory.Exists(filePath) && Directory.GetFiles(filePath).Any())
            {
                // puts allowed files (with valid ext) in arr
                var allowedFiles = Directory.GetFiles(filePath).Where(file => ValidExtension(Path.GetExtension(file))).ToArray();

                // initializes arr with amount of files that are allowed
                _availableImportableFiles = new string[allowedFiles.Count()];

                for (int i = 0; i < allowedFiles.Count(); i++)
                {
                    _availableImportableFiles[i] = Path.GetFileName(allowedFiles[i]);
                }
            }
            else
            {
                // if folder exists but has no files
                _availableImportableFiles = new string[1];
                _availableImportableFiles[0] = "No available sudoku files found!";
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Sudoku file folder can't be found.");
            Console.WriteLine(e);
            return;
        }
    }


    public BoardFile ImportFromPath(FileInfo fileInfo)
    {
        Debug.Assert(fileInfo.DirectoryName != null, "fileInfo.DirectoryName != null");
        string data = File.ReadAllText(fileInfo.FullName);
        string extension = fileInfo.Extension;

        if (ValidExtension(extension))
        {
            return new BoardFile(data, extension);
        }

        return new BoardFile(data, extension);

        var sb = new StringBuilder("Wrong file. Accepted extensions are");

        for (int i = 0; i < _validExtensions.Length; i++)
        {
            if (i == _validExtensions.Length - 1)
            {
                sb.Append($" {_validExtensions[i]}.");
                continue;
            }
            sb.Append($" {_validExtensions[i]},");
        }

        throw new InvalidDataException(sb.ToString());
    }

    public bool ValidExtension(string extension)
    {
        return _validExtensions.Contains(extension);
    }

    public string[] GetAvailableImportableFiles()
    {
        return _availableImportableFiles;
    }
}