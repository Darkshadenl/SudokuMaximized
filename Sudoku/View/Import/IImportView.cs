namespace Sudoku.View.Import;

public interface IImportView
{
    public void ShowWelcome();
    public FileInfo HandleImportUserInput(List<string> availableFiles, List<string> extensions);

}