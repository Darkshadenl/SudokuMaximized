using GenerateLib.Boards;
using GenerateLib.Builder;
using GenerateLib.Import;

namespace GenerateLib.Interpreters;

public class Interpreter : IInterpreter
{
    private readonly BoardBuildDirector _director;
    private readonly BoardBuilder _boardBuilder;
    public Interpreter(BoardBuildDirector director, BoardBuilder boardBuilder)
    {
        _director = director;
        _boardBuilder = boardBuilder;
    }

    public List<AbstractBoard> Interpret(List<BoardFile> boardFileList)
    {
        var templist = new List<AbstractBoard>();

        foreach(var boardFile in boardFileList)
        {
            switch (boardFile.Extension)
            {
                case ".4x4":
                    _director.BoardBuilder = _boardBuilder;
                    _director.Construct4X4Board(boardFile);
                    templist.Add(_boardBuilder.Build());
                    return templist;
                case ".6x6":
                    _director.BoardBuilder = _boardBuilder;
                    _director.Construct6X6Board(boardFile);
                    templist.Add(_boardBuilder.Build());
                    return templist;
                case ".9x9":
                    _director.BoardBuilder = _boardBuilder;
                    _director.ConstructRegularBoard(boardFile);
                    templist.Add(_boardBuilder.Build());
                    return templist;
                // TODO case ".jigsaw": 
                //     break;
                case ".samurai":
                    _director.BoardBuilder = _boardBuilder;
                    _director.ConstructSamuraiBoard(boardFile);
                    templist.Add(_boardBuilder.Build());
                    if(boardFile == boardFileList.Last())
                        return templist;
                    continue;
                default:
                    throw new ArgumentException("Invalid board file extension");
            }
        }

        return templist;
    }
}