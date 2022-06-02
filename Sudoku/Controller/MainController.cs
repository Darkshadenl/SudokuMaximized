using Microsoft.Extensions.DependencyInjection;

namespace Sudoku.Controller;

public class MainController
{
    public GameController GameController { get; }
    public MenuController MenuController { get; }
    public ImportController ImportController { get; }

    public MainController(GameController gameController,
        MenuController menuController, ImportController importController)
    {
        (GameController, MenuController, ImportController) =
            (gameController, menuController, importController);
        GameController.Controller = this;
        MenuController.Controller = this;
        ImportController.Controller = this;
    } 
    


    public void Run()
    {
        var board = ImportController.RunImport();
        GameController.RunGame(board);
    }
}