using GenerateLib.Boards;
using GenerateLib.Builder;
using GenerateLib.Import;
using GenerateLib.SolveAlgo;
using Generating.Import;

namespace GenerateLib.Interpreters;

public class RegularInterpreter : IBoardInterpreter
{
    private BoardBuildDirector _director;
    private BoardBuilder _boardBuilder;
    
    public void Setup(BoardBuildDirector director, BoardBuilder boardBuilder)
    {
        _director = director;
        _boardBuilder = boardBuilder;
    }

    public AbstractBoard Interpret(BoardFile boardFile)
    {
        _director.BoardBuilder = _boardBuilder;
        _director.ConstructRegularBoard(boardFile);
        var regularBoard = _boardBuilder.Build();
        
        // return a board.
        return regularBoard;
    }
}