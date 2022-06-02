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

    public FileInfo HandleImportUserInput(string[] availableFiles, string[] extensions)
    {
        FileInfo fileInfo = null;

        while (fileInfo == null)
        {
            // prints available sudoku files
            PrintAvailableSudokuFiles(availableFiles);

            Console.WriteLine("\nPlease enter the sudoku file (with extension) or full path you wish to import.");

            // prints allowed extensions
            PrintAllowedExtensions(extensions);

            var filePath = @"" + Console.ReadLine();

            // removal of any whitespaces 
            filePath.Trim();

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

    private void PrintAvailableSudokuFiles(string[] availableFiles)
    {
        Console.WriteLine("\nAvailable sudoku files: ");

        // prints all sudoku files available
        for (int i = 0; i < availableFiles.Count(); i++)
        {
            if (i != availableFiles.Count() - 1)
            {
                // enters every 3 items so sudokufiles doesnt go offscreeeeeeeen
                if (i % 3 == 2)
                {
                    Console.WriteLine(availableFiles[i] + ", ");
                }
                else
                {
                    Console.Write(availableFiles[i] + ", ");
                }
            }
            else
            {
                Console.Write(availableFiles[i] + "\n");
            }
        }
    }

    private void PrintAllowedExtensions(string[] extensions)
    {
        Console.Write("Allowed file extensions: ");
        for (int x = 0; x < extensions.Count(); x++)
        {
            if (x != extensions.Count() - 1)
            {
                Console.Write(extensions[x] + ", ");
            }
            else
            {
                Console.Write(extensions[x] + "\n\n");
            }
        }
    }
}