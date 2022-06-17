using Construction.Boards;
using Import.Import;

namespace Construction.Builder;

public class BoardBuilder : IBoardBuilder
{
    private AbstractBoard _board;
    private IBoardConfig _boardConfig;
    
    public BoardBuilder()
    {
        Reset();
    }

    public IBoardBuilder Reset()
    {
        _board = new Board();
        return this;
    }
    
    public IBoardConfig SetBoardFile(BoardFile boardFile)
    {
        _boardConfig = new BoardConfig(boardFile, _board);
        return _boardConfig;
    }

    public AbstractBoard Build()
    {
        return _boardConfig.Build();
    }
    
}