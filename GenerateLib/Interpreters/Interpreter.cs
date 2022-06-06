﻿using GenerateLib.Boards;
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

    public AbstractBoard Interpret(BoardFile b)
    {
        switch (b.Extension)
        {
            case ".4x4":
                _director.BoardBuilder = _boardBuilder;
                _director.Construct4X4Board(b);
                return _boardBuilder.Build();
            case ".6x6":
                _director.BoardBuilder = _boardBuilder;
                _director.Construct6X6Board(b);
                return _boardBuilder.Build();
            case ".9x9":
                _director.BoardBuilder = _boardBuilder;
                _director.ConstructRegularBoard(b);
                return _boardBuilder.Build();
            // TODO case ".jigsaw": 
            //     break;
            case ".samurai":
                _director.BoardBuilder = _boardBuilder;
                _director.ConstructSamuraiBoard(b);
                return _boardBuilder.Build();
            default:
                throw new ArgumentException("Invalid board file extension");
        }
    }
}