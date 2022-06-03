using GenerateLib.Boards;
using GenerateLib.Import;

namespace GenerateLib.Builder;

public class BoardBuilder : IBoardBuilder
{
    private AbstractBoard _board;
    private IBoardConfig _boardConfig;
    
    public BoardBuilder()
    {
        // initialize board here
        _board = new Board();

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