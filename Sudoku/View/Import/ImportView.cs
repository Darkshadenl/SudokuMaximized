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

    public FileInfo HandleImportUserInput(List<string> availableFiles, List<string> extensions)
    {
        FileInfo fileInfo = null;

        while (fileInfo == null)
        {
            PrintAvailableSudokuFiles(availableFiles);

            Console.WriteLine("\nPlease enter the sudoku file (with extension) or full path you wish to import.");

            PrintAllowedExtensions(extensions);

            var filePath = @"" + Console.ReadLine();
            if (filePath.Length == 0)
            {
                filePath = "C:\\Users\\qmb\\Documents\\Repos\\SudokuMaximized\\Sudoku\\Resources\\Sudoku-files\\puzzle2.9x9";
                // filePath = "C:\\Users\\qmb\\Documents\\Repos\\SudokuMaximized\\Sudoku\\Resources\\Sudoku-files\\puzzle2.samurai";
            }
            

            filePath.Trim();

            if (availableFiles.Contains(filePath))
            {
                fileInfo = new FileInfo("Resources\\Sudoku-files\\" + filePath);
            }
            else if (File.Exists(filePath))
            {
                fileInfo = new FileInfo(filePath);
            }
        }

        return fileInfo;
    }

    private void PrintAvailableSudokuFiles(List<string> availableFiles)
    {
        Console.WriteLine("\nAvailable sudoku files: ");

        // prints all sudoku files available
        foreach (var file in availableFiles)
        {
            // if file is not last
            if (file != availableFiles.Last())
            {
                // enters every 3 items so sudokufiles doesnt go offscreeeeeeeen
                if (availableFiles.IndexOf(file) % 3 == 2)
                {
                    Console.WriteLine(file + ", ");
                }
                else
                {
                    Console.Write(file + ", ");
                }
                continue;
            }
            // prints last file
            Console.WriteLine(file);
        }
    }

    private void PrintAllowedExtensions(List<string> extensions)
    {
        Console.Write("Allowed file extensions: ");

        foreach (var ext in extensions)
        {
            if (ext != extensions.Last())
            {
                Console.Write(ext + ", ");
                continue;
            }
            Console.WriteLine(ext + "\n");
        }
    }
}