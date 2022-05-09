using Sudoku.View.Menu;

namespace Sudoku.Controller;

public class MenuController
{
    private MenuView _menuView;
    
    public MenuController()
    {
        // _menuView = menuView;
        Console.WriteLine($"Menuview injected: ${_menuView}");
    }
}