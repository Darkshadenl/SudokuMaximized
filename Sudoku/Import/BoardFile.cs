namespace Sudoku.Import;

public class BoardFile : IBoardFile
{
    public string Data { get; }
    public string Extension { get; }

    public BoardFile(string data, string extension)
    {
        Data = data;
        Extension = extension;
    }
}