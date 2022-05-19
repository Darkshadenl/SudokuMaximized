using GenerateLib.Boards;
using GenerateLib.Builder;
using GenerateLib.Import;
using GenerateLib.SolveAlgo;
using Generating.Import;

namespace GenerateLib.Interpreters;

public class JigsawInterpreter
{
    // public AbstractBoard Interpret(BoardFile boardFile)
    // {
    //     Board board = new Board();
    //     int[][] b = new int[9][];
    //     int rowNumber = -1;
    //     
    //     for (int i = 0; i < boardFile.Data.Length; i++)
    //     {
    //         if (i % 9 == 0)
    //         {
    //             rowNumber++;
    //             b[rowNumber] = new int[9];
    //         }
    //         char c = boardFile.Data[i];
    //         b[rowNumber][i%9] = int.Parse(c.ToString()); 
    //     }
    //
    //     return board.CreateBoardBuild(b);
    // }
    //
    // public void Setup(BoardBuildDirector director, BoardBuilder boardBuilder)
    // {
    //     throw new NotImplementedException();
    // }
}