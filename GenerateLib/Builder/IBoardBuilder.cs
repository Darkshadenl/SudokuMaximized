using GenerateLib.Boards;
using GenerateLib.Import;

namespace GenerateLib.Builder;

public interface IBoardBuilder
{
    public IBoardBuilder Reset();

    public IBoardConfig SetBoardFile(BoardFile boardFile);
    // public IBoardBuilder SetRows(int i);
    // public IBoardBuilder SetCols(int i);
    // public IBoardBuilder SetSquares(int i);
    // public IBoardBuilder SetType(string type);
    // public IBoardBuilder SetSquareLength(int i);
    public AbstractBoard Build();
}