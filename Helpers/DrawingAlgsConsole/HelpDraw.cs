using Helpers.Viewable;

namespace Helpers.DrawingAlgsConsole;

public class HelpDraw : IDraw
{
    public HelpDraw()
    {
        Console.SetWindowSize(80, 50);
        Console.SetBufferSize(150, 150);
    }
    
    private Dictionary<int, string> lines = new()
    {
        {6, "----------------------------------------"},
        {9, "----------------------------------------------------------------"},
        {4, "----------------------------"},
    };

    public void DrawRegularBoard(int size, List<IViewable> board)
    {
        // check if size is even or odd
        ConfigureVarsBasedOnOddEven(size, 
            out var squareHeight, 
            out var verticalSquareAmount, 
            out var squareWidth);

        // if you take a sudokuboard, and only look at the upper row of squares.
        // This is amount of cells this row contains.
        int squareRowCellAmount = size * squareHeight;
        int boardCellAmount = squareRowCellAmount * verticalSquareAmount;

        int colOffset = 6;
        int squareHorizontalOffset = 6;

        int baseCol = 0;
        int baseRow = 2;
        int col = baseCol;
        int row = baseRow;

        var boardDataIndex = 0;
        var data = board.ToArray();

        var line = lines.First(e => e.Key == size).Value;;

        Console.Write(line);
        
        for (int j = 0; j < boardCellAmount; j++)
        {
            row = baseRow;
            col = baseCol;

            var viewable = data[boardDataIndex];

            // loop through every possible helpvalue and check if it has been filled. 
            // If it has been filled, then draw the helpvalue. Else, draw a blank space.
            for (int i = 1; i < 10; i++)
            {
                //     always 3, so that possible values 1 to 9 can be aligned in rows of 3.
                if ((i - 1) % 3 == 0 && i - 1 != 0) 
                {
                    GetCursorVerticalLine(viewable);
                    row++;
                    col = baseCol;
                }

                Console.SetCursorPosition(col, row);
                col++;

                if (viewable.PossibleValues.Contains(i) || viewable.Value == i) Console.Write($"{i}");
                else Console.Write(" ");
            }

            GetCursorVerticalLine(viewable);
            boardDataIndex++;
            var offset = colOffset;

            // add horizontal offset to widen area between squares
            if ((j + 1) % squareWidth == 0 && j != 0)
            {
                offset += squareHorizontalOffset;
            }
            baseCol += offset;

            // If, looking horizontally, X squares have been drawn, move to next squares below these X.
            // If confused, look at the baseRow change. 
            if ((j + 1) % size == 0 && j != 0)
            {
                Console.WriteLine();
                Console.WriteLine(line);
                baseRow += 4;
                baseCol = 0;
            }

            if ((j + 1) % squareRowCellAmount == 0 && j != 0)
            {
                Console.WriteLine();
                Console.WriteLine();
                baseRow += 3;
            }
        }
    }

    private void ConfigureVarsBasedOnOddEven(int size, out int squareHeight, 
        out int verticalSquareAmount, out int squareWidth)
    {
        if (size % 2 == 0) // even
        {
            squareWidth = size / 2;
            squareHeight = (int) Math.Sqrt(size);
            verticalSquareAmount = size / 2;
        }
        else
        {
            squareWidth = (int) Math.Sqrt(size);
            squareHeight = squareWidth;
            verticalSquareAmount = (int) Math.Sqrt(size);
        }
    }

    private void GetCursorVerticalLine(IViewable viewable)
    {
        if (viewable.IsCursor) Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("|");
        Console.ResetColor();
    }

    public void DrawJigSawBoard(int size, List<IViewable> board)
    {
        throw new NotImplementedException();
    }

    public void DrawSamuraiBoard(int size, List<IViewable> board)
    {
        // samurai is just regular board 
        DrawRegularBoard(size, board);
    }
}