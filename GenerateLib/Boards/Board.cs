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

        var amountOfBoards = boardFile.GetAmountBoards();

        // save cells of the 4 corners
        var celList = new List<Cell>();

        // add all boards to SudokuBoards EXCEPT THE MIDDLE BOARD
        for (int x = 0; x < amountOfBoards; x++)
        {
            // adds board to the list of boards
            SudokuBoards.Add(new SudokuBoard());

            // if boardtype is samurai
            if (x == 2 && boardFile.Extension == "." + BoardTypes.samurai.ToString())
            {
                // we fill middle board last
                continue;
            }

            var sudokuBoard = SudokuBoards[x] as SudokuBoard;
            sudokuBoard.BoardHeight = Rows;
            sudokuBoard.BoardWidth = Columns;

            // Setup 
            var squares = CreateSquares();
            var rows = CreateRows();
            var cols = CreateColumns();

            var data = boardFile.ConvertData(Columns, Rows, x);

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

                    if (x == 0 && activeSquare == squares[8] && !celList.Contains(cell))
                    {
                        celList.Add(cell);
                    }
                    else if (x == 1 && activeSquare == squares[6] && !celList.Contains(cell))
                    {
                        celList.Add(cell);
                    }
                    else if (x == 3 && activeSquare == squares[2] && !celList.Contains(cell))
                    {
                        celList.Add(cell);
                    }
                    else if (x == 4 && activeSquare == squares[0] && !celList.Contains(cell))
                    {
                        celList.Add(cell);
                    }
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
                SudokuBoards[x].Add(column);
            }
            foreach (var row in rows)
            {
                SudokuBoards[x].Add(row);
            }
            // sq are used in solve alg
            foreach (var s in squares)
            {
                SudokuBoards[x].Add(s);
            }
        }

        if (amountOfBoards > 0 && boardFile.Extension == "." + BoardTypes.samurai.ToString())
        {
            // we fill middle board LAST
            var sudokuBoard = SudokuBoards[2] as SudokuBoard;
            sudokuBoard.BoardHeight = Rows;
            sudokuBoard.BoardWidth = Columns;

            // Setup 
            var squares = CreateSquares();
            var rows = CreateRows();
            var cols = CreateColumns();

            var data = boardFile.ConvertData(Columns, Rows, 2);

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

                    Cell cell = new Cell(value, columnX, rowY, value > 0);

                    if (activeSquare == squares[0] || activeSquare == squares[2] || activeSquare == squares[6] || activeSquare == squares[8])
                    {
                        cell = celList[i + j];
                        cell.X = j;
                        cell.Y = i;
                        cell.Row.First().X = j;
                        cell.Row.First().Y = i;

                        cell.Row[0].Components[i].X = j;
                        cell.Row[0].Components[i].Y = i;
                        cell.Row[0].Components[i].Value = 0;

                        cell.Column.First().X = j;
                        cell.Column.First().Y = i;

                        cell.Column[0].Components[i].X = j;
                        cell.Column[0].Components[i].Y = i;
                        cell.Column[0].Components[i].Value = 0;


                        cell.Square.First().X = j;
                        cell.Square.First().Y = i;
                        //foreach (var tempComponent in cell.Square[0].Components)
                        //{
                        //    tempComponent.X = j;
                        //    tempComponent.Y = i;
                        //    tempComponent.Value = 0;
                        //    //tempComponent.
                        //}

                        col.Add(cell);
                        row.Add(cell);
                        activeSquare.Add(cell);
                    }
                    else
                    {
                        //regular uncopied cells
                        col.Add(cell);
                        row.Add(cell);
                        activeSquare.Add(cell);

                        cell.Row.Add(row);
                        cell.Column.Add(col);
                        cell.Square.Add(activeSquare);
                    }

                    if (rowY == StartCursorX && columnX == StartCursorY)
                    {
                        Cursor = cell;
                        cell.IsCursor = true;
                    }
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
            // just build no data
            foreach (var column in cols)
            {
                SudokuBoards[2].Add(column);
            }
            foreach (var row in rows)
            {
                SudokuBoards[2].Add(row);
            }
            // sq are used in solve alg
            foreach (var s in squares)
            {
                SudokuBoards[2].Add(s);
            }
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