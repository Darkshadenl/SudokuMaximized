namespace Sudoku.Import;

public class Import
{
    public BoardFile ImportFromPath(string path)
    {
        string data = File.ReadAllText(path);
        string extension = Path.GetExtension(path);
        return new BoardFile(data, extension);
    }
}