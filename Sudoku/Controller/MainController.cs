using Microsoft.Extensions.DependencyInjection;

namespace Sudoku.Controller;

public class MainController
{
    private readonly GameController _gameController;
    private readonly MenuController _menuController;
    private readonly ImportController _importController;

    public MainController(GameController gameController,
        MenuController menuController, ImportController importController) => 
    (_gameController, _menuController, _importController) =
            (gameController, menuController, importController);


    public void Run()
    {
        
    }
}