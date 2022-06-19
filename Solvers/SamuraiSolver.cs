using Abstraction;
using IComponent = Abstraction.IComponent;

namespace Solvers;

public class SamuraiSolver : AbstractSolver
{
    public List<IComponent> SudokuBoards { get; set; }
    
    public override List<IComponent> SolveBoards(List<IComponent> sudokuBoards)
    {
        SudokuBoards = sudokuBoards;
        var solver = new BackTrackingAlgo();
        
        var board3 = SudokuBoards[2];
        Controller.CurrentBoardIndex = 2;
        solver.SolveBoard(board3);
        
        var board1 = SudokuBoards[0];
        Controller.CurrentBoardIndex = 0;
        solver.SolveBoard(board1);
        
        var board2 = SudokuBoards[3];
        Controller.CurrentBoardIndex = 3;
        solver.SolveBoard(board2);


        var board4 = SudokuBoards[1];
        Controller.CurrentBoardIndex = 1;
        solver.SolveBoard(board4);
        
        var board5 = SudokuBoards[4];
        Controller.CurrentBoardIndex = 4;
        solver.SolveBoard(board5);

        

        return sudokuBoards;
    }

}