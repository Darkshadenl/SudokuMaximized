using Microsoft.Extensions.DependencyInjection;
using Sudoku;
using Sudoku.Controller;

var s = Startup.Init();

var main = s.GetRequiredService<MainController>();
main.Run();

