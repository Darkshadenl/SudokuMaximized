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

        
        // Setup =
        var sudokuBoard = SudokuBoard as SudokuBoard;
        sudokuBoard.BoardHeight = (int) Rows;
        sudokuBoard.BoardWidth = (int) Columns;
        
        // var cols = new Column[(int) Columns];
        // var rows = new Row[(int) Rows];
        var squares = new Square[(int) SquareLength][];
        var data = boardFile.ConvertData((int) Columns);
        Cell cursor = null;

        for (int i = 0; i < squares.Length; i++)
        {
            squares[i] = new Square[(int) SquareLength];
        }

        // Build
        for (int i = 0; i < Columns; i++)
        {
            for (int j = 0; j < Rows; j++)
            {
                var columnX = j;
                var rowY = i;
                var row = new Row(rowY, columnX);
                var col = new Column(rowY, columnX);
                
                
                Square activeSquare = squares[rowY % (int) SquareLength][columnX % (int) SquareLength];
                if (activeSquare == null)
                {
                    activeSquare = new Square(rowY % (int) SquareLength, columnX % (int) SquareLength);
                    squares[columnX % (int) SquareLength][columnX % (int) SquareLength] = activeSquare;
                }

                int value = data[rowY][columnX];
                var cell = new Cell(value, columnX, rowY);

                if (rowY == StartCursorX && columnX == StartCursorY)
                {
                    cell.IsCursor = true;
                    Cursor = cell;
                    cols[columnX].HasCursor = true;
                    rows[rowY].HasCursor = true;
                    activeSquare.HasCursor = true;
                }

                cols[columnX].Add(cell);
                rows[rowY].Add(cols[columnX]);
                activeSquare.Add(rows[rowY]);
                
                cell.Row = rows[rowY];
                cell.Column = cols[columnX];
                cell.Square = activeSquare;
            }
        }
        foreach (var column in cols)
        {
            SudokuBoard.Add(column);
        }

        foreach (var row in rows)
        {
            SudokuBoard.Add(row);
        }

        foreach (var square in squares)
        {
            foreach (var square1 in square)
            {
                SudokuBoard.Add(square1);
            }
        }

        return this;
    }
    
    // Deprecated
    public AbstractBoard CreateBoard(int[][] mdArray)
    {
        var columns = new Column[mdArray[0].Length];
        var rows = new Row[mdArray.Length];
        var squares = new Square[mdArray.Length / 3][];
        
        squares[0] = new Square[mdArray.Length / 3];
        squares[1] = new Square[mdArray.Length / 3];
        squares[2] = new Square[mdArray.Length / 3]; 
        
        // done till here ^^
        
        // create rows, cols and squares
        for (int i = 0; i < mdArray.Length; i++)
        {
            if (rows[i] == null)
            {
                rows[i] = new Row(i);
            }
            
            for (int j = 0; j < mdArray[i].Length; j++)
            {
                var c = j;
                var r = i;
                Square activeSquare;
                if (columns[j] == null)
                {
                    columns[j] = new Column(j);
                }
                
                int value = mdArray[i][j];
                var cell = new Cell(value, j, i);
                
                rows[i].Add(cell);
                columns[j].Add(cell);
                cell.Row = rows[i];
                cell.Column = columns[j];

                activeSquare = squares[i % 3][j % 3];
                if (activeSquare == null)
                {
                    activeSquare = new Square(i % 3, j % 3);
                    squares[i % 3][j % 3] = activeSquare;
                }
                activeSquare.Add(cell);
                cell.Square = activeSquare;
            }
        }
        foreach (var column in columns)
        {
            SudokuBoard.Add(column);
        }

        foreach (var row in rows)
        {
            SudokuBoard.Add(row);
        }

        foreach (var square in squares)
        {
            foreach (var square1 in square)
            {
                SudokuBoard.Add(square1);
            }
        }

        return this;
    }
    
}