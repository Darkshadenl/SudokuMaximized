using Sudoku.View.Menu;

namespace Sudoku.Controller;

public class MenuController
{
    private MenuView _menuView;
    
    public MenuController(MenuView menuView)
    {
        _menuView = menuView;
    }
}