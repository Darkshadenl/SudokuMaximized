using Sudoku.Model.Viewable;

namespace Sudoku.Visitor;

public class RegularBoardPrintVisitor : IPrintBoardVisitor
{
    public void Draw(List<IViewable> board)
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

            Console.Write($" {board[index].Value} ");
        }

        Console.Write("|");
        Console.WriteLine();
        Console.WriteLine(horizontalLine);
    }
}