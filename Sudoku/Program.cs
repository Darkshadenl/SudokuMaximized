using Microsoft.Extensions.DependencyInjection;
using Sudoku;
using Sudoku.Controller;

var t = DotNetEnv.Env.Load();

var s = Startup.Init();

var main = s.GetRequiredService<MainController>();
main.Run();

