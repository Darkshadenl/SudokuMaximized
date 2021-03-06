using Construction.Boards;
using Helpers.Enums;

namespace Construction.Builder;

public interface IBoardConfig
{
    public IBoardConfig SetRows(int i);
    public IBoardConfig SetCols(int i);
    public IBoardConfig SetSquares(int i);
    public IBoardConfig SetType(BoardTypes type);
    public IBoardConfig SetSquareLength(int i);
    public AbstractBoard Build();
    public IBoardConfig SetCursorPosition(int x, int y);
}