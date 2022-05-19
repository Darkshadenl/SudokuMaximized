using GenerateLib.Components;
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
        {
            throw new Exception("Board not correctly configured.");
        }

        // Setup 
        var squares = CreateSquares();
        var rowsAndCols = CreateColsAndRows();
        var sudokuBoard = SudokuBoard as SudokuBoard;
        sudokuBoard.BoardHeight = (int) Rows;
        sudokuBoard.BoardWidth = (int) Columns;
        var data = boardFile.ConvertData((int) Columns);
        int startSquareNr = 0;
        var squareNr = startSquareNr;

        // Build
        for (int y = 0; y < Rows; y++)
        {
            var row = rowsAndCols[y];
            
            for (int x = 0; x < Columns; x++)
            {
                if (x % SquareLength == 0 && x != 0)
                {
                    squareNr++;
                }
                
                var activeSquare = squares[squareNr];
                int value = data[y][x];
                var columnX = x;
                var rowY = y;
                var cell = new Cell(value, columnX, rowY);

                if (x == StartCursorX && y == StartCursorY)
                {
                    cell.IsCursor = true;
                    Cursor = cell;
                    row.HasCursor = true;
                    row.SetColHasCursor((int) StartCursorX);
                    activeSquare.HasCursor = true;
                }
                activeSquare.Add(cell);
                row.AddCellToCol(cell);
            }
            if ((y + 1) % SquareLength == 0 && y != 0)
            {
                startSquareNr += (int) SquareLength;
                squareNr = startSquareNr;
            }
            else
            {
                squareNr = startSquareNr;
            }
        }

        // merge
        foreach (var s in squares)
        {
            sudokuBoard.Add(s);
        }
        
        foreach (var row in rowsAndCols)
        {
            SudokuBoard.Add(row);
        }
        
        return this;
    }
    
    private Square[] CreateSquares()
    {
        // var squares = new Square[(int) SquareLength!][];
        var squares = new List<Square>();

        // for (int i = 0; i < squares.Length; i++)
        // {
        //     squares[i] = new Square[(int) SquareLength];
        // }
        //
        // for (int i = 0; i < squares.Length; i++)
        // {
        //     for (int j = 0; j < squares[i].Length; j++)
        //     {
        //         squares[i][j] = new Square(i, j);
        //     }
        // }

        for (int i = 0; i < SquareLength; i++)
        {
            for (int j = 0; j < SquareLength; j++)
            {
                squares.Add(new Square(j, i));
            }
        }

        return squares.ToArray();
    }

    private Row[] CreateColsAndRows()
    {
        var rows = new Row[(int) Rows!];
        var cols = new Column[(int) Columns!];

        for (int i = 0; i < rows.Length; i++)
        {
            if (rows[i] == null)
                rows[i] = new Row();
            rows[i].Y = i;
        }

        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i] == null)
                cols[i] = new Column();
            cols[i].X = i;
        }

        foreach (var row in rows)
        {
            foreach (var column in cols)
            {
                row.Add(column);
            }
        }

        return rows;
    }
}