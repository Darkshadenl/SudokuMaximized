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
        var boardList = _importController.RunImport();
        var startNewGame = _gameController.RunGame(boardList);

        while (startNewGame)
        {
            boardList = _importController.RunImport();
            startNewGame = _gameController.RunGame(boardList);
        }

        Environment.Exit(0);
    }
}