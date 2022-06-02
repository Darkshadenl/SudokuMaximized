using Sudoku.Controller;

namespace Sudoku.View.Import;

public class ImportView : IImportView
{
    private ImportController _importController;
    
    public void SetController(ImportController controller)
    {
        _importController = controller;
    }

    public void ShowWelcome()
    {
        Console.Clear();
        Console.WriteLine("Welcome to the Sudoku importer!");
    }

    public FileInfo HandleImportUserInput(string[] availableFiles)
    {
        FileInfo fileInfo = null;

        while (fileInfo == null)
        {
            Console.WriteLine("\nAvailable sudoku files: ");
            // prints all files avaiable
            for (int i = 0; i < availableFiles.Count(); i++)
            {
                Console.Write(availableFiles[i] + " ");
            }

            Console.WriteLine("\n\nPlease enter the sudoku file (with extension) or full path you wish to import.");
            Console.WriteLine("Accepted file extensions: .4x4, .6.6, .9.9, .jigsaw, .samurai");

            Console.Write("User input: ");
            var filePath = @"" + Console.ReadLine();
            filePath = "C:\\Users\\qmb\\Documents\\Repos\\SudokuMaximized\\Sudoku\\Resources\\Sudoku-files\\puzzle.9x9";

            // if chosen file is from the available list then ...
            if (availableFiles.Contains(filePath))
            {
                fileInfo = new FileInfo("Resources\\Sudoku-files\\" + filePath);
            }
            // if the file path is absolute then this!
            else if (File.Exists(filePath))
            {
                fileInfo = new FileInfo(filePath);
            }
        }

        return fileInfo;
    }
}