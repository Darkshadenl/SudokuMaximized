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

    public FileInfo HandleImportUserInput()
    {
        FileInfo fileInfo = null;

        while (fileInfo == null)
        {
            Console.WriteLine("Please enter the path to the file you want to import (full path with extension): ");
            var filePath = @"" + Console.ReadLine();

            if (File.Exists(filePath))
            {
                fileInfo = new FileInfo(filePath);
            }
        }

        return fileInfo;
    }
}