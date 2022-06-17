using Construction.Boards;
using Import.Import;

namespace Construction.Builder;

public interface IBoardBuilder
{
    public IBoardBuilder Reset();

    public IBoardConfig SetBoardFile(BoardFile boardFile);

    public AbstractBoard Build();
}