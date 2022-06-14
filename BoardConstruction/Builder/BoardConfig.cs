using BoardConstruction.Boards;
using Helpers.Helpers;
using Import.Import;

namespace BoardConstruction.Builder;

public class BoardConfig : IBoardConfig
{
    private readonly BoardFile _boardFile;
    private readonly AbstractBoard _board;

    public BoardConfig(BoardFile boardFile, AbstractBoard board)
    {
        _boardFile = boardFile;
        _board = board;
    }

    public IBoardConfig SetRows(int i)
    {
        _board.Rows = i;
        return this;
    }

    public IBoardConfig SetCols(int i)
    {
        _board.Columns = i;
        return this;
    }

    public IBoardConfig SetSquares(int i)
    {
        _board.Squares = i;
        return this;
    }

    public IBoardConfig SetType(BoardTypes type)
    {
        _board.Type = type;
        return this;
    }

    public IBoardConfig SetSquareLength(int i)
    {
        _board.SquareLength = i;
        return this;
    }
    
    public IBoardConfig SetCursorPosition(int x, int y)
    {
        if (x < 0 || y < 0)
            throw new ArgumentException("Cursor position cannot be negative");

        if (_board.Rows == null || y > _board.Rows)
            throw new ArgumentException("First setup rows");
        
        if (_board.Columns == null || x > _board.Columns)
            throw new ArgumentException("First setup columns");
        
        _board.StartCursorX = x;
        _board.StartCursorY = y;
        return this;
    }
    public AbstractBoard Build()
    {
        return _board.CreateBoardBuild(_boardFile);
    }

}