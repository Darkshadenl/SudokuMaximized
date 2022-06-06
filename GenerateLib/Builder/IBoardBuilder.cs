using GenerateLib.Boards;
using GenerateLib.Import;

namespace GenerateLib.Builder;

public interface IBoardBuilder
{
    public IBoardBuilder Reset();

    public IBoardConfig SetBoardFile(BoardFile boardFile);

    public AbstractBoard Build();
}