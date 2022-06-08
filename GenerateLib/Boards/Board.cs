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
            // We need to replace the cells of the corners of the middleboard, otherwise its value won't update. 
            // This means we need to replace it in 3 different places. Rows, cols and squares. 
            // Cells also need to know their new rows, cols and squares
            RefactorCornersSamurai();
        }

        return this;
    }

    private void RefactorCornersSamurai()
    {
        var clonedSquares = new List<int>();

        var lowerRightSquare = ExtractSquareFromBoard(SudokuBoards[4] as SudokuBoard, 0);   // linksboven van board 4
        var lowerLeftSquare = ExtractSquareFromBoard(SudokuBoards[3] as SudokuBoard, 2);    // rechtsboven van board 3
        var upperRightSquare = ExtractSquareFromBoard(SudokuBoards[1] as SudokuBoard, 6);   // linksonder van board 1
        var upperLeftSquare = ExtractSquareFromBoard(SudokuBoards[0] as SudokuBoard, 8);    // rechtsonder van board 0

        var middleUpperLeftSquare = ExtractSquareFromBoard(SudokuBoards[2] as SudokuBoard, 0);
        var middleUpperRightSquare = ExtractSquareFromBoard(SudokuBoards[2] as SudokuBoard, 2);
        var middleLowerLeftSquare = ExtractSquareFromBoard(SudokuBoards[2] as SudokuBoard, 6);
        var middleLowerRightSquare = ExtractSquareFromBoard(SudokuBoards[2] as SudokuBoard, 8);

        // Got needed cells (in the form of squares). 
        // Taking upperleft and middleUpperleft as example.
        // upperLeftSquare is leading. 
        // Cells of upperLeftSquare need to replace cells of middleUpperLeftSquare.
        // Cells of upperLeftSquare need to know the row and col of corresponding cell in middleUpperLeftSquare.

        var middleSquares = new List<Square> { middleUpperLeftSquare, middleUpperRightSquare, middleLowerLeftSquare, middleLowerRightSquare };
        var otherSquares = new List<Square> { upperLeftSquare, upperRightSquare, lowerLeftSquare, lowerRightSquare };
        for (int i = 0; i < middleSquares.Count; i++)
        {
            // needed data
            var middleSquareCells = middleSquares[i].Components.Cast<Cell>().ToList();
            var otherSquareCells = otherSquares[i].Components.Cast<Cell>().ToList();

            for (int j = 0; j < middleSquareCells.Count; j++)
            {
                var middleSquareCell = middleSquareCells[j];
                var otherSquareCell = otherSquareCells[j];

                // add cols, rows of middleSquareComponents to otherSquareComponents
                otherSquareCell.Columns.Add(middleSquareCell.Columns[0]);
                otherSquareCell.Rows.Add(middleSquareCell.Rows[0]);

                // pre replace middlecell location
                // you dont want to replace object or you lose reference
                // only property changing

                //var coords = middleSquareCell.Coordinates.Clone() as Coordinates;

                // setting cursor for samurai middle board
                if (middleSquareCell.Coordinates.X == StartCursorX && middleSquareCell.Coordinates.Y == StartCursorY)
                {
                    Cursor = middleSquareCell;
                    middleSquareCell.IsCursor = true;
                }

                // middleSquareCell.Column/Row should know the otherSquareCell, replacing the previous middleSquareCell
                // This should be enough because col/row of middleSquareCell is referred by Sudokuboard. 
                middleSquareCell.Columns[0].ReplaceCell(middleSquareCell, otherSquareCell);
                middleSquareCell.Rows[0].ReplaceCell(middleSquareCell, otherSquareCell);
                middleSquareCell.Squares[0].ReplaceCell(middleSquareCell, otherSquareCell);

                // fix x/y of new middle square
                // post replace middlecell location
                //middleSquareCell.Coordinates.X = coords.X;
                //middleSquareCell.Coordinates.Y = coords.Y;


            }
        }
        var t = 1;
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

                cell.Rows.Add(row);
                cell.Columns.Add(col);
                cell.Squares.Add(activeSquare);
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