using GenerateLib.Components;
using GenerateLib.Helpers;
using GenerateLib.Import;
using System.Linq;

namespace GenerateLib.Boards;

public class Board : AbstractBoard
{
    public Board()
    {
        SudokuBoards = new List<Component>();
    }

    public override AbstractBoard CreateBoardBuild(BoardFile boardFile)
    {
        if (Columns == null || Rows == null || SquareLength == null ||
            StartCursorX == null || StartCursorY == null)
            throw new Exception("Board not correctly configured.");

        bool isSamurai = boardFile.Extension == "." + BoardTypes.samurai;
        var amountOfBoards = boardFile.GetAmountBoards();
        
        for (int boardIndex = 0; boardIndex < amountOfBoards; boardIndex++)
        {
            SudokuBoards.Add(new SudokuBoard());
            
            var sudokuBoard = SudokuBoards[boardIndex] as SudokuBoard;
            sudokuBoard.BoardHeight = Rows;
            sudokuBoard.BoardWidth = Columns;

            // Setup 
            var data = boardFile.ConvertData(Columns, Rows, boardIndex);
            int startSquareNr = 0;
            var squareNr = startSquareNr;
            int squareHeight = 0;
            
            if (Rows % 2 == 0) squareHeight = Rows / SquareLength;
            else squareHeight = SquareLength;

            // Build
            BuildABoard(squareNr, data, squareHeight, startSquareNr, boardIndex);
        }

        if (isSamurai)
        {
            AddExtraSquaresToMiddleBoardCells();
            FixMiddleBoardRows(); // TODO ga verder hier
        }

        #region idk 
                // if (amountOfBoards > 0 && boardFile.Extension == "." + BoardTypes.samurai.ToString())
        // {
        //     // we fill middle board LAST
        //     var sudokuBoard = SudokuBoards[2] as SudokuBoard;
        //     sudokuBoard.BoardHeight = Rows;
        //     sudokuBoard.BoardWidth = Columns;
        //
        //     // Setup 
        //     var squares = CreateSquares();
        //     var rows = CreateRows();
        //     var cols = CreateColumns();
        //
        //     var data = boardFile.ConvertData(Columns, Rows, 2);
        //
        //     int startSquareNr = 0;
        //     var squareNr = startSquareNr;
        //     int squareHeight = 0;
        //
        //     if (Rows % 2 == 0) squareHeight = Rows / SquareLength;
        //     else squareHeight = SquareLength;
        //
        //     // Build
        //     for (int j = 0; j < Rows; j++)
        //     {
        //         var rowY = j;
        //
        //         for (int i = 0; i < Columns; i++)
        //         {
        //             var columnX = i;
        //
        //             var row = rows[rowY];
        //             var col = cols[columnX];
        //
        //             if (columnX % SquareLength == 0 && columnX != 0)
        //                 squareNr++;
        //
        //             var activeSquare = squares[squareNr];
        //             int value = data[rowY][columnX];
        //
        //             Cell cell = new Cell(value, columnX, rowY, value > 0);
        //
        //             if (activeSquare == squares[0] || activeSquare == squares[2] || activeSquare == squares[6] || activeSquare == squares[8])
        //             {
        //                 cell = squaresToAddToMiddleBoard[i + j];
        //                 cell.X = j;
        //                 cell.Y = i;
        //                 cell.Row.First().X = j;
        //                 cell.Row.First().Y = i;
        //
        //                 cell.Row[0].Components[i].X = j;
        //                 cell.Row[0].Components[i].Y = i;
        //                 cell.Row[0].Components[i].Value = 0;
        //
        //                 cell.Column.First().X = j;
        //                 cell.Column.First().Y = i;
        //
        //                 cell.Column[0].Components[i].X = j;
        //                 cell.Column[0].Components[i].Y = i;
        //                 cell.Column[0].Components[i].Value = 0;
        //
        //
        //                 cell.Square.First().X = j;
        //                 cell.Square.First().Y = i;
        //                 //foreach (var tempComponent in cell.Square[0].Components)
        //                 //{
        //                 //    tempComponent.X = j;
        //                 //    tempComponent.Y = i;
        //                 //    tempComponent.Value = 0;
        //                 //    //tempComponent.
        //                 //}
        //
        //                 col.Add(cell);
        //                 row.Add(cell);
        //                 activeSquare.Add(cell);
        //             }
        //             else
        //             {
        //                 //regular uncopied cells
        //                 col.Add(cell);
        //                 row.Add(cell);
        //                 activeSquare.Add(cell);
        //
        //                 cell.Row.Add(row);
        //                 cell.Column.Add(col);
        //                 cell.Square.Add(activeSquare);
        //             }
        //
        //             if (rowY == StartCursorX && columnX == StartCursorY)
        //             {
        //                 Cursor = cell;
        //                 cell.IsCursor = true;
        //             }
        //         }
        //
        //         if ((rowY + 1) % squareHeight == 0 && rowY != 0)
        //         {
        //             startSquareNr += squareHeight;
        //             squareNr = startSquareNr;
        //         }
        //         else
        //         {
        //             squareNr = startSquareNr;
        //         }
        //     }
        //
        //     // merge
        //     // cols/rows are used to build the board 
        //     // just build no data
        //     foreach (var column in cols)
        //     {
        //         SudokuBoards[2].Add(column);
        //     }
        //     foreach (var row in rows)
        //     {
        //         SudokuBoards[2].Add(row);
        //     }
        //     // sq are used in solve alg
        //     foreach (var s in squares)
        //     {
        //         SudokuBoards[2].Add(s);
        //     }
        // }

        #endregion

        return this;
    }

    private void FixMiddleBoardRows()
    {
        // board 0, rows 678 range 678
        // board 1, rows 678, range 012
        // board 3, rows 012, range 678
        // board 4, rows 012, range 012

        var middleBoard = SudokuBoards[2] as SudokuBoard;
        var middleBRow = middleBoard.Components.Where(c => c is Row).ToList();

        // board 0
        var board = SudokuBoards[0] as SudokuBoard;
        var rows = board.Components.Where(c => c is Row).ToList();
        
        var firstRow = GetRangeFromRow((rows[6] as Row)!, 6, 8);
        var secondRow = GetRangeFromRow((rows[7] as Row)!, 6, 8);
        var ThirdRow = GetRangeFromRow((rows[8] as Row)!, 6, 8);

        var firstMiddleRow = GetRangeFromRow((rows[6] as Row)!, 6, 8);


        // board 1

        // board 2

        // board 3

    }

    private void SetRangeOfRow(Row row, int start, int end)
    {
        
    }

    private List<Component> GetRangeFromRow(Row row, int start, int end)
    {
        return row.Components.GetRange(start, end).ToList();
    }

    private void AddExtraSquaresToMiddleBoardCells()
    {
        var upperLeftSquare = ExtractSquareFromBoard(SudokuBoards[0] as SudokuBoard, 8);    // rechtsonder van board 0
        var upperRightSquare = ExtractSquareFromBoard(SudokuBoards[1] as SudokuBoard, 6);   // linksonder van board 1
        var lowerLeftSquare = ExtractSquareFromBoard(SudokuBoards[3] as SudokuBoard, 2);    // rechtsboven van board 3
        var lowerRightSquare = ExtractSquareFromBoard(SudokuBoards[4] as SudokuBoard, 0);   // linksboven van board 4

        var middleUpperLeftSquare = ExtractSquareFromBoard(SudokuBoards[2] as SudokuBoard, 8);
        var middleUpperRightSquare = ExtractSquareFromBoard(SudokuBoards[2] as SudokuBoard, 6);
        var middleLowerLeftSquare = ExtractSquareFromBoard(SudokuBoards[2] as SudokuBoard, 2);
        var middleLowerRightSquare = ExtractSquareFromBoard(SudokuBoards[2] as SudokuBoard, 0);
        
        var middleSquares = new List<Square>{ middleUpperLeftSquare, middleUpperRightSquare, middleLowerLeftSquare, middleLowerRightSquare };
        var otherSquares = new List<Square> { upperLeftSquare, upperRightSquare, lowerLeftSquare, lowerRightSquare};

        for (int i = 0; i < middleSquares.Count; i++)
        {
            var middleSquareComponents = middleSquares[i].Components;

            for (int j = 0; j < middleSquareComponents.Count; j++)
            {
                var middleSquareCell = middleSquareComponents[j] as Cell;
                middleSquareCell!.Square.Add(otherSquares[i]);
            }
        }
    }


    private void BuildABoard(int squareNr, int[][] data, int squareHeight, int startSquareNr, int boardIndex)
    {
        var rows = CreateRows();
        var cols = CreateColumns();
        var squares = CreateSquares();
        
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
                Cell cell = new Cell(value, columnX, rowY, value > 0);

                if (rowY == StartCursorX && columnX == StartCursorY)
                {
                    Cursor = cell;
                    cell.IsCursor = true;
                }

                col.Add(cell);
                row.Add(cell);
                activeSquare.Add(cell);

                cell.Row.Add(row);
                cell.Column.Add(col);
                cell.Square.Add(activeSquare);
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
        // cols/rows are used to build the board
        foreach (var column in cols)
        {
            SudokuBoards[boardIndex].Add(column);
        }
        foreach (var row in rows)
        {
            SudokuBoards[boardIndex].Add(row);
        }
        foreach (var s in squares)
        {
            SudokuBoards[boardIndex].Add(s);
        }
    }


    private Square ExtractSquareFromBoard(SudokuBoard sudokuBoard, int index)
    {
        var squares = sudokuBoard.Components.Where(c => c is Square);
        return (squares.ElementAt(index) as Square)!;
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