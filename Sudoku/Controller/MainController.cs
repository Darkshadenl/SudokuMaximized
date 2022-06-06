using Microsoft.Extensions.DependencyInjection;

namespace Sudoku.Controller;

public class MainController
{
    private readonly GameController _gameController;
    private readonly MenuController _menuController;
    private readonly ImportController _importController;

    public MainController(GameController gameController,
        MenuController menuController, ImportController importController)
    {
        (_gameController, _menuController, _importController) =
            (gameController, menuController, importController);

        _gameController.Controller = this;
        _menuController.Controller = this;
        _importController.Controller = this;
    }

    public void Run()
    {
        var board = _importController.RunImport();
        var startNewGame = _gameController.RunGame(board);

        while (startNewGame)
        {
            board = _importController.RunImport();
            startNewGame = _gameController.RunGame(board);
        }

        Environment.Exit(0);
    }
}