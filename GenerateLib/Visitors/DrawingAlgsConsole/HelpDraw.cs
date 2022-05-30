using GenerateLib.Viewable;

namespace GenerateLib.Visitors.DrawingAlgsConsole;

public class HelpDraw : IDraw
{
    public HelpDraw()
    {
        Console.SetWindowSize(99, 99);
        Console.SetBufferSize(200, 200);
    }

    public void DrawRegularBoard(int size, List<IViewable> board)
    {
        /*
         *  123|123|123 || 123|123|123 ||
         *  456|456|456 || 456|456|456 ||
         *  789|789|789 || 789|789|789 ||
         *  ----------------------------------------
         *  123|123|123 || 123|123|123
         *  456|456|456 || 456|456|456
         *  789|789|789 || 789|789|789
         *  ------------------------
         *  123|123|123 || 123|123|123
         *  456|456|456 || 456|456|456
         *  789|789|789 || 789|789|789
         *  _________________________________________
         *
         * 
         */
        var squareSize = (int) Math.Sqrt(size);
        var boardSquareAmount = size * squareSize;
        
        int colOffset = 6;
        int squareHorizontalOffset = 2;

        int baseCol = 0;
        int baseRow = 2;
        
        int col = baseCol;
        int row = baseRow;

        var boardDataIndex = 0;
        var data = board.ToArray();

        var line = "--------------------------------------------------------";
        
        
        Console.Write(line);
        for (int j = 0; j < boardSquareAmount; j++)
        {
            row = baseRow;
            col = baseCol;

            var viewable = data[boardDataIndex];
            
            // loop through every possible helpvalue and check if it has been filled. 
            // If it has been filled, then draw the helpvalue. Else, draw a blank space.
            for (int i = 1; i < 10; i++)
            {
                if ((i - 1) % 3 == 0 && i - 1 != 0)
                {
                    GetCursorLine(viewable);
                    row++;
                    col = baseCol;
                }
                Console.SetCursorPosition(col, row);
                col++;


                if (viewable.PossibleValues.Contains(i) || viewable.Value == i) Console.Write($"{i}");
                else Console.Write(" ");
            }

            GetCursorLine(viewable);
            boardDataIndex++;
            var offset = colOffset;

            if ((j + 1) % 3 == 0 && j != 0)
            {
                offset += squareHorizontalOffset;
            }

            baseCol += offset;
            
            // If, looking horizontally, 3 squares have been drawn, move to next squares below these 3.
            // If confused, look at the baseRow change. 
            if ((j + 1) % 9 == 0 && j != 0)
            {
                Console.WriteLine();
                Console.WriteLine(line);
                baseRow += 4;
                baseCol = 0;
            }
        }

        var t = 't';
    }

    private void GetCursorLine(IViewable viewable)
    {
        if (viewable.IsCursor) Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("|");
        Console.ResetColor();
    }

    public void DrawJigSawBoard(int size, List<IViewable> board)
    {
        throw new NotImplementedException();
    }
}