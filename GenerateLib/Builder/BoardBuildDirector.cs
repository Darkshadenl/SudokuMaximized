using GenerateLib.Helpers;
using GenerateLib.Import;

namespace GenerateLib.Builder;

public class BoardBuildDirector
{
    public IBoardBuilder BoardBuilder { get; set; }

    public void ConstructRegularBoard(BoardFile boardFile)
    {
        BoardBuilder
            .Reset()
            .SetBoardFile(boardFile)
            .SetCols(9)
            .SetRows(9)
            .SetSquares(9)
            .SetSquareLength(3)
            .SetCursorPosition(0, 0)
            .SetType(BoardTypes.nine);
    }

    public void Construct6X6Board(BoardFile boardFile)
    {
        BoardBuilder
            .Reset()
            .SetBoardFile(boardFile)
            .SetCols(6)
            .SetRows(6)
            .SetSquares(6)
            .SetSquareLength(3)
            .SetCursorPosition(0, 0)
            .SetType(BoardTypes.six);
    }

    public void Construct4X4Board(BoardFile boardFile)
    {
        BoardBuilder
            .Reset()
            .SetBoardFile(boardFile)
            .SetCols(4)
            .SetRows(4)
            .SetSquares(4)
            .SetSquareLength(2)
            .SetCursorPosition(0, 0)
            .SetType(BoardTypes.four);
    }


    public void ConstructJigsawBoard()
    {

    }

    public void ConstructSamuraiBoard(BoardFile boardFile)
    {
        BoardBuilder
            .Reset()
            .SetBoardFile(boardFile)
            .SetCols(9)
            .SetRows(9)
            .SetSquares(9)
            .SetSquareLength(3)
            .SetCursorPosition(0, 0)
            .SetType(BoardTypes.samurai);
    }
}