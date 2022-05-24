using GenerateLib.Viewable;

namespace GenerateLib.DrawingAlgsConsole;

public class DefinitiveDraw : IDraw
{
    public void DrawRegularBoard(int size, List<IViewable> board)
    {
        var verC = 0;
        var squareSize = (int) Math.Sqrt(size);

        var set = new Dictionary<int, string>();
        set.Add(6, "----------------------");
        set.Add(9, "-------------------------------");
        set.Add(4, "---------------");

        var horizontalLine = set.First(e => e.Key == size).Value;

        Console.WriteLine(horizontalLine);

        for (var index = 0; index < board.Count; index++)
        {
            if (index != 0 && index % size == 0)
            {
                Console.Write("|");
                Console.WriteLine();
                if (verC == squareSize - 1)
                {
                    Console.WriteLine(horizontalLine);
                    verC = 0;
                }
                else
                {
                    verC++;
                }
            }

            if (index % squareSize == 0)
            {
                Console.Write("|");
            }

            var boardValue = board[index].Value == 0 ? " " : board[index].Value.ToString();
            if (board[index].IsCursor)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                if (board[index].Value == 0) Console.Write(" _ ");
                else Console.Write($" {boardValue} ");
                ;
                Console.ResetColor();
            }
            else
            {
                Console.Write($" {boardValue} ");
            }
        }

        Console.Write("|");
        Console.WriteLine();
        Console.WriteLine(horizontalLine);
    }
    
    public void DrawJigSawBoard(int size, List<IViewable> board)
    {
        throw new NotImplementedException();
    }
}