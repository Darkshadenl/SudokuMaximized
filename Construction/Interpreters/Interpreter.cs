using Construction.Boards;
using Construction.Builder;
using Import.Import;

namespace Construction.Interpreters;

public class Interpreter : IInterpreter
{
    private readonly BoardBuildDirector _director;
    private readonly BoardBuilder _boardBuilder;

    public Interpreter(BoardBuildDirector director, BoardBuilder boardBuilder)
    {
        _director = director;
        _boardBuilder = boardBuilder;
    }

    public AbstractBoard Interpret(BoardFile b)
    {
        
        switch (b.Extension)
        {
            case ".4x4":
                ValidDataSize(4, b.Data);
                _director.BoardBuilder = _boardBuilder;
                _director.Construct4X4Board(b);
                return _boardBuilder.Build();
            case ".6x6":
                ValidDataSize(6, b.Data);
                _director.BoardBuilder = _boardBuilder;
                _director.Construct6X6Board(b);
                return _boardBuilder.Build();
            case ".9x9":
                ValidDataSize(9, b.Data);
                _director.BoardBuilder = _boardBuilder;
                _director.ConstructRegularBoard(b);
                return _boardBuilder.Build();
            case ".samurai":
                ValidDataSize(9, b.Data);
                _director.BoardBuilder = _boardBuilder;
                _director.ConstructSamuraiBoard(b);
                return _boardBuilder.Build();
            default:
                throw new ArgumentException("Invalid board file extension");
        }
    }

    private void ValidDataSize(int length, string[] data)
    {
        
        if (data == null || data[0].Length < length || data[0] == null )
        {
            throw new ArgumentException("No data in supplied file");
        }
    }
}