using GenerateLib.Viewable;

namespace GenerateLib.Visitors;

public class ConsoleVisitor : IPrintBoardVisitor
{

    public void DrawRegular(List<IViewable> board)
    {
        var verC = 0;
        var horizontalLine = "-------------------------------";

        Console.WriteLine(horizontalLine);

        for (var index = 0; index < board.Count; index++)
        {
            if (index != 0 && index % 9 == 0)
            {
                Console.Write("|");
                Console.WriteLine();
                if (verC == 2)
                {
                    Console.WriteLine(horizontalLine);
                    verC = 0;
                }
                else
                {
                    verC++;
                }
                
            }

            if (index % 3 == 0)
            {
                Console.Write("|");
            }

            var boardValue = board[index].Value == 0 ? " " : board[index].Value.ToString();
            Console.Write($" {boardValue} ");
        }

        Console.Write("|");
        Console.WriteLine();
        Console.WriteLine(horizontalLine);
    }

    public void DrawJigSaw(List<IViewable> board)
    {
        throw new NotImplementedException();
    }
}