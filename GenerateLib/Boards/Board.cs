﻿using GenerateLib.Components;
using GenerateLib.Import;

namespace GenerateLib.Boards;

public class Board : AbstractBoard
{
    public Board()
    {
        SudokuBoard = new SudokuBoard();
    }

    public override AbstractBoard CreateBoardBuild(BoardFile boardFile)
    {
        if (Columns == null || Rows == null || SquareLength == null || 
            StartCursorX == null || StartCursorY == null)
            throw new Exception("Board not correctly configured.");

        // Setup 
        var squares = CreateSquares();
        var rows = CreateRows();
        var cols = CreateColumns();
        
        var sudokuBoard = SudokuBoard as SudokuBoard;
        sudokuBoard!.BoardHeight = Rows;
        sudokuBoard.BoardWidth = Columns;
        
        var data = boardFile.ConvertData(Columns, Rows);
        int startSquareNr = 0;
        var squareNr = startSquareNr;
        int squareHeight = 0;

        if (Rows % 2 == 0) squareHeight = Rows / SquareLength;
        else squareHeight = SquareLength;

        // Build

        for (int j = 0; j < Rows; j++)
        {
            var rowY = j;
            for (int i = 0; i < Columns; i++)
            {
                var columnX = i;
                
                var row = rows[rowY];
                var col = cols[columnX];
                
                if (columnX % SquareLength == 0 && columnX != 0)
                    squareNr++;
                
                var activeSquare = squares[squareNr];
                
                int value = data[rowY][columnX];
                var cell = new Cell(value, columnX, rowY, value > 0);
        
                if (rowY == StartCursorX && columnX == StartCursorY)
                {
                    Cursor = cell;
                    cell.IsCursor = true;
                }
        
                col.Add(cell);
                row.Add(cell);
                activeSquare.Add(cell);
                
                cell.Row = row;
                cell.Column = col;
                cell.Square = activeSquare;
            }
            
            if ((rowY + 1) % squareHeight == 0 && rowY != 0)
            {
                startSquareNr += squareHeight;
                squareNr = startSquareNr;
            }
            else
            {
                squareNr = startSquareNr;
            }
        }
        
        // merge

        foreach (var column in cols)
        {
            SudokuBoard.Add(column);
        }
        
        foreach (var row in rows)
        {
            SudokuBoard.Add(row);
        }
        
        foreach (var s in squares)
        {
            sudokuBoard.Add(s);
        }
        
        return this;
    }
    
    private Square[] CreateSquares()
    {
        var squares = new List<Square>();

        for (int i = 0; i < Squares; i++)
        {
            squares.Add(new Square(i));
        }

        return squares.ToArray();
    }

    private Column[] CreateColumns()
    {
        var cols = new Column[Columns];
        for (int i = 0; i < cols.Length; i++)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            if (cols[i] == null)
                cols[i] = new Column();
            cols[i].X = i;
        }

        return cols.ToArray();
    }

    private Row[] CreateRows()
    {
        var rows = new Row[Rows];

        for (int i = 0; i < rows.Length; i++)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            if (rows[i] == null)
                rows[i] = new Row();
            rows[i].Y = i;
        }

        return rows;
    }
}