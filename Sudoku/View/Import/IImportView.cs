namespace Sudoku.View.Import;

public interface IImportView
{
    public void ShowWelcome();
    public FileInfo HandleImportUserInput(string[] availableFiles, string[] extensions);

}