using Sudoku.Import;
using Sudoku.Model;

namespace Sudoku.Interpreters;

public class JigsawInterpreter : IBoardInterpreter
{
    public AbstractBoard Interpret(BoardFile boardFile)
    {
        Solver s = new Solver();
        RegularBoard regularBoard = new RegularBoard(s);
        int[][] board = new int[9][];
        int rowNumber = -1;
        
        for (int i = 0; i < boardFile.Data.Length; i++)
        {
            if (i % 9 == 0)
            {
                rowNumber++;
                board[rowNumber] = new int[9];
            }
            char c = boardFile.Data[i];
            board[rowNumber][i%9] = int.Parse(c.ToString()); 
        }

        return regularBoard.CreateBoard(board);
    }
}