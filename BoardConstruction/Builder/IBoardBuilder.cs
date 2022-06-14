using BoardConstruction.Boards;
using Import.Import;

namespace BoardConstruction.Builder;

public interface IBoardBuilder
{
    public IBoardBuilder Reset();

    public IBoardConfig SetBoardFile(BoardFile boardFile);

    public AbstractBoard Build();
}