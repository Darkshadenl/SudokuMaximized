using GenerateLib.Boards;
using GenerateLib.Builder;
using GenerateLib.Import;

namespace GenerateLib.Interpreters;

public class Interpreter : IInterpreter
{
    private readonly BoardBuildDirector _director;
    private readonly BoardBuilder _boardBuilder;

    public AbstractBoard Interpret(BoardFile boardFile)
    {
        switch (boardFile.Extension)
        {
            case ".4x4":
                _director.BoardBuilder = _boardBuilder;
                _director.Construct4X4Board(boardFile);
                return _boardBuilder.Build();
            case ".6x6":
                _director.BoardBuilder = _boardBuilder;
                _director.Construct6X6Board(boardFile);
                return _boardBuilder.Build();
            case ".9x9":
                _director.BoardBuilder = _boardBuilder;
                _director.ConstructRegularBoard(boardFile);
                return _boardBuilder.Build();
            // TODO case ".jigsaw": 
            //     break;
            // TODO case ".samurai":
            //     break;
            default:
                throw new ArgumentException("Invalid board file extension");
        }
    }

    public Interpreter(BoardBuildDirector director, BoardBuilder boardBuilder)
    {
        _director = director;
        _boardBuilder = boardBuilder;
    }
}