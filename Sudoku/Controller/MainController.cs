using Microsoft.Extensions.DependencyInjection;

namespace Sudoku.Controller;

public class MainController
{
    private GameController _gameController;
    private MenuController _menuController;
    private ImportController _importController;

    public GameController GC { get => _gameController; }
    public MenuController MC { get => _menuController; }
    public ImportController IC { get => _importController; }

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
        var startNewGame = _gameController.RunGame(board.First());

        while (startNewGame)
        {
            board = _importController.RunImport();
            startNewGame = _gameController.RunGame(board.First());
        }

        Environment.Exit(0);
    }
}