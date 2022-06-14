using Microsoft.Extensions.DependencyInjection;

namespace Sudoku.Controller;

public class MainController
{
    private readonly GameController _gameController;
    private readonly ImportController _importController;

    public MainController(GameController gameController,
        ImportController importController)
    {
        (_gameController, _importController) =
            (gameController, importController);

        _gameController.Controller = this;
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