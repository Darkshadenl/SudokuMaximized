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
        var boardFile = _importController.RunImport();
        var board = _importController.Interpret(boardFile);
        var startNewGame = _gameController.RunGame(board);

        while (startNewGame)
        {
            boardFile = _importController.RunImport();
            board = _importController.Interpret(boardFile);
            startNewGame = _gameController.RunGame(board);
        }

        Environment.Exit(0);
    }
}