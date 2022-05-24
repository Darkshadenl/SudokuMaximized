using System.Diagnostics;
using System.Text;
using GenerateLib.Import;
using Generating.Import;

namespace Sudoku.Model.Import;

public class ImportHandler
{
    private string[] _validExtensions = // TODO config file
    {
        ".4x4",
        ".6x6",
        ".9x9",
        ".jigsaw",
        ".samurai"
    };

    public BoardFile ImportFromPath(FileInfo fileInfo)
    {
        Debug.Assert(fileInfo.DirectoryName != null, "fileInfo.DirectoryName != null");
        string data = File.ReadAllText(fileInfo.FullName);
        string extension = fileInfo.Extension;

        if (ValidExtension(extension))
        {
            return new BoardFile(data, extension);
        }

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

    private bool ValidExtension(string extension)
    {
        return _validExtensions.Contains(extension);
    }
}